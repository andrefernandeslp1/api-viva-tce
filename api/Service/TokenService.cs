using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using API.Models;

public class TokenService : ITokenService
{
    private IConfiguration _config;

    public TokenService(IConfiguration Configuration)
    {
        _config = Configuration;
    }

    public string GenerateToken(Usuario usuario)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, usuario.Email),
        new Claim(ClaimTypes.Role, usuario.Role),
        new Claim("id", usuario.Id.ToString()),
        new Claim("nome", usuario.Nome)
    };

    if(usuario.FornecedorId is not null)
        claims.Add(new Claim("fornecedorId", usuario.FornecedorId.ToString()));

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Audience = _config["Jwt:Audience"],
        Issuer = _config["Jwt:Issuer"] 
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

}
