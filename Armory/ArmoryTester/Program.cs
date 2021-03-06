﻿using System;
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
            // http://eu.wowarmory.com/character-sheet.xml?r=Sporeggar&n=Lok%C3%AD

            string guildName = "The Dominion";
            //string realmName = "Aggramar";
            //string characterName = "Vohbo";
        
            string realmName = "Sporeggar";
            string characterName = "Lokí";
            //string characterName = "Zoing";
            //string characterName = "Hellstrike";
            //string characterName = "Licia";
            //string characterName = "Dinli";

           /* Console.Write("Realm: ");
            realmName = Console.ReadLine();

            Console.Write("Guild: ");
            guildName = Console.ReadLine();

            Console.Write("Character: ");
            characterName = Console.ReadLine();*/

            Armory armory = new Armory
            {
                Region = Region.Europe
            };


            /*if (guildName != string.Empty)
            {
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
            }

            if ((realmName != string.Empty) && (guildName != string.Empty))
            {
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
                        foreach (var rank in guild.Members.Keys)
                        {
                            foreach (var member in guild.Members[rank])
                            {
                                //member.LoadDetail(CharacterDetail.CharacterSheet);
                                Console.WriteLine(string.Format("  - [{4}] {0}, {1} Level {2} {3}",
                                                                member.Name,
                                                                member.Gender,
                                                                member.Level,
                                                                member.Class,
                                                                rank));
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Guild {0} - {1} does not exist.", guildName, realmName);
                }

                Console.WriteLine(new string('-', 50));
            }

            if (characterName != string.Empty)
            {
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
            }
            */
            if ((realmName != string.Empty) && (characterName != string.Empty))
            {
                Character character = armory.LoadCharacter(realmName,
                                                           characterName,
                                                           CharacterDetail.Basic |
                                                           CharacterDetail.CharacterSheet);

                character.LoadDetail(CharacterDetail.Reputation);
                if (character != null)
                {
                    StringBuilder professions = new StringBuilder();
                    foreach (Skill prof in character.Professions)
                    {
                        professions.Append("    ");
                        professions.Append(prof.Name);
                        professions.Append(" (");
                        professions.Append(prof.Level);
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
                                                    (character.Titles.Count > 0) ? titles.ToString() : ""
                                                    ));

                    character.LoadDetail(CharacterDetail.Reputation);
                    Console.WriteLine("  Reputation:");
                    foreach (Reputation rep in character.Reputation)
                    {
                        Console.WriteLine("    {0} - {1} ({2})", rep.Name, rep.Level, rep.Value);
                    }
                    Console.WriteLine();

                    Console.WriteLine("  Exalted With:");
                    var exaltedReps = character.Reputation.GetFilteredReputation(ReputationLevel.Exalted);
                    if (exaltedReps.Count > 0)
                    {
                        foreach (Reputation rep in exaltedReps)
                        {
                            Console.WriteLine("    {0} - {1} ({2})", rep.Name, rep.Level, rep.Value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("    None");
                    }
                    Console.WriteLine();

                    character.LoadDetail(CharacterDetail.Skills);
                    Console.WriteLine("  Skills:");
                    foreach (Skill skill in character.Skills)
                    {
                        Console.WriteLine("    [{0}] {1} ({2})", skill.Type, skill.Name, skill.Level);
                    }

                    Console.WriteLine();
                    character.LoadDetail(CharacterDetail.Talents);
                    Console.WriteLine("Talents: {0}", character.Talents);
                    Console.WriteLine("Talents Url: {0}", character.TalentsUrl);
                }
                else
                {
                    Console.WriteLine("Character {0} - {1} does not exist.", characterName, realmName);
                }
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
// http://eu.wowarmory.com/layout/item-tooltip.xsl
// http://eu.wowarmory.com/item-tooltip.xml?i=30973