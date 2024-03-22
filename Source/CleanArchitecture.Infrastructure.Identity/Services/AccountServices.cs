using CleanArchitecture.Application.DTOs.Account.Requests;
using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.UserInterfaces;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity.Services
{
    public class AccountServices(UserManager<ApplicationUser> userManager, IAuthenticatedUserService authenticatedUser, SignInManager<ApplicationUser> signInManager, JWTSettings jwtSettings, ITranslator translator) : IAccountServices
    {
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

        public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.NotFound, translator.GetString(TranslatorMessages.AccountMessages.Account_notfound_with_UserName(request.UserName)), nameof(request.UserName)));
            }
            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.FieldDataInvalid, translator.GetString(TranslatorMessages.AccountMessages.Invalid_password()), nameof(request.Password)));
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

            return new BaseResult<AuthenticationResponse>(response);
        }

        public async Task<BaseResult<AuthenticationResponse>> AuthenticateByUserName(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.NotFound, translator.GetString(TranslatorMessages.AccountMessages.Account_notfound_with_UserName(username)), nameof(username)));
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

            return new BaseResult<AuthenticationResponse>(response);
        }

        public async Task<BaseResult<string>> RegisterGostAccount()
        {
            var user = new ApplicationUser()
            {
                UserName = GenerateRandomString(7)
            };

            var identityResult = await userManager.CreateAsync(user);

            if (identityResult.Succeeded)
                return new BaseResult<string>(user.UserName);

            return new BaseResult<string>(identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)));

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
