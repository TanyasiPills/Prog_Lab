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

            if(input == reverse) Console.WriteLine("\nA megadott szöveg palindrom");
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

            if(valid) Console.WriteLine("A megadott email cím érvényes");
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
    
        
    }
}
