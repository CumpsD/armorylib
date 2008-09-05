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

namespace ArmoryLib.Character.RangedDetail
{
    // TODO: What is percent doing here?
    // TODO: Add some properties for formules with the speed
    // TODO: Add properties for haste
    // TODO: Find out what rating means?

    // (Rogue)
    // <weaponSkill rating="0" value="309"/>
    // <damage dps="127.9" max="245" min="190" percent="0" speed="1.70"/>
    // <speed hastePercent="0.00" hasteRating="0" value="1.70"/>

    // (Hunter)
    // <weaponSkill rating="0" value="350"/>
    // <damage dps="295.7" max="823" min="668" percent="0" speed="2.52"/>
    // <speed hastePercent="0.00" hasteRating="0" value="2.52"/>
    public class RangedSlot
    {
        public int WeaponSkill { get; private set; }
        public double DPS { get; private set; }
        public int MinDamage { get; private set; }
        public int MaxDamage { get; private set; }
        public double WeaponSpeed { get; private set; }

        internal RangedSlot(int skill, double dps, int min, int max, double weaponSpeed)
        {
            WeaponSkill = skill;
            DPS = dps;
            MinDamage = min;
            MaxDamage = max;
            WeaponSpeed = weaponSpeed;
        }

        public override string ToString()
        {
            return string.Format("Skill: {0}, DPS: {1}, Min: {3}, Max: {3}, Speed: {4}",
                WeaponSkill,
                DPS,
                MinDamage,
                MaxDamage,
                WeaponSpeed);
        }
    }
}
