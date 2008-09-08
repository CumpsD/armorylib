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

using ArmoryLib.Character.SpellDetail;

namespace ArmoryLib.Character
{
    // <bonusHealing value="1053"/>
    // <penetration value="0"/>
    public class Spell
    {
        public Arcane Arcane { get; private set; }
        public Fire Fire { get; private set; }
        public Frost Frost { get; private set; }
        public Holy Holy { get; private set; }
        public Nature Nature { get; private set; }
        public Shadow Shadow { get; private set; }

        public int BonusHealing { get; private set; }
        public ManaRegen ManaRegen { get; private set; }
        public Hit Hit { get; private set; }
        public int SpellPenetration { get; private set; }
        public PetBonus PetBonus { get; private set; }

        internal Spell(Arcane arcane,
                       Fire fire,
                       Frost frost,
                       Holy holy,
                       Nature nature,
                       Shadow shadow,
                       ManaRegen manaRegen,
                       Hit hit,
                       int bonusHealing,
                       int spellPenetration,
                       PetBonus petBonus)
        {
            Arcane = arcane;
            Fire = fire;
            Frost = frost;
            Holy = holy;
            Nature = nature;
            Shadow = shadow;
            ManaRegen = manaRegen;
            Hit = hit;
            BonusHealing = bonusHealing;
            SpellPenetration = spellPenetration;
            PetBonus = petBonus;
        }

        public override string ToString()
        {
            return string.Format("{1}{0}" +
                                 "{2}{0}" +
                                 "{3}{0}" +
                                 "{4}{0}" +
                                 "{5}{0}" +
                                 "{6}{0}" +
                                 "{7}{0}" +
                                 "{8}{0}" +
                                 "Bonus Healing: {9}{0}" +
                                 "Spell Penetration: {10}{0}" +
                                 "{11}",
                                 Environment.NewLine,
                                 Arcane,
                                 Fire,
                                 Frost,
                                 Holy,
                                 Nature,
                                 Shadow,
                                 ManaRegen,
                                 Hit,
                                 BonusHealing,
                                 SpellPenetration,
                                 (PetBonus.AttackPower != -1) ? PetBonus + Environment.NewLine : "");
        }
    }
}
