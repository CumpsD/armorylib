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

using ArmoryLib.Character.Resistance;

namespace ArmoryLib.Character
{
    public class Resistances
    {
        public Arcane Arcane { get; private set; }
        public Fire Fire { get; private set; }
        public Frost Frost { get; private set; }
        public Holy Holy { get; private set; }
        public Nature Nature { get; private set; }
        public Shadow Shadow { get; private set; }

        internal Resistances(Arcane arcane,
                             Fire fire,
                             Frost frost,
                             Holy holy,
                             Nature nature,
                             Shadow shadow)
        {
            Arcane = arcane;
            Fire = fire;
            Frost = frost;
            Holy = holy;
            Nature = nature;
            Shadow = shadow;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                                 Arcane,
                                 Fire,
                                 Frost,
                                 Holy,
                                 Nature,
                                 Shadow);
        }
    }
}
