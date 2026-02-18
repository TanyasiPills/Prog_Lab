namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> data = new List<int>();
            data.Add(int.Parse(Console.ReadLine()));
            data.Add(int.Parse(Console.ReadLine()));
            data.Add(int.Parse(Console.ReadLine()));

            int number = 0;

            for (int i = 0; i < data.Count; i++)
            {
                if (Math.Abs(data[i]) > Math.Abs(number) || (Math.Abs(data[i]) == Math.Abs(number) && data[i] > number))
                {
                    number = data[i];
                }
            }

            Console.WriteLine($"{number}");
        }
    }
}
