using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace c_sharp_serializer_gabee1987
{
    class RegexValidation
    {
        #region Validation methods

        public static bool IsNameValid(string input)
        {
            string firstNameRegex = @"^[A-Za-zÀ-ú]";
            string whiteSpaceRegex = @"+\s";
            string lastNameRegex = @"[A-Za-zÀ-ú]+$";
            string pattern = firstNameRegex + whiteSpaceRegex + lastNameRegex;

            if (input == null)
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(input, pattern);
            }
        }

        public static bool IsPhoneValid(string input)
        {
            string countryCode = @"((?:\+?3|0)6)";
            string separator = @"(?:[ /-]?|\()?";
            string areaCode = @"(\d{1,2})";
            string separator2 = @"(?:-| |\/|\))?";
            string phoneNumberPart1 = @"(\d{3})";
            string separator3 = @"[ -\/]?";
            string phoneNumberPart2 = @"(\d{3,4})";
            string pattern = countryCode + separator + areaCode + separator2 + phoneNumberPart1 + separator3 + phoneNumberPart2;

            if (input == null)
            {
                return false;
            }
            else if (input.Length != Regex.Match(input, pattern).Length)
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(input, pattern);
            }
        }

        public static bool IsAddressValid(string input)
        {
            return true;
        }

        #endregion

        #region Format methods

        public static string ReformatPhoneNumber(string input)
        {
            string countryCode = @"((?:\+?3|0)6)";
            string separator = @"(?:[ /-]?|\()?";
            string areaCode = @"(\d{1,2})";
            string separator2 = @"(?:-| |\))?";
            string phoneNumberPart1 = @"(\d{3})";
            string separator3 = @"[- ]?";
            string phoneNumberPart2 = @"(\d{3,4})";
            string pattern = countryCode + separator + areaCode + separator2 + phoneNumberPart1 + separator3 + phoneNumberPart2;
            string strippedInput = GetNumbers(input);

            Match m = Regex.Match(strippedInput, pattern);

            string formattedPhoneNumber = String.Format("+{0} {1} {2}-{3}",
                                 m.Groups[1].Value,
                                 m.Groups[2].Value,
                                 m.Groups[3].Value,
                                 m.Groups[4].Value
                                 );

            return formattedPhoneNumber;
        }

        public static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        #endregion

    }
}
