using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    public class TalentSpec
    {
        public int FirstTree { get; private set; }
        public int SecondTree { get; private set; }
        public int ThirdTree { get; private set; }

        public string SpecAbbreviation
        {
            get { return string.Format("{0}/{1}/{2}", FirstTree, SecondTree, ThirdTree); }
        }

        internal TalentSpec(int firstTree, int secondTree, int thirdTree)
        {
            FirstTree = firstTree;
            SecondTree = secondTree;
            ThirdTree = thirdTree;
        }
    }
}
