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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib
{
    public enum Race
    {
        Human = 1,
        Orc = 2,
        Dwarf = 3,
        NightElf = 4,
        Undead = 5,
        Tauren = 6,
        Gnome = 7,
        Troll = 8,
        BloodElf = 10,
        Draenei = 11,
    }

    public static class RaceExtensions
    {
        public static string Humanize(this Race race)
        {
            switch (race)
            {
                case Race.Human: return "Human";
                case Race.Orc: return "Orc";
                case Race.Dwarf: return "Dwarf";
                case Race.NightElf: return "Night Elf";
                case Race.Undead: return "Undead";
                case Race.Tauren: return "Tauren";
                case Race.Gnome: return "Gnome";
                case Race.Troll: return "Troll";
                case Race.BloodElf: return "Blood Elf";
                case Race.Draenei: return "Draenei";
                default: return string.Empty;
            }
        }
    }
}
