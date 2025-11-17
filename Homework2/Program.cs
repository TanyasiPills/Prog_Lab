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

            for (int i = 0; i < solution.Length-1; i++)
            {
                if (solution[i] == '(' && solution[i+1] == ')')
                {
                    solutionCount++;
                    i++;
                }
            }
            Console.WriteLine(solution);
            Console.WriteLine(solutionCount);
        }
    }
}
