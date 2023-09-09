using FluentAssertions;
using System.Text.RegularExpressions;

namespace PhoneNumbers
{
    public class PhoneNumbersSolutionTests
    {
        public class FilterOutValidPhoneNumbers
        {
            private const string inputFile = "file.txt";

            [Fact]
            public void WhenFileEmpty_ReturnsEmpty()
            {
                InputFileContains(Array.Empty<string>());

                var valid = PhoneNumbersSolution.FilterOutValid(inputFile);

                valid.Should().BeEmpty();
            }

            [Theory]
            [InlineData("", "empty")]
            [InlineData("a", "not a degit")]
            [InlineData("1234-123-1234", "extra digit")]
            [InlineData("123-1234-1234", "extra digit")]
            [InlineData("123-123-12345", "extra digit")]
            [InlineData("12-123-1234", "not enough digits")]
            [InlineData("123-12-1234", "not enough digits")]
            [InlineData("123-123-123", "not enough digits")]
            [InlineData("(123)-123-1234", "dash after bracket instead of space")]
            [InlineData("(1234) 123-1234", "extra digit")]
            [InlineData("(123) 1234-1234", "extra digit")]
            [InlineData("(123) 123-12345", "extra digit")]
            [InlineData("(12) 123-1234", "not enough digits")]
            [InlineData("(123) 12-1234", "not enough digits")]
            [InlineData("(123) 123-123", "not enough digits")]
            public void WhenFileHasAnInvalidNumber_ReturnsEmpty(string input, string reason)
            {
                InputFileContains(Array.Empty<string>());

                var valid = PhoneNumbersSolution.FilterOutValid(inputFile);

                valid.Should().BeEmpty($"{input} is/has {reason}");
            }

            [Theory]
            [InlineData("123-123-1234", "a valid number format is: xxx-xxx-xxxx where x is digit.")]
            [InlineData("(123) 123-1234", "a valid number format is: (xxx) xxx-xxxx where x is digit.")]
            public void WhenFileHasAValidNumber_ReturnsAll(string validNumber, string reason)
            {
                InputFileContains(validNumber);

                var valid = PhoneNumbersSolution.FilterOutValid(inputFile);

                var expected = new[] { validNumber };
                valid.Should().BeEquivalentTo(expected, reason);
            }

            [Fact]
            public void WhenFileHasBothValidAndInvalidNubmers_ReturnsOnlyValid()
            {
                const string valid1 = "123-123-1234";
                const string valid2 = "(123) 123-1234";
                // missing 1 digit.
                const string invalid1 = "12-123-1234"; 
                InputFileContains(valid1, valid2, invalid1);

                var valid = PhoneNumbersSolution.FilterOutValid(inputFile);

                var expected = new[] { valid1, valid2 };
                valid.Should().BeEquivalentTo(expected);
            }

            void InputFileContains(params string[] numbers)
            {
                File.WriteAllLines(inputFile, numbers);
            }
        }
    }

    internal class PhoneNumbersSolution
    {
        private static readonly Regex regexToMatchValidPhones;

        static PhoneNumbersSolution()
        {
            const string digits3 = "\\d\\d\\d";
            const string digits4 = "\\d\\d\\d\\d";

            const string pattern1 = $"{digits3}-{digits3}-{digits4}";
            const string pattern2 = $"\\({digits3}\\) {digits3}-{digits4}";
            regexToMatchValidPhones = new Regex($"({pattern1})|({pattern2})", RegexOptions.Compiled);
        }

        internal static string[] FilterOutValid(string file)
        {
            var phoneNumbers = File.ReadAllText(file);
            var matches = regexToMatchValidPhones.Matches(phoneNumbers);

            return matches.Select(match => match.Value).ToArray();
        }
    }
}