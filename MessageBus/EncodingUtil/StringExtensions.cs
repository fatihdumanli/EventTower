using System.Text;

namespace MessageBus.Extensions
{
    public static class StringExtensions 
    {
        public static byte[] ToByteArray(this string phrase)
        {
            return Encoding.UTF8.GetBytes(phrase);
        }
    }
}