using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BjjTrainer_API.Services_API
{
    public class JwtTokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly ApplicationDbContext _context;

        public JwtTokenService(IConfiguration configuration, ApplicationDbContext context)
        {
            _secretKey = configuration["Jwt:SecretKey"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _context = context;
        }

        public (string accessToken, string refreshToken) GenerateTokens(string userId, string username)
        {
            var accessToken = GenerateToken(userId, username);
            var refreshToken = CreateRefreshToken(userId);

            return (accessToken, refreshToken.Token);
        }

        public string GenerateToken(string userId, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(30), // Shorter expiration for access token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken CreateRefreshToken(string userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();

            return refreshToken;
        }

        public RefreshToken GetRefreshToken(string token) =>
            _context.RefreshTokens.SingleOrDefault(rt => rt.Token == token && rt.IsActive);

        // New method to handle refreshing the token
        public async Task<(string accessToken, string refreshToken)?> RefreshTokenAsync(string token)
        {
            var existingRefreshToken = GetRefreshToken(token);

            if (existingRefreshToken == null || existingRefreshToken.IsExpired)
            {
                return null; // Invalid or expired token
            }

            // Revoke the old refresh token
            existingRefreshToken.Revoked = DateTime.UtcNow;
            _context.RefreshTokens.Update(existingRefreshToken);
            await _context.SaveChangesAsync();

            // Generate a new access and refresh token
            var userId = existingRefreshToken.UserId;
            var user = await _context.ApplicationUsers.FindAsync(userId);
            if (user == null) return null;

            var newAccessToken = GenerateToken(userId, user.UserName);
            var newRefreshToken = CreateRefreshToken(userId);

            return (newAccessToken, newRefreshToken.Token);
        }
    }
}
