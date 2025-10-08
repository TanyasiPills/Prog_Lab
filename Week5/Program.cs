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
            using (StreamReader rd = new StreamReader(projDir + "\\NHANES_1999-2018.csv")) lines = rd.ReadToEnd().Split('\n').Skip(1).ToArray();
            lines = lines.Take(lines.Length - 1).ToArray();

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
                gender[i] = (float)double.Parse(c[2].Replace('.',','));
                age[i] = (float)double.Parse(c[3].Replace('.', ','));
                bmi[i] = (float)double.Parse(c[4].Replace('.', ','));
                bs[i] = (float)double.Parse(c[5].Replace('.', ','));
            }

            float maleAvgBmi = 0;
            int maleCount = 0;
            float femaleAvgBmi = 0;
            int femaleCount = 0;

            int highBloodSugarCount = 0;

            float maxBmi = 0;
            float maxBmiBloodSugar = 0;

            float overweightAgeAvg = 0;
            int overweightCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (gender[i] == 1)
                {
                    maleCount++;
                    maleAvgBmi += bmi[i];
                }
                else
                {
                    femaleCount++;
                    femaleAvgBmi += bmi[i];
                }

                if (bs[i] > 5.6f) highBloodSugarCount++;

                if (bmi[i]> maxBmi)
                {
                    maxBmi = bmi[i];
                    maxBmiBloodSugar = bs[i];
                }

                if (bmi[i] >= 30.0f)
                {
                    overweightCount++;
                    overweightAgeAvg += age[i];
                }
            }

            Console.WriteLine($"A felmérésben az átlagos testtömegindexek:\n-férfi: {Math.Round(maleAvgBmi/maleCount,2)}\n-nő: {Math.Round(femaleAvgBmi/femaleCount,2)}");
            Console.WriteLine($"Az alanyok {Math.Round((float)highBloodSugarCount/lines.Length*100,2)}%-nak 5.6-nál magasabb a vércukorszintje");
            Console.WriteLine($"A legnagyobb BMI-vel rendelkező alany vércukorszintje: {Math.Round(maxBmiBloodSugar,2)}");
            Console.WriteLine($"A túlsúlyos alanyok átlag életkora: {Math.Round(overweightAgeAvg/overweightCount,2)}");
        }
    }
}
