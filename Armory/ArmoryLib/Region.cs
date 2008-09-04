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

using ArmoryLib.Exceptions;

namespace ArmoryLib
{
    public enum Region
    {
        USA,
        Oceanic,
        Europe,
        Korea,
        China,
        Taiwan
    }

    public static class RegionExtensions
    {
        public static string RegionAbbreviation(this Region region)
        {
            switch (region)
            {
                case Region.USA:
                case Region.Oceanic:
                    return "US";
                case Region.Europe:
                    return "EU";
                case Region.Korea:
                    return "KR";
                case Region.China:
                    return "CN";
                case Region.Taiwan:
                    return "TW";
            }

            throw new InvalidRegionException();
        }
    }
}
