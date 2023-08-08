using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Sam.CleanArchitecture.Application.DTOs.Account.Requests;
using Sam.CleanArchitecture.Application.DTOs.Account.Responses;
using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Application.Interfaces.UserInterfaces;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.Settings;
using Sam.CleanArchitecture.Infrastructure.Identity.Models;

namespace Sam.CleanArchitecture.Infrastructure.Identity.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAuthenticatedUserService authenticatedUser;
        private readonly JWTSettings jwtSettings;


        public AccountServices(UserManager<ApplicationUser> userManager, IAuthenticatedUserService authenticatedUser, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.authenticatedUser = authenticatedUser;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
        }

        public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
        {
            var user = await userManager.FindByIdAsync(authenticatedUser.UserId);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var identityResult = await userManager.ResetPasswordAsync(user, token, model.Password);

            if (identityResult.Succeeded)
                return new BaseResult();

            return new BaseResult(identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)));
        }

        public async Task<BaseResult> ChangeUserName(ChangeUserNameRequest model)
        {
            var user = await userManager.FindByIdAsync(authenticatedUser.UserId);

            user.UserName = model.UserName;

            var identityResult = await userManager.UpdateAsync(user);

            if (identityResult.Succeeded)
                return new BaseResult();

            return new BaseResult(identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)));
        }

        public async Task<Result<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new Result<AuthenticationResponse>(new Error(ErrorCode.NotFound, "Account notfound with UserName", nameof(request.UserName)));
            }
            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return new Result<AuthenticationResponse>(new Error(ErrorCode.FieldDataInvalid, "Invalid password", nameof(request.Password)));
            }

            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            var jwToken = await GenerateJwtToken(user);

            AuthenticationResponse response = new AuthenticationResponse()
            {
                Id = user.Id.ToString(),
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwToken),
                Email = user.Email,
                UserName = user.UserName,
                Roles = rolesList.ToList(),
                IsVerified = user.EmailConfirmed,
            };

            return new Result<AuthenticationResponse>(response);
        }

        public async Task<Result<AuthenticationResponse>> AuthenticateByUserName(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new Result<AuthenticationResponse>(new Error(ErrorCode.NotFound, "Account notfound with UserName", nameof(username)));
            }

            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            var jwToken = await GenerateJwtToken(user);

            AuthenticationResponse response = new AuthenticationResponse()
            {
                Id = user.Id.ToString(),
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwToken),
                Email = user.Email,
                UserName = user.UserName,
                Roles = rolesList.ToList(),
                IsVerified = user.EmailConfirmed,
            };

            return new Result<AuthenticationResponse>(response);
        }

        public async Task<Result<string>> RegisterGostAccount()
        {
            var user = new ApplicationUser()
            {
                UserName = GenerateRandomString(7)
            };

            var identityResult = await userManager.CreateAsync(user);

            if (identityResult.Succeeded)
                return new Result<string>(user.UserName);

            return new Result<string>(identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)));

            string GenerateRandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var random = new Random();
                var result = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                {
                    int index = random.Next(chars.Length);
                    result.Append(chars[index]);
                }

                return result.ToString();
            }
        }
        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
        {
            await userManager.UpdateSecurityStampAsync(user);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: await GetClaimsAsync(),
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;

            async Task<IList<Claim>> GetClaimsAsync()
            {
                var result = await signInManager.ClaimsFactory.CreateAsync(user);
                return result.Claims.ToList();
            }
        }

    }
}
