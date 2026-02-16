using System.IO;

namespace SD_Week1
{
    #region Feladat1
    enum Species
    {
        Dog,
        Panda,
        Rabbit
    }

    class Animal
    {
        string name;
        bool gender;
        int weight;
        Species species;

        public Animal(string name, bool gender, int weight, Species species)
        {
            this.name = name;
            this.gender = gender;
            this.weight = weight;
            this.species = species;
        }

        public string Name { get => name; }
        public bool Gender { get => gender; }
        public int Weight { get => weight; }
        internal Species Species { get => species; }

        public override string ToString()
        {
            return $"Name: {name}, Gender: {(gender ? "Male" : "Female")}, Weight: {weight}";
        }
    }

    class Cage
    {
        Animal[] animals;
        int animalCount = 0;

        public Cage(int cageSize)
        {
            animals = new Animal[cageSize];
        }

        public Cage(string path)
        {
            int lineCount = File.ReadLines(path).Count();
            Animal[] animals = new Animal[lineCount];

            using (var sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    Add(new Animal(data[0], bool.Parse(data[1]), int.Parse(data[2]), (Species)int.Parse(data[3])));
                }
            }
        }

        public bool Add(Animal animal)
        {
            for (int i = 0; i < animals.Length; i++)
            {
                if (animals[i] == null)
                {
                    animals[i] = animal;
                    animalCount++;
                    return true;
                }
            }

            return false;
        }

        public void Remove(string name)
        {
            for (int i = 0; i < animals.Length; i++)
            {
                if (animals[i] != null && animals[i].Name == name)
                {
                    animals[i] = null;
                    animalCount--;
                }
            }
        }

        public int HowManyPerSpecies(Species spice)
        {
            int data = 0;

            for (int i = 0; i < animals.Length; i++)
                if (animals[i].Species == spice) data++;

            return data;
        }

        public bool IsAnySpeciesAndGender(Species spice, bool gender)
        {
            for (int i = 0; i < animals.Length; i++)
            {
                if (animals[i].Species == spice && animals[i].Gender == gender)
                    return true;
            }

            return false;
        }

        public List<Species> SpeciesInCage()
        {
            List<Species> spices = new List<Species>();

            for (int i = 0; i < animals.Length; i++)
            {
                if (!spices.Contains(animals[i].Species))
                    spices.Add(animals[i].Species);
            }

            return spices;
        }

        public float AvgweightOfSpecies(Species spice)
        {
            float data = 0;
            int animalCount = 0;

            for (int i = 0; i < animals.Length; i++)
            {
                if (animals[i].Species == spice)
                {
                    data += animals[i].Weight;
                    animalCount++;
                }
            }

            return data / animalCount;
        }

        public bool AnimalPair()
        {
            List<Animal> onePerSpice = new List<Animal>();

            for (int i = 0; i < animals.Length; i++)
            {
                if (!onePerSpice.Select(e => e.Species).ToHashSet().Contains(animals[i].Species))
                    onePerSpice.Add(animals[i]);
                else
                {
                    if (onePerSpice.Where(e => e.Species == animals[i].Species).First().Gender != animals[i].Gender)
                        return true;
                }
            }

            return false;
        }

        public void Print()
        {
            foreach (var item in animals)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }
    }

    #endregion

    #region Feladat2
    class Runner
    {
        string name;
        int pace;
        int time;
        bool gavup;

        Random random = new Random();

        public Runner(string name, int pace)
        {
            this.name = name;
            this.pace = pace;
        }

        public string Name { get => name; set => name = value; }
        public int Pace { get => pace; set => pace = value; }
        public int Time { get => time; set => time = value; }
        public bool Gavup { get => gavup; set => gavup = value; }

        public int Move()
        {
            if (gavup) return 0;

            if (time > 60 && time <= 90) gavup = random.Next(1000) == 1;
            else if (time > 90 && time <= 120) gavup = random.Next(2000) == 1;
            else if (time > 120 && time <= 180) gavup = random.Next(3000) == 1;
            else if (time > 180) gavup = random.Next(5000) == 1;

            time++;
            return 60 * pace;
        }
    }

    enum TeamType
    {
        SOLO,
        DUO,
        TRIO,
        QUANTUPLE
    }

    class Team
    {
        Random random;

        string[] nameList = { "yo", "ya", "zsa", "na", "ey", "oh" };

        List<Runner> runners;

        int currentRunner = 0;
        int currentRunnerDistance = 0;

        int allDistance = 0;
        int runningTime = 0;

        bool gavup = false;

        public Team(TeamType type)
        {
            switch (type)
            {
                case TeamType.SOLO:
                    GenerateRunners(1);
                    break;
                case TeamType.DUO:
                    GenerateRunners(2);
                    break;
                case TeamType.TRIO:
                    GenerateRunners(3);
                    break;
                case TeamType.QUANTUPLE:
                    GenerateRunners(5);
                    break;
            }
        }

        public void Move()
        {
            runners[currentRunner].Move();
        }

        void GenerateRunners(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
                runners.Add(new Runner(nameList[random.Next(0, nameList.Length)], random.Next(3, 10)));
        }
    }
    #endregion

    internal class Program
    {
        #region Feladat1
        static public Cage[] cages = new Cage[4];

        static public Cage MostPerSpecies(Species spice)
        {
            (int, Cage) best = (0, cages[0]);

            foreach (var item in cages)
            {
                int curCount = item.HowManyPerSpecies(spice);
                if (curCount > best.Item1)
                    best = (curCount, item);
            }

            return best.Item2;
        }

        static public void LoadCages(string folderPath)
        {
            string[] txtFiles = Directory.GetFiles(folderPath, "*.txt");

            int loopCount = txtFiles.Length > cages.Length ? cages.Length : txtFiles.Length;

            for (int i = 0; i < loopCount; i++)
            {
                cages[i] = (new Cage(txtFiles[i]));
            }
        }

        static void Feladat1()
        {
            cages[0] = new Cage(3);
            cages[0].Add(new Animal("józsef", true, 23, Species.Panda));
            cages[0].Add(new Animal("Zsuzsa", false, 4, Species.Rabbit));
            cages[0].Add(new Animal("William", true, 10, Species.Dog));

            cages[1] = new Cage(2);
            cages[1].Add(new Animal("Ham", true, 3, Species.Rabbit));
            cages[1].Add(new Animal("Erzsi", false, 8, Species.Dog));


            cages[2] = new Cage(1);
            cages[2].Add(new Animal("Kata", false, 20, Species.Panda));

            cages[3] = new Cage(2);
            cages[3].Add(new Animal("Cecil", false, 9, Species.Dog));
            cages[3].Add(new Animal("Gyuri", true, 2, Species.Rabbit));


            int cageCount = 0;
            foreach (var cage in cages)
            {
                Console.WriteLine($"Cage{++cageCount}");
                cage.Print();

            }
        }
        #endregion

        static void Feladat2()
        {

        }


        static void Main(string[] args)
        {
            //Feladat1();

            Feladat2();
        }
    }
}
