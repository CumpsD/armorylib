using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArmoryLib;

namespace ArmoryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Armory armory = new Armory
            {
                Region = Region.Europe
            };

            /*
             * Specs:
             *  - Character
             *      - SearchCharacter(string name)
             *      - LoadCharacter(string name, string realm)
             *  - Guild
             *      - SearchGuild(string name)
             *      - LoadGuild(string name, string realm)
             *  - Item?
             *      - LoadItem(????)
             * */
        }
    }
}
