namespace SD_Week9;

public enum SortingMethod
{
    Selection,
    Bubble,
    Insertion
}

public class NotOrderedItems(string message, IComparable[] data) : Exception(message)
{
    public IComparable[] Data { get; } = data;
}

public class OrderedItemHandler
{
    public IComparable[] Data { get; }

    public OrderedItemHandler(IComparable[] data)
    {
        this.Data = data;
    }

    public bool IsOrdered(bool isAscending = true)
    {
        for (int i = 0; i < Data.Length - 1; i++)
        {
            int cmp = Data[i].CompareTo(Data[i + 1]);
            if (isAscending ? cmp > 0 : cmp < 0)
                return false;
        }
        return true;
    }

    public void Sort(SortingMethod sortingMethod, bool isAscending = true)
    {
        switch (sortingMethod)
        {
            case SortingMethod.Selection:
                for (int i = 0; i < Data.Length-1; i++)
                {
                    int min = i;
                    for (int j = i+1; j < Data.Length; j++) 
                        if (Data[j].CompareTo(Data[min]) < 0) min = j;

                    if (i != min) (Data[i], Data[min]) = (Data[min], Data[i]);
                }
                break;
            
            case SortingMethod.Bubble:
                int n = Data.Length;
                while (n > 1)
                {
                    int lastSwap = 0;

                    for (int j = 1; j < n; j++)
                    {
                        if (Data[j].CompareTo(Data[j - 1]) < 0)
                        {
                            (Data[j], Data[j - 1]) = (Data[j - 1], Data[j]);
                            lastSwap = j;
                        }
                    }

                    n = lastSwap;
                }
                break;
            
            case SortingMethod.Insertion:
                for (int i = 1; i < Data.Length; i++)
                {
                    IComparable key = Data[i];
                    int j = i - 1;
                    while (j >= 0 && Data[j].CompareTo(key) > 0)
                    {
                        Data[j + 1] = Data[j];
                        j--;
                    }

                    Data[j + 1] = key;
                }
                break;
        }

        if (!isAscending) Reverse();
    }

    void Reverse()
    {
        IComparable last = Data[0];
        
        for (int i = 1; i < Data.Length; i++) Data[i-1] = Data[i];
        
        Data[Data.Length-1] = last;
    }

    public IComparable BinarySearch(IComparable element)
    {
        if (!IsOrdered()) Reverse();
        if (!IsOrdered()) throw new NotOrderedItems("Not ordered array", Data);
        
        int left = 0;
        int right = Data.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            switch (element.CompareTo(Data[mid]))
            {
                case 0:
                    return Data[mid];
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
    
    public IComparable RecursiveBinarySearch(IComparable element, IComparable[] data)
    {
        if (!IsOrdered()) Reverse();
        if (!IsOrdered()) throw new NotOrderedItems("Not ordered array", data);
        
        return CaclRecBinarySearch(element, data);
    }

    IComparable CaclRecBinarySearch(IComparable element, IComparable[] data)
    {
        if (data.Length == 0) return null;
        
        int mid = (data.Length-1) / 2;
        switch (element.CompareTo(Data[mid]))
        {
            case 0:
                return Data[mid];
            case > 0:
                IComparable[] right = new IComparable[mid + 1];
                for (int i = mid + 1; i < data.Length; i++)
                    right[i - (mid + 1)] = data[i];
                return RecursiveBinarySearch(element, right);
            case < 0:
                IComparable[] left = new IComparable[mid];
                for (int i = 0; i < mid; i++)
                    left[i] = data[i];
                return RecursiveBinarySearch(element, left);
        }
    }

    public int MinimumNotSmaller(IComparable target)
    {
        if(!IsOrdered()) throw new NotOrderedItems("Not ordered array", Data);

        int left = 0;
        int right = Data.Length - 1;

        while (left < right)
        {
            int mid = (left + right) / 2;
            if (target.CompareTo(Data[mid]) > 0) left = mid + 1;
            else right = mid;
        }

        return left;
    }
    
    public int MinimumBigger(IComparable target)
    {
        if(!IsOrdered()) throw new NotOrderedItems("Not ordered array", Data);

        int left = 0;
        int right = Data.Length - 1;

        while (left < right)
        {
            int mid = (left + right) / 2;
            if (target.CompareTo(Data[mid]) >= 0) left = mid + 1;
            else right = mid;
        }

        return left;
    }
    
    public int Count(IComparable target)
    {
        if(!IsOrdered()) throw new NotOrderedItems("Not ordered array", Data);

        return MinimumBigger(target) - MinimumNotSmaller(target);   
    }
    
    public int CountInRange(IComparable start, IComparable end)
    {
        if(!IsOrdered()) throw new NotOrderedItems("Not ordered array", Data);

        return MinimumBigger(end) - MinimumNotSmaller(start);   
    }
}

class PhoneBookItem : IComparable
{
    public string Name { get; }
    public string Number { get; }

    public PhoneBookItem(string name, string number)
    {
        this.Name = name;
        this.Number = number;
    }
    
    
    public int CompareTo(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        if (obj is not PhoneBookItem || obj is not string) throw new ArgumentException();

        string data;
        if (obj is PhoneBookItem item) data = item.Name;
        else data = obj as string;

        int i = 0;
        while (i <  data.Length && i < Name.Length)
        {
            if (data[i] < Name[i]) return 1;
            else if (data[i] > Name[i]) return -1;
            else i++;
        }

        return 0;
    }

    public override bool Equals(object? obj)
    {
        var item = obj as PhoneBookItem;
        if (item == null)
        {
            if (item.Name == Name && item.Number == Number) return true;
            else return false;
        }
        else throw new ArgumentException();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}