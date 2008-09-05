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
    // <hitRating increasedHitPercent="6.72" value="106"/> (Rogue)
    // <hitRating increasedHitPercent="4.95" value="78"/> (Hunter)
    public class Hit
    {
        public int Rating { get; private set; }
        public double Percent { get; private set; }

        private int Level { get; set; }

        internal Hit(int rating, double percent, int level)
        {
            Rating = rating;
            Percent = percent;
            Level = level;
        }

        public override string ToString()
        {
            return string.Format("Rating: {0}, Change To Hit Against Level {1}: {2}%",
                                 Rating,
                                 Level,
                                 Percent);
        }
    }
}
