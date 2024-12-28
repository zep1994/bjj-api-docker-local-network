using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BjjTrainer_API.Services_API.Users
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

        // Get the refresh token based on the token value
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
            existingRefreshToken.Revoked = DateTime.UtcNow; // Mark as revoked
            _context.RefreshTokens.Update(existingRefreshToken);
            await _context.SaveChangesAsync();

            // Generate new tokens
            var user = await _context.ApplicationUsers.FindAsync(existingRefreshToken.UserId);
            if (user == null)
            {
                return null; // User not found
            }

            // Generate new access token
            var newAccessToken = GenerateToken(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("IsCoach", user.IsCoach.ToString()) // Add IsCoach claim
            });

            // Create a new refresh token
            var newRefreshToken = CreateRefreshToken(user.Id);

            // Save the new refresh token
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return (newAccessToken, newRefreshToken.Token);
        }

        // Helper method to generate an access token
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Access token validity
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Helper method to create a new refresh token
        private RefreshToken CreateRefreshToken(string userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7), // Set refresh token expiration
                Created = DateTime.UtcNow
            };

            return refreshToken;
        }
    }
}