﻿/// <summary>** BEGIN LICENSE BLOCK *****
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
    //<pvp>
    //  <lifetimehonorablekills value="1469"/>
    //  <arenacurrency value="315"/>
    //</pvp>
    public class PvpInfo
    {
        public int LifeTimeKills { get; private set; }
        public int ArenaPoints { get; private set; }

        internal PvpInfo(int lifeTimeKills, int arenaPoints)
        {
            LifeTimeKills = lifeTimeKills;
            ArenaPoints = arenaPoints;
        }

        public override string ToString()
        {
            return string.Format("HKs: {0}{1}", 
                                 LifeTimeKills, 
                                 (ArenaPoints != 0) ? string.Format(", Arena Points: {0}", ArenaPoints) : "");
        }
    }
}
