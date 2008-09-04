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

namespace ArmoryLib.Character
{
    // TODO: PetBonus, check on Dinli for hunter XML

    // <armor base="2171" effective="2171" percent="17.06" petBonus="-1"/>
    public class Armor
    {
        public double ReducePhysicalDamagePercent { get; private set; }
        public int BaseArmor { get; private set; }
        public int EffectiveArmor { get; private set; }

        internal Armor(double reduce, int baseStat, int effectiveStat)
        {
            ReducePhysicalDamagePercent = reduce;
            BaseArmor = baseStat;
            EffectiveArmor = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, Reduces Physical Damage Taken By {2}%",
                BaseArmor,
                EffectiveArmor,
                ReducePhysicalDamagePercent);
        }
    }
}
