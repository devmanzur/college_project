using System.Text.RegularExpressions;

namespace Snapkart.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool ValidPhoneNumber(this string number)
        {
            if (number == null) return false;
            if (
                number.StartsWith("011") ||
                number.StartsWith("013") ||
                number.StartsWith("014") ||
                number.StartsWith("015") ||
                number.StartsWith("016") ||
                number.StartsWith("017") ||
                number.StartsWith("018") ||
                number.StartsWith("019")
            )
            {
                return number.Length == 11;
            }

            return false;
        }
    }
}