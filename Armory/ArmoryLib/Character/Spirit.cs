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
    // <spirit base="57" effective="57" healthRegen="20" manaRegen="-1"/>
    public class Spirit
    {
        public int HealthRegen { get; private set; } // hp regen ooc
        public int ManaRegen { get; private set; } // Mp5 / 5second rule
        public int BaseSpirit { get; private set; }
        public int EffectiveSpirit { get; private set; }

        internal Spirit(int health, int mana, int baseStat, int effectiveStat)
        {
            HealthRegen = health;
            ManaRegen = mana;
            BaseSpirit = baseStat;
            EffectiveSpirit = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, Health Regen Out Of Combat: {2}{3}",
                BaseSpirit,
                EffectiveSpirit,
                HealthRegen,
                (ManaRegen != -1) ? string.Format(", Mana Regen Per 5 Seconds, 5 Seconds Rule: {0}", ManaRegen) : "");
        }
    }
}
