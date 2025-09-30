namespace Week4
{
    internal class Program
    {
        static Random rnd = new Random();

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
            Feladat11();
        }

        static void Feladat1()
        {
            char[] vowies = { 'a', 'e', 'i', 'u', 'o' };

            Console.WriteLine("Kérem adja meg az elemezendő szöveget: ");
            string input = Console.ReadLine();
            input = input.ToLower().Replace(" ", "");

            int chars = 0, numbers = 0, vowels = 0;

            for (int s = 0; s < input.Length; s++)
            {
                int index = input[s];

                if (48 <= index && index <= 57) numbers++;
                else if (97 <= index && index <= 122)
                {
                    chars++;
                    if (vowies.Contains((char)index)) vowels++;
                }
            }

            Console.WriteLine($"\nSzámok: {numbers}\nBetük: {chars}\nMagánhangzók: {vowels}");
        }

        static void Feladat2()
        {
            Console.WriteLine("Kérem adja meg az elemezendő szöveget: ");
            string input = Console.ReadLine();

            input = input.ToLower().Trim().Replace(" ", "");
            string reverse = new string(input.Reverse().ToArray());

            if (input == reverse) Console.WriteLine("\nA megadott szöveg palindrom");
            else Console.WriteLine("\nA megadott szöveg nem palindrom");
        }

        static string ParseCarNumber(string input)
        {
            input = input.ToLower().Trim().Replace(" ", "");
            if (input.Length != 7 && input.Length != 8) throw new Exception("ParseCarNumber: Invalid input text");

            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (i == 4 && input.Length != 8) output += "-";
                output += input[i].ToString().ToUpper();
                if (i == 1) output += " ";
            }

            return output;
        }

        static void Feladat3()
        {
            Console.WriteLine("Kérem adja meg a rendszámot: ");
            string input = Console.ReadLine();

            string output = ParseCarNumber(input);

            Console.WriteLine($"A sztenderd alapján megformázott renszám: {output}");
        }

        static void Feladat4()
        {
            Console.WriteLine("Add meg a generálandó rendszámok mennyiségét: ");
            int repeat = int.Parse(Console.ReadLine());
            Console.WriteLine();

            for (int r = 0; r < repeat; r++)
            {
                string input = "";
                for (int i = 0; i < 4; i++) input += (char)rnd.Next(65, 91);
                for (int i = 0; i < 3; i++) input += (char)rnd.Next(48, 58);

                Console.WriteLine(ParseCarNumber(input));
            }
        }

        static void Feladat5()
        {
            Console.WriteLine("Add meg az elenőrzéshez az email címet: ");
            string email = Console.ReadLine();

            bool valid = false;

            if (email.Contains('@'))
            {
                string[] parts = email.Split('@');
                if (parts.Length == 2 && parts[0].Length > 0 && parts[1].Contains('.'))
                {
                    string[] end = parts[1].Split('.');
                    if (end[0].Length > 0 && end[1].Length >= 2)
                    {
                        valid = true;
                    }
                }

                if (valid && parts[0].Contains('.'))
                {
                    string[] start = parts[0].Split(".");
                    if (start[0].Length < 1 || start[1].Length < 1) valid = false;
                }
            }

            if (valid) Console.WriteLine("A megadott email cím érvényes");
            else Console.WriteLine("A megadott email cím nem érvényes");
        }

        static string GenNeptun()
        {
            string output = "";

            output += (char)rnd.Next(65, 91);
            for (int i = 0; i < 5; i++)
            {
                int decider = rnd.Next(0, 2);
                if (decider == 1) output += (char)rnd.Next(65, 91);
                else output += (char)rnd.Next(48, 58);
            }

            return output;
        }

        static void Feladat6()
        {
            Console.WriteLine("Add meg a hasonlításhoz használandó neptun kódot: ");
            string neptunCode = Console.ReadLine().ToUpper();

            int count = 0;
            string generated;
            do
            {
                generated = GenNeptun();
                count++;
            } while (generated != neptunCode);

            Console.WriteLine($"A megadott neptunkódot {count} generálás után kaptuk meg");
        }

        static void Feladat7()
        {
            Console.WriteLine("Add meg a spongecase-re átalakítandó szöveget: ");
            string input = Console.ReadLine();

            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                int decider = rnd.Next(0, 2);
                if (decider == 1) output += input[i].ToString().ToUpper();
                else output += input[i].ToString().ToLower();
            }

            Console.WriteLine($"\nA kapott szöveg: {output}");
        }

        static void Feladat8()
        {
            Console.WriteLine("Adja meg a bemeneti karakterláncot (type empty to quit)");

            string input = "";

            while (true)
            {
                string tmp = Console.ReadLine().Trim();
                if (tmp.Length == 0) break;
                input += ((input.Length > 0) ? "\n" : "") + tmp;
            }

            string[][] table = input.Split('\n').Select(e => e.Split(';').ToArray()).ToArray();

            for (int i = 0; i < table.Length; i++)
            {
                for (int j = 0; j < table[i].Length; j++)
                {
                    if (j == 0) Console.Write("|");
                    Console.Write($"{table[i][j]}|");
                }
                Console.WriteLine();
            }
        }

        static void Feladat9()
        {
            char[] validOpen = { '(', '[', '{' };
            char[] validClose = { ')', ']', '}' };

            Console.WriteLine("Adja meg a zárójel sorozatot: ");
            string input = Console.ReadLine();

            List<char> parentheses = new List<char>();
            bool valid = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (validOpen.Contains(input[i])) {
                    parentheses.Add(input[i]);
                    continue;
                }

                if (validClose.Contains(input[i])) {
                    int openIndex = Array.IndexOf(validOpen, parentheses.Last());
                    int closeIndex = Array.IndexOf(validClose, input[i]);
                    if (openIndex == closeIndex) {
                        parentheses.RemoveAt(parentheses.Count - 1);
                        continue;
                    }

                    valid = false;
                    break;
                }
            }

            if (parentheses.Count > 0) valid = false;

            if (valid) Console.WriteLine("\nA sorozat szabályos");
            else Console.WriteLine("\nA sorozat nem szabályos");
        }

        static void Render(char[,] buffer, (int, int) cursorPos)
        {
            string tmp = "";
            for (int y = 0; y < buffer.GetLength(1); y++)
            {
                tmp += "|";
                for (int x = 0; x < buffer.GetLength(0); x++)
                {
                    if (x == cursorPos.Item1 && y == cursorPos.Item2)
                    {
                        Console.Write(tmp);

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(buffer[x, y]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;

                        tmp = "";
                    }
                    else tmp += buffer[x, y];
                }

                tmp += "|\n";
            }
            Console.WriteLine(tmp);
            Console.WriteLine($"Cursor Pos: {cursorPos.Item1};{cursorPos.Item2}");
        }

        static void Movement(ref (int, int) cursorPos, ConsoleKey key, (int, int) bound)
        {
            if (key == ConsoleKey.LeftArrow) cursorPos.Item1 = Math.Clamp(--cursorPos.Item1, 0, bound.Item1);
            else if (key == ConsoleKey.RightArrow) cursorPos.Item1 = Math.Clamp(++cursorPos.Item1, 0, bound.Item1);
            else if (key == ConsoleKey.UpArrow) cursorPos.Item2 = Math.Clamp(--cursorPos.Item2, 0, bound.Item2);
            else if (key == ConsoleKey.DownArrow) cursorPos.Item2 = Math.Clamp(++cursorPos.Item2, 0, bound.Item2);
            //return cursorPos;
        }

        static void Feladat10()
        {
            Console.WindowHeight = (Console.BufferHeight / 200);
            ConsoleKey[] movement = { ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.RightArrow };

            (int, int) bound = (Console.BufferWidth - 10, (Console.BufferHeight / 200) - 10);
            char[,] buffer = new char[bound.Item1, bound.Item2];
            for (int y = 0; y < buffer.GetLength(1); y++) for (int x = 0; x < buffer.GetLength(0); x++) buffer[x, y] = ' ';

            (int, int) cursorPos = (0, 0);

            Render(buffer, cursorPos);

            while (true) {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();

                if (movement.Contains(key.Key))
                {
                    Movement(ref cursorPos, key.Key, bound);
                    //Console.WriteLine($"{cursorPos.Item1};{cursorPos.Item2}");
                }
                else if (key.Key == ConsoleKey.Enter) cursorPos = (0, cursorPos.Item2 + 1);
                else if (key.Key == ConsoleKey.Backspace) buffer[--cursorPos.Item1, cursorPos.Item2] = ' ';
                else buffer[cursorPos.Item1++, cursorPos.Item2] = key.KeyChar;

                Render(buffer, cursorPos);
            }
        }


        //11. feladat konkrétan csak másolás volt mert amint megtudtam hogy kell csinálni
        //nem találtam ki jobbat és csak átmásoltam, a megjegyzéseket is benne hagytam hogy feltűnő legyen :/
        static void Feladat11()
        {
            const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

            Console.WriteLine("Add meg a base64 szerű kódolásba áttévendő szöveget");
            string input = Console.ReadLine();

            var result = new System.Text.StringBuilder();
            int padding = (3 - (input.Length % 3)) % 3;

            // process 3 bytes at a time
            for (int i = 0; i < input.Length; i += 3)
            {
                int chunk = (input[i] << 16) |
                            ((i + 1 < input.Length ? input[i + 1] : 0) << 8) |
                            ((i + 2 < input.Length ? input[i + 2] : 0));

                for (int j = 18; j >= 0; j -= 6)
                {
                    int index = (chunk >> j) & 0x3F; // 6 bits
                    result.Append(Alphabet[index]);
                }
            }
            // handle padding
            if (padding > 0)
                result.Length -= padding; // cut off extra chars
            result.Append(new string('=', padding)); // add '=' like Base64

            Console.WriteLine(result.ToString());
        }
    }
}
