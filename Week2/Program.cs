namespace Week2
{
    internal class Program
    {
        static Random rnd = new();

        static void Main(string[] args)
        {
            //Feladat1();
            //Feladat2();
            //Feladat3();
            //Feladat4();
            //Feladat5();
            //Feladat6();
            //Feladat7();
            //Feladat8();
            //Feladat9();
            //Feladat10();
            //Feladat11_12();
            Feladat13();
        }

        static void Feladat1()
        {
            Console.Write("Kérem adjon meg egy pozitív egész számot: ");
            if (!uint.TryParse(Console.ReadLine(), out uint n))
            {
                Console.WriteLine("A megadott érték nem volt megfelelő");
                return;
            }

            Console.Write("A megadott számíg a páros számok: ");
            for (int i = 2; i < n; i += 2)
            {
                Console.Write($"{i}, ");
            }
        }

        static void Feladat2()
        {
            Console.Write("Kérem adja meg a jelszavát: ");
            string pwd = Console.ReadLine();

            bool needPwd = true;
            int attempts = 3;
            while (needPwd && attempts > 0)
            {
                Console.Clear();
                Console.WriteLine($"Kérem ismét adja meg a jelszavát ({attempts} próbálkozás áll rendelkezésre): ");
                string input = Console.ReadLine();
                if (input == pwd)
                {
                    needPwd = false;
                    Console.WriteLine("Sikeres bejelentkezés");
                }
                else
                {
                    attempts--;
                }
            }
            if (attempts == 0)
            {
                Console.Clear();
                Console.WriteLine("Hiba: Túl sok próbálkozás, capcha elvégzése szükséges");
            }
        }

        static void Feladat3()
        {
            Console.Write("Kérem adjon meg egy számot 1 és 1000 között: ");

            int number = int.Parse(Console.ReadLine());
            int guess = 0;
            int attemps = 0;

            do
            {
                guess = rnd.Next(1, 1001);
                attemps++;
            } while (number != guess);

            Console.WriteLine($"A szám: {number}, a gép {attemps} próbálkozás után találta el.");
        }

        static void Feladat4()
        {
            bool readyToPlay = false;

            Console.Write("Kérem adja meg a játékosok számát: ");
            int playerCount = int.Parse(Console.ReadLine());
            int currentPlayer = 1;

            while (!readyToPlay)
            {
                Console.WriteLine($"Enter lenyomásával dob a(z) {currentPlayer}. játékos");
                Console.ReadLine();
                int random = rnd.Next(1, 7);
                Console.WriteLine($"A dobott érték: {random}\n" +
                    $"");
                if (random == 6)
                {
                    readyToPlay = true;
                    Console.WriteLine($"A(z) {currentPlayer}. játékos kezdhet");
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer > playerCount)
                    {
                        currentPlayer = 1;
                    }
                }
            }
        }

        static void Feladat5()
        {
            int numberToGuess = rnd.Next(1, 10);

            Console.WriteLine("Adjon meg pozitív egész számokat hogy kitalálja a generált számot! (maximum 10)");

            int attempts = 1;
            bool found = false;

            while (!found)
            {
                Console.Write($"{attempts}. próbálkozás: ");
                int value = int.Parse(Console.ReadLine());
                if (value < numberToGuess)
                {
                    Console.WriteLine("A keresett szám nagyobb");
                    attempts++;
                }
                else if (value > numberToGuess)
                {
                    Console.WriteLine("A keresett szám kisebb");
                    attempts++;
                }
                else
                {
                    found = true;
                    Console.WriteLine($"Gratulálok, kitalálta a számot {attempts} próbálkozásból!");
                }
            }
        }

        static void Feladat6()
        {
            Console.Write("Kérem adjon meg egy pozitív egész számot: ");
            int n = int.Parse(Console.ReadLine());

            if (n % 2 == 0)
            {
                Console.WriteLine("A megadott szám páros");
            }
            else
            {
                Console.WriteLine("A megadott szám páratlan");
            }

            int dividers = 0;
            for (int i = 2; i <= n / 2; i++)
            {
                if (n % i == 0)
                {
                    dividers++;
                }
            }

            Console.WriteLine($"Továbbá {dividers} osztólya van");

            if (dividers == 0)
            {
                Console.WriteLine("Tehát a megadott szám prím");
            }
            else
            {
                Console.WriteLine("Tehát a megadott szám nem prím, hanem összetett");

            }
        }
    
        static void Feladat7()
        {
            Console.Write("Kérem adjon meg egy pozitív egész számot: ");
            int n = int.Parse(Console.ReadLine());

            int value = 1;
            while(n > 1)
            {
                value *= n;
                n--;
            }

            Console.WriteLine($"A szám faktoriálisa: {value}");
        }

        static void Feladat8()
        {
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (y % 2 == 0)
                    {
                        Console.Write("---");
                    }
                    else
                    {
                        string num = (((x == 0) ? 1 : x) * ((y < 3) ? 1 : y/2)).ToString();
                        if(x == 0 &&  y == 1) num = " ";
                        if (num.Length == 1) num = " " + num;
                        Console.Write(num + "|");
                    }
                }
                Console.WriteLine();
            }
        }

        static void Feladat9()
        {
            Console.Write("Adjon meg egy időintervalumot másodpercben: ");
            int seconds = int.Parse(Console.ReadLine());
            string originalTime = $"{seconds/60}:{((seconds > 9) ? "" : "0")}{seconds%60}";
            while (seconds > 0)
            {
                Console.Clear();
                Console.WriteLine($"Megadott idő - {originalTime}");
                Console.WriteLine($"Hátralévő idő - {seconds / 60}:{((seconds > 9) ? "" : "0")}{seconds % 60}");
                seconds--;
                Thread.Sleep(1000);
            }
            Console.WriteLine("\nAz idő lejárt!");
        }

        static void Feladat10()
        {
            Console.Write("Kérem adjon meg egy pozitív egész számot: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write($"{n} (10) = ");
            for (int i = 31; i >= 0; i--)
            {
                int bit = (int)(n/ Math.Pow(2, i));
                if(bit > 0) n -= (int)Math.Pow(2, i);
                
                Console.Write($"{bit}");
                if(i%8 == 0) Console.Write(" ");
            }
        }

        static void Feladat11_12()
        {
            string[] signs = { "/_\\", "\\-/", "[-]", "[+]" };
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue };

            int kredit = 100;
            int bet = 1;

            bool game = true;

            int[] results = new int[3];
            int prev = 0;

            while (game)
            {
                if(kredit <= 0)
                {
                    game = false;
                    continue;
                }

                Console.WriteLine($"Kredited mennyisége: {kredit}");
                Console.WriteLine($"Jelenlegi tétel: {bet}");

                ConsoleKeyInfo input = Console.ReadKey(intercept: true);

                if (input.Key == ConsoleKey.Escape) 
                {
                    game = false;
                    continue;
                }
                
                if(input.Key == ConsoleKey.UpArrow)
                {
                    if (input.Modifiers == ConsoleModifiers.Shift) bet += 10;
                    else if (input.Modifiers == ConsoleModifiers.Control) bet += 50;
                    else bet++;

                    if (bet > kredit) bet = kredit;

                    Console.Clear();
                    continue;
                }

                if (input.Key == ConsoleKey.DownArrow)
                {
                    if (input.Modifiers == ConsoleModifiers.Shift) bet -= 10;
                    else if (input.Modifiers == ConsoleModifiers.Control) bet -= 50;
                    else bet--;

                    if (bet < 1) bet = 1;

                    Console.Clear();
                    continue;
                }

                if (input.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("\n-----------------");
                    (int, int) tmpPos = Console.GetCursorPosition();
                    Console.WriteLine("\n-----------------");
                    Console.SetCursorPosition(tmpPos.Item1, tmpPos.Item2);

                    for (int i = 0; i < 3; i++)
                    {
                        (int, int) pos = Console.GetCursorPosition();
                        int value = 0;

                        for (int j = 1; j <= 10; j++)
                        {
                            Console.SetCursorPosition(pos.Item1, pos.Item2);
                            Console.Write("     ");
                            Console.SetCursorPosition(pos.Item1, pos.Item2);

                            do {
                                value = rnd.Next(1, 5);
                            } while (value == prev);

                            Console.ForegroundColor = colors[value - 1];
                            Console.Write($"|{signs[value - 1]}| ");
                            Console.ForegroundColor = ConsoleColor.White;

                            Thread.Sleep(j * 20);
                        }

                        results[i] = value;
                    }

                    Console.SetCursorPosition(0, tmpPos.Item2 + 2);

                    if (results.ToHashSet().Count == 2) {
                        kredit += bet * 10;
                        Console.WriteLine($"\nYou won {bet * 10} kredits!\nPress any key to continue");
                    }
                    else if (results.ToHashSet().Count == 1) {
                        kredit += bet * 50;
                        Console.WriteLine($"\nYou won {bet * 50} kredits!\nPress any key to continue");
                    }
                    else {
                        kredit -= bet;
                        Console.WriteLine($"\nYou lost {bet} kredits\nPress any key to continue");
                    }

                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
        }

        static void Feladat13()
        {
            Console.BufferWidth = 200;
            Console.WindowWidth = 200;
            Console.WriteLine("A kezdő érték 100");
            Console.WriteLine("A szórás(a) opciók: 0, 20, 40");
            Console.WriteLine("A szorzó(r) opciók: -1.1, 0, 1.1");
            Console.Write("Nyomj bármilyen gombot az inditáshoz");
            Console.ReadKey();
            Console.WriteLine();

            float[] mults = { -1.1f, 0, 1.1f };
            int[] spreads = {0, 20, 40 };
            float[] curResults = Enumerable.Repeat(100f, mults.Length*spreads.Length).ToArray();

            for (int mult = 0; mult < mults.Length; mult++)
            {
                for (int spread = 0; spread < spreads.Length; spread++)
                {
                    Console.Write($"r:{mults[mult]};a:{spreads[spread]} \t ");
                }
            }

            Console.WriteLine();
            for (int i = 1; i <= 10; i++)
            {
                for (int mult = 0; mult < mults.Length; mult++)
                {
                    for (int spread = 0; spread < spreads.Length; spread++)
                    {
                        float cur = curResults[mult * spreads.Length + spread];
                        cur = cur * mults[mult] + rnd.Next(-spreads[spread], spreads[spread] + 1);

                        float display = MathF.Round(cur, 1);
                        int tabCount = 2 - (display.ToString().Length/8);
                        string tabs = "";
                        for (int t = 0; t < tabCount; t++) tabs += "\t";

                        Console.Write($"{display}{tabs} ");

                        curResults[mult * spreads.Length + spread] = cur;
                    }
                }

                Console.WriteLine();
                Thread.Sleep(1000);
            }
        }
    }
}
