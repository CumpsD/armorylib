using System;

using ArmoryLib.Exceptions;

namespace ArmoryLib
{
    public enum Region
    {
        USA,
        Oceanic,
        Europe,
        Korea,
        China,
        Taiwan
    }

    public static class RegionExtensions
    {
        public static string RegionAbbreviation(this Region region)
        {
            switch (region)
            {
                case Region.USA:
                case Region.Oceanic:
                    return "US";
                case Region.Europe:
                    return "EU";
                case Region.Korea:
                    return "KR";
                case Region.China:
                    return "CN";
                case Region.Taiwan:
                    return "TW";
            }

            throw new InvalidRegionException();
        }
    }
}
