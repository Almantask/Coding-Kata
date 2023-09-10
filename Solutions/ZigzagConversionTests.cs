namespace Solutions
{
    using System.Text;
    using static Environment;
    /*
     
    The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows like this:
    (you may want to display this pattern in a fixed font for better legibility)

    P   A   H   N
    A P L S I I G
    Y   I   R
    And then read line by line: "PAHNAPLSIIGYIR"

    Write the code that will take a string and make this conversion given a number of rows:

    string convert(string s, int numRows);
 

    Example 1:

    Input: s = "PAYPALISHIRING", numRows = 3
    Output: "PAHNAPLSIIGYIR"
    Example 2:

    Input: s = "PAYPALISHIRING", numRows = 4
    Output: "PINALSIGYAHRPI"
    Explanation:
    P     I    N
    A   L S  I G
    Y A   H R
    P     I
    Example 3:

    Input: s = "A", numRows = 1
    Output: "A"
 

     */

    public class ZigzagConversionTests
    {
        public class Convert
        {
            [Theory]
            [InlineData("")]
            [InlineData("a")]
            [InlineData("ab")]
            public void When1Row_ReturnsTheSameText(string text)
            {
                var converted = ZigzagConversion.Convert(text, 1);

                converted.Should().Be(text);
            }

            [Theory]
            [MemberData(nameof(ZigzagExpectations))]
            public void WhenMoreThan2Rows_ReturnsTextInZigzag(string text, int rows, string expected)
            {
                var converted = ZigzagConversion.Convert(text, rows);

                converted.Should().Be(expected);
            }

            public static IEnumerable<object[]> ZigzagExpectations
            {
                get
                {
                    // 2x1
                    // a
                    // b
                    yield return new object[] { "ab", 2, $"a{NewLine}b"};

                    // 2x2
                    // a c
                    // b
                    yield return new object[] { "abc", 2, $"a c{NewLine}b"};

                    // 2x2
                    // a c
                    // b d
                    yield return new object[] { "abcd", 2, $"a c{NewLine}b d"};

                    // 3x2
                    // a
                    // b d
                    // c
                    yield return new object[] { "abcd", 3, $"a{NewLine}b d{NewLine}c" };

                    // 3x3
                    // a   e
                    // b d
                    // c
                    yield return new object[] { "abcde", 3, $"a   e{NewLine}b d{NewLine}c" };

                    // 3x3
                    // a   e
                    // b d f
                    // c
                    yield return new object[] { "abcdef", 3, $"a   e{NewLine}b d f{NewLine}c" };
                    
                    // 3x3
                    // a   e
                    // b d f
                    // c   g
                    yield return new object[] { "abcdefg", 3, $"a   e{NewLine}b d f{NewLine}c   g" };

                    // 4x4
                    // a     g
                    // b   f
                    // c e
                    // d
                    yield return new object[] { "abcdefg", 4, $"a     g{NewLine}b   f{NewLine}c e{NewLine}d" };
                }
            }
        }
    }

    internal class ZigzagConversion
    {
        internal static string Convert(string text, int rows)
        {
            // Pattern:
            // First column goes all the way down in chars for as many rows as there are:
            // col = 0; row = 0 -> rows
            // Then +1 +1 for the next, repeat for (rows - 1)
            var lineBuilders = new StringBuilder[rows];
            for (int i = 0; i < lineBuilders.Length; i++)
            {
                lineBuilders[i] = new StringBuilder();
            }

            var previous = -1;
            var current = 0;
            while (previous != current)
            {
                // Draw column
                previous = current;
                for (var i = 0; i < rows; i++)
                {
                    if (current < text.Length)
                    {
                        char character = text[current];
                        lineBuilders[i].Append(character);
                        current++;
                    }
                }

                // Draw diagonal
                for (var i = 0; i < rows - 2; i++)
                {
                    if (current < text.Length)
                    {
                        char character = text[current];
                        lineBuilders[rows - 1 - i].Append($"{new string(' ', i * 2 + 1)}{character}");
                        current++;
                    }

                }
                if (current < text.Length)
                {
                    // Add blanks before the next column
                    var whitepsaces = (rows - 2) * 2 + 1;
                    if (whitepsaces > 0)
                    {
                        lineBuilders[0].Append(new string(' ', (rows - 2) * 2 + 1));
                    }
                }

            }

            var resultBuilder = new StringBuilder();
            for (int i = 0; i < lineBuilders.Length; i++)
            {
                StringBuilder lineBuilder = lineBuilders[i];
                resultBuilder.AppendLine(lineBuilder.ToString());
            }

            var converted = resultBuilder.ToString();

            return converted.Trim();
        }
    }
}
