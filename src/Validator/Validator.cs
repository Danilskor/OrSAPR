using System;

namespace Validator
{
    /// <summary>
    /// Checking data for valid
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Checks for the entered text, as well as
        /// to a length that must be no more than 50 characters.
        /// </summary>
        /// <param name="text">Input text.</param>
        /// <param name="length">Maximum line length.</param>
        /// <param name="field">Field name</param>
        public static void AssertStringOnLength(string text, int length, string field)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($@"You entered an empty line in the {field}");
            }

            if (text.Length > length)
            {
                throw new ArgumentException($"Length {field} should be less " +
                                            $"{length} letters.");
            }
        }

        /// <summary>
        /// Checking a string for digits
        /// </summary>
        /// <param name="text">Input text.</param>
        /// <param name="field">Field name</param>
        public static void AssertStringOnNumbers(string text, string field)
        {
            if (!double.TryParse(text, out double numberResult))
            {
                throw new ArgumentException($"String {field} should contain only numbers");
            }
        }
        
        /// <summary>
        /// Сhecks if the double value is included in the interval
        /// </summary>
        /// <param name="value"> Value of the parameter</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        /// <param name="field">Field name</param>
        public static void AssertNumberOnRange(double number, double min, double max, string field)
        {
            if (number < min || number > max)
            {
                throw new ArgumentException($"{field} should be more than {min} and less than {max}");
            }
        }
    }
}
