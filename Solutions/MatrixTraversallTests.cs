//namespace Solutions
//{
//    public class MatrixTraversallTests
//    {
//        // Given an m x n matrix, return all elements of the matrix in spiral order:
//        // - Starts from top left corner
//        // - Goes around the edges
//        // - Moves towards the center

//        public class ToSpiralOrder
//        {
//            [Theory]
//            [MemberData(nameof(TestData))]
//            public void Expectation(int[][] matrix, int[] expected)
//            {
//                var inSpiralOrder = Matrix.ToSpiralOrder(matrix);

//                inSpiralOrder.Should().BeEquivalentTo(expected, opt => opt.WithStrictOrdering());
//            }

//            public static IEnumerable<object[]> TestData
//            {
//                get
//                {
//                    yield return new object[] { };
//                    //yield return new object[]
//                    //{
//                    //new int[][]
//                    //{
//                    //    new int[]{ 1 }
//                    //},
//                    //new int[]{ 1 },
//                    //};

//                    //yield return new object[]
//                    //{
//                    //new int[][]
//                    //{
//                    //    new int[]{ 1, 2 },
//                    //    new int[]{ 3, 4 }
//                    //},
//                    //new int[]{ 1, 2, 4, 3 }
//                    //};
//                }
//            }
//        }
//    }

//    internal class Matrix
//    {
//        internal static int[] ToSpiralOrder(int[][] matrix)
//        {
//            var traversed = new int[matrix.Length*matrix[0].Length];
//            var currentTraversed = 0;
//            // M - rows
//            // N - cols

//            var nMax = matrix.Length;
//            var nMin = -1; // Exception to make sure we go first time all the way to the top
//            var mMax = matrix[0].Length;
//            var mMin = 0;

//            var borderM = mMax;
//            var boderN = nMax;

//            var currentM = 0;
//            var currentN = 0;
             
//            // To avoid inifinite loop.
//            int maxTimesToTraverse = 100;

//            while (maxTimesToTraverse > 0)
//            {
//                if (!IterateRight())
//                {
//                    return traversed;
//                }

//                if (!IterateDown())
//                {
//                    return traversed;
//                }

//                if (!IterateLeft())
//                {
//                    return traversed;
//                }

//                if (!IterateUp())
//                {
//                    return traversed;
//                }

//                maxTimesToTraverse--;
//            }

//            return matrix[0];

//            // The gist of the algorithm is to change the increments and maxes, so that the direction "rotates".
//            bool IterateRight()
//            {
//                var movedAhead = Iterate(0, 1);
//                mMax--;
//                borderM = mMax;

//                return movedAhead;
//            }

//            bool IterateDown()
//            {
//                var movedAhead = Iterate(1, 0);
//                nMin++;
//                boderN = nMin;

//                return movedAhead;
//            }

//            bool IterateUp()
//            {
//                var movedAhead = Iterate(-1, 0);
//                mMin++;
//                borderM = mMin;

//                return movedAhead;
//            }

//            bool IterateLeft()
//            {
//                var movedAhead = Iterate(0, -1);
//                nMax--;
//                boderN = nMax;

//                return movedAhead;
//            }

//            bool Iterate(int mIncrement, int nIncrement)
//            {
//                var oldTraversed = currentTraversed;
//                if(mIncrement != 0)
//                {
//                    for (; currentM < borderM; currentM += mIncrement)
//                    {
//                        traversed[currentTraversed] = matrix[currentM][currentN];

//                        currentTraversed++;
//                    }
//                    currentM--;
//                }

//                if(nIncrement != 0)
//                {
//                    for (; currentN < boderN; currentN += nIncrement)
//                    {
//                        traversed[currentTraversed] = matrix[currentM][currentN];

//                        currentTraversed++;
//                    }
//                    currentN--;
//                }

//                return currentTraversed != oldTraversed;
//            }
//        }
//    }
//}
