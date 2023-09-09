namespace Solutions
{
    public class ReverseIntegerTests
    {
        /*
         
        In:x [-231, 230]  
        Out: 
        - x with its digits reversed. 
        - If reversing x causes the value to go out of range , then return 0.

        Assume the environment does not allow you to store 64-bit integers (signed or unsigned).

        Example 1:

        Input: x = 123
        Output: 321
        Example 2:

        Input: x = -123
        Output: -321
        Example 3:

        Input: x = 120
        Output: 21
         */

        public class Reverse
        {
            [Theory]
            [InlineData(-232)]
            [InlineData(132)]
            public void WhenOutOfRange_ReturnsInvalid0(int integer)
            {
                var reversed = ReverseInteger.Reverse(integer);

                reversed.Should().Be(ReverseInteger.Invalid);
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(1, 1)]
            [InlineData(12, 21)]
            [InlineData(121, 121)]
            [InlineData(120, 21)]
            [InlineData(-1, -1)]
            [InlineData(-12, -21)]
            public void WhenInRange_ReturnsReversed(int integer, int expected)
            {
                var reversed = ReverseInteger.Reverse(integer);

                reversed.Should().Be(expected);
            }
        }
    }

    internal class ReverseInteger
    {
        public const int Invalid = 0;

        internal static int Reverse(int integer)
        {
            var numberAsString = integer.ToString();
            
            var isNegative = integer < 0;
            if(isNegative)
            {
                numberAsString = numberAsString.Substring(1);
            }

            var reversedAsString = new string(numberAsString.Reverse().ToArray());
            var reversedAsNumber = int.Parse(reversedAsString);

            const int min = -231;
            const int max = 230;
            var isOutOfRange = (reversedAsNumber < min || reversedAsNumber > max);
            if (isOutOfRange)
            {
                return Invalid;
            }

            if(isNegative)
            {
                reversedAsNumber = reversedAsNumber * -1;
            }

            return reversedAsNumber;
        }
    }
}
