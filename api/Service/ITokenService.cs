using System;

public interface ITokenService
{
    string GenerateToken(string email, string role, int id, string nome);
}
