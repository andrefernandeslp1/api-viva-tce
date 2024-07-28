using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        // Cria uma instância do SHA256
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Converte a senha para bytes e calcula o hash
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Converte o array de bytes para uma string hexadecimal
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public bool Verify(string hashedPassword, string passwordToVerify)
    {
        string hashOfInput = Hash(passwordToVerify);
        return hashOfInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
    }

}
