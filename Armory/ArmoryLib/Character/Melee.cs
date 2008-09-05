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

using ArmoryLib.Character.MeleeDetail;

namespace ArmoryLib.Character
{
    public class Melee
    {
        public MainHand MainHand { get; private set; }
        public OffHand OffHand { get; private set; }
        public AttackPower AttackPower { get; private set; }
        public Hit Hit { get; private set; }
        public Crit Crit { get; private set; }
        public Expertise Expertise { get; private set; }

        internal Melee(MainHand mainHand,
                       OffHand offHand,
                       AttackPower attackPower,
                       Hit hit,
                       Crit crit,
                       Expertise expertise)
        {
            MainHand = mainHand;
            OffHand = offHand;
            AttackPower = attackPower;
            Hit = hit;
            Crit = crit;
            Expertise = expertise;
        }

        public override string ToString()
        {
            return string.Format("MainHand: {1}{0}" +
                                 "OffHand: {2}{0}" +
                                 "AttackPower: {3}{0}" +
                                 "Hit: {4}{0}" +
                                 "Crit: {5}{0}" +
                                 "Expertise: {6}{0}",
                                 Environment.NewLine,
                                 MainHand,
                                 OffHand,
                                 AttackPower,
                                 Hit,
                                 Crit,
                                 Expertise);
        }
    }
}
