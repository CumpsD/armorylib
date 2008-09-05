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

namespace ArmoryLib.Character.Stat
{
    // <strength attack="82" base="92" block="-1" effective="92"/>
    public class Strength
    {
        public int AddedAttackPower { get; private set; }
        public int AddedBlock { get; private set; }
        public int BaseStrength { get; private set; }
        public int EffectiveStrength { get; private set; }

        internal Strength(int ap, int baseStat, int block, int effectiveStat)
        {
            AddedAttackPower = ap;
            AddedBlock = block;
            BaseStrength = baseStat;
            EffectiveStrength = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, +AP: {2}{3}",
                BaseStrength,
                EffectiveStrength,
                AddedAttackPower,
                (AddedBlock != -1) ? string.Format(", +Block: {0}", AddedBlock) : "");
        }
    }
}
