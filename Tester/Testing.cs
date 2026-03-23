using SD_Week1;
using SD_Week4;
using SD_ZH_Example1;

namespace Tester
{
    public class PrimeTests
    {
        [TestCase(7, true)]
        [TestCase(4, false)]
        public void IsPrimeTest(int number, bool result)
        {
            PrimeTool tmp = new PrimeTool(number);
            Assert.IsTrue(tmp.IsPrime() == result);
        }
    }

    public class ArrayTests
    {
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
    }

    public class StackTests
    {
        [TestCase(0, false)]
        [TestCase(1, true)]
        public void CanPush(int size, bool result)
        {
            Stack tmp = new Stack(size);
            Assert.IsTrue(tmp.Push('t') == result);
        }

        [TestCase(3, false)]
        [TestCase(2, true)]
        public void CanPop(int popCount, bool result)
        {
            Stack tmp = new Stack(2);
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
            Stack tmp = new Stack(3);
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
            Stack tmp = new Stack(3);
            for (int i = 0; i < pushCount; i++)
            {
                tmp.Push('t');
            }

            Assert.IsTrue(tmp.Full() == result);
        }
    }

    public class AnimalTests
    {

        [TestCase(1, true)]
        [TestCase(0, false)]
        public void CanFitInCage(int size, bool result)
        {
            Cage tmp = new Cage(size);

            Assert.IsTrue(tmp.Add(new Animal("J�zsef", true, 10, Species.Panda)) == result);
        }
    }
    
    public class ZHTests
    {
        [TestCase(100, false, 400)]
        [TestCase(20, true, 200)]
        [TestCase(20000, true, -1)]
        public void EnvelopePrice(int weight, bool fromLocker, double result)
        {
            Envelope env = new Envelope(weight, "somewhere", "something");
            
            if (result == -1)
            {
                Assert.Throws<OverweightException>(() => env.CaculatePrice(fromLocker));
                return;
            }
            
            Assert.AreEqual(result, env.CaculatePrice(fromLocker));
        }
        
        [TestCase(200, 1400)]
        [TestCase(20, -1)]
        public void FragilePrice(int weight,  double result)
        {
            FragileParcel env = new FragileParcel(weight, "somewhere", Positioning.Vertical);
            
            if (result == -1)
            {
                Assert.Throws<DeliveryException>(() => env.CaculatePrice(true));
                return;
            }
            
            Assert.AreEqual(result, env.CaculatePrice(false));
        }
        
        [Test]
        public void FragileCreation()
        {
            Assert.Throws<IncorrectOrientationException>(() => new FragileParcel(10, "somewhere", Positioning.Arbitrary));
        }
        
        [Test]
        public void CourierPickup()
        {
            Courier cur = new Courier(10);
            
            cur.PickupItem(new Envelope(100, "somewhere", "something"));
            
            Assert.AreEqual(cur.ActualWeight, 100);
        }

        [Test]
        public void CourierSort()
        {
            Courier cur = new Courier(10);

            IDeliverable[] result =
            {
                new FragileParcel(100, "somewhere", Positioning.Vertical),
                new FragileParcel(120, "somewhere", Positioning.Vertical),
                new FragileParcel(140, "somewhere", Positioning.Vertical)
            };
            
            cur.PickupItem(result[0]);
            cur.PickupItem(result[1]);
            cur.PickupItem(result[2]);
            cur.PickupItem(new Envelope(100, "somewhere", "something"));
            cur.PickupItem(new Envelope(100, "somewhere", "something"));

            IDeliverable[] ja = cur.FragilesSorted();
            
            Assert.AreEqual(cur.ActualWeight, 560);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(result[i], ja[i]);
            }
        }

        
        
    }
}
