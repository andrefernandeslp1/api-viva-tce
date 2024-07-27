using System;
using API.Models;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}
