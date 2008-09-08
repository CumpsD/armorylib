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
    public class Effects
    {
        public List<BuffDebuff> Buffs { get; private set; }
        public List<BuffDebuff> Debuffs { get; private set; }

        internal Effects(List<BuffDebuff> buffs,
                         List<BuffDebuff> debuffs)
        {
            Buffs = buffs;
            Debuffs = debuffs;
        }

        public override string ToString()
        {
            StringBuilder buffs = new StringBuilder();
            foreach (BuffDebuff buff in Buffs)
            {
                buffs.Append(buff.Name);
                buffs.Append(" - ");
                buffs.Append(buff.Effect);
                buffs.Append(Environment.NewLine);
            }

            StringBuilder debuffs = new StringBuilder();
            foreach (BuffDebuff debuff in Debuffs)
            {
                debuffs.Append(debuff.Name);
                debuffs.Append(" - ");
                debuffs.Append(debuff.Effect);
                debuffs.Append(Environment.NewLine);
            }

            return string.Format("Buffs:{0}{1}{0}Debuffs:{0}{2}", 
                                  Environment.NewLine,
                                  (buffs.Length > 0) ? buffs.ToString() : "None", 
                                  (debuffs.Length > 0) ? debuffs.ToString() : "None");
        }
    }
}
