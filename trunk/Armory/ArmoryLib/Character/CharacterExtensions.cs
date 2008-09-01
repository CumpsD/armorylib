using System;
using System.Xml;
using System.Collections.Generic;
using System.Web;

using G = ArmoryLib.Guild.Guild;

namespace ArmoryLib.Character
{
    public static class CharacterExtensions
    {
        public static List<Character> SearchCharacter(this Armory armory, string characterName)
        {
            // TODO: Take multiple pages into account!

            // http://eu.wowarmory.com/search.xml?searchQuery=Zoing&searchType=characters
            string searchString = string.Format("search.xml?searchQuery={0}&searchType=characters",
                                                HttpUtility.UrlEncode(characterName));

            XmlDocument searchResults = armory.Request(searchString);
            XmlNodeList characterNodes = searchResults.SelectNodes("/page/armorySearch/searchResults/characters/character");

            List<Character> characters = new List<Character>();

            foreach (XmlNode characterNode in characterNodes)
            {
                // <character battleGroup="Vindication" battleGroupId="61" class="Rogue" classId="4" faction="Horde" factionId="1" gender="Female" genderId="1" guild="The Dominion" guildId="222341" level="70" name="Zoing" race="Blood Elf" raceId="10" realm="Sporeggar" url="r=Sporeggar&amp;n=Zoing"/>
                G guild = new G(
                            true,
                            armory.Region,
                            (Faction)Enum.Parse(typeof(Faction), characterNode.Attributes["factionId"].Value),
                            characterNode.Attributes["guild"].Value,
                            characterNode.Attributes["realm"].Value,
                            characterNode.Attributes["battleGroup"].Value,
                            string.Format("r={0}&amp;n={1}&amp;p=1", 
                                          characterNode.Attributes["realm"].Value,
                                          characterNode.Attributes["guild"].Value));

                Character character = new Character(
                                            true,
                                            armory.Region,
                                            (Faction)Enum.Parse(typeof(Faction), characterNode.Attributes["factionId"].Value),
                                            characterNode.Attributes["name"].Value,
                                            characterNode.Attributes["realm"].Value,
                                            characterNode.Attributes["battleGroup"].Value,
                                            (Gender)Enum.Parse(typeof(Gender), characterNode.Attributes["genderId"].Value),
                                            (Race)Enum.Parse(typeof(Race), characterNode.Attributes["raceId"].Value),
                                            (Class)Enum.Parse(typeof(Class), characterNode.Attributes["classId"].Value),
                                            Convert.ToInt32(characterNode.Attributes["level"].Value),
                                            characterNode.Attributes["url"].Value,
                                            guild);

                characters.Add(character);
            }

            return characters;
        }

        public static Character LoadCharacter(this Armory armory, string realmName, string characterName)
        {
            // http://eu.wowarmory.com/character-sheet.xml?r=Sporeggar&n=Zoing
            string searchString = string.Format("character-sheet.xml?r={0}&n={1}",
                                    HttpUtility.UrlEncode(realmName),
                                    HttpUtility.UrlEncode(characterName));

            XmlDocument searchResults = armory.Request(searchString);
            XmlNode characterDetails = searchResults.SelectSingleNode("/page/characterInfo");

            if (characterDetails != null)
            {
                XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/character");

                // <character battleGroup="Vindication" charUrl="r=Sporeggar&amp;n=Zoing" class="Rogue" classId="4" faction="Horde" factionId="1" gender="Female" genderId="1" guildName="The Dominion" guildUrl="r=Sporeggar&amp;n=The+Dominion&amp;p=1" lastModified="31 August 2008" level="70" name="Zoing" prefix="" race="Blood Elf" raceId="10" realm="Sporeggar" suffix="">
                G guild = new G(
                            true,
                            armory.Region,
                            (Faction)Enum.Parse(typeof(Faction), characterNode.Attributes["factionId"].Value),
                            characterNode.Attributes["guildName"].Value,
                            characterNode.Attributes["realm"].Value,
                            characterNode.Attributes["battleGroup"].Value,
                            characterNode.Attributes["guildUrl"].Value);

                Character character = new Character(
                                                    false,
                                                    armory.Region,
                                                    (Faction)Enum.Parse(typeof(Faction), characterNode.Attributes["factionId"].Value),
                                                    characterNode.Attributes["name"].Value,
                                                    characterNode.Attributes["realm"].Value,
                                                    characterNode.Attributes["battleGroup"].Value,
                                                    (Gender)Enum.Parse(typeof(Gender), characterNode.Attributes["genderId"].Value),
                                                    (Race)Enum.Parse(typeof(Race), characterNode.Attributes["raceId"].Value),
                                                    (Class)Enum.Parse(typeof(Class), characterNode.Attributes["classId"].Value),
                                                    Convert.ToInt32(characterNode.Attributes["level"].Value),
                                                    characterNode.Attributes["charUrl"].Value,
                                                    guild);

                return character;
            }
            else
            {
                return null;
            }
        }
    }
}
