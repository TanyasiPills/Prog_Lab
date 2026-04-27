namespace SD_Week10;

public class Participant : IComparable
{
    private string name;
    private int age;
    public Participant(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

    static Participant Parse(string line)
    {
        string[] parts = line.Split(' ');
        return new Participant(parts[0], int.Parse(parts[1]));
    }
    
    public int CompareTo(object? obj)
    {
        if (obj is not Participant p) return -1;
        if(Equals(obj)) return 0;
        return name.CompareTo(p.name);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Participant p) return (p.name == this.name);
        return false;
    }

    public override string ToString()
    {
        return $"Name: {this.name}, Age: {this.age}";
    }
}

public class NotSetException(IComparable[] array) : Exception
{
    public IComparable[] erroredOn = array;
}

public class ParticipantList
{
    private IComparable[] array;

    private void Check(IComparable[] array)
    {
        for (int i = 0; i < array.Length-1; i++)
            if(array[i].CompareTo(array[i+1]) > -1)
                throw new NotSetException(array);
    }
    
    public ParticipantList(IComparable[] array)
    {
        Check(array);
        this.array = array;
    }
    
    public IComparable BinarySearch(IComparable element)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            switch (element.CompareTo(array[mid]))
            {
                case 0:
                    return array[mid];
                case > 0:
                    left = mid + 1;
                    break;
                case < 0:
                    right = mid - 1;
                    break;
            }
        }

        return null;
    }

    public bool Contains(IComparable element)
    {
        return BinarySearch(element) != null;
    }

    public bool Subset(IComparable[] sub)
    {
        return Subset(array, sub);
    }

    public static bool Subset(IComparable[] main, IComparable[] sub)
    {
        foreach (var item in sub)
        {
            bool exists = false;

            foreach (var check in main)
            {
                if (check.Equals(item))
                {
                    exists = true;
                    break;
                }
            }
            if (!exists) return false;
        }

        return true;
    }

    public IComparable[] Intersection(IComparable[] other)
    {
        return Intersection(this, new ParticipantList(other));
    }

    public static IComparable[] Intersection(ParticipantList first, ParticipantList second)
    {
        IComparable[] result = new IComparable[Math.Min(first.array.Length, second.array.Length)];
        int count = 0;

        int a = 0;
        int b = 0;
        while (a < first.array.Length && b < second.array.Length)
        {
            switch (first.array[a].CompareTo(second.array[b]))
            {
                case < 0:
                    a++;
                    break;
                case > 0:
                    b++;
                    break;
                case 0:
                    result[count++] = first.array[a++];
                    b++;
                    break;
            }
        }
        return result.Take(count).ToArray();
    }

    public IComparable[] Difference(IComparable[] other)
    {
        return Difference(this, new ParticipantList(other));
    }

    public static IComparable[] Difference(ParticipantList first, ParticipantList second)
    {
        IComparable[] result = new IComparable[first.array.Length];
        int count = 0;

        int a = 0;
        int b = 0;
        while (a < first.array.Length && b < second.array.Length)
        {
            switch (first.array[a].CompareTo(second.array[b]))
            {
                case < 0:
                    result[count++] = first.array[a++];
                    break;
                case > 0:
                    b++;
                    break;
                case 0:
                    a++;
                    b++;
                    break;
            }
        }

        while (a < first.array.Length) result[count++] = first.array[a++];
        
        return result.Take(count).ToArray();
    }

    public IComparable[] Union(IComparable[] other)
    {
        return Union(this,new ParticipantList(other));
    }
    
    public static IComparable[] Union(ParticipantList first, ParticipantList second)
    {
        IComparable[] result = new IComparable[first.array.Length+second.array.Length];
        int count = 0;

        int a = 0;
        int b = 0;
        while (a < first.array.Length && b < second.array.Length)
        {
            switch (first.array[a].CompareTo(second.array[b]))
            {
                case < 0:
                    result[count++] = first.array[a++];
                    break;
                case > 0:
                    result[count++] = second.array[b++];
                    break;
                case 0:
                    result[count++] = second.array[a++];
                    b++;
                    break;
            }
        }
        
        while (a < first.array.Length)
            result[count++] = first.array[a++];

        while (b < second.array.Length)
            result[count++] = second.array[b++];
        
        return result.Take(count).ToArray();
    }

    public override string ToString()
    {
        string output = "";
        for (int i = 0; i < array.Length; i++) output += array[i].ToString()+"\n";
        return output;
    }

    static ParticipantList Parse(string[] lines)
    {
        IComparable[] array = new IComparable[lines.Length];
        int count = 0;
        
        foreach (var line in lines)
        {
            string[] parts = line.Split(' ');
            array[count++] =  new Participant(parts[0], int.Parse(parts[1]));
        }

        return new  ParticipantList(array);
    }
}

public class NotDisjointRequiredOptinalListException : Exception;

public class NotInvitedParticipantsException : Exception;

public class Meeting
{
    ParticipantList present;
    ParticipantList invited;
    ParticipantList optional;

    public Meeting(ParticipantList invited, ParticipantList optional)
    {
        if(ParticipantList.Intersection(invited, optional).Length > 0)
            throw new NotDisjointRequiredOptinalListException();
        
        this.invited = invited;
        this.optional = optional;
    }

    public Meeting(ParticipantList present, ParticipantList invited, ParticipantList optional)
    {
        if(ParticipantList.Intersection(invited, optional).Length > 0)
            throw new NotDisjointRequiredOptinalListException();
        
        if(ParticipantList.Difference(new ParticipantList(ParticipantList.Union(invited, optional)), present).Length > 0)
            throw new NotInvitedParticipantsException();
        
        this.present = present;
        this.invited = invited;
        this.optional = optional;
    }

    public void AddParticipants(ParticipantList participants)
    {
        if(ParticipantList.Difference(new ParticipantList(ParticipantList.Union(invited, optional)), present).Length > 0)
            throw new NotInvitedParticipantsException();
        
        this.present = participants;
    }

    public ParticipantList ParticipatedRequired()
    {
        return new ParticipantList(ParticipantList.Intersection(present, invited));
    }
    public ParticipantList NotParticipatedRequired()
    {
        return new ParticipantList(ParticipantList.Difference(invited, present));
    }
    public ParticipantList ParticipatedOptional()
    {
        return new ParticipantList(ParticipantList.Intersection(present, optional));
    }
    public ParticipantList NotParticipatedOptional()
    {
        return new ParticipantList(ParticipantList.Difference(optional, present));
    }
    public ParticipantList NotParticipated()
    {
        return new ParticipantList(ParticipantList.Difference(new ParticipantList(ParticipantList.Union(invited, optional)), present));
    }

    static Meeting Parse(string[] required, string[] optional, string[] participants)
    {
        IComparable[] req = new IComparable[required.Length];
        IComparable[] opt = new IComparable[optional.Length];
        IComparable[] par = new IComparable[participants.Length];

        int count = 0;
        foreach (var item in required) {
            string[] parts = item.Split(' ');
            req[count++] = new Participant(parts[0], int.Parse(parts[1]));
        }

        count = 0;
        foreach (var item in optional) {
            string[] parts = item.Split(' ');
            opt[count++] = new Participant(parts[0], int.Parse(parts[1]));
        }
        
        count = 0;
        foreach (var item in participants) {
            string[] parts = item.Split(' ');
            par[count++] = new Participant(parts[0], int.Parse(parts[1]));
        }
        
        return new Meeting(new ParticipantList(req), new ParticipantList(opt), new ParticipantList(par));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}