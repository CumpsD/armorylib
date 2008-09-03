using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    // TODO: PetBonus, check on Dinli for hunter XML

    // <intellect base="43" critHitPercent="-1.00" effective="43" mana="-1" petBonus="-1"/>
    public class Intellect
    {
        public int AddedMana { get; private set; }
        public double AddedCritPercent { get; private set; }
        public int BaseIntellect { get; private set; }
        public int EffectiveIntellect { get; private set; }

        internal Intellect(int mana, int baseStat, double critPercent, int effectiveStat)
        {
            AddedMana = mana;
            AddedCritPercent = critPercent;
            BaseIntellect = baseStat;
            EffectiveIntellect = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}{2}{3}",
                BaseIntellect,
                EffectiveIntellect,
                (AddedMana != -1) ? string.Format(", +Mana: {0}", AddedMana) : "",
                (AddedCritPercent != -1) ? string.Format(", +Crit%: {0}%", AddedCritPercent) : "");
        }
    }
}
