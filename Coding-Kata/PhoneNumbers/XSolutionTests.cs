using FluentAssertions;
using System.Collections.Generic;

namespace PhoneNumbers
{
    public class WordLettersEqualizerSolutionTests
    {
        /*
         
        You are given a 0-indexed string word, consisting of lowercase English letters.
        You need to select one index and remove the letter at that index from word so that the frequency of every letter present in word is equal.

        Return true if it is possible to remove one letter so that the frequency of all letters in word are equal, and false otherwise.

        Note:

        The frequency of a letter x is the number of times it occurs in the string.
        You must:
            - remove exactly one letter
            - cannot chose to do nothing.
 

        Example 1:

        Input: word = "abcc"
        Output: true
        Explanation: Select index 3 and delete it: word becomes "abc" and each character has a frequency of 1.
        Example 2:

        Input: word = "aazz"
        Output: false
        Explanation: We must delete a character, so either the frequency of "a" is 1 and the frequency of "z" is 2, or vice versa. It is impossible 
        to make all present letters have equal frequency.
 

        Constraints:

        2 <= word.length <= 100
        word consists of lowercase English letters only.
         
         */

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
