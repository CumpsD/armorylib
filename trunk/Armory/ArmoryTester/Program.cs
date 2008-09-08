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
            //string realmName = "Aggramar";
            //string characterName = "Vohbo";

            string realmName = "Sporeggar";
            //string characterName = "Hellstrike";
            //string characterName = "Licia";
            string characterName = "Zoing";
            //string characterName = "Dinli";

            Armory armory = new Armory
            {
                Region = Region.Europe
            };
            /*
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

                if (guild.IsDetailLoaded(GuildDetail.Roster))
                {
                    foreach (Character member in guild.Members)
                    {
                        Console.WriteLine(string.Format("  - {0}, {1} Level {2} {3}",
                                                        member.Name,
                                                        member.Gender,
                                                        member.Level,
                                                        member.Class));
                    }
                }
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
                    Console.WriteLine(string.Format("{1} - {2} - {3} - Level {4} - {5}{0}\t{6} - {7} - {8} - {9}",
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

            Console.WriteLine(new string('-', 50));
            */
            Character character = armory.LoadCharacter(realmName, 
                                                       characterName, 
                                                       CharacterDetail.Basic | 
                                                       CharacterDetail.CharacterSheet);
            if (character != null)
            {
                StringBuilder professions = new StringBuilder();
                foreach (Profession prof in character.Professions)
                {
                    professions.Append("    ");
                    professions.Append(prof.Name);
                    professions.Append(" (");
                    professions.Append(prof.Skill);
                    professions.Append(")");
                    professions.Append(Environment.NewLine);
                }

                StringBuilder titles = new StringBuilder();
                titles.Append(Environment.NewLine);
                titles.Append("  Available Titles:");
                titles.Append(Environment.NewLine);
                foreach (Title title in character.Titles)
                {
                    titles.Append("    ");
                    titles.Append(title.FormattedTitle);
                    titles.Append(Environment.NewLine);
                }

                Console.WriteLine("Details for {0} - {1}:", characterName, realmName);
                Console.WriteLine(string.Format("{1}{0}" +
                                                "  {2} - {3} - Level {4} - {5}{0}" +
                                                "  {6} - {7} - {8} - {9}{0}" +
                                                "  Spec: {10}{0}{0}" +
                                                "  PvP: {11}{0}{0}" + 
                                                "  Strength: {12}{0}" +
                                                "  Agility: {13}{0}" +
                                                "  Stamina: {14}{0}" +
                                                "  Intellect: {15}{0}" +
                                                "  Spirit: {16}{0}" +
                                                "  Armor: {17}{0}{0}" +
                                                "  Resistances: {18}{0}{0}" +
                                                "  Melee: {0}    {19}{0}" +
                                                "  Ranged: {0}    {20}{0}" +
                                                "  Defense: {0}    {21}{0}" +
                                                "  Spell: {0}    {22}{0}" +
                                                "  {23}{0}" +
                                                "  {24}{0}" +
                                                "  Total Health: {25}{0}" +
                                                "  Total {26}: {27}{0}{0}" +
                                                "  Title: {28}{0}" +
                                                "  {29}",
                                                Environment.NewLine,
                                                character.Name,
                                                character.Faction,
                                                character.Gender,
                                                character.Level,
                                                character.Race,
                                                character.Class,
                                                character.BattleGroup,
                                                character.Realm,
                                                character.Guild.Name,
                                                character.TalentSpec.SpecAbbreviation,
                                                character.PvpInfo,
                                                character.Stats.Strength,
                                                character.Stats.Agility,
                                                character.Stats.Stamina,
                                                character.Stats.Intellect.ToString()
                                                    .Replace(", Pet", Environment.NewLine + new string(' ', 13) + "Pet"),
                                                character.Stats.Spirit.ToString()
                                                    .Replace(", Health", Environment.NewLine + new string(' ', 10) + "Health")
                                                    .Replace(", Mana", Environment.NewLine + new string(' ', 10) + "Mana"),
                                                character.Stats.Armor.ToString()
                                                    .Replace(", Reduces", Environment.NewLine + new string(' ', 9) + "Reduces")
                                                    .Replace(", Pet", Environment.NewLine + new string(' ', 9) + "Pet"),
                                                character.Stats.Resistances,
                                                character.Stats.Melee.ToString()
                                                    .Replace(Environment.NewLine, Environment.NewLine + "    "),
                                                character.Stats.Ranged.ToString()
                                                    .Replace(Environment.NewLine, Environment.NewLine + "    ")
                                                    .Replace(", Pet", Environment.NewLine + new string(' ', 17) + "Pet"),
                                                character.Stats.Defense.ToString()
                                                    .Replace(Environment.NewLine, Environment.NewLine + "    ")
                                                    .Replace(", Reduces", Environment.NewLine + new string(' ', 11) + "Reduces")
                                                    .Replace(", Pet", Environment.NewLine + new string(' ', 11) + "Pet"),
                                                character.Stats.Spell.ToString()
                                                    .Replace(Environment.NewLine, Environment.NewLine + "    "),
                                                character.Effects.ToString()
                                                    .Replace(Environment.NewLine, Environment.NewLine + "    ")
                                                    .Replace("  Debuffs", "Debuffs"),
                                                (professions.ToString() != string.Empty) ? string.Format("{1}  Professions:{1}{0}", professions.ToString(), Environment.NewLine) : "",
                                                character.Stats.TotalHealth,
                                                character.Stats.SecondaryBar.Type,
                                                character.Stats.SecondaryBar.Effective,
                                                (character.Title != null) ? character.Title : "None",
                                                (character.Titles.Count > 0) ? string.Format("{0}{1}", titles.ToString(), Environment.NewLine) : ""
                                                ));
            }
            else
            {
                Console.WriteLine("Character {0} - {1} does not exist.", characterName, realmName);
            }

            /*
             * Specs:
             *  - Character
             *      - SearchCharacter(string name) - OK
             *      - LoadCharacter(string name, string realm)
             *  - Guild
             *      - SearchGuild(string name) - OK
             *      - LoadGuild(string name, string realm) - OK
             *  - Item?
             *      - LoadItem(????)
             *  - Enchants and Gems needed for character tab
             * */
        }
    }
}

// items: wowhead (public)
