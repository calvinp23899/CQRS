using Ecommerce.Core.DTOs.Settings;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Infrastructure.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSetting;

        public TokenService(IOptions<JWTSettings> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }

        public async Task<string> GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSetting.SecretKey);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("FullName", user.Firstname + " " + user.Lastname),
            };
            var signingCred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSetting.TokenExpireTime),
                signingCredentials: signingCred
            );
            var accessToken = tokenHandler.WriteToken(tokenOptions); 
            return accessToken;
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            user.ExpiredRefreshToken = DateTime.UtcNow.AddDays(_jwtSetting.RefreshTokenExpireTime);
            return refreshToken;
        }
    }
}
