using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    public class Stats
    {
        public Strength Strength { get; private set; }
        public Agility Agility { get; private set; }
        public Stamina Stamina { get; private set; }
        public Intellect Intellect { get; private set; }
        /*public Spirit Spirit { get; private set; }
        public Armor Armor { get; private set; }*/

        internal Stats(Strength strength,
                       Agility agility,
                       Stamina stamina,
                       Intellect intellect)
        {
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            Intellect = intellect;
        }
    }
}
