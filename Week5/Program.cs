using System.Runtime.ExceptionServices;

namespace Week5
{
    internal class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;


            //Feladat1(projectDirectory);
            //Feladat2(projectDirectory);
            //Feladat3(projectDirectory);
            Feladat4(projectDirectory);

        }
        static void Feladat1(string projDir)
        {
            using (StreamReader rd = new StreamReader(projDir + "\\colorem_ipsum.txt"))
            {
                while (!rd.EndOfStream)
                {
                    string line = rd.ReadLine();
                    string[] sections = line.Split("#");
                    Enum.TryParse(sections[0], true, out ConsoleColor color);
                    Console.ForegroundColor = color;
                    Console.WriteLine(sections[1]);
                }
            }
        }

        static void Feladat2(string projDir)
        {
            int weekCount = 0;

            using (StreamWriter wr = new StreamWriter(projDir + "\\winning_numbers.txt"))
            {
                bool goAgain = false;
                do
                {
                    int[] winners = new int[5];

                    for (int i = 0; i < winners.Length; i++)
                    {
                        int random = 0;

                        do random = rnd.Next(1, 91);
                        while (winners.Contains(random));

                        winners[i] = random;
                    }

                    string output = "";

                    output += $"On {(DateTime.Today.Date.AddDays(weekCount * 7)).ToString().Split(" ").ToList().Take(3).Aggregate((a, b) => a += b).ToString()} the numbers were: ";

                    for (int i = 0; i < winners.Length; i++) output += $"{winners[i]} ";

                    Console.WriteLine(output);
                    wr.WriteLine(output);

                    Console.Write("Another week? [Y/N]  ");
                    string input = Console.ReadLine().ToLower();

                    if (input == "y") goAgain = true;
                    weekCount++;

                } while (goAgain);
            }
        }
    
        static void Feladat3(string projDir)
        {
            using (StreamReader rd = new StreamReader(projDir + "\\ant_instructions.txt"))
            {
                int[] firstLine = rd.ReadLine().Split(" ").Select(e => int.Parse(e)).ToArray();
                int x = firstLine[0], y = firstLine[1], degree = firstLine[2];

                while (!rd.EndOfStream)
                {
                    string[] sections = rd.ReadLine().ToLower().Split(" ");

                    if (sections[0] == "left") degree += int.Parse(sections[1]);
                    else if (sections[0] == "right") degree -= int.Parse(sections[1]);
                    else if (sections[0] == "go")
                    {
                        x += (int)Math.Cos(degree) * int.Parse(sections[1]);
                        y += (int)Math.Sin(degree) * int.Parse(sections[1]);
                    }
                    Console.WriteLine(degree);
                }

                Console.WriteLine($"Position after commands: x={x}, y={y}, heading={degree % 360} degree");
            }
        }

        static void Feladat4(string projDir)
        {
            string[] lines;
            using (StreamReader rd = new StreamReader(projDir + "\\NHANES_1999-2018.txt")) lines = rd.ReadToEnd().Split('\n').Skip(1).ToArray();

            int[] ids = new int[lines.Length];
            string[] date = new string[lines.Length];
            float[] gender = new float[lines.Length];
            float[] age = new float[lines.Length];
            float[] bmi = new float[lines.Length];
            float[] bs = new float[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] c = lines[i].Split(',');
                ids[i] = int.Parse(c[0]);
                date[i] = c[1];
                gender[i] = float.Parse(c[2]);
                age[i] = float.Parse(c[3]);
                bmi[i] = float.Parse(c[4]);
                bs[i] = float.Parse(c[5]);
            }

        }
    }
}
