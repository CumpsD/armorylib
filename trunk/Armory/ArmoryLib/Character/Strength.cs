using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    // <strength attack="82" base="92" block="-1" effective="92"/>
    public class Strength
    {
        public int AddedAttackPower { get; private set; }

        // TODO: Check if Strength really adds block value for warriors?
        public int AddedBlock { get; private set; }

        public int BaseStrength { get; private set; }
        public int EffectiveStrength { get; private set; }

        internal Strength(int ap, int baseStat, int block, int effectiveStat)
        {
            AddedAttackPower = ap;
            AddedBlock = block;
            BaseStrength = baseStat;
            EffectiveStrength = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, +AP: {2}{3}",
                BaseStrength,
                EffectiveStrength,
                AddedAttackPower,
                (AddedBlock != -1) ? string.Format(", +Block: {0}", AddedBlock) : "");
        }
    }
}
