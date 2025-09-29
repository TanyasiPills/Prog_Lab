namespace Week4
{
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

        static void Feladat3()
        {
            Console.WriteLine("Kérem adja meg a rendszámot: ");
            string input = Console.ReadLine();

            input = input.ToLower().Trim().Replace(" ", "");
            if (input.Length != 7 && input.Length != 8) throw new Exception("Invalid input text");

            string output = "";

            for (int i = 0; i < input.Length; i++)
            {  
                if (i == 4 && input.Length != 8) output += "-";
                output += input[i].ToString().ToUpper();
                if(i == 1) output += " ";
            }

            Console.WriteLine($"A sztenderd alapján megformázott renszám: {output}");
        }
    }
}
