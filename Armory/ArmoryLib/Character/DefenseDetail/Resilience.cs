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

namespace ArmoryLib.Character.DefenseDetail
{
    // TODO: Figure out properties

    // <resilience damagePercent="1.22" hitPercent="0.61" value="24.00"/>   (Rogue)
    // <resilience damagePercent="13.70" hitPercent="6.85" value="270.00"/> (Hunter)
    public class Resilience
    {
        public double Rating { get; private set; }

        internal Resilience(double rating)
        {
            Rating = rating;
        }

        public override string ToString()
        {
            return string.Format("Rating: {0}",
                                 Rating);
        }
    }
}
