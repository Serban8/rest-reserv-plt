﻿using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthorizationService
    {
        private readonly string _securityKey;


        public AuthorizationService(IConfiguration config)
        {
            _securityKey = config["JWT:SecurityKey"];
        }

        public string GetToken(User user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roleClaim = new Claim("role", role);
            var idClaim = new Claim("userId", user.Id.ToString());
            var infoClaim = new Claim("email", user.Email);
            var usernameClaim = new Claim("fullName", user.FirstName + " " + user.LastName);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Issuer = "RRP-API-Backend",
                Audience = "RRP-API-Anyone",
                Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, infoClaim, usernameClaim }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptior);
            var tokenString = jwtTokenHandler.WriteToken(token);

            return tokenString;
        }

        public bool ValidateToken(string tokenString)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
            };

            if (!jwtTokenHandler.CanReadToken(tokenString.Replace("Bearer ", string.Empty)))
            {
                return false;
            }

            jwtTokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);
            return validatedToken != null;
        }

        public int GetUserIdFromToken(string tokenString)
        {
            tokenString = tokenString.Replace("Bearer ", string.Empty);
            if (!ValidateToken(tokenString))
            {
                throw new ArgumentException("Invalid Token");
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtTokenHandler.ReadJwtToken(tokenString);
            var claims = jwtSecurityToken.Claims ?? throw new ArgumentException("Invalid token. No claims found.");
            var userID = claims.FirstOrDefault(c => c.Type == "userId") ?? throw new ArgumentException("Invalid token. UserID claim not found.");

            return int.Parse(userID.Value);
        }
    }
}
