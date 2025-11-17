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
        Status status = Status.Scheduled;

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
            this.status = Status.Delayed;
        }

        public void UpdateStatus(Status statusIn)
        {
            status = statusIn;
        }

        public void UpdateStatus()
        {
            if (delay > 0) status = Status.Delayed;
            else status = Status.Scheduled;
        }

        private string AllDataRaw()
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
            status = Status.Delayed;
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

        public string AllData { get => AllDataRaw(); }
    }

    class GroundControl
    {
        DateTime now;
        List<Flight> flights = new List<Flight>();

        public void AddFlight(params Flight[] flightsIn)
        {
            foreach (var item in flightsIn) flights.Add(item);
        }

        public float AvarageDelay()
        {
            return (float)Math.Round(flights.Where(e => e.Status != Status.Canceled).Average(e => (float)e.Delay), 2);
        }

        public void DisplayFlightData()
        {
            foreach (var item in flights)
            {
                Console.WriteLine(item.AllData);
            }
        }
    }

    public enum Status
    {
        Scheduled,
        Delayed,
        Canceled
    }

    class ExamResult
    {
        string neptun = "";
        int point;

        public ExamResult(string neptun, int point)
        {
            Neptun = neptun;
            Point = point;
        }

        public ExamResult()
        {
            Random rnd = new Random();
            for (int i = 0; i < 6; i++) neptun += (char)rnd.Next(65, 91);
            point = rnd.Next(0, 101);
        }

        public Grades Grade(int[] array)
        {
            Grades finalGrade = Grades.Elegtelen;

            for (int i = 0; i < array.Length; i++)
            {
                if (point >= array[i]) finalGrade = (Grades)i;
            }

            return finalGrade;
        }

        private string SetNeptun(string value)
        {
            if (value.Length == 6) return value;
            else throw new Exception("Nem megfelelő neptunkód érték");
        }
        private int SetPoint(int value)
        {
            if (value >= 0 && value <= 100) return value;
            else throw new Exception("Nem megfelelő pont érték");
        }
        public string Neptun { get => neptun; set => neptun = SetNeptun(value); }
        public int Point { get => point; set => point = SetPoint(value); }
        public bool Passed { get => point >= 50; }
    }
    
    public enum Grades
    {
        Elegtelen,
        Elegseges,
        Kozepes,
        Jo,
        Jeles
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Feladat1();
            //Feladat2();
            Feladat3();
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
            Flight fl1 = new Flight(12, "Ozz Birodalma", DateTime.Now);
            Flight fl2 = new Flight(13, "A Konyha", DateTime.UtcNow, 10);

            GroundControl gc = new GroundControl();
            gc.AddFlight(fl1, fl2);

            gc.DisplayFlightData();
            Console.WriteLine($"Avarage delay is {gc.AvarageDelay()} minutes\n");

            gc = new GroundControl();
            fl1 = new Flight(12, "Los Santos", DateTime.Now);
            fl2 = new Flight(13, "Unknown", DateTime.UtcNow);
            fl2.Cancel();
            fl1.DelayFlight(15);
            fl1.UpdateStatus();

            gc.AddFlight(fl1, fl2);
            gc.DisplayFlightData();
            Console.WriteLine($"Avarage delay is {gc.AvarageDelay()} minutes");
        }
        
        static void Feladat3()
        {
            Console.Write("Add meg hogy hány eredményt szeretnél: ");
            int input = int.Parse(Console.ReadLine());

            List<ExamResult> list = new List<ExamResult>();

            for (int i = 0; i < input; i++)
            {
                list.Add(new ExamResult());
            }

            Console.WriteLine(list.Where(e => e.Passed).Aggregate("Sikeres dolgozatok neptun kódjai:\n", (a, b) => a += $"{b.Neptun}\n"));
            Console.WriteLine($"Pontszámok átlaga: {list.Average(e => e.Point)}");
            Console.WriteLine($"A legmagasabb pontszámot elérő diák nemtun kódja: {list.MaxBy(e => e.Point).Neptun}");
        }
    }
}

// if they are inside of a () like: (()() ()() ()()) then it means 2^3 so () ( ()() ) means 1 + 2^1 = 3