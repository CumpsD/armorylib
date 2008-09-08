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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArmoryLib.Character.StatsDetail;

namespace ArmoryLib.Character
{
    public class Stats
    {
        public Strength Strength { get; private set; }
        public Agility Agility { get; private set; }
        public Stamina Stamina { get; private set; }
        public Intellect Intellect { get; private set; }
        public Spirit Spirit { get; private set; }
        public Armor Armor { get; private set; }

        public Resistances Resistances { get; internal set; }
        public Melee Melee { get; internal set; }
        public Ranged Ranged { get; internal set; }
        public Defenses Defense { get; internal set; }
        public Spell Spell { get; internal set; }

        internal Stats(Strength strength,
                       Agility agility,
                       Stamina stamina,
                       Intellect intellect,
                       Spirit spirit,
                       Armor armor)
        {
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            Intellect = intellect;
            Spirit = spirit;
            Armor = armor;
        }
    }
}
