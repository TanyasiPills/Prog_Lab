namespace TrainingCosts
{
    public class YearlyCosts
    {
        public MonthlyCosts[] Costs { get; set; } = new MonthlyCosts[12];

        public static YearlyCosts LoadFrom(string folderName)
        {
            if (!Directory.Exists(folderName)) throw new DirectoryNotFoundException();

            YearlyCosts result = new YearlyCosts();
            foreach (string filename in Directory.GetFiles(folderName))
            {
                int index = int.Parse(filename.Substring(filename.Length - 6,2));
                result.Costs[index] = MonthlyCosts.LoadFrom(filename);
            }
            return result;
        }

        public int MonthWithMost()
        {
            int index = -1;
            int max = 0;

            for (int i = 0; i < Costs.Length; i++)
            {
                int total = Costs[i].TotalCost();
                if (total > max)
                {
                    index = i;
                    max = total;
                }
            }          
            
            return index;
        }
        
        public int MonthWithMostForSport(TrainingType sport)
        {
            int index = -1;
            int max = 0;

            for (int i = 0; i < Costs.Length; i++)
            {
                int total = Costs[i].TotalFilteredCost((item) => item.Type == sport);
                if (total > max)
                {
                    index = i;
                    max = total;
                }
            }          
            
            return index;
        }

        public TrainingCost[] GetCostAcrossMonths(int month1, int month2)
        {
            TrainingCost[] costs = new TrainingCost[Costs[month1].TrainingCosts.Length];
            int index = 0;
            
            foreach (var item1 in Costs[month1].TrainingCosts)
            {
                foreach (var item2 in Costs[month2].TrainingCosts)
                {
                    if(item1.Type == item2.Type && item1.Description == item2.Description)
                    {
                        costs[index++] = item1;
                    }
                }
            }
            
            return costs.Take(index).ToArray();
        }

        public int[] GetCountPerYear()
        {
            int[] data = new int[4];
            
            foreach (var month in Costs)
            {
                foreach (var item in month.TrainingCosts)
                {
                    data[(int)item.Type]++;
                }
            }
            
            return data;
        }
    }
}
