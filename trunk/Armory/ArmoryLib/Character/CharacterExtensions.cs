﻿/// <summary>** BEGIN LICENSE BLOCK *****
/// Version: LGPL 3
/// 
/// Copyright 2008 David Cumps <david@cumps.be>
/// 
/// This file is part of ArmoryLib.
///
/// ArmoryLib is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Lesser General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// ArmoryLib is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
///
/// You should have received a copy of the GNU Lesser General Public License
/// along with ArmoryLib.  If not, see <http://www.gnu.org/licenses/>.
/// **** END LICENSE BLOCK ****
/// </summary>
using System;
using System.Xml;
using System.Collections.Generic;
using System.Web;

using ArmoryLib.Guild;
using G = ArmoryLib.Guild.Guild;
using ArmoryLib.Character.StatsDetail;
using ArmoryLib.Character.ResistancesDetail;
using M = ArmoryLib.Character.MeleeDetail;
using R = ArmoryLib.Character.RangedDetail;
using D = ArmoryLib.Character.DefenseDetail;

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
                    Stats stats = LoadStats(character, searchResults);
                    LoadResistances(stats, searchResults);
                    LoadMelee(character, stats, searchResults);
                    LoadRanged(character, stats, searchResults);
                    LoadDefense(stats, searchResults);

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
            //<pvp>
            //  <lifetimehonorablekills value="1469"/>
            //  <arenacurrency value="315"/>
            //</pvp>
            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/pvp");
            
            PvpInfo pvpInfo = new PvpInfo(
                Convert.ToInt32(characterNode.SelectSingleNode("lifetimehonorablekills").Attributes["value"].Value),
                Convert.ToInt32(characterNode.SelectSingleNode("arenacurrency").Attributes["value"].Value));

            character.PvpInfo = pvpInfo;
        }

        private static Stats LoadStats(Character character, XmlDocument searchResults)
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
                Convert.ToInt32(staminaNode.Attributes["effective"].Value),
                Convert.ToInt32(staminaNode.Attributes["petBonus"].Value));

            XmlNode intellectNode = characterNode.SelectSingleNode("intellect");
            Intellect intellect = new Intellect(
                Convert.ToInt32(intellectNode.Attributes["mana"].Value),
                Convert.ToInt32(intellectNode.Attributes["base"].Value),
                Convert.ToDouble(intellectNode.Attributes["critHitPercent"].Value, Util.NumberFormatter),
                Convert.ToInt32(intellectNode.Attributes["effective"].Value),
                Convert.ToInt32(intellectNode.Attributes["petBonus"].Value));

            XmlNode spiritNode = characterNode.SelectSingleNode("spirit");
            Spirit spirit = new Spirit(
                Convert.ToInt32(spiritNode.Attributes["healthRegen"].Value),
                Convert.ToInt32(spiritNode.Attributes["manaRegen"].Value),
                Convert.ToInt32(spiritNode.Attributes["base"].Value),
                Convert.ToInt32(spiritNode.Attributes["effective"].Value));

            XmlNode armorNode = characterNode.SelectSingleNode("armor");
            Armor armor = new Armor(
                Convert.ToDouble(armorNode.Attributes["percent"].Value, Util.NumberFormatter),
                Convert.ToInt32(armorNode.Attributes["base"].Value),
                Convert.ToInt32(armorNode.Attributes["effective"].Value),
                Convert.ToInt32(armorNode.Attributes["petBonus"].Value));

            Stats stats = new Stats(strength,
                                    agility,
                                    stamina,
                                    intellect,
                                    spirit,
                                    armor);

            character.Stats = stats;
            return stats;
        }

        private static void LoadResistances(Stats stats, XmlDocument searchResults)
        {
            //<resistances>
            //  <arcane petBonus="-1" value="5"/>
            //  <fire petBonus="-1" value="5"/>
            //  <frost petBonus="-1" value="5"/>
            //  <holy petBonus="-1" value="0"/>
            //  <nature petBonus="-1" value="5"/>
            //  <shadow petBonus="-1" value="5"/>
            //</resistances>
            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/resistances");

            XmlNode arcaneNode = characterNode.SelectSingleNode("arcane");
            Arcane arcane = new Arcane(
                Convert.ToInt32(arcaneNode.Attributes["value"].Value));

            XmlNode fireNode = characterNode.SelectSingleNode("fire");
            Fire fire = new Fire(
                Convert.ToInt32(fireNode.Attributes["value"].Value));

            XmlNode frostNode = characterNode.SelectSingleNode("frost");
            Frost frost = new Frost(
                Convert.ToInt32(frostNode.Attributes["value"].Value));

            XmlNode holyNode = characterNode.SelectSingleNode("holy");
            Holy holy = new Holy(
                Convert.ToInt32(holyNode.Attributes["value"].Value));

            XmlNode natureNode = characterNode.SelectSingleNode("nature");
            Nature nature = new Nature(
                Convert.ToInt32(natureNode.Attributes["value"].Value));

            XmlNode shadowNode = characterNode.SelectSingleNode("shadow");
            Shadow shadow = new Shadow(
                Convert.ToInt32(shadowNode.Attributes["value"].Value));

            Resistances resistances = new Resistances(arcane,
                                                      fire,
                                                      frost,
                                                      holy,
                                                      nature,
                                                      shadow);

            stats.Resistances = resistances;
        }

        private static void LoadMelee(Character character, Stats stats, XmlDocument searchResults)
        {
            //<melee>
            //  <mainHandDamage dps="184.7" max="532" min="429" percent="0" speed="2.60"/>
            //  <offHandDamage dps="138.5" max="242" min="174" percent="0" speed="1.50"/>
            //  <mainHandSpeed hastePercent="0.00" hasteRating="0" value="2.60"/>
            //  <offHandSpeed hastePercent="0.00" hasteRating="0" value="1.50"/>
            //  <power base="528" effective="1218" increasedDps="87.0"/>
            //  <hitRating increasedHitPercent="6.72" value="106"/>
            //  <critChance percent="19.90" plusPercent="7.29" rating="161"/>
            //  <expertise additional="0" percent="2.50" rating="0" value="10"/>
            //</melee>
            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/melee");

            XmlNode mainHandNode = characterNode.SelectSingleNode("mainHandDamage");
            XmlNode mainHandSpeedNode = characterNode.SelectSingleNode("mainHandSpeed");
            M.MainHand mainHand = new M.MainHand(
                Convert.ToDouble(mainHandNode.Attributes["dps"].Value, Util.NumberFormatter),
                Convert.ToInt32(mainHandNode.Attributes["min"].Value),
                Convert.ToInt32(mainHandNode.Attributes["max"].Value),
                Convert.ToDouble(mainHandNode.Attributes["speed"].Value, Util.NumberFormatter));

            XmlNode offHandNode = characterNode.SelectSingleNode("offHandDamage");
            XmlNode offHandSpeedNode = characterNode.SelectSingleNode("offHandSpeed");
            M.OffHand offHand = new M.OffHand(
                Convert.ToDouble(mainHandNode.Attributes["dps"].Value, Util.NumberFormatter),
                Convert.ToInt32(mainHandNode.Attributes["min"].Value),
                Convert.ToInt32(mainHandNode.Attributes["max"].Value),
                Convert.ToDouble(mainHandNode.Attributes["speed"].Value, Util.NumberFormatter));

            XmlNode apNode = characterNode.SelectSingleNode("power");
            M.AttackPower attackPower = new M.AttackPower(
                Convert.ToInt32(apNode.Attributes["base"].Value),
                Convert.ToInt32(apNode.Attributes["effective"].Value),
                Convert.ToDouble(apNode.Attributes["increasedDps"].Value, Util.NumberFormatter));

            XmlNode hitNode = characterNode.SelectSingleNode("hitRating");
            M.Hit hit = new M.Hit(
                Convert.ToInt32(hitNode.Attributes["value"].Value),
                Convert.ToDouble(hitNode.Attributes["increasedHitPercent"].Value, Util.NumberFormatter),
                character.Level);

            XmlNode critNode = characterNode.SelectSingleNode("critChance");
            M.Crit crit = new M.Crit(
                Convert.ToInt32(critNode.Attributes["rating"].Value),
                Convert.ToDouble(critNode.Attributes["percent"].Value, Util.NumberFormatter),
                Convert.ToDouble(critNode.Attributes["plusPercent"].Value, Util.NumberFormatter));

            XmlNode expertiseNode = characterNode.SelectSingleNode("expertise");
            M.Expertise expertise = new M.Expertise(
                Convert.ToDouble(expertiseNode.Attributes["percent"].Value, Util.NumberFormatter));

            Melee melee = new Melee(mainHand,
                                    offHand,
                                    attackPower,
                                    hit,
                                    crit,
                                    expertise);

            stats.Melee = melee;
        }

        private static void LoadRanged(Character character, Stats stats, XmlDocument searchResults)
        {
            //<ranged>
            //  <weaponSkill rating="0" value="350"/>
            //  <damage dps="295.7" max="823" min="668" percent="0" speed="2.52"/>
            //  <speed hastePercent="0.00" hasteRating="0" value="2.52"/>
            //  <power base="831" effective="1662" increasedDps="118.0" petAttack="365.64" petSpell="213.90"/>
            //  <hitRating increasedHitPercent="4.95" value="78"/>
            //  <critChance percent="30.55" plusPercent="13.14" rating="290"/>
            //</ranged>
            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/ranged");

            XmlNode rangedSkillNode = characterNode.SelectSingleNode("weaponSkill");
            XmlNode rangedDpsNode = characterNode.SelectSingleNode("damage");
            XmlNode rangedSpeedNode = characterNode.SelectSingleNode("speed");
            R.RangedSlot rangedSlot = new R.RangedSlot(
                Convert.ToInt32(rangedSkillNode.Attributes["value"].Value),
                Convert.ToDouble(rangedDpsNode.Attributes["dps"].Value, Util.NumberFormatter),
                Convert.ToInt32(rangedDpsNode.Attributes["min"].Value),
                Convert.ToInt32(rangedDpsNode.Attributes["max"].Value),
                Convert.ToDouble(rangedDpsNode.Attributes["speed"].Value, Util.NumberFormatter));

            XmlNode apNode = characterNode.SelectSingleNode("power");
            R.AttackPower attackPower = new R.AttackPower(
                Convert.ToInt32(apNode.Attributes["base"].Value),
                Convert.ToInt32(apNode.Attributes["effective"].Value),
                Convert.ToDouble(apNode.Attributes["increasedDps"].Value, Util.NumberFormatter),
                Convert.ToDouble(apNode.Attributes["petAttack"].Value, Util.NumberFormatter),
                Convert.ToDouble(apNode.Attributes["petSpell"].Value, Util.NumberFormatter));

            XmlNode hitNode = characterNode.SelectSingleNode("hitRating");
            R.Hit hit = new R.Hit(
                Convert.ToInt32(hitNode.Attributes["value"].Value),
                Convert.ToDouble(hitNode.Attributes["increasedHitPercent"].Value, Util.NumberFormatter),
                character.Level);

            XmlNode critNode = characterNode.SelectSingleNode("critChance");
            R.Crit crit = new R.Crit(
                Convert.ToInt32(critNode.Attributes["rating"].Value),
                Convert.ToDouble(critNode.Attributes["percent"].Value, Util.NumberFormatter),
                Convert.ToDouble(critNode.Attributes["plusPercent"].Value, Util.NumberFormatter));

            Ranged ranged = new Ranged(rangedSlot,
                                       attackPower,
                                       hit,
                                       crit);

            stats.Ranged = ranged;
        }

        private static void LoadDefense(Stats stats, XmlDocument searchResults)
        {
            //<defenses>
            //  <armor base="6540" effective="6540" percent="38.25" petBonus="2289"/>
            //  <defense decreasePercent="0.00" increasePercent="0.00" plusDefense="0" rating="0" value="350.00"/>
            //  <dodge increasePercent="0.00" percent="15.27" rating="0"/>
            //  <parry increasePercent="0.00" percent="5.00" rating="0"/>
            //  <block increasePercent="0.00" percent="0.00" rating="0"/>
            //  <resilience damagePercent="13.70" hitPercent="6.85" value="270.00"/>
            //</defenses>

            XmlNode characterNode = searchResults.SelectSingleNode("/page/characterInfo/characterTab/defenses");

            XmlNode defenseNode = characterNode.SelectSingleNode("defense");
            D.Defense defense = new D.Defense(
                Convert.ToDouble(defenseNode.Attributes["value"].Value, Util.NumberFormatter));

            XmlNode dodgeNode = characterNode.SelectSingleNode("dodge");
            D.Dodge dodge = new D.Dodge(
                Convert.ToInt32(dodgeNode.Attributes["rating"].Value),
                Convert.ToDouble(dodgeNode.Attributes["percent"].Value, Util.NumberFormatter),
                Convert.ToDouble(dodgeNode.Attributes["increasePercent"].Value, Util.NumberFormatter));

            XmlNode parryNode = characterNode.SelectSingleNode("parry");
            D.Parry parry = new D.Parry(
                Convert.ToInt32(parryNode.Attributes["rating"].Value),
                Convert.ToDouble(parryNode.Attributes["percent"].Value, Util.NumberFormatter),
                Convert.ToDouble(parryNode.Attributes["increasePercent"].Value, Util.NumberFormatter));

            XmlNode blockNode = characterNode.SelectSingleNode("block");
            D.Block block = new D.Block(
                Convert.ToInt32(blockNode.Attributes["rating"].Value),
                Convert.ToDouble(blockNode.Attributes["percent"].Value, Util.NumberFormatter),
                Convert.ToDouble(blockNode.Attributes["increasePercent"].Value, Util.NumberFormatter));

            XmlNode resilienceNode = characterNode.SelectSingleNode("resilience");
            D.Resilience resilience = new D.Resilience(
                Convert.ToDouble(resilienceNode.Attributes["value"].Value, Util.NumberFormatter));

            Defenses defenses = new Defenses(defense,
                                             block,
                                             dodge,
                                             parry,
                                             resilience);

            defenses.Armor = stats.Armor;
            stats.Defense = defenses;
        }
    }
}