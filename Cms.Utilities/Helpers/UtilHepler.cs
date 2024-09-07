using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cms.Utilities.Helpers
{
    public static class UtilHepler
    {
        public static int SubDate(DateTime dt)
        {
            TimeSpan diff2 = DateTime.Now.Subtract(dt);
            return diff2.Days + 1;
        }
        public static string GetUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
                return "/uploaded/images/" + url;
            return null;
        }
        public static string GetUrl(string url, int type)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (type == 0)
                    return "/uploaded/images/" + url;
                else if (type == 1)
                    return "/uploaded/product/" + url;
            }


            return null;
        }
        public static List<string> GetImages(string images)
        {
            var lstImage = images.Split(",");
            List<string> newImages = new List<string>();
            foreach (var item in lstImage)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    newImages.Add("/uploaded/product/" + item);
                }
            }
            return newImages;
        }

        public static List<Guid> GetEmployees(string employees)
        {
            var lstEmployee = employees.Split(",");
            List<Guid> newEmployees = new List<Guid>();
            foreach (var item in lstEmployee)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    newEmployees.Add(Guid.Parse(item));
                }
            }
            return newEmployees;
        }
        public static string GetFirstSplit(string images)
        {
            if (!string.IsNullOrEmpty(images))
            {
                var array = images.Split(",");
                return array[0];
            }
            return null;
        }
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its length is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length <= maxLength)
                return str;

            var pLen = postfix?.Length ?? 0;

            var result = str.Substring(0, maxLength - pLen);
            if (!string.IsNullOrEmpty(postfix))
            {
                result += postfix;
            }

            return result;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : new string(str.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            return stringsToValidate.Any(string.IsNullOrEmpty);
        }
    }
}
