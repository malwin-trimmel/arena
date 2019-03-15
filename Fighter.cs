using System;

namespace Arena
{
    class Fighter
    {
        private static Random RANDOM = new Random();

        private string Name;
        private int Strength;
        private int fightCount;

        public Fighter(string NameParam, int StrengthParam)
        {
            Name = NameParam;
            Strength = StrengthParam;
            fightCount = 0;
        }

        
        public string getName()
        {
            return Name;
        }

        public int getStrength()
        {
            return Strength;
        }

        public int getfightCount()
        {
            return fightCount;
        }

        public void CountUpFight()
        {
            fightCount = fightCount + 1;
        }

        public void LevelUp(int moreStrength) //zufallsstärke
        {
            double randomNumber = RANDOM.NextDouble();
            
            double randomMoreStrength = (double)moreStrength * randomNumber;

            Strength = Convert.ToInt32(randomMoreStrength) + Strength;
        }


        public static Fighter createFighterRandomStrength(string name, int minStrength, int maxStrength)
        {
            return new Fighter(name, 1); // Meine Hausübung nicht wichtig 1. Überprüfen ob er
        }
    }
}