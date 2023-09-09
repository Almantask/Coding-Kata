using FluentAssertions;

namespace Solutions
{
    public class ChildrenMoneyDistributionTests
    {
        public class CountMaximumNumberOfChildrenWith8Bucks
        {
            [Theory]
            [InlineData(1, 2)]
            [InlineData(2, 3)]
            [InlineData(4, 2)]
            public void WhenDistributionNotPossible_ReturnsMinus1(int money, int children)
            {
                var countChildrenWith8 = ChildrenMoneyDistribution.CountMaximumNumberOfChildrenWith8Bucks(money, children);

                const int invalid = -1;
                countChildrenWith8.Should().Be(invalid, "there must be more or the same money than children");
            }

            [Theory]
            [InlineData(9, 2, 1)]
            [InlineData(16, 2, 2)]
            [InlineData(24, 2, 2)]
            [InlineData(19, 3, 2)]
            [InlineData(20, 2, 2)]
            [InlineData(20, 3, 1)]
            public void WhenDistributionPossible_ReturnsCountOfChildrenWith8Bucks(int money, int children, int expectedCount)
            {
                var countChildrenWith8 = ChildrenMoneyDistribution.CountMaximumNumberOfChildrenWith8Bucks(money, children);

                const int invalid = -1;
                countChildrenWith8.Should().Be(expectedCount);
            }
        }
    }

    public static class ChildrenMoneyDistribution
    {
        public static int CountMaximumNumberOfChildrenWith8Bucks(int money, int children)
        {
            var canDistribute = money >= children;

            if(!canDistribute)
            {
                return -1;
            }

            var maxWith8 = money / 8;

            if(maxWith8 >= children)
            {
                // everyone gets at least 8.
                return children;
            }

            var isLeftover4 = money % 8 == 4;

            if(isLeftover4)
            {
                maxWith8--;
            }

            return maxWith8;
        }
    }
}
