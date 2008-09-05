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

using ArmoryLib.Character.RangedDetail;

namespace ArmoryLib.Character
{
    public class Ranged
    {
        public RangedSlot RangedSlot { get; private set; }
        public AttackPower AttackPower { get; private set; }
        public Hit Hit { get; private set; }
        public Crit Crit { get; private set; }

        internal Ranged(RangedSlot rangedSlot,
                        AttackPower attackPower,
                        Hit hit,
                        Crit crit)
        {
            RangedSlot = rangedSlot;
            AttackPower = attackPower;
            Hit = hit;
            Crit = crit;
        }

        public override string ToString()
        {
            return string.Format("Ranged: {1}{0}" +
                                 "AttackPower: {2}{0}" +
                                 "Hit: {3}{0}" +
                                 "Crit: {4}{0}",
                                 Environment.NewLine,
                                 RangedSlot,
                                 AttackPower,
                                 Hit,
                                 Crit);
        }
    }
}
