namespace TrainingCosts
{
    public enum TrainingType
    {
        Cycling,
        Hiking,
        Running,
        Swimming
    }

    public class TrainingCost
    {
        public TrainingType Type { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public int Cost { get; set; }

        public static TrainingCost Parse(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (input == string.Empty) throw new ArgumentException(nameof(input));

            string[] items = input.Split(',');
            if (items.Length != 4) throw new ArgumentException(nameof(input));

            TrainingCost result = new TrainingCost();
            result.Type = (TrainingType)Enum.Parse(typeof(TrainingType), items[0]);
            result.Description = items[1];
            result.Date = DateOnly.Parse(items[2]);
            result.Cost = int.Parse(items[3]);
            return result;
        }
    }
}
