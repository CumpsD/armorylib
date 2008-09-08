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

namespace ArmoryLib.Character.SpellDetail
{
    // <manaRegen casting="6.00" notCasting="141.00"/>
    public class ManaRegen
    {
        public double Casting { get; private set; }
        public double NotCasting { get; private set; }

        internal ManaRegen(double casting, double notcasting)
        {
            Casting = casting;
            NotCasting = notcasting;
        }

        public override string ToString()
        {
            return string.Format("Mana Regen: {0} (Casting), {1} (Not Casting)",
                                 Casting,
                                 NotCasting);
        }
    }
}
