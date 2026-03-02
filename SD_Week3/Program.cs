using System.Net.Http.Headers;

namespace SD_Week3
{
    #region Feladat1
    class Number : IComparable
    {
        float value;

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if (obj == this) return 0;

            Number other = obj as Number;
            if (other.value > this.value)
                return -1;
            else if (other.value < this.value)
                return 1;
            return 0;
        }
    }
    #endregion


    #region Feladat2
    interface IPaymentInstrument
    {
        bool Pay(float amount);
    }

    interface IProperty
    {
        string Owner { get; set; }
    }

    interface ILostProperty
    {
        DateTime DateOfLost { get; set; }
    }

    class Credit : IPaymentInstrument
    {
        float amountOfCredit;

        public bool Pay(float amount)
        {
            if(amount < amountOfCredit)
            {
                 amountOfCredit -= amount;
                 return true;
            }
            return false;
        }
    }

    class PlasticCard: IProperty
    {
        string owner;

        public PlasticCard(string owner)
        {
            this.owner = owner;
        }

        public string Owner { get => owner; set => owner = value; }
    }
    class CreditCard : PlasticCard, IPaymentInstrument
    {
        float balance;

        public CreditCard(string owner, float balance) : base(owner)
        {
            this.balance = balance;
        }

        public bool Pay(float amount)
        {
            if (amount < balance)
            {
                balance -= amount;
                return true;
            }
            return false;
       }
    }

    class BlockedCreditCard: CreditCard, ILostProperty
    {
        bool lost;

        public BlockedCreditCard(string owner, float balance) : base(owner, balance)
        {
            lost = true;
        }

        public DateTime DateOfLost { get; set; }
    }
    #endregion

    #region Feladat3
    interface IRealEstate
    {
        float TotalValue();
    }

    interface IRent
    {
        float GetCost(int months);

        bool IsBooked { get; set; }

        bool Book(int months);
    }

    abstract class Flat : IRealEstate
    {
        protected float area;
        protected int roomCount;
        protected int inhabitantsCount;
        float unitPrice;

        protected Flat(float area, int roomCount, int inhabitantsCount, float unitPrice)
        {
            this.area = area;
            this.roomCount = roomCount;
            this.inhabitantsCount = inhabitantsCount;
            this.unitPrice = unitPrice;
        }

        public int InhabitantsCount { get => inhabitantsCount; }

        abstract public bool MoveIn(int newInhabitants);

        public float TotalValue()
        {
            return area * unitPrice;
        }

        public override string ToString()
        {
            return $"Terület: {area}, Szobák száma: {roomCount}, Négyzetméter ár: {unitPrice}";
        }
    }

    class Lodgings : Flat, IRent
    {
        int bookedMonth;

        public Lodgings(float area, int roomCount, float unitPrice) : base(area, roomCount, 0, unitPrice)
        {
            this.bookedMonth = 0;
        }

        public bool IsBooked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Book(int months)
        {
            if (!IsBooked)
            {
                bookedMonth = months;
                return true;
            }
            else return false;
        }

        public float GetCost(int months)
        {
            if (InhabitantsCount == 0) return 0;
            return TotalValue() / (240 * InhabitantsCount);
        }

        public override bool MoveIn(int newInhabitants)
        {
            if (IsBooked && (inhabitantsCount + 1) / area >= 2 && roomCount > Math.Ceiling((float)(inhabitantsCount + 1) / 8))
            {
                inhabitantsCount++;
                return true;
            }
            else return false;
        }
    }

    class FamilyApartment : Flat
    {
        int childrenCount;

        public FamilyApartment(float area, int roomCount, float unitPrice) : base(area, roomCount, 0, unitPrice)
        {
            childrenCount = 0;
        }

        public override bool MoveIn(int newInhabitants)
        {
            for (int i = 0; i < newInhabitants; i++)
            {
                if ((inhabitantsCount + 1 - (0.5f * childrenCount)) / area >= 10 && roomCount > Math.Ceiling((float)(inhabitantsCount + 1 - (0.5f * childrenCount)) / 2))
                {
                    inhabitantsCount++;
                }
                else return false;
            }

            return true;
        }

        public bool ChildIsBorn()
        {
            if (inhabitantsCount - childrenCount >= 2)
            {
                inhabitantsCount++;
                childrenCount++;
                return true;
            }
            else return false;
        }

        public override string ToString()
        {
            return base.ToString() + $" Gyerekek száma: {childrenCount}";
        }
    }

    class Garage : IRent, IRealEstate
    {
        float area;
        float unitPrice;
        bool isHeated;
        int months;
        bool isOccupied;

        public bool IsBooked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Garage(float area, float unitPrice, bool isHeated)
        {
            this.area = area;
            this.unitPrice = unitPrice;
            this.isHeated = isHeated;
            months = 0;
            isOccupied = false;
        }

        public bool Book(int months)
        {
            if (IsBooked) return false;
            else
            {
                this.months = months;
                return true;
            }
        }

        public float GetCost(int months)
        {
            return TotalValue() / 120 * (isHeated ? 1.5f : 1);
        }

        public float TotalValue()
        {
            return area * unitPrice;
        }

        public void UpdateOccupied()
        {
            isOccupied = !isOccupied;
        }

        public override string ToString()
        {
            return $"Terület: {area},  Fűtött: {(isHeated ? "Igen" : "Nem")}, Négyzetméter ár: {unitPrice}";
        }
    }

    class ApartmentHouse
    {
        List<IRealEstate> auto_property = new List<IRealEstate>();
        int houseCount;
        int garageCount;
        int maxHouseCount;
        int maxGarageCount;

        public ApartmentHouse(int maxHouseCount, int maxGarageCount)
        {
            this.maxHouseCount = maxHouseCount;
            this.maxGarageCount = maxGarageCount;
        }

        internal List<IRealEstate> Auto_property { get => auto_property; }

        public bool AddRealEstate(IRealEstate estate)
        {
            if(estate as Garage != null && garageCount +1 <= maxGarageCount)
            {
                garageCount++;
                return true;
            } else if(estate as Flat != null && garageCount + 1 <= maxHouseCount)
            {
                houseCount++;
                return true;
            }

            return false;
        }

        public int InhabitantsCount()
        {
            int data = 0;

            foreach (var item in auto_property)
            {
                if(item as Flat != null)
                {
                    data += ((Flat)item).InhabitantsCount;
                }
            }

            return data;
        }

        public float TotalValue()
        {
            float data = 0f;

            foreach (var item in auto_property)
            {
                if(item as Flat != null && ((Flat)item).InhabitantsCount > 0)
                {
                    data += ((Flat)item).TotalValue();
                }
                else if(item as Garage != null && ((Garage)item).IsBooked)
                {
                    data += ((Garage)item).TotalValue();
                }
            }

            return data;
        }

        static ApartmentHouse LoadFromFile(string fileName)
        {
            ApartmentHouse housing;

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line = sr.ReadLine();

                {
                    string[] baseData = line.Split(" ");
                    housing = new ApartmentHouse(int.Parse(baseData[0]), int.Parse(baseData[1]));
                }

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] data = line.Split(" ");

                    switch (data[0])
                    {
                        case "Albérlet":
                            housing.AddRealEstate(new Lodgings(float.Parse(data[1]), int.Parse(data[2]), float.Parse(data[3])));
                            break;
                        case "Garázs":
                            housing.AddRealEstate(new Garage(float.Parse(data[1]), float.Parse(data[2]), bool.Parse(data[3])));
                            break;
                        case "Családi":
                            housing.AddRealEstate(new FamilyApartment(float.Parse(data[1]), int.Parse(data[2]), float.Parse(data[3])));
                            break;
                        default:
                            break;
                    }
                }
            }

            return housing;
        }
    }
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            ApartmentHouse house = new ApartmentHouse(10, 10);
            Garage garage = new Garage(10, 10, true);
            FamilyApartment family = new FamilyApartment(100, 10, 10);
            Lodgings doge = new Lodgings(20, 10, 10);

            Console.WriteLine("Garage cost for 10 months:" + garage.GetCost(10));
            family.MoveIn(3);
            Console.WriteLine(family.ToString());
            Console.WriteLine("Family home total value:" + family.TotalValue());
            doge.Book(100);

            house.AddRealEstate(garage);
            house.AddRealEstate(family);
            house.AddRealEstate(doge);
            Console.WriteLine("Apertment value as a whole:" + house.TotalValue());
        }
    }
}
