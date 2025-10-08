using System.Reflection.Metadata.Ecma335;

namespace Week6
{
    class Book
    {
        string author;
        string title;
        int realeaseDate;
        int pageCount;

        public Book(string author, string title, int realeaseDate, int pageCount)
        {
            this.author = author;
            this.title = title;
            this.realeaseDate = realeaseDate;
            this.pageCount = pageCount;
        }

        public string AllData()
        {
            return $"{author}: {title}, {realeaseDate} ({pageCount} page)";
        }
    }

    class Rectangle
    {
        int x, y;
        ConsoleColor color;

        public Rectangle(int x, int y, ConsoleColor color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public void Draw()
        {
            Console.BackgroundColor = color;
            Console.WriteLine(string.Concat(Enumerable.Repeat("\t" + new string(' ', x) + "\n", y)));
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public bool IsValid()
        {
            return Area() > 0;
        }

        float Area()
        {
            return x * y;
        }
    }

    class Runner
    {
        string name;
        int id;
        int speed; // m/s
        int distance;

        public Runner(string name, int id, int speed)
        {
            this.name = name;
            this.id = id;
            this.speed = speed;
            this.distance = 0;
        }

        public void RefreshDistance(int deltaTime) // s 
        {
            distance += deltaTime * speed;
        }

        public void Show()
        {
            Console.WriteLine($"{id}{new string(' ', distance)}{name[0]}");
        }

        public int GetDistance()
        {
            return distance;
        }
    }

    class Crypter
    {
        int shift;

        public Crypter(int shift)
        {
            this.shift = shift;
        }

        private string TransformMessage(string input, int shift)
        {
            return input.Aggregate("", (x, y) => x += (char)((y + shift) % 256));
        }

        public string Encode(string input)
        {
            return TransformMessage(input, shift);
        }

        public string Decode(string input)
        {
            return TransformMessage(input, 256 - shift);
        }
    }

    class Human
    {
        int id;
        string date;
        float gender;
        float age;
        float bmi;
        float bs;

        public Human(int id, string date, float gender, float age, float bmi, float bs)
        {
            this.id = id;
            this.date = date;
            this.gender = gender;
            this.age = age;
            this.bmi = bmi;
            this.bs = bs;
        }

        public int Id { get => id; set => id = value; }
        public string Date { get => date; set => date = value; }
        public float Gender { get => gender; set => gender = value; }
        public float Age { get => age; set => age = value; }
        public float Bmi { get => bmi; set => bmi = value; }
        public float Bs { get => bs; set => bs = value; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Feladat1();
            //Feladat2();
            //Feladat3();
            //Feladat4();
            Feladat5();
        }

        static void Feladat1()
        {
            Book example = new Book("J.R.R. Tolkien", "The Hobbit - or There and Back Again", 1937, 312);
            Console.WriteLine(example.AllData());
        }

        static void Feladat2()
        {
            Rectangle rec1 = new Rectangle(10, 7, ConsoleColor.Blue);
            Rectangle rec2 = new Rectangle(2, -3, ConsoleColor.Red);

            Console.WriteLine($"x = 10 ; y = 7 => {rec1.IsValid()}");
            Console.WriteLine($"x = 2 ; y = -3 => {rec2.IsValid()}\n");

            rec1.Draw();
        }

        static void Feladat3()
        {
            int finish = 30;

            Runner rn1 = new Runner("Csabó", 283, 4);
            Runner rn2 = new Runner("Józsi", 983, 3);

            while(rn1.GetDistance() < finish || rn2.GetDistance() < finish)
            {
                rn1.RefreshDistance(1);
                rn2.RefreshDistance(1);

                rn1.Show();
                rn2.Show();
                Console.WriteLine();
            }
        }

        static void Feladat4()
        {
            Crypter crp = new Crypter(12);

            string example = "Egy easy peasy test";
            Console.WriteLine(example);
            string encoded = crp.Encode(example);
            Console.WriteLine(encoded);
            Console.WriteLine(crp.Decode(encoded));
        }
        
        static void Feladat5()
        {
            string projDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            List<Human> data = new List<Human>();

            using (StreamReader rd = new StreamReader(projDir + "\\NHANES_1999-2018.csv")) data = rd.ReadToEnd().Split('\n').Skip(1).Select(e => e.Split(',')).Select(e => new Human(int.Parse(e[0]), e[1], float.Parse(e[2].Replace('.', ',')), float.Parse(e[3].Replace('.', ',')), float.Parse(e[4].Replace('.', ',')), float.Parse(e[4].Replace('.', ',')))).ToList();

            Console.WriteLine($"A felmérésben az átlagos testtömegindexek:\n-férfi: {data.Where(e => e.Gender == 1.0).Average(e => e.Bmi)}\n-nő: {data.Where(e => e.Gender == 2.0).Average(e => e.Bmi)}");
            Console.WriteLine($"Az alanyok {(float)data.Where(e => e.Bs > 5.6).ToList().Count / data.Count}%-nak 5.6-nál magasabb a vércukorszintje");
            Console.WriteLine($"A legnagyobb BMI-vel rendelkező alany vércukorszintje: {data.MaxBy(e => e.Bmi).Bs}");
            Console.WriteLine($"A túlsúlyos alanyok átlag életkora: {data.Where(e => e.Bmi >= 30).Average(e => e.Age)}");
        }
    }
}
