using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArmoryLib;
using ArmoryLib.Guild;
using ArmoryLib.Character;

namespace ArmoryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string guildName = "The Dominion";
            string realmName = "Sporeggar";
            string characterName = "Zoing";

            Armory armory = new Armory
            {
                Region = Region.Europe
            };

            List<Guild> guilds = armory.SearchGuild(guildName);
            guilds.Sort();

            if (guilds.Count > 0)
            {
                Console.WriteLine("Search results for {0}:", guildName);
                foreach (Guild g in guilds)
                {
                    Console.WriteLine(string.Format("{0} - {1} - {2} - {3}",
                                                    g.Name,
                                                    g.Faction,
                                                    g.BattleGroup,
                                                    g.Realm));
                }
            }
            else
            {
                Console.WriteLine("No results for {0}.", guildName);
            }

            Console.WriteLine(new string('-', 50));

            Guild guild = armory.LoadGuild(realmName, guildName);
            if (guild != null)
            {
                Console.WriteLine("Details for {0} - {1}:", guildName, realmName);
                Console.WriteLine(string.Format("{1} - {2} - {3} - {4} - {5}.",
                                                Environment.NewLine,
                                                guild.Name,
                                                guild.Faction,
                                                guild.BattleGroup,
                                                guild.Realm,
                                                guild.MemberCountText));
            }
            else
            {
                Console.WriteLine("Guild {0} - {1} does not exist.", guildName, realmName);
            }

            Console.WriteLine(new string('-', 50));

            List<Character> characters = armory.SearchCharacter(characterName);
            characters.Sort();

            if (characters.Count > 0)
            {
                Console.WriteLine("Search results for {0}:", characterName);
                foreach (Character c in characters)
                {
                    Console.WriteLine(string.Format("{1} - {2} - {3} - {4} - {5}{0}\t{6} - {7} - {8} - {9}",
                                                    Environment.NewLine,
                                                    c.Name,
                                                    c.Faction,
                                                    c.Gender,
                                                    c.Level,
                                                    c.Race,
                                                    c.Class,
                                                    c.BattleGroup,
                                                    c.Realm,
                                                    c.Guild.Name));
                }
            }
            else
            {
                Console.WriteLine("No results for {0}.", characterName);
            }

            /*
             * Specs:
             *  - Character
             *      - SearchCharacter(string name) - OK
             *      - LoadCharacter(string name, string realm)
             *  - Guild
             *      - SearchGuild(string name) - OK
             *      - LoadGuild(string name, string realm)
             *  - Item?
             *      - LoadItem(????)
             * */
        }
    }
}

// guilds: http://be.imba.hu/ (requires login)
// chars: http://be.imba.hu/?zone=EU&realm=Sporeggar&character=Zoing (public)
// items: wowhead (public)
