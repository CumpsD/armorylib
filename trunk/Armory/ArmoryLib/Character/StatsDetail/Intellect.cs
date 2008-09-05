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

namespace ArmoryLib.Character.StatsDetail
{
    // TODO: PetBonus, check on Dinli for hunter XML

    // <intellect base="43" critHitPercent="-1.00" effective="43" mana="-1" petBonus="-1"/>
    public class Intellect
    {
        public int AddedMana { get; private set; }
        public double AddedCritPercent { get; private set; }
        public int BaseIntellect { get; private set; }
        public int EffectiveIntellect { get; private set; }

        internal Intellect(int mana, int baseStat, double critPercent, int effectiveStat)
        {
            AddedMana = mana;
            AddedCritPercent = critPercent;
            BaseIntellect = baseStat;
            EffectiveIntellect = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}{2}{3}",
                BaseIntellect,
                EffectiveIntellect,
                (AddedMana != -1) ? string.Format(", +Mana: {0}", AddedMana) : "",
                (AddedCritPercent != -1) ? string.Format(", +Crit%: {0}%", AddedCritPercent) : "");
        }
    }
}
