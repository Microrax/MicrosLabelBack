using System.Text;

namespace MicrosLabel.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string value) => value.ToStream(Encoding.UTF8);

        public static Stream ToStream(this string value, Encoding encoding) => new MemoryStream(encoding.GetBytes(value ?? string.Empty));
    }
}
