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

namespace ArmoryLib.Character.MeleeDetail
{
    // TODO: Figure out the meaning of these numbers

    // Reduces change to be dodged or parried by <percent>%
    // Expertise rating: <value>
    // Additional? Rating?
    // <expertise additional="0" percent="2.50" rating="0" value="10"/>
    public class Expertise
    {
        public double Percent { get; private set; }

        internal Expertise(double percent)
        {
            Percent = percent;
        }

        public override string ToString()
        {
            return string.Format("-Change to Dodge/Parry: {0}%",
                                 Percent);
        }
    }
}
