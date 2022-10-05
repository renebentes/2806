using System.Security.Cryptography;
using System.Text;

namespace Blog.Extensions;

public static class StringExtensions
{
    public static string ToSha256(this string value)
    {
        using var algorithm = SHA256.Create();
        var hash = new StringBuilder();

        foreach (byte data in algorithm.ComputeHash(Encoding.ASCII.GetBytes(value)))
        {
            hash.Append(data.ToString("x2"));
        }

        return hash.ToString();
    }
}
