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

namespace ArmoryLib.Character.RangedDetail
{
    // TODO: Have some tests to figure out if this AP number matches the total from items, agi, str, ...

    // <power base="376" effective="1066" increasedDps="76.0" petAttack="-1.00" petSpell="-1.00"/>    (Rogue)
    // <power base="831" effective="1662" increasedDps="118.0" petAttack="365.64" petSpell="213.90"/> (Hunter)
    public class AttackPower
    {
        public int BaseAP { get; private set; }
        public int EffectiveAP { get; private set; }
        public double DPSIncrease { get; private set; }
        public double PetBonusAP { get; private set; }
        public double PetBonusSpell { get; private set; }

        internal AttackPower(int baseStat, int effectiveStat, double dpsIncrease, double petBonusAp, double petBonusSpell)
        {
            BaseAP = baseStat;
            EffectiveAP = effectiveStat;
            DPSIncrease = dpsIncrease;
            PetBonusAP = petBonusAp;
            PetBonusSpell = petBonusSpell;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, +DPS: {2}{3}{4}",
                                 BaseAP,
                                 EffectiveAP,
                                 DPSIncrease,
                                 (PetBonusAP != -1) ? string.Format(", Pet Bonus AP: {0}", PetBonusAP) : "",
                                 (PetBonusSpell != -1) ? string.Format(", Pet Bonus Spell: {0}", PetBonusSpell) : "");
        }
    }
}
