namespace SD_Week4
{
    public class PrimeTool
    {
        int number;

        public PrimeTool(int number)
        {
            this.number = number;
        }

        public bool IsPrime()
        {
            if (number < 1)
                return false;

            if (number == 1 || number == 2)
                return true;

            for (int i = 2; i * i <= number; i ++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }

    public class ArrayStatistics
    {
        int[] numbers;

        public ArrayStatistics(int[] numbers)
        {
            this.numbers = numbers;
        }

        public int Total() => numbers.Length;

        public bool Contains(int number)
        {
            foreach (var item in numbers)
            {
                if (item == number) return true;
            }
            return false;
        }

        public bool Sorted()
        {
            int prev = numbers[0];
            foreach (var item in numbers)
            {
                if (prev > item) return false;
                else prev = item;
            }
            return true;
        }

        public int FirstGreater(int number)
        {
            for (int i = 0; i < numbers.Length; i++) {
                if (number < numbers[i]) return i;
            }
            return -1;
        }

        public int CountEvens()
        {
            int evens = 0;

            foreach (var item in numbers)
            {
                if (item % 2 == 0) evens++;
            }
            return evens;
        }

        public int MaxIndex()
        {
            int maxIndex = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > numbers[maxIndex]) maxIndex = i;
            }

            return maxIndex;
        }

        public void Sort()
        {
            for (int i = numbers.Length-1; i > 0 ; i--)
            {
                for (int z = 0; z < i; z++)
                {
                    if (numbers[z] > numbers[z+1])
                    {
                        int tmp = numbers[z+1];
                        numbers[z+1] = numbers[z];
                        numbers[z] = tmp;
                    }
                }
            }
        }
    }

    public class Stack
    {
        char[] stack;

        public Stack(int size)
        {
            stack = new char[size];
        }

        int ptr = 0;

        public bool Push(char chr)
        {
            if (ptr == stack.Length) return false;
            stack[ptr++] = chr;
            return true;
        }

        public bool Pop(out char chr)
        {
            chr = '\0';
            if (ptr == 0) return false;
            chr = stack[--ptr];
            return true;
        }

        public bool Empty()
        {
            return ptr == 0;
        }

        public bool Full()
        {
            return ptr == stack.Length;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
