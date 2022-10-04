using System.Security.Cryptography;
using System.Text;

namespace Blog.Extensions;

public static class StringExtensions
{
    public static string ToSha256(this string value)
    {
        using var algorithm = SHA256.Create();
        var hash = new StringBuilder();
        byte[] crypto = algorithm.ComputeHash(Encoding.ASCII.GetBytes(value));

        foreach (byte data in crypto)
        {
            hash.Append(data.ToString("x2"));
        }

        return hash.ToString();
    }
}
