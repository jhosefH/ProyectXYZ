using System.Data.SqlClient;
using System.Data;
using ApiXYZServices.DataObjects;
using Dapper;
using static ApiXYZServices.Utilities.Enumerables.ApiEnumerables;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ApiXYZServices.Utilities
{
    public  class Util
    {

        private static IConfiguration _config;

        public  Util(IConfiguration config)
        {
            _config = config;
        }
     
        public static string GenerateJWT(string usuario)
        {
            byte[] keyBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            string secretKey = Convert.ToBase64String(keyBytes);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken.ToString();
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }


    }
}
