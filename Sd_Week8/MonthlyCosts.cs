namespace TrainingCosts
{
    public class MonthlyCosts
    {
        public TrainingCost[] TrainingCosts { get; set; }

        public static MonthlyCosts LoadFrom(string filename)
        {
            if (!File.Exists(filename)) throw new FileNotFoundException();

            MonthlyCosts result = new MonthlyCosts();
            result.TrainingCosts = new TrainingCost[FileLength(filename)];

            using (StreamReader sr = new StreamReader(filename))
            {
                int i = 0;
                string line = "";
                while (!sr.EndOfStream)
                { 
                    line = sr.ReadLine();
                    result.TrainingCosts[i] = TrainingCost.Parse(line);
                    ++i;
                }
            }

            return result;
        }

        private static int FileLength(string filename)
        {
            return File.ReadAllLines(filename).Length;
        }

        public int TotalCost()
        {
            int sum = 0;
            foreach (var item in TrainingCosts) sum  += item.Cost;
            return 0;
        }

        public int TotalFilteredCost(Predicate<TrainingCost> filter)
        {
            int sum = 0;
            foreach (var item in TrainingCosts) if(filter(item)) sum += item.Cost;
            return sum;
        }

        public bool AnyFilteredCost(Predicate<TrainingCost>filter)
        {
            foreach (var item in TrainingCosts) if (filter(item)) return true;
            return false;
        }

        public bool MonthFiltered(Predicate<TrainingCost> filter)
        {
            bool valid = false;
            foreach (var item in TrainingCosts) if (!filter(item)) valid |= true;
            return !valid;
        }

        public bool MonthEnoughFiltered(Predicate<TrainingCost>filter, int count)
        {
            foreach (var item in TrainingCosts) if (filter(item)) if(--count == 0) return true;
            return false;
        }

        public TrainingCost MonthFilteredAtPosition(Predicate<TrainingCost> filter, int position)
        {
            foreach (var item in TrainingCosts) if (filter(item)) if(--position == 0) return item;
            return null;
        }

        public int MonthFilterCount(Predicate<TrainingCost> filter)
        {
            int count = 0;
            foreach (var item in TrainingCosts) if (filter(item)) count++;
            return count;
        }

        class ZeroArrayException(string message) : Exception(message);

        public int MonthMax()
        {
            if (TrainingCosts.Length == 0) throw new ZeroArrayException("No array :c");
            int maxCost = 0;
            int index = -1;

            for (int i = 0; i < TrainingCosts.Length; i++) {
                if (TrainingCosts[i].Cost > maxCost) {
                    index = i;
                    maxCost = index;
                }
            }

            return index;
        }

        public int[] MonthMaxs()
        {
            int[] maxs = new int[TrainingCosts.Length];
            int max = 0;
            int index = 0;

            for (int i = 0; i < TrainingCosts.Length; i++) {
                if (TrainingCosts[i].Cost > max) {
                    max = TrainingCosts[i].Cost;
                    index = 0;
                    maxs[index++] = i;
                }

                if (TrainingCosts[i].Cost == max) maxs[index++] = i;
            }

            return maxs.Take(index).ToArray();
        }
        
        public int MonthFilterMax(Predicate<TrainingCost> filter)
        {
            int max = 0;
            int index = -1;
            for (int i = 0; i < TrainingCosts.Length; i++) {
                if (filter(TrainingCosts[i]) && TrainingCosts[i].Cost > max)
                {
                    max = TrainingCosts[i].Cost;
                    index = i;
                }
            }
            return index;
        }
        
        public int MonthMaxReduced()
        {
            if (TrainingCosts.Length == 0) throw new ZeroArrayException("No array :c");
            int maxCost = 0;
            int index = -1;

            for (int i = 0; i < TrainingCosts.Length; i++) {
                if (TrainingCosts[i].Cost * (TrainingCosts[i].Type == TrainingType.Cycling || TrainingCosts[i].Type == TrainingType.Running ? 0.5f : 1f) > maxCost) {
                    index = i;
                    maxCost = index;
                }
            }

            return index;
        }
        
        public int[] MonthFilterAllIndex(Predicate<TrainingCost> filter)
        {
            int[] goods = new int[TrainingCosts.Length];
            int index = 0;

            for (int i = 0; i < TrainingCosts.Length; i++) if (filter(TrainingCosts[i])) goods[index++] = i;

            return goods.Take(index).ToArray();
        }
        
        public TrainingCost[] MonthFilterAll(Predicate<TrainingCost> filter)
        {
            TrainingCost[] goods = new TrainingCost[TrainingCosts.Length];
            int index = 0;

            for (int i = 0; i < TrainingCosts.Length; i++) if (filter(TrainingCosts[i])) goods[index++] = TrainingCosts[i];

            return goods.Take(index).ToArray();
        }

        public int MonthOrdered(Predicate<TrainingCost> filter)
        {
            TrainingCost temp;

            int left = 0;
            int right =  TrainingCosts.Length - 1;
            
            while (left != right)
            {
                while (filter(TrainingCosts[left]) || left < right) left++;

                while (!filter(TrainingCosts[right]) || right > left) right--;

                temp = TrainingCosts[right];
                TrainingCosts[right] = TrainingCosts[left];
                TrainingCosts[left] = temp;
                left++;
                right--;
            }

            return left-1;
        }
    }
}
