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
            this.delay = 0;
        }

        public Flight(int id, string destination, DateTime departure, int delay)
        {
            this.id = id;
            this.destination = destination;
            this.departure = departure;
            this.delay = delay;
        }

        private void UpdateStatus(Status statusIn)
        {
            status = statusIn;
        }

        private void UpdateStatus()
        {
            if (delay > 0) status = Status.Delayed;
            else status = Status.Scheduled;
        }

        private string AllData()
        {
            string output = "";
            switch (status)
            {
                case Status.Scheduled:
                    output = "is on time";
                    break;
                case Status.Delayed:
                    output = $"is delayed by {delay} minutes";
                    break;
                case Status.Canceled:
                    output = "is canceled";
                    break;
            }

            return $"Flight {id} {output}. ({departure.Date})";
        }

        public DateTime EstimatedDeparture()
        {
            return departure.AddMinutes(delay);
        }

        public void DelayFlight(int delayIn)
        {
            delay = delayIn;
        }
        public void Cancel() 
        {
            status = Status.Canceled;
        }

        public int Id { get => id; set => id = value; }
        public string Destination { get => destination; set => destination = value; }
        public DateTime Departure { get => departure; set => departure = value; }
        public int Delay { get => delay; set => delay = value; }
        public Status Status { get => status; set => status = value; }
    }

    class GroundControl
    {
        DateTime now;
        List<Flight> flights;
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
