using System.Text.RegularExpressions;

namespace Snapkart.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool ValidPhoneNumber(this string number)
        {
            return Regex.Match(number, @"^\+8801([13-9]\d{1})[\d]{7}$").Success;
        }
    }
}