namespace Week8
{

    public enum Position
    {
        Kapus,
        Vedo,
        Szelso,
        Tamado
    }

    class Player
    {
        string name;
        Position pos;

        string[] positionString = { "Kapus", "Védő", "Szélső", "Támadó"};

        public Player(string name, Position pos)
        {
            this.name = name;
            this.pos = pos;
        }

        public string Name { get => name; }
        public Position Pos { get => pos; }

        public override string ToString()
        {
            return $"{positionString[(int)pos]} - {name}";
        }
    }

    class Team
    {
        Player[] players = new Player[5];
        int numberOfPlayers;

        public bool IsIncluded(Player player)
        {
            return players.Contains(player);
        }

        /*
        public bool IsAvailable(Player player)
        {
             players.Select(e => e.Pos).ToHashSet();
        }
        */

        public bool IsFull { get => numberOfPlayers >= 5; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
