using System;
using System.Xml;
using System.Collections.Generic;
using System.Web;

using ArmoryLib.Guild;
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
                            GuildDetail.Basic,
                            armory.Region,
                            (Faction)Enum.Parse(typeof(Faction), characterNode.Attributes["factionId"].Value),
                            characterNode.Attributes["guild"].Value,
                            characterNode.Attributes["realm"].Value,
                            characterNode.Attributes["battleGroup"].Value,
                            string.Format("r={0}&amp;n={1}&amp;p=1", 
                                          characterNode.Attributes["realm"].Value,
                                          characterNode.Attributes["guild"].Value));

                Character character = new Character(
                                            CharacterDetail.Basic,
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
            return LoadCharacter(armory, realmName, characterName, CharacterDetail.Basic);
        }

        public static Character LoadCharacter(this Armory armory, string realmName, string characterName, CharacterDetail loadDetail)
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
                            GuildDetail.Basic,
                            armory.Region,
                            (Faction)Enum.Parse(typeof(Faction), characterNode.Attributes["factionId"].Value),
                            characterNode.Attributes["guildName"].Value,
                            characterNode.Attributes["realm"].Value,
                            characterNode.Attributes["battleGroup"].Value,
                            characterNode.Attributes["guildUrl"].Value);

                Character character = new Character(
                                                    CharacterDetail.Basic,
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

                if (loadDetail.ContainsDetail(CharacterDetail.CharacterSheet))
                {
                    LoadTalentSpec(character, searchResults);
                    LoadPvpInfo(character, searchResults);
                    LoadStats(character, searchResults);

                    // Indicate we finished loading extra detail
                    character.LoadedDetail(CharacterDetail.CharacterSheet);
                }

                return character;
            }
            else
            {
                return null;
            }
        }

        private static void LoadTalentSpec(Character character, XmlDocument searchResults)
        {
            // <talentSpec treeOne="20" treeThree="0" treeTwo="41"/>
            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/talentSpec");

            TalentSpec spec = new TalentSpec(
                Convert.ToInt32(characterNode.Attributes["treeOne"].Value),
                Convert.ToInt32(characterNode.Attributes["treeTwo"].Value),
                Convert.ToInt32(characterNode.Attributes["treeThree"].Value));

            character.TalentSpec = spec;
        }

        private static void LoadPvpInfo(Character character, XmlDocument searchResults)
        {
            // TODO: What is arenacurrency?
            //<pvp>
            //  <lifetimehonorablekills value="1463"/>
            //  <arenacurrency value="0"/>
            //</pvp>
            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/pvp");
            
            PvpInfo pvpInfo = new PvpInfo(
                Convert.ToInt32(characterNode.SelectSingleNode("lifetimehonorablekills").Attributes["value"].Value));

            character.PvpInfo = pvpInfo;
        }

        private static void LoadStats(Character character, XmlDocument searchResults)
        {
            //<baseStats>
            //  <strength attack="82" base="92" block="-1" effective="92"/>
            //  <agility armor="642" attack="311" base="163" critHitPercent="7.73" effective="321"/>
            //  <stamina base="90" effective="377" health="3590" petBonus="-1"/>
            //  <intellect base="43" critHitPercent="-1.00" effective="43" mana="-1" petBonus="-1"/>
            //  <spirit base="57" effective="57" healthRegen="20" manaRegen="-1"/>
            //  <armor base="2181" effective="2181" percent="17.12" petBonus="-1"/>
            //</baseStats>
            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/baseStats");

            XmlNode strengthNode = characterNode.SelectSingleNode("strength");
            Strength strength = new Strength(
                Convert.ToInt32(strengthNode.Attributes["attack"].Value),
                Convert.ToInt32(strengthNode.Attributes["base"].Value),
                Convert.ToInt32(strengthNode.Attributes["block"].Value),
                Convert.ToInt32(strengthNode.Attributes["effective"].Value));

            XmlNode agilityNode = characterNode.SelectSingleNode("agility");
            Agility agility = new Agility(
                Convert.ToInt32(agilityNode.Attributes["armor"].Value),
                Convert.ToInt32(agilityNode.Attributes["attack"].Value),
                Convert.ToInt32(agilityNode.Attributes["base"].Value),
                Convert.ToDouble(agilityNode.Attributes["critHitPercent"].Value, Util.NumberFormatter),
                Convert.ToInt32(agilityNode.Attributes["effective"].Value));

            XmlNode staminaNode = characterNode.SelectSingleNode("stamina");
            Stamina stamina = new Stamina(
                Convert.ToInt32(staminaNode.Attributes["health"].Value),
                Convert.ToInt32(staminaNode.Attributes["base"].Value),
                Convert.ToInt32(staminaNode.Attributes["effective"].Value));

            XmlNode intellectNode = characterNode.SelectSingleNode("intellect");
            Intellect intellect = new Intellect(
                Convert.ToInt32(intellectNode.Attributes["mana"].Value),
                Convert.ToInt32(intellectNode.Attributes["base"].Value),
                Convert.ToDouble(intellectNode.Attributes["critHitPercent"].Value, Util.NumberFormatter),
                Convert.ToInt32(intellectNode.Attributes["effective"].Value));

            Stats stats = new Stats(strength,
                                    agility,
                                    stamina,
                                    intellect);

            character.Stats = stats;
        }
    }
}
