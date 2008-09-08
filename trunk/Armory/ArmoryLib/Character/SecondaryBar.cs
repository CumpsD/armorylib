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

namespace ArmoryLib.Character
{
    // <secondBar casting="-1" effective="100" notCasting="-1" type="e"/>
    // <secondBar casting="6" effective="8971" notCasting="141" type="m"/>
    // <secondBar casting="-1" effective="100" notCasting="-1" perFive="-1" type="r"/>
    public class SecondaryBar
    {
        public SecondBar Type { get; private set; }
        public int Effective { get; private set; }
        public int Casting { get; private set; }
        public int NotCasting { get; private set; }

        internal SecondaryBar(SecondBar type,
                              int effective,
                              int casting,
                              int notCasting)
        {
            Type = type;
            Effective = effective;
            Casting = casting;
            NotCasting = notCasting;
        }
    }
}
