using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    // TODO: PetBonus, check on Dinli for hunter XML

    // <stamina base="90" effective="377" health="3590" petBonus="-1"/>
    public class Stamina
    {
        public double AddedHealth { get; private set; }
        public int BaseStamina { get; private set; }
        public int EffectiveStamina { get; private set; }

        internal Stamina(int health, int baseStat, int effectiveStat)
        {
            AddedHealth = health;
            BaseStamina = baseStat;
            EffectiveStamina = effectiveStat;
        }

        public override string ToString()
        {
            return string.Format("Base: {0}, Effective: {1}, +Health: {2}",
                BaseStamina,
                EffectiveStamina,
                AddedHealth);
        }
    }
}
