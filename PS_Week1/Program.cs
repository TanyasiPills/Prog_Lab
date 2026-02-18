namespace Labor01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Feladat1_2();
            //Feladat3();
            //Feladat4();
            //Feladat5();
            //Feladat6();
            Feladat7_8();
            //Feladat9_10();
            //Feladat11();
            //Feladat12();
            //Feladat13();
        }

        static void Feladat1_2()
        {
            Console.WindowWidth = 60;
            Console.WindowHeight = 20;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Hello, World!");
            Console.SetCursorPosition(0, 0);
            Console.ReadLine();
            Console.WindowHeight = 25;
            Console.WindowWidth = 80;
            Console.CursorVisible = true;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Feladat3()
        {
            Console.Write("Kérem a nevét: ");
            string nev = Console.ReadLine();
            Console.WriteLine($"Nos üdv, {nev}");
            Console.ReadLine();
        }

        static void Feladat4()
        {
            Console.Write("Add meg a születési éved: ");
            int birth = int.Parse(Console.ReadLine());

            DateTime now = DateTime.Now;
            int age = now.Year - birth;
            Console.WriteLine($"Az életkorod: {age} év");
        }

        static void Feladat5()
        {
            Console.Write("Kérlek add meg a magasságod(m): ");
            float magassag = float.Parse(Console.ReadLine().Replace('.', ','));
            Console.Write("Kérlek add meg a testtömeged(kg): ");
            float testtomeg = float.Parse(Console.ReadLine().Replace('.', ','));
            Console.WriteLine($"A felhasználó BMI-je: {Math.Round(testtomeg / Math.Pow(magassag, 2),0)}");
        }

        static void Feladat6()
        {
            Console.Write("Az időtartam másodpercben: ");
            int time = int.Parse(Console.ReadLine());
            Console.WriteLine($"Az időtartam formázva: {time/60}:{(time%60<10 ? "0":"")}{time%60}");
        }

        static void Feladat7_8()
        {
            string pwd1 = "";
            string pwd2 = "";

            Console.Write("Kérem a jelszavát: ");
            pwd1 = ReadPWD();
            Console.Write("Kérem adja meg a jelszavát még egyszer: ");
            pwd2 = ReadPWD();

            if(pwd1 == pwd2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A jelszó megerősítve");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A jelszavak nem egyeznek meg");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string ReadPWD()
        {
            int rootKeyPos;
            string pwd = "";

            int prevKeyPos = Console.GetCursorPosition().Left;
            rootKeyPos = prevKeyPos;

            bool needPWD = true;
            while (needPWD)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                {
                    needPWD = false;
                    break;
                }

                if (key.Key == ConsoleKey.Backspace && pwd.Length > rootKeyPos)
                {
                    pwd = pwd.Remove(pwd.Length - 1);
                    prevKeyPos--;
                    Console.SetCursorPosition(prevKeyPos, Console.GetCursorPosition().Top);
                    Console.Write(' ');
                    Console.SetCursorPosition(prevKeyPos, Console.GetCursorPosition().Top);
                    continue;
                } else
                {
                    Console.SetCursorPosition(prevKeyPos, Console.GetCursorPosition().Top);
                    Console.Write('#');
                }

                    pwd += key.KeyChar;
                prevKeyPos++;
            }

            Console.WriteLine();

            return pwd;
        }

        static void Feladat9_10()
        {
            Console.Write("Adja meg az egyenletet: ");
            string input = Console.ReadLine();
            input = input.Trim();
            string prev = "";
            input.Split(new char[]{ '(', ')' }).Where(e => e != "").Reverse().ToList().ForEach((e) => { 
                string inp = e+prev;
                int res = Calculate(inp);
                prev = res.ToString();
            });

            Console.WriteLine($"Eredmény: {prev}");
        }

        static int Calculate(string input)
        {
            input = input.Trim();

            string opString = input.Contains('+') ? "+" : input.Contains('-') ? "-" : input.Contains('*') ? "*" : input.Contains('/') ? "/" : "";
            char op = opString[0];

            int a = int.Parse(input.Split(op)[0]);
            int b = int.Parse(input.Split(op)[1]);

            return op switch
            {
                '+' => (int)(a + b),
                '-' => (int)(a - b),
                '*' => (int)(a * b),
                '/' => b != 0 ? (int)(a / b) : throw new DivideByZeroException(),
                _ => throw new InvalidOperationException($"A művelet nem értelmezhető: {input}"),
            };
        }

        static void Feladat11()
        {
            Console.Write("Adj meg egy számot: ");
            int n = int.Parse(Console.ReadLine());
            if(n < 0 || n > 9)
            {
                Console.WriteLine("A szám nkiesik a értékkészletből");
                return;
            }
            string[] numbers = { "nulla", "egy", "kettő", "három", "négy", "öt", "hat", "hét", "nyolc", "kilenc" };
            Console.WriteLine($"Az álltalad megadott szám: "+numbers[n]);
        }

        static void Feladat12()
        {
            string[] vowels = { "a", "e", "i", "o", "u", "á", "é", "í", "ó", "ö", "ő", "ú", "ü", "ű" };
            Console.Write("Kérek egy karaktert: ");
            char c = Console.ReadKey().KeyChar;
            if(vowels.Contains(c.ToString().ToLower()))
                Console.WriteLine("\nA megadott karakter magánhangzó");
            else
                Console.WriteLine("\nA megadott karakter nem magánhangzó");
        }

        static void Feladat13()
        {
            float vValue = 0;
            Console.Write("V(m^3)= ");
            float V = float.Parse(Console.ReadLine().Replace('.', ',')); 
            Console.Write("R1(m^3)= ");
            float R1 = float.Parse(Console.ReadLine().Replace('.', ','));
            Console.Write("R2(m^3)= ");
            float R2 = float.Parse(Console.ReadLine().Replace('.', ','));
            Console.Write("T(hour)= ");
            int T = int.Parse(Console.ReadLine());

            for (int i = 0; i < T; i++)
            {
                vValue += R1 + R2;
            }

            if(vValue < V) Console.WriteLine($"-> A tartáj {vValue/V}%-ban lesz tele");
            else if(vValue > V) Console.WriteLine($"-> A tartály {vValue-V} m^3-rel lesz túltöltve");
            else Console.WriteLine("-> A tartály tökéletesen megtelik");
        }
    }
}
