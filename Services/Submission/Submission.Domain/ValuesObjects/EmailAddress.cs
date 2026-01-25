using Blocks.Core;
using Blocks.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Submission.Domain.ValuesObjects
{
    public class EmailAddress : StringValueObject
    {
        public EmailAddress(string value) => Value = value;

        public static EmailAddress Create(string value)
        {
            Guard.ThrowIfNullOrWhiteSpace(value);

            if (!IsValidEmail(value))
            {
                throw new ArgumentException("Invalid email address format.", nameof(value));
            }

            return new EmailAddress(value);
        }

        private static bool IsValidEmail(string value)
        {
            var pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }
    }
}
