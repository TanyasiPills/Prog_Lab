using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SD_Week4;

namespace Tester
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(7, true)]
        [TestCase(4, false)]
        public void IsPrimeTest(int number, bool result)
        {
            PrimeTool tmp = new PrimeTool(number);
            Assert.IsTrue(tmp.IsPrime() == result);
        }

        [TestCase(new int[] { 1, 2, 3, 6 }, true)]
        [TestCase(new int[] { 7, 2, 4, 3 }, false)]
        public void Contains(int[] numbers, bool result)
        {
            ArrayStatistics tmp = new ArrayStatistics(numbers);
            Assert.IsTrue(tmp.Contains(6) == result);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, true)]
        [TestCase(new int[] { 1, 2, 4, 3 }, false)]
        public void IsSorted(int[] numbers, bool result)
        {
            ArrayStatistics tmp = new ArrayStatistics(numbers);
            Assert.IsTrue(tmp.Sorted() == result);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, true)]
        [TestCase(new int[] { 7, 2, 4, 3 }, true)]
        public void CanSort(int[] numbers, bool result)
        {
            ArrayStatistics tmp = new ArrayStatistics(numbers);
            tmp.Sort();
            Assert.IsTrue(tmp.Sorted() == result);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, -1)]
        [TestCase(new int[] { 7, 2, 4, 3 }, 0)]
        public void FirstGreater(int[] numbers, int result)
        {
            ArrayStatistics tmp = new ArrayStatistics(numbers);
            Assert.IsTrue(tmp.FirstGreater(5) == result);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, 2)]
        [TestCase(new int[] { 7, 1, 9, 3 }, 0)]
        public void CountEvens(int[] numbers, int result)
        {
            ArrayStatistics tmp = new ArrayStatistics(numbers);
            Assert.IsTrue(tmp.CountEvens() == result);
        }


        [TestCase(new int[] { 1, 2, 4, 3 }, 2)]
        public void MaxIndex(int[] numbers, int result)
        {
            ArrayStatistics tmp = new ArrayStatistics(numbers);
            Assert.IsTrue(tmp.MaxIndex() == result);
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        public void CanPush(int size, bool result)
        {
            Stack tmp = new SD_Week4.Stack(size);
            Assert.IsTrue(tmp.Push('t') == result);
        }

        [TestCase(3, false)]
        [TestCase(2, true)]
        public void CanPop(int popCount, bool result)
        {
            Stack tmp = new SD_Week4.Stack(2);
            for (int i = 0; i < 2; i++)
            {
                tmp.Push('t');
            }
            char output;
            for (int i = 0; i < popCount; i++)
            {
                if (tmp.Pop(out output) == false)
                {
                    Assert.IsTrue(result == false);
                    return;
                }
            }
            Assert.IsTrue(result == true);
        }

        [TestCase(3, false)]
        [TestCase(0, true)]
        public void IsEmpty(int pushCount, bool result)
        {
            Stack tmp = new SD_Week4.Stack(3);
            for (int i = 0; i < pushCount; i++)
            {
                tmp.Push('t');
            }
            
            Assert.IsTrue(tmp.Empty() == result);
        }

        [TestCase(3, true)]
        [TestCase(0, false)]
        public void IsFull(int pushCount, bool result)
        {
            Stack tmp = new SD_Week4.Stack(3);
            for (int i = 0; i < pushCount; i++)
            {
                tmp.Push('t');
            }

            Assert.IsTrue(tmp.Full() == result);
        }
    }
}