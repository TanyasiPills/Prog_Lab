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
            return $"{positionString[(int)pos]} - Mezszám: {name}";
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

        
        public bool IsAvailable(Player player)
        {
            if (numberOfPlayers == 0) return true;

            if (player.Pos == Position.Szelso)
            {
                return players.Where(e => e.Pos == Position.Szelso).Count() < 2;
            }
            else return players.Where(e => e.Pos == player.Pos).Count() == 0;
        }

        public void Include(Player player)
        {
            players[numberOfPlayers] = player;
            numberOfPlayers++;
        }
        

        public bool IsFull { get => numberOfPlayers >= 5; }
        internal Player[] Players { get => players; set => players = value; }
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Team team = new Team();
            Player[] players = RandomPlayers(10);

            int cursorPos = 0;

            while (!team.IsFull)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if (i == cursorPos) Console.Write("> ");
                    else Console.Write("  ");

                    Console.WriteLine(players[i].ToString());
                }

                Console.Write("Team: [ ");
                for (int i = 0; i < team.NumberOfPlayers; i++)
                {
                    Console.Write($"{team.Players[i]} ");
                }
                Console.WriteLine("]");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow) cursorPos++;
                else if(keyInfo.Key == ConsoleKey.UpArrow) cursorPos--;
                Math.Clamp(cursorPos, 0, players.Length - 1);

                Console.Clear();

                if(keyInfo.Key == ConsoleKey.Spacebar)
                {
                    if (!team.IsAvailable(players[cursorPos]))
                    {
                        Console.WriteLine("A játékos poziciója már foglalt");
                    }
                    else team.Include(players[cursorPos]);
                }
            }
        }

        static Player[] RandomPlayers(int num)
        {
            Random rnd = new Random();
            Player[] players = new Player[num];
            for (int i = 0; i < num; i++)
            {
                players[i] = new Player(rnd.Next(10, num*10).ToString(), (Position)rnd.Next(0,4));
            }
            return players;
        }
    }
}
