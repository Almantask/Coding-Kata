using FluentAssertions;
using System.Collections.Generic;

namespace PhoneNumbers
{
    public class WordLettersEqualizerSolutionTests
    {
        public class CanEqualize
        {
            [Theory]
            [InlineData("ab", false)]
            [InlineData("aab", true)]
            [InlineData("aabb", false)]
            [InlineData("aabbb", true)]
            [InlineData("abcdd", true)]
            public void Returns_WhetherCanEqualizeFrequenciesOfLetters(string word, bool expectedCanEqualize)
            {
                var canEqualize = WordLettersEqualizerSolution.CanEqualize(word);

                canEqualize.Should().Be(expectedCanEqualize);
            }
        }
    }

    public static class WordLettersEqualizerSolution
    {
        public static bool CanEqualize(string word)
        {
            var lettersCount = new Dictionary<char, int>();
            foreach (var letter in word)
            {
                if(!lettersCount.ContainsKey(letter))
                {
                    lettersCount.Add(letter, 0);
                }
                lettersCount[letter]++;
            }

            // To equalize:
            // - we need to take 1 from letter of max count.
            // - all other letter counts must be the same

            var maxLetterCount = lettersCount.Values.Max();
            var highestFrequenceLetter = lettersCount.First(kvp => kvp.Value == maxLetterCount);
            lettersCount.Remove(highestFrequenceLetter.Key);

            var anyOtherLetterCount = lettersCount.First().Value;
            var areAllWithTheSameCount = lettersCount.All(count => count.Value == anyOtherLetterCount);

            return maxLetterCount - anyOtherLetterCount == 1;
        }
    }
}
