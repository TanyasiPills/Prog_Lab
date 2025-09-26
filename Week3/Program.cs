using System.Collections.Concurrent;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Week3
{
    internal class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            //Feladat1_2();
            //Feladat3_4();
            //Feladat5();
            //Feladat6();
            //Feladat7();
            //Feladat8();
            //Feladat9();
            //Feladat10();
            //Feladat11();
            Feladat12();
        }

        static void Feladat1_2()
        {
            string[] colors = { "Kör", "Káró", "Treff", "Pikk" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jumbó", "Dáma", "Király", "Ász" };
            string[] deck = new string[colors.Length * values.Length];

            for (int c = 0; c < colors.Length; c++)
            {
                for (int v = 0; v < values.Length; v++)
                {
                    deck[c * values.Length + v] = $"{colors[c]} {values[v]}";
                }
            }

            for (int i = 0; i < deck.Length - 1; i++)
            {
                int j = rnd.Next(i + 1, deck.Length);
                var temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }

            Console.Write("Az 52 elemű Fisher-Yates kevert tömb elemei:\n{\n");
            foreach (var card in deck)
            {
                Console.Write($"{card}, ");
            }
            Console.WriteLine("\n}");
        }

        static void Feladat3_4()
        {
            List<string> words = new List<string>();
            bool haveMore = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Kérem adjon meg egy szót:");
                string word = Console.ReadLine().ToLower();

                if (word == "stop") haveMore = false;
                else words.Add(word);
            } while (haveMore);

            Console.Clear();
            Console.WriteLine("Kérem adja meg a keresett szót");
            string searchedWord = Console.ReadLine().ToLower();

            int index = words.FindIndex(w => w == searchedWord);
            if (index != -1) Console.WriteLine($"A keresett szó a tömb {index + 1}. helyén található.");
            else Console.WriteLine("A keresett szó nem található a tömbben.");

        }

        static void Feladat5()
        {
            List<string> names = new List<string>();
            List<int> ages = new List<int>();
            List<bool> canProg = new List<bool>();

            bool run = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Kérem adja meg a nevét:");
                string name = Console.ReadLine();

                if (name == "")
                {
                    run = false;
                    break;
                }

                names.Add(name);
                Console.WriteLine("Kérem adja meg a korát:");
                int age = int.Parse(Console.ReadLine());
                ages.Add(age);
                Console.WriteLine("Tud programozni? (i/n)");
                char prog = char.Parse(Console.ReadLine().ToLower());
                if (prog == 'i') canProg.Add(true);
                else canProg.Add(false);
            } while (run);

            float avgAge = 0;
            foreach (int item in ages) avgAge += item;
            avgAge /= ages.Count;
            Console.WriteLine($"Átlag életkor: {MathF.Round(avgAge, 2)}");

            double avgNoProgAge = ages.Where((age, index) => !canProg[index]).Average();
            Console.WriteLine($"A programozási tapasztalat nélküliek átlag életkora: {avgNoProgAge}");

            (int, int) data = ages.Select((age, index) => (age, index)).Where(e => canProg[e.index]).MaxBy(e => e.age);
            Console.WriteLine($"A legidősebb programozó: {names[data.Item2]}, kora: {data.Item1}");

        }

        static void Feladat6()
        {
            int[,] numbers = new int[rnd.Next(2, 10), rnd.Next(2, 10)];

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = rnd.Next(0, 10);
                }
            }

            WriteMatrix(numbers);

            int[,] transposed = new int[numbers.GetLength(1), numbers.GetLength(0)];
            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    transposed[j, i] = numbers[i, j];
                }
            }

            WriteMatrix(transposed);
        }

        static void WriteMatrix(int[,] matrix)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    Console.Write($"{matrix[x, y]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Feladat7()
        {
            int[,] fishData = new int[rnd.Next(4, 10), rnd.Next(2, 4)];

            for (int i = 0; i < fishData.GetLength(0); i++)
            {
                for (int j = 0; j < fishData.GetLength(1); j++)
                {
                    fishData[i, j] = rnd.Next(0, 3);
                }
            }

            for (int y = -2; y < fishData.GetLength(1); y++)
            {
                for (int x = -2; x < fishData.GetLength(0); x++)
                {
                    if (y == -2)
                    {
                        if (x >= 0) Console.Write($"{x + 1}|");
                        else if (x == -1) Console.Write("  |");
                        else Console.Write("  ");

                        if (x == fishData.GetLength(0) - 1) Console.Write("\t <-Horgászok");
                    }
                    else if (x == -2 && y >= 0)
                    {
                        Console.Write($"|{y + 1}|");
                    }
                    else if (y == -1 || x == -1)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        if (x == 0) Console.Write("|");
                        Console.Write($"{fishData[x, y]}|");

                        if (x == fishData.GetLength(0) - 1 && y == fishData.GetLength(1) - 1) Console.WriteLine("\n^\n|\nHalfajták");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n\nKifogott halak fajtánként:");
            for (int i = 0; i < fishData.GetLength(1); i++)
            {
                int sum = 0;
                for (int j = 0; j < fishData.GetLength(0); j++)
                {
                    sum += fishData[j, i];
                }

                Console.WriteLine($"{i + 1}: {sum}");
            }
            Console.WriteLine();

            bool noFish = false;

            int id = -1, max = 0;
            for (int x = 0; x < fishData.GetLength(0); x++)
            {
                int sum = 0;
                for (int y = 0; y < fishData.GetLength(1); y++)
                {
                    sum += fishData[x, y];
                }
                if (sum > max)
                {
                    id = x;
                    max = sum;
                }

                if (sum == 0) noFish = true;
            }
            Console.WriteLine($"A {id}.-dik horgász fogta a legtöbb halat, összesen {max}");

            if (noFish) Console.WriteLine("Volt olyan horgász aki egy halat se fogott");
            else Console.WriteLine("Nem volt olyan horgász aki egy halat se fogott volna");


        }

        static void Feladat8()
        {
            List<int> numbers = new List<int>();

            Console.WriteLine("Adja meg a lista első értékét:");
            int first = int.Parse(Console.ReadLine());
            numbers.Add(first);

            int calc = -1;
            while (calc != 1)
            {
                calc = numbers.Last();
                if (calc % 2 == 0) numbers.Add(calc / 2);
                else numbers.Add(calc * 3 + 1);
            }

            foreach (int item in numbers) Console.Write($"{item}, ");
        }

        static void Feladat9()
        {
            int[] x = { 1, 2, 3, 4, 5, 6, 7, 8 };

            for (int i = 0; i < x.Length / 2; i++)
            {
                int tmp = x[i];
                x[i] = x[x.Length - i - 1];
                x[x.Length - i - 1] = tmp;
            }

            foreach (var item in x) Console.Write($"{item}, ");
        }

        static void Feladat10()
        {
            int[] ar1 = new int[rnd.Next(4, 8)];
            for (int i = 0; i < ar1.Length; i++) ar1[i] = rnd.Next(10);

            List<int> ar2 = new List<int>();
            for (int i = 0; i < rnd.Next(4, 8); i++) ar2.Add(rnd.Next(10));

            foreach (var item in ar1) Console.Write($"{item}, ");
            Console.WriteLine();
            foreach (var item in ar2) Console.Write($"{item}, ");
            Console.WriteLine("\n");

            (List<int>, List<int>) seconds;
            seconds.Item1 = ar1.ToList().Where((e, index) => index % 2 == 0).ToList();
            seconds.Item2 = ar2.Where((e, index) => index % 2 == 0).ToList();

            foreach (var item in seconds.Item1) Console.Write($"{item}, ");
            Console.WriteLine();
            foreach (var item in seconds.Item2) Console.Write($"{item}, ");
            Console.WriteLine("\n");


            (List<int>, List<int>) reverse;
            reverse.Item1 = ar1.Reverse().ToList();
            reverse.Item2 = ar2.ToArray().Reverse().ToList();

            foreach (var item in reverse.Item1) Console.Write($"{item}, ");
            Console.WriteLine();
            foreach (var item in reverse.Item2) Console.Write($"{item}, ");
            Console.WriteLine("\n");

            (int[,], int[,]) matrixes;

            int size1 = (int)Math.Ceiling(Math.Sqrt(ar1.Length));
            matrixes.Item1 = new int[size1, size1];
            for (int i = 0; i < size1; i++) for (int j = 0; j < size1; j++) {
                    if (i * size1 + j < ar1.Length) matrixes.Item1[j, i] = ar1[i * size1 + j];
                    else matrixes.Item1[j, i] = 0;
                }

            int size2 = (int)Math.Ceiling(Math.Sqrt(ar2.Count));
            matrixes.Item2 = new int[size2, size2];
            for (int i = 0; i < size2; i++) for (int j = 0; j < size2; j++) {
                    if (i * size2 + j < ar2.Count) matrixes.Item2[j, i] = ar2[i * size2 + j];
                    else matrixes.Item2[j, i] = 0;
                }

            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size1; j++) Console.Write($"{matrixes.Item1[j, i]} ");
                Console.WriteLine();
            }
            Console.WriteLine();
            for (int i = 0; i < size2; i++)
            {
                for (int j = 0; j < size2; j++) Console.Write($"{matrixes.Item2[j, i]} ");
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

        //cant really allow all the matrix sizes,
        //works with symethric ones and with those which's one side is at least two times the other
        static void Feladat11()
        {
            int[,] numbers = new int[9, 9];

            int xSize = numbers.GetLength(0);
            int ySize = numbers.GetLength(1);

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    numbers[i, j] = rnd.Next(0, 10);
                }
            }
            int xSizeCalc = numbers.GetLength(0) - 1;
            int ySizeCalc = numbers.GetLength(1) - 1;
            float xCenter = (float)xSizeCalc / 2;
            float yCenter = (float)ySizeCalc / 2;
            List<int>[] layers = new List<int>[(int)Math.Ceiling(Math.Min(xCenter, yCenter)) + ((numbers.GetLength(0) % 2 == 1 || numbers.GetLength(1) % 2 == 1) ? 1 : 0)];

            ConsoleColor[] colors = { ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Yellow };

            for (int i = 0; i < layers.Length; i++) layers[i] = new List<int>();

            int xRange = Math.Max(xSize / 2 - (int)MathF.Floor(((float)ySize) / 2), 0);
            int yRange = Math.Max(ySize / 2 - (int)MathF.Floor(((float)xSize) / 2), 0);
            if (yRange > xRange) yRange--;
            if (xRange > yRange) xRange--;

            /*if(xRange % 2 == yRange % 2)
            {
                if (yRange > xRange) yRange++;
                if (xRange > yRange) xRange++;
            }*/

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    float xDis = Math.Abs(x - xCenter);
                    float yDis = Math.Abs(y - yCenter);

                    float maxDis = (float)Math.Max(xDis - xRange, yDis - yRange);
                    int layer = (int)Math.Floor(Math.Max(maxDis, 0));

                    
                    layers[layer].Add(numbers[x,y]);
                    /*
                    Console.ForegroundColor = colors[layer % colors.Length];
                    Console.Write($"{numbers[x,y]} ");
                    Console.ForegroundColor = ConsoleColor.White;
                    */
                }
                //Console.WriteLine();
            }

            //Console.WriteLine();

            int[] indexes = new int[layers.Length];
            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = 0;
            }

            while (true)
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    int tmp = layers[i].Last();
                    for (int x = layers[i].Count-1; x > 0; x--)
                    {
                        layers[i][x] = layers[i][x - 1];
                    }
                    layers[i][0] = tmp;
                }

                Console.Clear();
                for (int y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        float xDis = Math.Abs(x - xCenter);
                        float yDis = Math.Abs(y - yCenter);

                        float maxDis = (float)Math.Max(xDis - xRange, yDis - yRange);
                        int layer = (int)Math.Floor(Math.Max(maxDis, 0));


                        Console.ForegroundColor = colors[layer % colors.Length];
                        Console.Write($"{layers[layer][indexes[layer]]} ");
                        Console.ForegroundColor = ConsoleColor.White;

                        indexes[layer] = (indexes[layer] + 1) % layers[layer].Count;
                    }
                    Console.WriteLine();
                }

                Thread.Sleep(400);
            }
        }
    
        static void Feladat12()
        {
            //bool[,] grid = new bool[rnd.Next(1, 10), rnd.Next(1, 10)];
            bool[,] grid = new bool[3,3];
            int xSize = grid.GetLength(0);
            int ySize = grid.GetLength(1);

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    grid[x, y] = rnd.Next(0, 2) == 1;
                    Console.Write($"{(grid[x, y] ? "T" : "F")} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            int rndX = rnd.Next(0, xSize - 1);
            int rndY = rnd.Next(0, ySize - 1);

            if(Progress(rndX, rndY, grid)) Console.WriteLine($"x={rndX}; y={rndY} esetén van lehetséges út");
            else Console.WriteLine($"x={rndX}; y={rndY} esetén nincs lehetséges út");
        }

        static bool Progress(int x, int y, bool[,] grid)
        {
            if (!grid[x, y]) return false;

            bool result = false;
            grid[x, y] = false;

            if (x > 1 && grid[x-1, y])  result |= Progress(x - 1, y, grid);
            if(x < grid.GetLength(0)-1 && grid[x+1, y]) result |= Progress(x + 1, y, grid);

            if (y > 1 && grid[x, y-1]) result |= Progress(x, y-1, grid);
            if (y < grid.GetLength(1) - 1 && grid[x, y+1]) result |= Progress(x, y+1, grid);

            if (x == grid.GetLength(0) - 1 && y == grid.GetLength(1) - 1 && grid[x,y]) result = true;
            return result;
        }
    }
}

