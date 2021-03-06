# C# World Of Warcraft Armory Library
This library allows you to query the [World Of Warcraft Armory](http://eu.wowarmory.com/) from your projects.

## Supported Regions
 * USA
 * Oceanic
 * Europe
 * Korea
 * China
 * Taiwan
 
## Available Features

### Guild

 * Search
 * Roster
 
### Character

* Search
 * Basic Sheet (Except Items)

## Library API & Examples.

### API

ArmoryLib provides a very simply API, consisting out of the following 4 methods:

``` csharp
armory.SearchCharacter(characterName);
armory.LoadCharacter(realmName, characterName);
armory.SearchGuild(guildName);
armory.LoadGuild(realmName, guildName);
```

To use these, simply put a reference to ```ArmoryLib``` and create an instance of Armory.

``` csharp
Armory armory = new Armory
{
    Region = Region.Europe
};
```

The two main objects are Guild and Character.

By default, using ```LoadCharacter(realmName, characterName)``` will only load the Basic detail.

To load additional detail, use the ```LoadDetail(characterDetail)``` method on an existing Character object.

``` csharp
character.LoadDetail(CharacterDetail.Reputation);
```

Available detail levels are:
  * Basic
  * CharacterSheet
  * Reputation
  * Skills
  * Talents
  * Arena

To see all available properties for Guild and Character, have a look at the example [Program in the ArmoryTester project](https://github.com/CumpsD/armorylib/blob/master/Armory/ArmoryTester/Program.cs), below you can see examples of the output of this program.

### Examples

Supported Armory Functions are shows by using output examples.

#### Guild Search Output

The following information is available when searching for a guild:

```
Search results for The Dominion:
The Dominion - Alliance - Blackout - Agamaggan
The Dominion - Alliance - Blackout - Al'Akir
The Dominion - Alliance - Blackout - Aszune
The Dominion - Alliance - Blackout - Emerald Dream
The Dominion - Alliance - Nightfall - Darkmoon Faire
The Dominion - Alliance - Ruin - Steamwheedle Cartel
The Dominion - Alliance - Vindication - Hellfire
The Dominion - Horde - Blackout - Aggramar
The Dominion - Horde - Blackout - Azjol-Nerub
The Dominion - Horde - Blackout - Bloodscalp
The Dominion - Horde - Blackout - Doomhammer
The Dominion - Horde - Blackout - Draenor
The Dominion - Horde - Conviction - Nordrassil
The Dominion - Horde - Conviction - Quel'Thalas
the dominion - Horde - Misery - Eonar
the dominion - Horde - Nightfall - Alonsus
The Dominion - Horde - Rampage - Lightning's Blade
The Dominion - Horde - Todbringer - Blackhand
The Dominion - Horde - Vindication - Sporeggar
```

#### Guild Output

The following output is available when looking at a guild:

```
Details for The Dominion - Sporeggar:
The Dominion - Horde - Vindication - Sporeggar - 31 members.
  - Zoing, Female Level 70 Rogue
  - Deake, Male Level 70 Rogue
  - Dinli, Female Level 70 Hunter
  - Mask, Female Level 70 Rogue
  - Lelina, Female Level 70 Rogue
  - Hellstrike, Male Level 70 Warlock
  - Lormath, Male Level 70 Priest
  - Celinda, Female Level 70 Rogue
  - Nefa, Male Level 70 Paladin
  - Raquela, Male Level 70 Paladin
  - Magoth, Male Level 69 Hunter
  - Latherini, Female Level 67 Paladin
  - Lantash, Male Level 62 Druid
  - Gorehooves, Male Level 61 Shaman
  - Rhyl, Male Level 60 Rogue
  - Azrel, Male Level 59 Hunter
  - Aprilia, Male Level 53 Rogue
  - Zerya, Female Level 49 Warlock
  - Licia, Female Level 36 Priest
  - Mixace, Male Level 34 Warrior
  - Toneros, Male Level 28 Warlock
  - Cyria, Female Level 28 Mage
  - Tza, Male Level 27 Warrior
  - Maltrius, Male Level 25 Priest
  - Julion, Male Level 24 Mage
  - Forsy, Male Level 24 Hunter
  - Lopotomi, Male Level 20 Rogue
  - Durav, Male Level 18 Warrior
  - Nurnunna, Female Level 13 Hunter
  - Umpha, Male Level 11 Druid
  - Nakhti, Male Level 10 Shaman
```

#### Character Search Output

The following output is available when searching for a character:

```
Search results for Zoing:
Zoing - Alliance - Female - Level 38 - Draenei
        Shaman - Blackout - Arathor - (No Guild)
Zoing - Alliance - Male - Level 70 - Human
        Mage - Blackout - Emerald Dream - Stormwatch
Zoing - Alliance - Male - Level 70 - Human
        Mage - Blutdurst - Mal'Ganis - S P Q R
Zoing - Alliance - Female - Level 31 - Draenei
        Shaman - Conviction - Laughing Skull - (No Guild)
Zoing - Alliance - Female - Level 25 - Gnome
        Warlock - Hinterhalt - Teldrassil - (No Guild)
Zoing - Alliance - Male - Level 28 - Human
        Warrior - Schattenbrand - Ambossar - Allianz Fighter
Zoing - Horde - Male - Level 70 - Troll
        Shaman - Blackout - Bladefist - Wolves of North Watch
Zoing - Horde - Male - Level 36 - Undead
        Rogue - Blackout - Draenor - (No Guild)
Zoing - Horde - Male - Level 10 - Undead
        Warrior - Bloodlust - Dragonmaw - (No Guild)
Zoing - Horde - Female - Level 13 - BloodElf
        Hunter - Blutdurst - Frostmourne - Blutpakt
Zoing - Horde - Male - Level 70 - Undead
        Warlock - Blutdurst - Kil'jaeden - (No Guild)
Zoing - Horde - Male - Level 48 - BloodElf
        Paladin - Rampage - Ragnaros - (No Guild)
Zoing - Horde - Female - Level 17 - Orc
        Warrior - Reckoning - Trollbane - Rockknuckles
Zoing - Horde - Female - Level 35 - Troll
        Hunter - Sturmangriff - Echsenkessel - Devils Rejects
Zoing - Horde - Male - Level 12 - Undead
        Mage - Todbringer - Baelgun - (No Guild)
Zoing - Horde - Female - Level 70 - BloodElf
        Rogue - Vindication - Sporeggar - The Dominion
```

#### Character Output

Currently the following values are fetched for the Character:

##### Rogue
```
Details for Zoing - Sporeggar:
Zoing
  Horde - Female - Level 70 - BloodElf
  Rogue - Vindication - Sporeggar - The Dominion
  Spec: 20/41/0

  PvP: HKs: 1824, Arena Points: 315

  Strength: Base: 92, Effective: 92, +AP: 82
  Agility: Base: 163, Effective: 320, +AP: 310, +Crit%: 7,71%, +Armor: 640
  Stamina: Base: 90, Effective: 369, +Health: 3510
  Intellect: Base: 43, Effective: 43
  Spirit: Base: 57, Effective: 57
          Health Regen Out Of Combat: 20
  Armor: Base: 2249, Effective: 2249
         Reduces Physical Damage Taken By 17,56%

  Resistances: Arcane: 5, Fire: 5, Frost: 5, Holy: 0, Nature: 5, Shadow: 5

  Melee:
    MainHand: DPS: 185,4, Min: 431, Max: 534, Speed: 2,6
    OffHand: DPS: 185,4, Min: 431, Max: 534, Speed: 2,6
    AttackPower: Base: 532, Effective: 1228, +DPS: 87
    Hit: Rating: 136, Change To Hit Against Level 70: 8,62%
    Crit: Rating: 161 (+7,29%), Crit%: 20%
    Expertise: -Change to Dodge/Parry: 2,5%

  Ranged:
    Ranged: Skill: 323, DPS: 128,6, Min: 246, Max: 246, Speed: 1,7
    AttackPower: Base: 380, Effective: 1076, +DPS: 76
    Hit: Rating: 136, Change To Hit Against Level 70: 8,62%
    Crit: Rating: 161 (+7,29%), Crit%: 18,92%

  Defense:
    Defense: Rating: 350
    Block: Rating: 0 (+0%), Block%: 0%
    Dodge: Rating: 36 (+1,9%), Dodge%: 17,31%
    Parry: Rating: 0 (+0%), Parry%: 5%
    Resilience: Rating: 24
    Armor: Base: 2249, Effective: 2249
           Reduces Physical Damage Taken By 17,56%

  Spell:
    Arcane: +0 dmg, 0% crit
    Fire: +0 dmg, 0% crit
    Frost: +0 dmg, 0% crit
    Holy: +0 dmg, 0% crit
    Nature: +0 dmg, 0% crit
    Shadow: +0 dmg, 0% crit
    Mana Regen: 0 (Casting), 0 (Not Casting)
    Rating: 0, Change To Hit Against Level 70: 0%
    Bonus Healing: 0
    Spell Penetration: 0

  Buffs:
    Honorless Target - You are currently worth no honor points to the enemy.

  Debuffs:
    None

  Professions:
    Herbalism (375)
    Skinning (375)

  Total Health: 7214
  Total Energy: 100

  Title: None

  Reputation:
    Darkspear Trolls - Revered (31963)
    Orgrimmar - Exalted (42999)
    Silvermoon City - Revered (41910)
    Thunder Bluff - Revered (30581)
    Undercity - Revered (34237)
    Frostwolf Clan - Revered (31736)
    The Defilers - Neutral (1250)
    Warsong Outriders - Neutral (1300)
    Ashtongue Deathsworn - Neutral (275)
    Cenarion Expedition - Honored (20448)
    Sporeggar - Friendly (8105)
    The Consortium - Honored (13985)
    The Mag'har - Honored (18562)
    Thrallmar - Honored (12660)
    Lower City - Friendly (8758)
    Sha'tari Skyguard - Neutral (870)
    Shattered Sun Offensive - Neutral (745)
    The Aldor - Friendly (8285)
    The Scryers - Hated (-9463)
    The Sha'tar - Honored (9009)
    Booty Bay - Honored (15373)
    Everlook - Honored (12553)
    Gadgetzan - Honored (14461)
    Ratchet - Honored (12053)
    Argent Dawn - Friendly (7386)
    Bloodsail Buccaneers - Hated (-17000)
    Cenarion Circle - Neutral (620)
    Gelkis Clan Centaur - Friendly (3250)
    Magram Clan Centaur - Hostile (-3250)
    Ravenholdt - Friendly (4005)
    Syndicate - Hated (-13175)
    The Violet Eye - Neutral (655)
    Thorium Brotherhood - Neutral (0)
    Timbermaw Hold - Neutral (1550)
    Tranquillien - Exalted (42999)
    Zandalar Tribe - Neutral (707)

  Exalted With:
    Orgrimmar - Exalted (42999)
    Tranquillien - Exalted (42999)

  Skills:
    [PrimaryProfession] Herbalism (375)
    [PrimaryProfession] Skinning (375)
    [SecondaryProfession] Cooking (148)
    [SecondaryProfession] First Aid (375)
    [SecondaryProfession] Fishing (2)
    [SecondaryProfession] Riding (300)
    [Weapon] Bows (201)
    [Weapon] Crossbows (45)
    [Weapon] Daggers (347)
    [Weapon] Defense (350)
    [Weapon] Guns (1)
    [Weapon] Maces (288)
    [Weapon] Swords (350)
    [Weapon] Thrown (323)
    [Weapon] Unarmed (214)
    [Class] Assassination (350)
    [Class] Combat (350)
    [Class] Lockpicking (350)
    [Class] Poisons (350)
    [Class] Subtlety (350)
    [Armor] Cloth (1)
    [Armor] Leather (1)
    [Language] Language: Orcish (300)
    [Language] Language: Thalassian (300)

Talents: 0053201054000000000003203050020050150123210510000000000000000000000
Talents Url: http://www.wow-europe.com/en/info/basics/talents/rogue/talents.html?tal=0053201054000000000003203050020050150123210510000000000000000000000
```

##### Hunter
```
Details for Dinli - Sporeggar:
Dinli
  Horde - Female - Level 70 - Troll
  Hunter - Vindication - Sporeggar - The Dominion
  Spec: 11/41/9

  PvP: HKs: 11816, Arena Points: 1004

  Strength: Base: 65, Effective: 75, +AP: 65
  Agility: Base: 156, Effective: 518, +AP: 508, +Crit%: 11,42%, +Armor: 1036
  Stamina: Base: 109, Effective: 693, +Health: 6750, Pet Bonus Stamina: 208%
  Intellect: Base: 77, Effective: 238, +Mana: 3290, +Crit%: 6,58%
  Spirit: Base: 84, Effective: 94
          Health Regen Out Of Combat: 15
          Mana Regen Per 5 Seconds, 5 Seconds Rule: 67
  Armor: Base: 6540, Effective: 6540
         Reduces Physical Damage Taken By 38,25%
         Pet Bonus Armor: 2289

  Resistances: Arcane: 0, Fire: 0, Frost: 0, Holy: 0, Nature: 0, Shadow: 0

  Melee:
    MainHand: DPS: 243,6, Min: 648, Max: 814, Speed: 3
    OffHand: DPS: 243,6, Min: 648, Max: 814, Speed: 3
    AttackPower: Base: 713, Effective: 1468, +DPS: 104
    Hit: Rating: 78, Change To Hit Against Level 70: 4,95%
    Crit: Rating: 290 (+13,14%), Crit%: 24,55%
    Expertise: -Change to Dodge/Parry: 0%

  Ranged:
    Ranged: Skill: 350, DPS: 295,7, Min: 823, Max: 823, Speed: 2,52
    AttackPower: Base: 831, Effective: 1662, +DPS: 118
                 Pet Bonus AP: 365,64
                 Pet Bonus Spell: 213,9
    Hit: Rating: 78, Change To Hit Against Level 70: 4,95%
    Crit: Rating: 290 (+13,14%), Crit%: 30,55%

  Defense:
    Defense: Rating: 350
    Block: Rating: 0 (+0%), Block%: 0%
    Dodge: Rating: 0 (+0%), Dodge%: 15,27%
    Parry: Rating: 0 (+0%), Parry%: 5%
    Resilience: Rating: 270
    Armor: Base: 6540, Effective: 6540
           Reduces Physical Damage Taken By 38,25%
           Pet Bonus Armor: 2289

  Spell:
    Arcane: +0 dmg, 6,58% crit
    Fire: +0 dmg, 6,58% crit
    Frost: +0 dmg, 6,58% crit
    Holy: +0 dmg, 6,58% crit
    Nature: +0 dmg, 6,58% crit
    Shadow: +0 dmg, 6,58% crit
    Mana Regen: 10 (Casting), 77 (Not Casting)
    Rating: 0, Change To Hit Against Level 70: 0%
    Bonus Healing: 0
    Spell Penetration: 0

  Buffs:
    Trueshot Aura - Increases attack power by 125.
    Aspect of the Cheetah - 30% increased movement speed.  Dazed if struck.

  Debuffs:
    None

  Professions:
    Enchanting (266)
    Mining (375)

  Total Health: 10834
  Total Mana: 6673

  Title: None

  Reputation:
    Darkspear Trolls - Exalted (42999)
    Orgrimmar - Exalted (42999)
    Silvermoon City - Revered (26055)
    Thunder Bluff - Revered (32597)
    Undercity - Revered (27228)
    Frostwolf Clan - Exalted (42999)
    The Defilers - Honored (16930)
    Warsong Outriders - Honored (10125)
    Ashtongue Deathsworn - Neutral (525)
    Cenarion Expedition - Revered (32832)
    Netherwing - Neutral (0)
    Ogri'la - Honored (16865)
    Sporeggar - Friendly (6365)
    The Consortium - Honored (10076)
    The Mag'har - Honored (17066)
    Thrallmar - Honored (19680)
    Lower City - Honored (20409)
    Sha'tari Skyguard - Revered (23830)
    Shattered Sun Offensive - Neutral (1085)
    The Aldor - Hated (-25580)
    The Scryers - Revered (23255)
    The Sha'tar - Honored (11853)
    Booty Bay - Honored (15002)
    Everlook - Honored (11801)
    Gadgetzan - Honored (15221)
    Ratchet - Honored (16073)
    Argent Dawn - Honored (13764)
    Bloodsail Buccaneers - Hated (-17825)
    Cenarion Circle - Honored (10406)
    Darkmoon Faire - Neutral (75)
    Gelkis Clan Centaur - Friendly (4355)
    Hydraxian Waterlords - Neutral (5)
    Keepers of Time - Honored (18385)
    Magram Clan Centaur - Hated (-8775)
    Ravenholdt - Neutral (247)
    Shen'dralar - Neutral (0)
    Syndicate - Hated (-11325)
    The Scale of the Sands - Neutral (0)
    The Violet Eye - Honored (14618)
    Thorium Brotherhood - Neutral (1500)
    Timbermaw Hold - Unfriendly (-2434)

  Exalted With:
    Darkspear Trolls - Exalted (42999)
    Orgrimmar - Exalted (42999)
    Frostwolf Clan - Exalted (42999)

  Skills:
    [PrimaryProfession] Enchanting (266)
    [PrimaryProfession] Mining (375)
    [SecondaryProfession] Cooking (283)
    [SecondaryProfession] First Aid (375)
    [SecondaryProfession] Fishing (237)
    [SecondaryProfession] Riding (225)
    [Weapon] Axes (266)
    [Weapon] Bows (350)
    [Weapon] Crossbows (350)
    [Weapon] Daggers (237)
    [Weapon] Defense (350)
    [Weapon] Guns (350)
    [Weapon] Polearms (280)
    [Weapon] Staves (74)
    [Weapon] Swords (242)
    [Weapon] Thrown (1)
    [Weapon] Two-Handed Axes (350)
    [Weapon] Two-Handed Swords (1)
    [Weapon] Unarmed (29)
    [Class] Beast Mastery (350)
    [Class] Marksmanship (350)
    [Class] Survival (350)
    [Armor] Cloth (1)
    [Armor] Leather (1)
    [Armor] Mail (1)
    [Language] Language: Orcish (300)
    [Language] Language: Troll (300)

Talents: 0520120100000000000005500201215013243105102301030000000000000000
Talents Url: http://www.wow-europe.com/en/info/basics/talents/hunter/talents.html?tal=0520120100000000000005500201215013243105102301030000000000000000
```

##### Warlock
```
Details for Hellstrike - Sporeggar:
Hellstrike
  Horde - Male - Level 70 - BloodElf
  Warlock - Vindication - Sporeggar - The Dominion
  Spec: 12/44/5

  PvP: HKs: 558

  Strength: Base: 48, Effective: 63, +AP: 53
  Agility: Base: 60, Effective: 60, +Crit%: 4,43%, +Armor: 120
  Stamina: Base: 85, Effective: 551, +Health: 5330, Pet Bonus Stamina: 165%
  Intellect: Base: 137, Effective: 397, +Mana: 5675, +Crit%: 6,54%
             Pet Bonus Intellect: 119%
  Spirit: Base: 131, Effective: 150
          Health Regen Out Of Combat: 10
          Mana Regen Per 5 Seconds, 5 Seconds Rule: 139
  Armor: Base: 1017, Effective: 1995
         Reduces Physical Damage Taken By 15,89%
         Pet Bonus Armor: 698

  Resistances: Arcane: 40, Fire: 40, Frost: 40, Holy: 0, Nature: 40, Shadow: 63

  Melee:
    MainHand: DPS: 47,4, Min: 49, Max: 113, Speed: 1,7
    OffHand: DPS: 47,4, Min: 49, Max: 113, Speed: 1,7
    AttackPower: Base: 53, Effective: 53, +DPS: 3
    Hit: Rating: 0, Change To Hit Against Level 70: 0%
    Crit: Rating: 0 (+0%), Crit%: 6,67%
    Expertise: -Change to Dodge/Parry: 0%

  Ranged:
    Ranged: Skill: 349, DPS: 118,7, Min: 279, Max: 279, Speed: 1,8
    AttackPower: Base: 50, Effective: 50, +DPS: 3
    Hit: Rating: 0, Change To Hit Against Level 70: 0%
    Crit: Rating: 0 (+0%), Crit%: 9,39%

  Defense:
    Defense: Rating: 345
    Block: Rating: 0 (+0%), Block%: 0%
    Dodge: Rating: 0 (+0%), Dodge%: 4,24%
    Parry: Rating: 0 (+0%), Parry%: 0%
    Resilience: Rating: 0
    Armor: Base: 1017, Effective: 1995
           Reduces Physical Damage Taken By 15,89%
           Pet Bonus Armor: 698

  Spell:
    Arcane: +464 dmg, 17,07% crit
    Fire: +464 dmg, 17,07% crit
    Frost: +464 dmg, 17,07% crit
    Holy: +464 dmg, 17,07% crit
    Nature: +464 dmg, 17,07% crit
    Shadow: +464 dmg, 17,07% crit
    Mana Regen: 36 (Casting), 175 (Not Casting)
    Rating: 18, Change To Hit Against Level 70: 1,43%
    Bonus Healing: 369
    Spell Penetration: 0
    Pet Bonus: +264 AP, +70 Spell (Granted from Fire Bonus)

  Buffs:
    Demon Armor - Increases armor by 660, Shadow resistance by 18 and restores 18 health every 5 sec.
    Demonic Knowledge - Increases Master's spell damage by a percentage of the active demon's Stamina plus Intellect.
    Master Demonologist - Increases damage caused by 5% and resistance to all magic schools by 0.

  Debuffs:
    None

  Professions:
    Enchanting (351)
    Tailoring (360)

  Total Health: 9085
  Total Mana: 8539

  Title: None

  Reputation:
    Darkspear Trolls - Revered (22796)
    Orgrimmar - Revered (27146)
    Silvermoon City - Revered (21205)
    Thunder Bluff - Honored (17140)
    Undercity - Honored (16392)
    Frostwolf Clan - Neutral (253)
    The Defilers - Neutral (390)
    Warsong Outriders - Neutral (1190)
    Cenarion Expedition - Honored (11926)
    Sporeggar - Friendly (5180)
    The Consortium - Friendly (6530)
    The Mag'har - Honored (19762)
    Thrallmar - Honored (10403)
    Lower City - Friendly (4950)
    Sha'tari Skyguard - Neutral (0)
    Shattered Sun Offensive - Revered (30445)
    The Aldor - Hated (-42000)
    The Scryers - Exalted (42305)
    The Sha'tar - Friendly (8999)
    Booty Bay - Honored (13235)
    Everlook - Honored (10940)
    Gadgetzan - Honored (12415)
    Ratchet - Honored (12861)
    Argent Dawn - Neutral (2646)
    Bloodsail Buccaneers - Hated (-15650)
    Cenarion Circle - Neutral (0)
    Darkmoon Faire - Neutral (0)
    Gelkis Clan Centaur - Friendly (5295)
    Magram Clan Centaur - Hated (-14075)
    Ravenholdt - Neutral (15)
    Syndicate - Hated (-10075)
    The Violet Eye - Neutral (860)
    Thorium Brotherhood - Neutral (1500)
    Timbermaw Hold - Honored (9200)
    Tranquillien - Neutral (0)

  Exalted With:
    The Scryers - Exalted (42305)

  Skills:
    [PrimaryProfession] Enchanting (351)
    [PrimaryProfession] Tailoring (360)
    [SecondaryProfession] First Aid (375)
    [SecondaryProfession] Riding (225)
    [Weapon] Daggers (281)
    [Weapon] Defense (345)
    [Weapon] Staves (289)
    [Weapon] Swords (232)
    [Weapon] Unarmed (10)
    [Weapon] Wands (349)
    [Class] Affliction (350)
    [Class] Demonology (350)
    [Class] Destruction (350)
    [Armor] Cloth (1)
    [Language] Language: Orcish (300)
    [Language] Language: Thalassian (300)

Talents: 0502220010000000000000352030133050100531351500000000000000000000
Talents Url: http://www.wow-europe.com/en/info/basics/talents/warlock/talents.html?tal=0502220010000000000000352030133050100531351500000000000000000000
```

##### Warrior
```
Details for Vohbo - Aggramar:
Vohbo
  Horde - Male - Level 70 - Tauren
  Warrior - Blackout - Aggramar - The Dominion
  Spec: 12/5/44

  PvP: HKs: 4

  Strength: Base: 165, Effective: 215, +AP: 410, +Block: 10
  Agility: Base: 91, Effective: 192, +Crit%: 6,96%, +Armor: 384
  Stamina: Base: 141, Effective: 1326, +Health: 13080
  Intellect: Base: 28, Effective: 36
  Spirit: Base: 53, Effective: 61
          Health Regen Out Of Combat: 36
  Armor: Base: 19480, Effective: 19840
         Reduces Physical Damage Taken By 65,27%

  Resistances: Arcane: 0, Fire: 0, Frost: 0, Holy: 0, Nature: 10, Shadow: 0

  Melee:
    MainHand: DPS: 153,1, Min: 177, Max: 275, Speed: 1,48
    OffHand: DPS: 153,1, Min: 177, Max: 275, Speed: 1,48
    AttackPower: Base: 620, Effective: 620, +DPS: 44
    Hit: Rating: 82, Change To Hit Against Level 70: 5,2%
    Crit: Rating: 0 (+0%), Crit%: 11,96%
    Expertise: -Change to Dodge/Parry: 6%

  Ranged:
    Ranged: Skill: 350, DPS: 131,8, Min: 294, Max: 294, Speed: 1,87
    AttackPower: Base: 252, Effective: 252, +DPS: 18
    Hit: Rating: 82, Change To Hit Against Level 70: 5,2%
    Crit: Rating: 0 (+0%), Crit%: 6,96%

  Defense:
    Defense: Rating: 370
    Block: Rating: 29 (+3,68%), Block%: 21,2%
    Dodge: Rating: 294 (+15,54%), Dodge%: 30,21%
    Parry: Rating: 63 (+2,66%), Parry%: 20,18%
    Resilience: Rating: 0
    Armor: Base: 19480, Effective: 19840
           Reduces Physical Damage Taken By 65,27%

  Spell:
    Arcane: +0 dmg, 0% crit
    Fire: +0 dmg, 0% crit
    Frost: +0 dmg, 0% crit
    Holy: +0 dmg, 0% crit
    Nature: +0 dmg, 0% crit
    Shadow: +0 dmg, 0% crit
    Mana Regen: 0 (Casting), 0 (Not Casting)
    Rating: 0, Change To Hit Against Level 70: 0%
    Bonus Healing: 0
    Spell Penetration: 0

  Buffs:
    None
  Debuffs:
    None

  Professions:
    Enchanting (375)
    Jewelcrafting (375)

  Total Health: 18558
  Total Rage: 100

  Title: Scarab Lord Vohbo

  Available Titles:
    Scarab Lord Vohbo
    Vohbo, Champion of the Naaru
    Vohbo, Hand of A'dal

  Reputation:
    Darkspear Trolls - Revered (38696)
    Orgrimmar - Exalted (42999)
    Silvermoon City - Honored (16741)
    Thunder Bluff - Revered (38830)
    Undercity - Revered (41581)
    Frostwolf Clan - Neutral (0)
    The Defilers - Neutral (0)
    Warsong Outriders - Neutral (0)
    Ashtongue Deathsworn - Exalted (42999)
    Cenarion Expedition - Exalted (42999)
    Netherwing - Exalted (42999)
    Ogri'la - Revered (23225)
    Sporeggar - Friendly (7157)
    The Consortium - Exalted (42762)
    The Mag'har - Revered (23748)
    Thrallmar - Exalted (42999)
    Lower City - Exalted (42999)
    Sha'tari Skyguard - Honored (18592)
    Shattered Sun Offensive - Honored (16351)
    The Aldor - Hated (-42000)
    The Scryers - Exalted (42005)
    The Sha'tar - Exalted (42999)
    Booty Bay - Honored (12918)
    Everlook - Honored (11771)
    Gadgetzan - Honored (12893)
    Ratchet - Honored (14493)
    Argent Dawn - Exalted (42999)
    Bloodsail Buccaneers - Hated (-23900)
    Brood of Nozdormu - Exalted (42999)
    Cenarion Circle - Exalted (42999)
    Darkmoon Faire - Neutral (350)
    Gelkis Clan Centaur - Friendly (3750)
    Hydraxian Waterlords - Exalted (42999)
    Keepers of Time - Exalted (42999)
    Magram Clan Centaur - Hated (-6750)
    Ravenholdt - Neutral (0)
    Shen'dralar - Neutral (1200)
    The Scale of the Sands - Exalted (42999)
    The Violet Eye - Exalted (42999)
    Thorium Brotherhood - Neutral (110)
    Timbermaw Hold - Revered (35174)
    Tranquillien - Exalted (42999)
    Zandalar Tribe - Exalted (42999)

  Exalted With:
    Orgrimmar - Exalted (42999)
    Ashtongue Deathsworn - Exalted (42999)
    Cenarion Expedition - Exalted (42999)
    Netherwing - Exalted (42999)
    The Consortium - Exalted (42762)
    Thrallmar - Exalted (42999)
    Lower City - Exalted (42999)
    The Scryers - Exalted (42005)
    The Sha'tar - Exalted (42999)
    Argent Dawn - Exalted (42999)
    Brood of Nozdormu - Exalted (42999)
    Cenarion Circle - Exalted (42999)
    Hydraxian Waterlords - Exalted (42999)
    Keepers of Time - Exalted (42999)
    The Scale of the Sands - Exalted (42999)
    The Violet Eye - Exalted (42999)
    Tranquillien - Exalted (42999)
    Zandalar Tribe - Exalted (42999)

  Skills:
    [PrimaryProfession] Enchanting (375)
    [PrimaryProfession] Jewelcrafting (375)
    [SecondaryProfession] Cooking (350)
    [SecondaryProfession] First Aid (375)
    [SecondaryProfession] Fishing (375)
    [SecondaryProfession] Riding (300)
    [Weapon] Axes (350)
    [Weapon] Bows (349)
    [Weapon] Crossbows (304)
    [Weapon] Daggers (350)
    [Weapon] Defense (370)
    [Weapon] Guns (350)
    [Weapon] Maces (350)
    [Weapon] Polearms (1)
    [Weapon] Swords (350)
    [Weapon] Two-Handed Axes (296)
    [Weapon] Two-Handed Maces (249)
    [Weapon] Two-Handed Swords (300)
    [Weapon] Unarmed (345)
    [Class] Arms (350)
    [Class] Fury (350)
    [Class] Protection (350)
    [Armor] Cloth (1)
    [Armor] Leather (1)
    [Armor] Mail (1)
    [Armor] Plate Mail (1)
    [Armor] Shield (1)
    [Language] Language: Orcish (300)
    [Language] Language: Taurahe (300)

Talents: 350003010000000000000000500000000000000000000055511033020103501351
Talents Url: http://www.wow-europe.com/en/info/basics/talents/warrior/talents.html?tal=350003010000000000000000500000000000000000000055511033020103501351
```
