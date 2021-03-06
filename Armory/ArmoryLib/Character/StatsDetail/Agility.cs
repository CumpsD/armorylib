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

namespace ArmoryLib.Character.StatsDetail
{
    // <agility armor="642" attack="311" base="163" critHitPercent="7.73" effective="321"/>
    public class Agility
    {
        public int AddedArmor { get; private set; }
        public int AddedAttackPower { get; private set; }
        public double AddedCritPercent { get; private set; }
        public int BaseAgility { get; private set; }
        public int EffectiveAgility { get; private set; }

        public int AddedAgility
        {
            get { return EffectiveAgility - BaseAgility; }
        }

        internal Agility(int armor, int ap, int baseStat, double critPercent, int effectiveStat)
        {
            AddedArmor = armor;
            AddedAttackPower = ap;
            AddedCritPercent = critPercent;
            BaseAgility = baseStat;
            EffectiveAgility = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}{2}, +Crit%: {3}%, +Armor: {4}",
                                 BaseAgility,
                                 EffectiveAgility,
                                 (AddedAttackPower != -1) ? string.Format(", +AP: {0}", AddedAttackPower) : "",
                                 AddedCritPercent,
                                 AddedArmor);
        }
    }
}
