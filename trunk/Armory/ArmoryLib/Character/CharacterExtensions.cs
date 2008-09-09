/// <summary>** BEGIN LICENSE BLOCK *****
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
using Res = ArmoryLib.Character.ResistancesDetail;
using M = ArmoryLib.Character.MeleeDetail;
using R = ArmoryLib.Character.RangedDetail;
using D = ArmoryLib.Character.DefenseDetail;
using S = ArmoryLib.Character.SpellDetail;

namespace ArmoryLib.Character
{
    // Contains Extensions Methods on Armory to load Characters.
    // Also deals with all XML parsing for a character.
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
                                            armory,
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

        private static XmlNode LoadDetailNode(Armory armory, CharacterDetail detail, string realmName, string characterName)
        {
            string searchString = string.Empty;
            switch (detail)
            {
                case CharacterDetail.Basic:
                case CharacterDetail.CharacterSheet:
                    // http://eu.wowarmory.com/character-sheet.xml?r=Sporeggar&n=Zoing
                    searchString = string.Format("character-sheet.xml?r={0}&n={1}",
                                                 HttpUtility.UrlEncode(realmName),
                                                 HttpUtility.UrlEncode(characterName));
                    break;
                case CharacterDetail.Reputation:
                    // http://eu.wowarmory.com/character-reputation.xml?r=Sporeggar&n=Zoing
                    searchString = string.Format("character-reputation.xml?r={0}&n={1}",
                                                 HttpUtility.UrlEncode(realmName),
                                                 HttpUtility.UrlEncode(characterName));
                    break;
                case CharacterDetail.Skills:
                    // http://eu.wowarmory.com/character-skills.xml?r=Sporeggar&n=Zoing
                    searchString = string.Format("character-skills.xml?r={0}&n={1}",
                                                 HttpUtility.UrlEncode(realmName),
                                                 HttpUtility.UrlEncode(characterName));
                    break;
                case CharacterDetail.Talents:
                    // http://eu.wowarmory.com/character-talents.xml?r=Sporeggar&n=Zoing
                    searchString = string.Format("character-talents.xml?r={0}&n={1}",
                                                 HttpUtility.UrlEncode(realmName),
                                                 HttpUtility.UrlEncode(characterName));
                    break;
            }

            if (searchString != string.Empty)
            {
                XmlDocument searchResults = armory.Request(searchString);
                XmlNode characterDetails = searchResults.SelectSingleNode("/page/characterInfo");
                return characterDetails;
            }
            else
            {
                return null;
            }
        }

        public static Character LoadCharacter(this Armory armory, string realmName, string characterName)
        {
            return LoadCharacter(armory, realmName, characterName, CharacterDetail.Basic);
        }

        public static Character LoadCharacter(this Armory armory, string realmName, string characterName, CharacterDetail loadDetail)
        {
            XmlNode characterDetails = LoadDetailNode(armory, CharacterDetail.Basic, realmName, characterName);

            if (characterDetails != null)
            {
                XmlNode characterNode = characterDetails.SelectSingleNode("character");

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
                                                    armory,
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
                    character.LoadDetail(CharacterDetail.CharacterSheet);
                }

                if (loadDetail.ContainsDetail(CharacterDetail.Reputation))
                {
                    character.LoadDetail(CharacterDetail.Reputation);
                }

                if (loadDetail.ContainsDetail(CharacterDetail.Skills))
                {
                    character.LoadDetail(CharacterDetail.Skills);
                }

                if (loadDetail.ContainsDetail(CharacterDetail.Talents))
                {
                    character.LoadDetail(CharacterDetail.Talents);
                }

                return character;
            }
            else
            {
                return null;
            }
        }

        public static void LoadDetail(this Character character, CharacterDetail extraDetail)
        {
            if (!character.DetailLoaded.ContainsDetail(extraDetail))
            {
                XmlNode searchResults;

                switch (extraDetail) {
                    case CharacterDetail.CharacterSheet:
                        searchResults = LoadDetailNode(character.UsedArmory, CharacterDetail.CharacterSheet, character.Realm, character.Name);

                        LoadTalentSpec(character, searchResults);
                        LoadPvpInfo(character, searchResults);
                        Stats stats = LoadStats(character, searchResults);
                        LoadResistances(stats, searchResults);
                        LoadMelee(character, stats, searchResults);
                        LoadRanged(character, stats, searchResults);
                        LoadDefense(stats, searchResults);
                        LoadBuffs(character, searchResults);
                        LoadSpell(character, stats, searchResults);
                        LoadProfessions(character, searchResults);
                        LoadBars(stats, searchResults);
                        LoadTitles(character, searchResults);
                        break;
                    case CharacterDetail.Reputation:
                        searchResults = LoadDetailNode(character.UsedArmory, CharacterDetail.Reputation, character.Realm, character.Name);

                        LoadReputation(character, searchResults);
                        break;
                    case CharacterDetail.Skills:
                        searchResults = LoadDetailNode(character.UsedArmory, CharacterDetail.Skills, character.Realm, character.Name);

                        LoadSkills(character, searchResults);
                        break;
                    case CharacterDetail.Talents:
                        searchResults = LoadDetailNode(character.UsedArmory, CharacterDetail.Talents, character.Realm, character.Name);

                        LoadTalents(character, searchResults);
                        break;
                }

                // Indicate we finished loading extra detail
                character.LoadedDetail(extraDetail);
            }
        }

        private static void LoadTalentSpec(Character character, XmlNode searchResults)
        {
            // <talentSpec treeOne="20" treeThree="0" treeTwo="41"/>
            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/talentSpec");

            TalentSpec spec = new TalentSpec(
                Convert.ToInt32(characterNode.Attributes["treeOne"].Value),
                Convert.ToInt32(characterNode.Attributes["treeTwo"].Value),
                Convert.ToInt32(characterNode.Attributes["treeThree"].Value));

            character.TalentSpec = spec;
        }

        private static void LoadPvpInfo(Character character, XmlNode searchResults)
        {
            //<pvp>
            //  <lifetimehonorablekills value="1469"/>
            //  <arenacurrency value="315"/>
            //</pvp>
            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/pvp");
            
            PvpInfo pvpInfo = new PvpInfo(
                Convert.ToInt32(characterNode.SelectSingleNode("lifetimehonorablekills").Attributes["value"].Value),
                Convert.ToInt32(characterNode.SelectSingleNode("arenacurrency").Attributes["value"].Value));

            character.PvpInfo = pvpInfo;
        }

        private static Stats LoadStats(Character character, XmlNode searchResults)
        {
            //<baseStats>
            //  <strength attack="82" base="92" block="-1" effective="92"/>
            //  <agility armor="642" attack="311" base="163" critHitPercent="7.73" effective="321"/>
            //  <stamina base="90" effective="377" health="3590" petBonus="-1"/>
            //  <intellect base="43" critHitPercent="-1.00" effective="43" mana="-1" petBonus="-1"/>
            //  <spirit base="57" effective="57" healthRegen="20" manaRegen="-1"/>
            //  <armor base="2181" effective="2181" percent="17.12" petBonus="-1"/>
            //</baseStats>
            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/baseStats");

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

        private static void LoadResistances(Stats stats, XmlNode searchResults)
        {
            //<resistances>
            //  <arcane petBonus="-1" value="5"/>
            //  <fire petBonus="-1" value="5"/>
            //  <frost petBonus="-1" value="5"/>
            //  <holy petBonus="-1" value="0"/>
            //  <nature petBonus="-1" value="5"/>
            //  <shadow petBonus="-1" value="5"/>
            //</resistances>
            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/resistances");

            XmlNode arcaneNode = characterNode.SelectSingleNode("arcane");
            Res.Arcane arcane = new Res.Arcane(
                Convert.ToInt32(arcaneNode.Attributes["value"].Value));

            XmlNode fireNode = characterNode.SelectSingleNode("fire");
            Res.Fire fire = new Res.Fire(
                Convert.ToInt32(fireNode.Attributes["value"].Value));

            XmlNode frostNode = characterNode.SelectSingleNode("frost");
            Res.Frost frost = new Res.Frost(
                Convert.ToInt32(frostNode.Attributes["value"].Value));

            XmlNode holyNode = characterNode.SelectSingleNode("holy");
            Res.Holy holy = new Res.Holy(
                Convert.ToInt32(holyNode.Attributes["value"].Value));

            XmlNode natureNode = characterNode.SelectSingleNode("nature");
            Res.Nature nature = new Res.Nature(
                Convert.ToInt32(natureNode.Attributes["value"].Value));

            XmlNode shadowNode = characterNode.SelectSingleNode("shadow");
            Res.Shadow shadow = new Res.Shadow(
                Convert.ToInt32(shadowNode.Attributes["value"].Value));

            Resistances resistances = new Resistances(arcane,
                                                      fire,
                                                      frost,
                                                      holy,
                                                      nature,
                                                      shadow);

            stats.Resistances = resistances;
        }

        private static void LoadMelee(Character character, Stats stats, XmlNode searchResults)
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
            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/melee");

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

        private static void LoadRanged(Character character, Stats stats, XmlNode searchResults)
        {
            //<ranged>
            //  <weaponSkill rating="0" value="350"/>
            //  <damage dps="295.7" max="823" min="668" percent="0" speed="2.52"/>
            //  <speed hastePercent="0.00" hasteRating="0" value="2.52"/>
            //  <power base="831" effective="1662" increasedDps="118.0" petAttack="365.64" petSpell="213.90"/>
            //  <hitRating increasedHitPercent="4.95" value="78"/>
            //  <critChance percent="30.55" plusPercent="13.14" rating="290"/>
            //</ranged>
            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/ranged");

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

        private static void LoadDefense(Stats stats, XmlNode searchResults)
        {
            //<defenses>
            //  <armor base="6540" effective="6540" percent="38.25" petBonus="2289"/>
            //  <defense decreasePercent="0.00" increasePercent="0.00" plusDefense="0" rating="0" value="350.00"/>
            //  <dodge increasePercent="0.00" percent="15.27" rating="0"/>
            //  <parry increasePercent="0.00" percent="5.00" rating="0"/>
            //  <block increasePercent="0.00" percent="0.00" rating="0"/>
            //  <resilience damagePercent="13.70" hitPercent="6.85" value="270.00"/>
            //</defenses>

            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/defenses");

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

        private static void LoadBuffs(Character character, XmlNode searchResults)
        {
            //<buffs>
            //  <spell effect="Increases attack power by 125." icon="ability_trueshot" name="Trueshot Aura"/>
            //  <spell effect="30% increased movement speed.  Dazed if struck." icon="ability_mount_jungletiger" name="Aspect of the Cheetah"/>
            //</buffs>

            XmlNode buffsNode = searchResults.SelectSingleNode("characterTab/buffs");
            List<BuffDebuff> buffs = new List<BuffDebuff>();
            XmlNodeList buffNodes = buffsNode.SelectNodes("spell");
            foreach (XmlNode buffNode in buffNodes)
            {
                BuffDebuff buff = new BuffDebuff(buffNode.Attributes["name"].Value,
                                                 buffNode.Attributes["effect"].Value);
                buffs.Add(buff);
            }

            XmlNode debuffsNode = searchResults.SelectSingleNode("characterTab/debuffs");
            List<BuffDebuff> debuffs = new List<BuffDebuff>();
            XmlNodeList debuffNodes = debuffsNode.SelectNodes("spell");
            foreach (XmlNode debuffNode in debuffNodes)
            {
                BuffDebuff debuff = new BuffDebuff(debuffNode.Attributes["name"].Value,
                                                   debuffNode.Attributes["effect"].Value);
                debuffs.Add(debuff);
            }

            Effects effects = new Effects(buffs, debuffs);
            character.Effects = effects;
        }

        private static void LoadSpell(Character character, Stats stats, XmlNode searchResults)
        {
            //<spell>
            //  <bonusDamage>
            //    <arcane value="1183"/>
            //    <fire value="1183"/>
            //    <frost value="1183"/>
            //    <holy value="1183"/>
            //    <nature value="1183"/>
            //    <shadow value="1183"/>
            //    <petBonus attack="674" damage="177" fromType="fire"/>
            //  </bonusDamage>
            //  <bonusHealing value="1053"/>
            //  <hitRating increasedHitPercent="14.51" value="183"/>
            //  <critChance rating="246">
            //    <arcane percent="21.03"/>
            //    <fire percent="21.03"/>
            //    <frost percent="21.03"/>
            //    <holy percent="21.03"/>
            //    <nature percent="21.03"/>
            //    <shadow percent="21.03"/>
            //  </critChance>
            //  <penetration value="0"/>
            //  <manaRegen casting="6.00" notCasting="141.00"/>
            //</spell>
            XmlNode characterNode = searchResults.SelectSingleNode("characterTab/spell");

            XmlNode arcaneDmgNode = characterNode.SelectSingleNode("bonusDamage/arcane");
            XmlNode arcaneCritNode = characterNode.SelectSingleNode("critChance/arcane");
            S.Arcane arcane = new S.Arcane(
                Convert.ToInt32(arcaneDmgNode.Attributes["value"].Value),
                Convert.ToDouble(arcaneCritNode.Attributes["percent"].Value, Util.NumberFormatter));

            XmlNode fireDmgNode = characterNode.SelectSingleNode("bonusDamage/fire");
            XmlNode fireCritNode = characterNode.SelectSingleNode("critChance/fire");
            S.Fire fire = new S.Fire(
                Convert.ToInt32(fireDmgNode.Attributes["value"].Value),
                Convert.ToDouble(fireCritNode.Attributes["percent"].Value, Util.NumberFormatter));

            XmlNode frostDmgNode = characterNode.SelectSingleNode("bonusDamage/frost");
            XmlNode frostCritNode = characterNode.SelectSingleNode("critChance/frost");
            S.Frost frost = new S.Frost(
                Convert.ToInt32(frostDmgNode.Attributes["value"].Value),
                Convert.ToDouble(frostCritNode.Attributes["percent"].Value, Util.NumberFormatter));

            XmlNode holyDmgNode = characterNode.SelectSingleNode("bonusDamage/holy");
            XmlNode holyCritNode = characterNode.SelectSingleNode("critChance/holy");
            S.Holy holy = new S.Holy(
                Convert.ToInt32(frostDmgNode.Attributes["value"].Value),
                Convert.ToDouble(frostCritNode.Attributes["percent"].Value, Util.NumberFormatter));

            XmlNode natureDmgNode = characterNode.SelectSingleNode("bonusDamage/nature");
            XmlNode natureCritNode = characterNode.SelectSingleNode("critChance/nature");
            S.Nature nature = new S.Nature(
                Convert.ToInt32(natureDmgNode.Attributes["value"].Value),
                Convert.ToDouble(natureCritNode.Attributes["percent"].Value, Util.NumberFormatter));

            XmlNode shadowDmgNode = characterNode.SelectSingleNode("bonusDamage/shadow");
            XmlNode shadowCritNode = characterNode.SelectSingleNode("critChance/shadow");
            S.Shadow shadow = new S.Shadow(
                Convert.ToInt32(shadowDmgNode.Attributes["value"].Value),
                Convert.ToDouble(shadowCritNode.Attributes["percent"].Value, Util.NumberFormatter));

            XmlNode manaRegenNode = characterNode.SelectSingleNode("manaRegen");
            S.ManaRegen manaRegen = new S.ManaRegen(
                Convert.ToDouble(manaRegenNode.Attributes["casting"].Value, Util.NumberFormatter),
                Convert.ToDouble(manaRegenNode.Attributes["notCasting"].Value, Util.NumberFormatter));

            XmlNode hitNode = characterNode.SelectSingleNode("hitRating");
            S.Hit hit = new S.Hit(
                Convert.ToInt32(hitNode.Attributes["value"].Value),
                Convert.ToDouble(hitNode.Attributes["increasedHitPercent"].Value, Util.NumberFormatter),
                character.Level);

            XmlNode bonusHealingNode = characterNode.SelectSingleNode("bonusHealing");
            int bonusHealing = Convert.ToInt32(bonusHealingNode.Attributes["value"].Value);

            XmlNode spellPenetrationNode = characterNode.SelectSingleNode("penetration");
            int spellPenetration = Convert.ToInt32(spellPenetrationNode.Attributes["value"].Value);
         
            XmlNode petBonusNode = characterNode.SelectSingleNode("bonusDamage/petBonus");

            School school;
            switch (petBonusNode.Attributes["fromType"].Value)
            {
                case "arcane":
                    school = School.Arcane;
                    break;
                case "fire":
                    school = School.Fire;
                    break;
                case "frost":
                    school = School.Frost;
                    break;
                case "holy":
                    school = School.Holy;
                    break;
                case "nature":
                    school = School.Nature;
                    break;
                case "shadow":
                    school = School.Shadow;
                    break;
                default:
                    school = School.None;
                    break;
            }

            S.PetBonus petBonus = new S.PetBonus(
                Convert.ToInt32(petBonusNode.Attributes["attack"].Value),
                Convert.ToInt32(petBonusNode.Attributes["damage"].Value),
                school);

            Spell spell = new Spell(arcane,
                                    fire,
                                    frost,
                                    holy,
                                    nature,
                                    shadow,
                                    manaRegen,
                                    hit,
                                    bonusHealing,
                                    spellPenetration,
                                    petBonus);

            stats.Spell = spell;
        }

        private static void LoadProfessions(Character character, XmlNode searchResults)
        {
            //<professions>
            //  <skill key="herbalism" max="375" name="Herbalism" value="375"/>
            //  <skill key="skinning" max="375" name="Skinning" value="375"/>
            //</professions>
            XmlNode professionsNode = searchResults.SelectSingleNode("characterTab/professions");

            List<Skill> professions = new List<Skill>();
            XmlNodeList professionNodes = professionsNode.SelectNodes("skill");
            foreach (XmlNode skillNode in professionNodes)
            {
                Skill profession = new Skill(skillNode.Attributes["name"].Value,
                                             Convert.ToInt32(skillNode.Attributes["value"].Value),
                                             SkillType.PrimaryProfession);
                professions.Add(profession);
            }

            character.Professions = professions;
        }

        private static void LoadBars(Stats stats, XmlNode searchResults)
        {
            //<characterBars>
            //  <health effective="7394"/>
            //  <secondBar casting="-1" effective="100" notCasting="-1" type="e"/>
            //  <secondBar casting="6" effective="8971" notCasting="141" type="m"/>
            //  <secondBar casting="-1" effective="100" notCasting="-1" perFive="-1" type="r"/>
            //</characterBars>
            XmlNode barsNode = searchResults.SelectSingleNode("characterTab/characterBars");

            XmlNode hpNode = barsNode.SelectSingleNode("health");
            int hp = Convert.ToInt32(hpNode.Attributes["effective"].Value);

            XmlNode secondaryNode = barsNode.SelectSingleNode("secondBar");

            SecondBar barType;
            switch (secondaryNode.Attributes["type"].Value)
            {
                case "r":
                    barType = SecondBar.Rage;
                    break;
                case "m":
                    barType = SecondBar.Mana;
                    break;
                case "e":
                    barType = SecondBar.Energy;
                    break;
                default:
                    barType = SecondBar.None;
                    break;
            }

            SecondaryBar secondBar = new SecondaryBar(
                barType,
                Convert.ToInt32(secondaryNode.Attributes["effective"].Value),
                Convert.ToInt32(secondaryNode.Attributes["casting"].Value),
                Convert.ToInt32(secondaryNode.Attributes["notCasting"].Value));

            stats.TotalHealth = hp;
            stats.SecondaryBar = secondBar;
        }

        private static void LoadTitles(Character character, XmlNode searchResults)
        {
            //<title value="Scarab Lord %s"/>
            //<knownTitles>
            //  <title value="Scarab Lord %s"/>
            //  <title value="%s, Champion of the Naaru"/>
            //  <title value="%s, Hand of A'dal"/>
            //</knownTitles>
            XmlNode knownTitlesNode = searchResults.SelectSingleNode("characterTab/knownTitles");

            XmlNode selectedTitleNode = searchResults.SelectSingleNode("characterTab/title");
            string selectedTitle = selectedTitleNode.Attributes["value"].Value;

            List<Title> titles = new List<Title>();
            XmlNodeList titlesNodes = knownTitlesNode.SelectNodes("title");
            foreach (XmlNode titleNodes in titlesNodes)
            {
                string titleText = titleNodes.Attributes["value"].Value;
                Title title = new Title(character.Name, titleText, (titleText == selectedTitle));
                titles.Add(title);
            }

            character.Titles = titles;
        }

        private static void LoadReputation(Character character, XmlNode searchResults)
        {
            //<reputationTab>
            //  <factionCategory key="horde" name="Horde">
            //    <faction key="darkspeartrolls" name="Darkspear Trolls" reputation="31963"/>
            //    <faction key="orgrimmar" name="Orgrimmar" reputation="42999"/>
            //    <faction key="silvermooncity" name="Silvermoon City" reputation="41910"/>
            //    <faction key="thunderbluff" name="Thunder Bluff" reputation="30581"/>
            //    <faction key="undercity" name="Undercity" reputation="34237"/>
            //  </factionCategory>
            XmlNodeList factionCategoriesNodes = searchResults.SelectNodes("reputationTab/factionCategory");

            List<Reputation> reputation = new List<Reputation>();
            foreach (XmlNode factionCategoryNode in factionCategoriesNodes)
            {
                XmlNodeList factionNodes = factionCategoryNode.SelectNodes("faction");

                foreach (XmlNode factionNode in factionNodes)
                {
                    Reputation reputationItem = new Reputation(factionNode.Attributes["name"].Value,
                                                               factionNode.Attributes["key"].Value,
                                                               Convert.ToInt32(factionNode.Attributes["reputation"].Value));
                    reputation.Add(reputationItem);
                }
            }

            character.Reputation = reputation;
        }

        private static void LoadSkills(Character character, XmlNode searchResults)
        {
            //<skillTab>
            //  <skillCategory key="professions" name="Professions">
            //    <skill key="herbalism" max="375" name="Herbalism" value="375"/>
            //    <skill key="skinning" max="375" name="Skinning" value="375"/>
            //  </skillCategory>
            XmlNodeList skillCategoriesNodes = searchResults.SelectNodes("skillTab/skillCategory");

            List<Skill> skills = new List<Skill>();
            foreach (XmlNode skillCategoryNode in skillCategoriesNodes)
            {
                XmlNodeList skillNodes = skillCategoryNode.SelectNodes("skill");

                SkillType type;
                switch (skillCategoryNode.Attributes["key"].Value)
                {
                    case "professions":
                        type = SkillType.PrimaryProfession;
                        break;
                    case "secondaryskills":
                        type = SkillType.SecondaryProfession;
                        break;
                    case "weaponskills":
                        type = SkillType.Weapon;
                        break;
                    case "classskills":
                        type = SkillType.Class;
                        break;
                    case "armorproficiencies":
                        type = SkillType.Armor;
                        break;
                    case "languages":
                        type = SkillType.Language;
                        break;
                    default:
                        type = SkillType.None;
                        break;
                }

                foreach (XmlNode skillNode in skillNodes)
                {
                    Skill skill = new Skill(skillNode.Attributes["name"].Value,
                                            Convert.ToInt32(skillNode.Attributes["value"].Value),
                                            type);
                    skills.Add(skill);
                }
            }

            character.Skills = skills;
        }

        private static void LoadTalents(Character character, XmlNode searchResults)
        {
            //<talentTab>
            //  <talentTree value="0053201054000000000003203050020050150123210510000000000000000000000"/>
            //</talentTab>
            XmlNode talentTreeNode = searchResults.SelectSingleNode("talentTab/talentTree");

            character.Talents = talentTreeNode.Attributes["value"].Value;
        }
    }
}
