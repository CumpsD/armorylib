using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    public class PvpInfo
    {
        public int LifeTimeKills { get; private set; }

        internal PvpInfo(int lifeTimeKills)
        {
            LifeTimeKills = lifeTimeKills;
        }
    }
}
