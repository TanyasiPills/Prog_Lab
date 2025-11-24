namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string solution = "";
            int solutionCount = 0;

            using (StreamReader rd = new StreamReader("input.txt"))
            {
                int count = int.Parse(rd.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    string input = rd.ReadLine();
                    switch (input)
                    {
                        case "if":
                            solution += "(";
                            break;
                        case "else":
                            solution += ")(";
                            break;
                        case "endif":
                            solution += ")";
                            break;

                        default:
                            break;
                    }
                }
            }


            for (int index = 0; index < solution.Length-1; index++)
            {
                if (solution[index] == '(')
                {
                    index += 1;
                    solutionCount += Contains(ref index, solution);
                }
            }

            if (solutionCount == 0) solutionCount++;

            Console.WriteLine(solutionCount);
        }

        static int Contains(ref int index, string solution)
        {
            int permutations = 0;
            int contained = 0;

            while(true)
            {
                if (solution[index] == ')')
                {
                    break;
                }

                //solution[index] is always '(' going forward
                if (solution[index + 1] == ')')
                {
                    contained++;
                    index += 2;
                }
                else if (solution[index + 1] != ')')
                {
                    index += 1;
                    permutations += Contains(ref index, solution);
                }
            }

            if(contained != 0) permutations += (int)Math.Pow(2, contained / 2);
            if (contained % 2 != 0) permutations++;

            return (permutations != 0) ? permutations : 1;
        }
    }
}

// if they are inside of a () like: (()() ()() ()()) then it means 2^3 so () ( ()() ) means 1 + 2^1 = 3
