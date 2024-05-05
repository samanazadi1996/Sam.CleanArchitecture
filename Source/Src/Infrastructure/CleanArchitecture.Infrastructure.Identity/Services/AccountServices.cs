using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Interfaces.UserInterfaces;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Infrastructure.Identity.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Settings;

public class AccountServices : IAccountServices
    {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration configuration;

    //Note:Here validation is against jwt_token of provided by google (not by api)
    public AccountServices(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
        this.userManager = userManager;
        this.configuration = configuration;
        }

    public async Task<BaseResult<AuthenticationResponse>> AuthenticateWithGoogle(string googleJwtToken)
        {
        //steps
        //1. validate google jwt & fetch payload
        //2.for payload email check if account exists or not
        //3. if not exists then creates & logins(of Google type instead of local)
        //4. creates new jwt of user fetched from db
        //5. extracts roles of user from db
        //6. returns newly created jwt for further


        var payload = await ValidateGoogleJwtToken(googleJwtToken);
        if (payload == null)
            {
            return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.NotFound, "Invalid Google token."));
            }

        var user = await userManager.FindByEmailAsync(payload.Email);
        if (user == null)
            {
            user = new ApplicationUser { UserName = payload.Email, Email = payload.Email };
            var identityResult = await userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
                {
                return new BaseResult<AuthenticationResponse>(identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)));
                }

            await userManager.AddLoginAsync(user, new UserLoginInfo("Google", payload.Subject, "Google"));
            }

        var jwToken = GenerateJwtToken(user);

        var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
        AuthenticationResponse response = new AuthenticationResponse
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


    /// <summary>
    /// validate google jwt and return payloads
    /// </summary>
    /// <param name="googleJwtToken"></param>
    /// <returns></returns>
    private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleJwtToken(string googleJwtToken)
        {
        try
            {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                //Audience = new List<string>() { configuration["GoogleAuthSettings:ClientId"] }
                Audience = new List<string>() { configuration["Google:ClientId"] }
                //like "28358123213176-v7o7a3vs9sd269i8qtknjua8kddmine1.apps.googleusercontent.com"  //remove this
                };

            var payload = await GoogleJsonWebSignature.ValidateAsync(googleJwtToken, settings);
            return payload;
            }
        catch (Exception e)
            {
            Console.WriteLine(e.ToString());
            // The token is invalid
            return null;
            }
        }

    private JwtSecurityToken GenerateJwtToken(ApplicationUser user)
        {
        //here api generates new jwt for db fetched data , this is not related with google jwt
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),//new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            // Add more claims as needed
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
        }

    //make this as draft for timebeing
    public async Task<BaseResult<AuthenticationResponse>> AuthenticateByJwtTokenOfGoogleType2(string authorizationHeader)
        {
        //this is not working but need this properly to make standard also if this fixed then all will be fixed
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
            return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.NotFound, "auth header is missed man"));
            }

        string token = authorizationHeader.Substring("Bearer ".Length);

        // Here, the token is read from the authorization header

        try
            {
            var tokenValidationParameters = new TokenValidationParameters
                {
                ValidIssuer = configuration["Google:Issuer"], //jwtSettings.Issuer,
                ValidAudience = configuration["Google:ClientId"],//app client id
                ValidateIssuerSigningKey = false,
                //ValidateSignatureLast=false,
                SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);

                        return jwt;
                        },
                //     IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("ac3e3e558111c7c7a75c5b65134d22f63ee006d0"))
                //"ac3e3e558111c7c7a75c5b65134d22f63ee006d0"
                // ... other validation parameters (see previous explanation) ...
                };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var result = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            // ... (rest of your code for processing the validated token) ...
            }
        catch (SecurityTokenException ex)
            {
            return new BaseResult<AuthenticationResponse>(new Error(ErrorCode.NotFound, "not mannnnn"));
            }
        return null;
        }

    }
