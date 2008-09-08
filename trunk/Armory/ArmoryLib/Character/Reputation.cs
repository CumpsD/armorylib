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
    public static class ReputationExtensions
    {
        public static List<Reputation> GetFilteredReputation(this List<Reputation> reputations, ReputationLevel repLevel)
        {
            return reputations.FindAll(r => r.Level == repLevel);
        }
    }

    //<faction key="darkspeartrolls" name="Darkspear Trolls" reputation="31963"/>
    public class Reputation
    {
        public string Key { get; private set; }
        public string Name { get; private set; }
        public int Value { get; private set; }

        public ReputationLevel Level
        {
            get
            {
                if (Value.Between(0, 2999))
                {
                    return ReputationLevel.Neutral;
                }
                else if (Value.Between(3000, 8999))
                {
                    return ReputationLevel.Friendly;
                }
                else if (Value.Between(9000, 20999))
                {
                    return ReputationLevel.Honored;
                }
                else if (Value.Between(21000, 41999))
                {
                    return ReputationLevel.Revered;
                }
                else if (Value.Between(42000, 42999))
                {
                    return ReputationLevel.Exalted;
                }
                else if (Value.Between(-2999, -1))
                {
                    return ReputationLevel.Unfriendly;
                }
                else if (Value.Between(-5999, -3000))
                {
                    return ReputationLevel.Hostile;
                }
                else if (Value.Between(-42999 , -6000))
                {
                    return ReputationLevel.Hated;
                }

                return ReputationLevel.None;
            }
        }

        internal Reputation(string name, string key, int value)
        {
            Name = name;
            Key = key;
            Value = value;
        }
    }
}

/*
 * Exalted: 42000 - 42999
 * Revered: 21000 - 41999
 * Honored: 9000 - 20999
 * Friendly: 3000 - 8999
 * Neutral: 0 - 2999
 * Unfriendly: -1 - -2999
 * Hostile: -3000 - -5999
 * Hated: -6000 - -42000 (not 100% sure)
*/