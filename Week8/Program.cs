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
                return players.Where(e => e != null && e.Pos == Position.Szelso).Count() < 2;
            }
            else return players.Where(e => e != null && e.Pos == player.Pos).Count() == 0;
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

    class Field
    {
        int M;

        public Field(int m)
        {
            M = m;
        }

        public int TargetX { get => M - 1; }
        public int TargetY { get => M - 1; }

        public bool AllowedPosition(int x, int y)
        {
            if (x < 0 || x >= M) return false;
            if (y < 0 || y >= M) return false;
            return true;
        }

        public void Show()
        {
            for (int y = 0; y < M+2; y++)
            {
                string line = "";

                for (int x = 0; x < M+2; x++)
                {
                    if (y == 0 || y == M + 1)
                    {
                        if (x == 0) line += "-";
                        else line += "--";
                    }
                    else
                    {
                        if (x == 0) line += "|";
                        else if (x == M + 1) line += " |";
                        else line += "  ";
                    }
                }

                Console.WriteLine(line);
            }
        }
    }

    class Buffalo
    {
        int x, y;
        bool active = true;

        public Buffalo()
        {
            x = 0;
            y = 0;
        }

        public void Move(Field field)
        {
            if (field.TargetX == x && field.TargetY == y) return;

            Random rnd = new Random();

            int nextX = x, nextY = y;

            do
            {
                nextX = rnd.Next(0, 2);
                nextY = rnd.Next(0, 2);

            } while ((nextX == 0 && nextY == 0) || !field.AllowedPosition(x+nextX, y+nextY));

            x += nextX;
            y += nextY;
        }

        public void Deactivate()
        {
            active = false;
        }

        public void Show()
        {
            (int,int) pos = Console.GetCursorPosition();

            Console.SetCursorPosition(x*2+2,y+1);
            if (active) Console.ForegroundColor = ConsoleColor.Green;
            else Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("B");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(pos.Item1, pos.Item2);
        }

        public int X { get => x; }
        public int Y { get => y; }
        public bool Active { get => active; set => active = value; }
    }

    class Game
    {
        Field field;
        List<Buffalo> buffs = new List<Buffalo>();
        int won = 0; // 0 - player; 1 - buffalos

        public Game(int fieldSize, int buffaloCount)
        {
            field = new Field(fieldSize);
            for (int i = 0; i < buffaloCount; i++) buffs.Add(new Buffalo());
        }

        private void VisualizeElements()
        {
            Console.Clear();
            
            field.Show();

            buffs.Sort((a, b) => a.Active.CompareTo(b.Active));
            foreach (var item in buffs)
            {
                if(item.Active) item.Move(field);
                item.Show();
            }
        }

        private void Shoot()
        {
            Console.Write("X pos: ");
            int xIn = int.Parse(Console.ReadLine());

            Console.Write("Y pos: ");
            int yIn = int.Parse(Console.ReadLine());

            foreach(var item in buffs) if (item.X == xIn && item.Y == yIn) item.Deactivate();
        }

        public void Run()
        {
            do
            {
                if (IsOver) break;
                VisualizeElements();
                if (IsOver) break;
                Shoot();
            } while (!IsOver);

            Console.Clear();

            if(won == 0) Console.WriteLine("\nGame won by the player\n");
            else Console.WriteLine("\nGame won by the buffalos\n");
        }

        private bool CheckGameState()
        {
            bool state = false;

            if (buffs.Any(e => e.X == field.TargetX && e.Y == field.TargetY))
            {
                state = true;
                won = 1;
            }
            else if (buffs.Where(e => e.Active == true).Count() == 0)
            {
                state = true;
                won = 0;
            }

            return state;
        }

        public bool IsOver => CheckGameState();
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            //Feladat1();
            Feladat2();
        }

        static void Feladat1()
        {
            Team team = new Team();
            Player[] players = RandomPlayers(14);

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
                    Console.Write($"{team.Players[i].ToString()} ");
                }
                Console.WriteLine("]");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow) cursorPos++;
                else if (keyInfo.Key == ConsoleKey.UpArrow) cursorPos--;
                Math.Clamp(cursorPos, 0, players.Length - 1);

                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    if (!team.IsAvailable(players[cursorPos]))
                    {
                        Console.WriteLine("A játékos poziciója már foglalt");
                    }
                    else team.Include(players[cursorPos]);
                }
            }

            Console.Clear();
            Console.Write("\nA kész csapat: [ ");
            for (int i = 0; i < team.NumberOfPlayers; i++)
            {
                Console.Write($"{team.Players[i].ToString()} ");
            }
            Console.WriteLine("]\n");
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

        static void Feladat2()
        {
            Game game = new Game(4, 4);
            game.Run();
        }
    }
}
