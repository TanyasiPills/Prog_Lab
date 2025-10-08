using System.Xml;

namespace Week7
{
    class Mole
    {
        int position;
        Random rnd = new Random();

        public int Position { get => position; }

        public int TurnUp()
        {
            Console.WriteLine(new string(' ', position - 1) + "M");
            return position;
        }

        public void Hide(int min, int max)
        {
            position = rnd.Next(min, max+1);
        }
    }

    class Flight
    {
        int id;
        string destination;
        DateTime departure;
        int delay;
        Status status;

        public Flight(int id, string destination, DateTime departure)
        {
            this.id = id;
            this.destination = destination;
            this.departure = departure;
            this.delay = delay;
        }

        public Flight(int id, string destination, DateTime departure, int delay)
        {
            this.id = id;
            this.destination = destination;
            this.departure = departure;
            this.delay = delay;
        }

        public int Id { get => id; set => id = value; }
        public string Destination { get => destination; set => destination = value; }
        public DateTime Departure { get => departure; set => departure = value; }
        public int Delay { get => delay; set => delay = value; }
        public Status Status { get => status; set => status = value; }
    }

    public enum Status
    {
        Scheduled,
        Delayed,
        Canceled
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Feladat1();
            Feladat2();
        }

        static void Feladat1()
        {
            Mole mole = new Mole();

            while (true)
            {
                Console.Clear();
                3
                mole.Hide(1, 10);

                Console.WriteLine("Tippeld meg hogy hol van a vakond:");
                int guess = int.Parse(Console.ReadLine());

                if (mole.TurnUp() == guess) break;

                Console.WriteLine("\nNem találtad el :/");
                Console.ReadKey();
            }

            Console.WriteLine("\nMegtaláltad a vakondot! UvU");
        }

        static void Feladat2()
        {
            
        }
    }
}
