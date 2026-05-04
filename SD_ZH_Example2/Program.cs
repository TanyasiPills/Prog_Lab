namespace SD_ZH_Example2;

public class TimeException(string message) : Exception(message);

public class Time : IComparable
{
    private int hour, minute, second;

    public int Hour
    {
        get => hour;
        set {
            if(value > 3 || value < 0) throw new TimeException("Input was outside the acceptable range");
            hour = value;
        }
    }

    public int Minute
    {
        get => minute;
        set {
            if(value > 59 || value < 0) throw new TimeException("Input was outside the acceptable range");
            minute = value;
        }
    }

    public int Second
    {
        get => second;
        set {
            if(value > 59 || value < 0) throw new TimeException("Input was outside the acceptable range");
            second = value;
        }
    }

    public Time(int hour, int minute, int second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }
    
    public Time(int minute, int second) : this(0, minute, second) {}

    public static Time Parse(string input)
    {
        try
        {
            int hour, minute, second, i = 0;
            string[] parts = input.Split(':');
        
            if(parts.Length != 2 &&  parts.Length != 3) throw new TimeException("Invalid format for parsing");
            
            if (parts.Length == 3) hour = int.Parse(parts[i++]);
            else hour = 0;
        
            minute = int.Parse(parts[i++]);
            second = int.Parse(parts[i]);
        
            return  new Time(hour, minute, second);
        }
        catch (Exception e)
        {
            throw new TimeException("Invalid format for parsing");
        }
    }

    public override string ToString()
    {
        string output = "";
        if (hour > 0) output += $"0{Hour}:";
        output += $"{(minute < 10 ? "0" : "")}{Minute}:{(second < 10 ? "0" : "")}{Second}";
        return output;
    }

    public int CompareTo(object? obj)
    {
        if (obj is Time time)
        {
            if(Hour != time.Hour) return Hour - time.Hour;
            if(Minute != time.Minute) return Minute - time.Minute;
            if(Second != time.Second) return Second - time.Second;
            return 0;
        }

        return -1;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Time time)
            return hour == time.hour && minute == time.minute && second == time.second;

        return false;
    }
}

public class RunnerWithTime : IComparable
{
    public string Name { get; private set; }
    public Time Time { get; private set; }

    public RunnerWithTime(string name, Time time)
    {
        Name = name;
        Time = time;
    }

    public static RunnerWithTime Parse(string input)
    {
        string[] parts = input.Split(',');
        return new RunnerWithTime(parts[0], Time.Parse(parts[1]));
    }
    
    public int CompareTo(object? obj)
    {
        if (obj is RunnerWithTime runner)
        {
            int result = Time.CompareTo(runner.Time);
            return result != 0 ? result : runner.Name.CompareTo(Name);
        }

        return -1;
    }

    override public string ToString()
    {
        return $"{Name} ({Time})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is RunnerWithTime runner)
            return Name.Equals(runner.Name) && Time.Equals(runner.Time);
        
        return false;
    }
}

public class RaceResults
{
    private RunnerWithTime[] results;

    public RaceResults(int runnerCount, string[] inputs)
    {
        results = new RunnerWithTime[inputs.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            results[i] = RunnerWithTime.Parse(inputs[i]);
        }
        
        if(!IsSorted()) Sort();
    }

    private bool IsSorted()
    {
        for (int i = 0; i < results.Length-1; i++)
            if (results[i].CompareTo(results[i + 1]) == 1)
                return false;

        return true;
    }

    private void Sort()
    {
        for (int i = 1; i <results.Length; i++)
        {
            RunnerWithTime key = results[i];
            int j = i - 1;
            while (j >= 0 && results[j].CompareTo(key) > 0)
            {
                results[j + 1] = results[j];
                j--;
            }

            results[j + 1] = key;
        }
    }
    
    int LowerBound(Time time)
    {
        int left = 0;
        int right = results.Length - 1;

        while (left < right)
        {
            int mid = (left + right) / 2;
            if (time.CompareTo(results[mid].Time) > 0) left = mid + 1;
            else right = mid;
        }

        return left;
    }
    
    int UpperBound(Time time)
    {
        int left = 0;
        int right = results.Length - 1;

        while (left < right)
        {   
            int mid = (left + right) / 2;
            if (time.CompareTo(results[mid].Time) >= 0) left = mid + 1;
            else right = mid;
        }

        return left;
    }

    public RunnerWithTime[] Between(Time lower, Time upper)
    {
        int upperIdx = UpperBound(upper);
        int range = results.Length - upperIdx;
        RunnerWithTime[] result = new RunnerWithTime[range];
        for (int i = upperIdx; i < results.Length; i++)
        {
            int idx = i - upperIdx;
            result[idx] = results[idx];
        }
        
        return result;
    }

    public bool Contains(Predicate<RunnerWithTime> predicate, out RunnerWithTime runnerPerformance)
    {
        foreach (var result in results)
        {
            if (predicate(result))
            {
                runnerPerformance = result;
                return true;
            }
        }
        
        runnerPerformance = null;
        return false;
    }
}

public class Races
{
    public RaceResults[] RaceArray { get; private set; }

    public Races(RaceResults[] race)
    {
        RaceArray = race;
    }

    public Time BestPerformance(string name)
    {
        Time best = Time.Parse("03:59:59");
        
        foreach (var race in RaceArray)
        {
            if (race.Contains(e => e.Name == name, out var any) && any.Time.CompareTo(best) < 0)
                best = any.Time;
        }

        if (best.Equals(Time.Parse("03:59:59"))) return null;
        return best;
    }

    private RunnerWithTime[] Union(RunnerWithTime[] first, RunnerWithTime[] last)
    {
        RunnerWithTime[] result = new RunnerWithTime[first.Length+last.Length];
        int count = 0;

        int a = 0;
        int b = 0;
        while (a < first.Length && b < last.Length)
        {
            switch (first[a].CompareTo(last[b]))
            {
                case < 0:
                    result[count++] = first[a++];
                    break;
                case > 0:
                    result[count++] = last[b++];
                    break;
                case 0:
                    result[count++] = first[a++];
                    result[count++] = last[b++];
                    b++;
                    break;
            }
        }
        
        while (a < first.Length)
            result[count++] = first[a++];

        while (b < last.Length)
            result[count++] = last[b++];

        return result;
    }

    public RunnerWithTime[] AllBetween(Time lower, Time upper)
    {
        RunnerWithTime[] final = new RunnerWithTime[0];

        foreach (var race in RaceArray) final = Union(final, race.Between(lower, upper));

        return final;
    }
}

class Program
{
    static string[] ReadFile(string path)
    {
        string[] lines;
        
        using (StreamReader r = new StreamReader(path))
        {
            int count = int.Parse(r.ReadLine());
            lines = new string[count];
            for (int i = 0; i < count; i++) lines[i] = r.ReadLine();
        }

        return lines;
    }

    static Races ReadFolder(string path)
    {
        RaceResults[] raceResults;
        string[] files = Directory.GetFiles(path, "*.txt");
        raceResults = new RaceResults[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            string[] data = ReadFile(files[i]);
            raceResults[i] = new RaceResults(data.Length, data);
        }

        return new Races(raceResults);
    }
    
    static void Main(string[] args)
    {
        Console.Write("Adja meg a könyvtár elérési útját: ");
        string path = Console.ReadLine();
        
        Races races = ReadFolder(path);
        
        Console.Write("Adja meg a versenyző nevét akinek a legjobb idejét szeretné látni: ");
        string racer = Console.ReadLine();
        Console.WriteLine($"\n{races.BestPerformance(racer)}\n");

        RunnerWithTime[] between = races.AllBetween(Time.Parse("01:45:00"), Time.Parse("02:00:00"));
        
        Console.WriteLine("A félmarathont 01:45:00 és 02:00:00 idő között teljesítették:\n");
        foreach (var runner in between) Console.WriteLine(runner);
    }
}