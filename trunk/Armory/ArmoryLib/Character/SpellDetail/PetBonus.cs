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
    // <petBonus attack="674" damage="177" fromType="fire"/>
    public class PetBonus
    {
        public int AttackPower { get; private set; }
        public int SpellPower { get; private set; }
        public School BonusOriginatingSchool { get; private set; }

        internal PetBonus(int ap, int spell, School school)
        {
            AttackPower = ap;
            SpellPower = spell;
            BonusOriginatingSchool = school;
        }

        public override string ToString()
        {
            return string.Format("Pet Bonus: +{0} AP, +{1} Spell (Granted from {2} Bonus)",
                                 AttackPower,
                                 SpellPower,
                                 BonusOriginatingSchool);
        }
    }
}
