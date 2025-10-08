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

        }
    }
}
