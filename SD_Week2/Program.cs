using System.Drawing;

namespace SD_Week2
{

    #region Shapes
    abstract class Shape
    {
        bool isHoly; //god shall bless these mortal shapes
        Color color;

        public Shape(Color color, bool isHoly = false)
        {
            this.color = color;
            this.isHoly = isHoly;
        }

        public Color Color { get => color; set => color = value; }
        public void MakeHoly() => isHoly = true;
        public abstract float Perimeter();
        public abstract float Area();

        public override string ToString()
        {
            return $"{color}, {(isHoly ? "luykas" : "nem lyukas")}, {Perimeter()}, {Area()}";
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
    class Rectangle : Shape
    {
        float width;
        float height;

        public Rectangle(float width, float height, Color color) : base(color)
        {
            this.width = width;
            this.height = height;
        }

        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }

        public override float Area()
        {
            return width * height;
        }

        public override float Perimeter()
        {
            return 2 * width + 2 * height;
        }

        public override string ToString()
        {
            return base.ToString() + "Téglalapunk van, yeee";
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }

    class Square : Rectangle
    {
        public Square(float width, Color color) : base(width, width, color) { }

        public override string ToString()
        {
            return "Négyzet " + base.ToString();
        }
    }

    class Circle : Shape
    {
        float radius;

        public Circle(float radius, Color color) : base(color)
        {
            this.radius = radius;
        }

        public float Radius { get => radius; set => radius = value; }

        public override float Area()
        {
            return (float)(radius * radius * Math.PI);
        }

        public override float Perimeter()
        {
            return (float)(2 * radius * Math.PI);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
    #endregion


    #region Bank
    sealed class Owner
    {
        public string username;
    }

    abstract class BankService
    {
        Owner owner;
        protected BankService(Owner owner)
        {
            this.owner = owner;
            Console.WriteLine(owner.username);
        }

        public Owner Owner { get => owner; }
    }

    abstract class BankAccount : BankService
    {
        protected float ballance = 0;
        public BankAccount(Owner owner) : base(owner) {}

        public float Ballance { get => ballance; }

        public void Deposit(float amount) => ballance += amount;

        abstract public bool Withdraw(float amount);

        public BankCard NewCard(ulong cardNumber)
        {
            return new BankCard(Owner, this, cardNumber);
        }
    }

    class CreditAccount : BankAccount
    {
        float loanLimit;

        public CreditAccount(Owner owner, float loanLimit) : base(owner)
        {
            this.loanLimit = loanLimit;
        }

        public float LoanLimit { get => loanLimit; set => loanLimit = value; }

        public override bool Withdraw(float amount)
        {
            if (ballance - amount < -loanLimit) return false;

            ballance -= amount;

            return true;
        }
    }

    class SavingsAccount : BankAccount
    {
        static float baseInterest = 5f;

        public float interest;

        public SavingsAccount(Owner owner) : base(owner)
        {
            interest = baseInterest;
        }

        public override bool Withdraw(float amount)
        {
            if (ballance - amount < 0) return false;

            ballance -= amount;

            return true;
        }

        public void AddInterest()
        {
            ballance += ballance / 100 * interest;
        }
    }

    class BankCard : BankService
    {
        BankAccount bankAccount;
        ulong cardNumber;

        public BankCard(Owner owner, BankAccount bankAccount, ulong cardNumber) : base(owner)
        {
            this.bankAccount = bankAccount;
            this.cardNumber = cardNumber;
        }

        public ulong CardNumber { get => cardNumber;}

        public bool Purchase(float amount) => bankAccount.Withdraw(amount);
    }

    class Bank
    {
        BankAccount[] accounts;
        int accountCount = 0;

        public Bank(int maxAccountCount)
        {
            accounts = new BankAccount[maxAccountCount];
        }

        public BankAccount NewAccount(Owner owner, float loanMaxAmount)
        {
            accounts[accountCount] = (loanMaxAmount > 0) ? new CreditAccount(owner, loanMaxAmount) : new SavingsAccount(owner);
            return accounts[accountCount++];
        }

        public float TotalBalance(Owner owner)
        {
            float data = 0;

            foreach (var account in accounts)
            {
                if (account != null && account.Owner == owner)
                    data += account.Ballance;
            }

            return data;
        }

        public BankAccount MaximumBallanceAccount(Owner owner)
        {
            BankAccount data = null;
            float balerina = 0;

            foreach (var account in accounts)
            {
                if (account != null && account.Owner == owner && account.Ballance > balerina)
                {
                    data = account;
                    balerina = data.Ballance;
                }
            }

            return data;
        }

        public float TotalCreditLimit()
        {
            float data = 0;

            foreach(var account in accounts)
            {
                if (account is CreditAccount) data += ((CreditAccount)account).LoanLimit;
            }

            return data;
        }
    }
    #endregion


    internal class Program
    {

        #region Shapes
        static Shape[] shapes;

        // Behold, mortals! In the name of celestial rigor and with the unblinking gaze of a million righteous angels, 
        // I, herald of justice, scribe of divine wrath, and dispenser of holy judgment, do decree that this function 
        // shall ascend beyond mere mortal code. Here lies the sacred vessel through which the unholy will tremble, the impure 
        // shall quake, and the very bits and bytes of this program shall bask in the radiance of sanctity so profound that even 
        // the demons of corrupted memory shall flee in despair. Let all who dare invoke MakeHoly() feel the searing light of 
        // purity, the cleansing fire of divine logic, and the eternal blessedness of perfectly formatted, bug-free execution. 
        static void MakeHoly(Shape shape)
        {
            if (shape.Area() > shape.Perimeter())
                shape.MakeHoly();
        }

        static Shape MakeRectangle(float width, float height)
        {
            Shape data;
            Color color = Color.FromArgb(255, 10, 10, 10);

            if (width == height)
                data = new Square(width, color);
            else
                data = new Rectangle(width, height, color);

            return data;
        }

        static Shape Biggest(Shape[] shapes)
        {
            Shape data = shapes[0];

            for (int i = 1; i < shapes.Length; i++)
            {
                if (shapes[i].Area() > data.Area())
                    data = shapes[i];
            }

            return data;
        }

        static void Feladat1()
        {
            Color color = Color.FromArgb(255, 140, 240, 80);

            shapes = new Shape[] {
                new Circle(5, color),
                new Square(10, color),
                new Circle(7, color),
                new Square(3, color),
                new Rectangle(4,6, color)
            };
        }
        #endregion


        #region Bank
        static void Feladat2()
        {
            Owner owi = new Owner();
            owi.username = "József";

            Bank bank = new Bank(100);

            bank.NewAccount(owi, 1000);
            Console.WriteLine($"Total balance of {owi.username}: {bank.TotalBalance(owi)}");
            Console.WriteLine($"Account of ${owi.username} with the most bread: {bank.MaximumBallanceAccount(owi)}(can't see but its there, trust the man with the code)");
            Console.WriteLine($"The banks total credit limit: {bank.TotalCreditLimit()}");

        }
        #endregion


        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
        }
    }
}
