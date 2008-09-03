using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    // <agility armor="642" attack="311" base="163" critHitPercent="7.73" effective="321"/>
    public class Agility
    {
        public int AddedArmor { get; private set; }
        public int AddedAttackPower { get; private set; }
        public double AddedCritPercent { get; private set; }
        public int BaseAgility { get; private set; }
        public int EffectiveAgility { get; private set; }

        internal Agility(int armor, int ap, int baseStat, double critPercent, int effectiveStat)
        {
            AddedArmor = armor;
            AddedAttackPower = ap;
            AddedCritPercent = critPercent;
            BaseAgility = baseStat;
            EffectiveAgility = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, +AP: {2}, +Crit%: {3}%, +Armor: {4}",
                BaseAgility,
                EffectiveAgility,
                AddedAttackPower,
                AddedCritPercent,
                AddedArmor);
        }
    }
}
