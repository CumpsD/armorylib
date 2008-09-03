using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ArmoryLib
{
    internal class Util
    {
        internal static NumberFormatInfo NumberFormatter
        {
            get
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";
                provider.NumberGroupSizes = new int[] { 3 };
                return provider;
            }
        }
    }
}
