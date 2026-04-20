namespace SD_ZH_Example1;

public interface IDeliverable
{
    int Weight { get; set; }
    string Addressed { get; set; }

    double CaculatePrice(bool fromLocker);
}

public class OverweightException(string message) : Exception(message);

public class Envelope : IDeliverable
{
    public int Weight { get; set; }
    public string Addressed { get; set; }

    private string _description;

    public Envelope(int weight, string addressed, string description)
    {
        this.Weight = weight;
        this.Addressed = addressed;
        this._description = description;
    }
    
    public double CaculatePrice(bool fromLocker)
    {
        return Weight switch
        {
            <= 50 => 200,
            <= 500 => 400,
            <= 2000 => 1000,
            _ => throw new OverweightException("Too fucking fat dipshit")
        };
    }

    override public string ToString()
    {
        return $"Címzett: {Addressed} / Leírás: {_description} / Tömeg: {Weight}g";
    }
}

public enum Positioning
{
    Arbitrary,
    Vertical,
    Horizontal
}

public abstract class Parcel : IDeliverable, IComparable
{
    public int Weight { get; set; }
    public string Addressed { get; set; }

    private Positioning _position;
    public Positioning Position { get => _position; set => _position = value; }

    protected Parcel(int weight, string addressed, Positioning position) : this(weight, addressed)
    {
        this._position = position;
    }

    protected Parcel(int weight, string addressed)
    {
        this.Weight = weight;
        this.Addressed = addressed;
        this._position = Positioning.Arbitrary;
    }

    abstract public double CaculatePrice(bool fromLocker);

    string GetPositioning(Positioning pos)
    {
        return pos switch
        {
            Positioning.Arbitrary => "Nem Meghatározott",
            Positioning.Horizontal => "Horizontális",
            Positioning.Vertical => "Vertikális",
            _ => "He?"
        };
    }
    
    override public string ToString()
    {
        return $"Címzett: {Addressed} / Elhelyezés: {GetPositioning(_position)} / Tömeg: {Weight}g";
    }

    public int CompareTo(object? obj)
    {
        if (obj is Parcel other)
            return Weight.CompareTo(other.Weight);
        return 1;
    }
}

public class NormalParcel : Parcel
{
    private static Random _random = new Random();
    public NormalParcel(int weight, string addressed) : base(weight, addressed, (Positioning)_random.Next(3))
    {
    }

    public override double CaculatePrice(bool fromLocker)
    {
        return 500 + Weight - (fromLocker ? 250 : 0);
    }
}

public class DeliveryException(string message, IDeliverable parcel) : Exception(message)
{
    public IDeliverable Parcel = parcel;
}

public class IncorrectOrientationException(string message) : Exception(message);

public class FragileParcel : Parcel
{
    public FragileParcel(int weight, string addressed, Positioning position) : base(weight, addressed, position)
    {
        if (position == Positioning.Arbitrary) throw new IncorrectOrientationException("Parcel orientation must be set to a specific one");
    }

    public override double CaculatePrice(bool fromLocker)
    {
        if (fromLocker) throw new DeliveryException("Cant deliver a fragile parcel from the lockers", this);

        return 1000 + Weight * 2;
    }
}

public class Courier
{
    private IDeliverable[] parcels;
    private int actualWeight = 0;

    public int ActualWeight
    {
        get => actualWeight;
        set => actualWeight = value;
    }

    public Courier(int parcelCount)
    {
        parcels = new IDeliverable[parcelCount];
    }

    public void PickupItem(IDeliverable item)
    {
        for (int i = 0; i < parcels.Length; i++)
        {
            if (parcels[i] == null)
            {
                parcels[i] = item;
                actualWeight += item.Weight;
                return;
            }
        }

        throw new DeliveryException("Can't fit the package >:/", item);
    }

    public IDeliverable[] FragilesSorted()
    {
        IDeliverable[] fragiles = new IDeliverable[parcels.Length];
        int count = 0;
        foreach (var item in parcels)
        {
            if (item is FragileParcel parcel)
            {
                fragiles[count++] =  parcel;
            }
        }
        
        return fragiles.Take(count).ToArray().OrderBy(x => (FragileParcel)x).ToArray();
    }
}