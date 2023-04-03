using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Negocios.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Utilitarios
{
    public class JsonWebTokenNEG : IJsonWebTokenNEG
    {
        private protected readonly string? _key;
        public JsonWebTokenNEG(IConfiguration _configuration)
        {
            _key = _configuration.GetSection("JwtKey").ToString();
        }
        public string CreateToken(string psUsuario, string psPerfil, string psNombres, int piIdUsuario)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] tokenKey = Encoding.ASCII.GetBytes(_key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, psUsuario),
                    new Claim(ClaimTypes.Role, psPerfil),
                    new Claim(ClaimTypes.Name, psNombres),
                    new Claim("iIdUsuario", piIdUsuario.ToString()),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
