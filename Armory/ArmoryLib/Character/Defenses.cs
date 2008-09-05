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
using D = ArmoryLib.Character.DefenseDetail;

namespace ArmoryLib.Character
{
    public class Defenses
    {
        public D.Defense Defense { get; private set; }
        public D.Block Block { get; private set; }
        public D.Dodge Dodge { get; private set; }
        public D.Parry Parry { get; private set; }
        public D.Resilience Resilience { get; private set; }

        public Armor Armor { get; internal set; }

        internal Defenses(D.Defense defense,
                        D.Block block,
                        D.Dodge dodge,
                        D.Parry parry,
                        D.Resilience resilience)
        {
            Defense = defense;
            Block = block;
            Dodge = dodge;
            Parry = parry;
            Resilience = resilience;
        }

        public override string ToString()
        {
            return string.Format("Defense: {1}{0}" +
                                 "Block: {2}{0}" +
                                 "Dodge: {3}{0}" +
                                 "Parry: {4}{0}" +
                                 "Resilience: {5}{0}" +
                                 "Armor: {6}{0}",
                                 Environment.NewLine,
                                 Defense,
                                 Block,
                                 Dodge,
                                 Parry,
                                 Resilience,
                                 Armor);
        }
    }
}
