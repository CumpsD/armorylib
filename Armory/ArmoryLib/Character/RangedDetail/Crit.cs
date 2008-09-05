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
    // <critChance percent="18.26" plusPercent="7.29" rating="161"/>  (Rogue)
    // <critChance percent="30.55" plusPercent="13.14" rating="290"/> (Hunter)
    public class Crit
    {
        public int Rating { get; private set; }
        public double AddedCritPercent { get; private set; }
        public double Percent { get; private set; }

        internal Crit(int rating, double percent, double plusPercent)
        {
            Rating = rating;
            Percent = percent;
            AddedCritPercent = plusPercent;
        }

        public override string ToString()
        {
            return string.Format("Rating: {0} (+{1}%), Crit%: {2}%",
                                 Rating,
                                 AddedCritPercent,
                                 Percent);
        }
    }
}
