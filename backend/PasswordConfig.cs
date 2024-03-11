using System.Security.Cryptography;
using System.Text;

namespace music_api.Password;

/// Class to configure an password to send to database
public static class PasswordConfig{
    /// Generate an salt 
    public static string GenerateStringSalt(int qtd)
    {
        Random rnd = new Random();
        byte[] saltBytes = new byte[qtd];
        rnd.NextBytes(saltBytes);
        string salt = System.Convert.ToBase64String(saltBytes);
        return salt;
    }

    public static byte[] GetSaltBytesByString(string salt)
        =>  Encoding.Default.GetBytes(salt);

    public static string GetSaltStringByBytes(byte[] salt)
        => Encoding.Default.GetString(salt);
    
    public static byte[] GetPbdkf2Bytes(string password, byte[] saltBytes, int interactions, int outputBytes)
    {
        var pbdkf2  = new Rfc2898DeriveBytes(password, saltBytes);
        pbdkf2.IterationCount = interactions;
        return pbdkf2.GetBytes(outputBytes);
    }

    public static string GetHash(string password, string salt)
    {
        string str = password + salt;
        using var sha = SHA256.Create();                 // cria algoritimo de Hash
        var bytes = Encoding.UTF8.GetBytes(str);         // 
        var hashBytes = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}