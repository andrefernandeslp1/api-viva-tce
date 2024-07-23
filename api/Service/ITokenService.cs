using System;

public interface ITokenService
{
    string GenerateToken(string email, bool isAdmin);
}
