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

namespace ArmoryLib.Character.StatsDetail
{
    // <stamina base="90" effective="377" health="3590" petBonus="-1"/>   (Rogue)
    // <stamina base="109" effective="693" health="6750" petBonus="208"/> (Hunter)
    public class Stamina
    {
        public int AddedHealth { get; private set; }
        public int BaseStamina { get; private set; }
        public int EffectiveStamina { get; private set; }
        public int PetBonusStamina { get; private set; }

        public int AddedStamina
        {
            get { return EffectiveStamina - BaseStamina; }
        }

        internal Stamina(int health, int baseStat, int effectiveStat, int petBonus)
        {
            AddedHealth = health;
            BaseStamina = baseStat;
            EffectiveStamina = effectiveStat;
            PetBonusStamina = petBonus;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, +Health: {2}{3}",
                BaseStamina,
                EffectiveStamina,
                AddedHealth,
                (PetBonusStamina != -1) ? string.Format(", Pet Bonus Stamina: {0}%", PetBonusStamina) : "");
        }
    }
}
