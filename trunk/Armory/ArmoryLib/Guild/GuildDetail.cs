using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Guild
{
    [Flags]
    public enum GuildDetail
    {
        None = 0,
        Basic = 1,
        Roster = 2
    }

    internal static class GuildDetailExtensions
    {
        internal static bool ContainsDetail(this GuildDetail searchedDetail, GuildDetail detailToSearch)
        {
            return ((searchedDetail & detailToSearch) == detailToSearch);
        }
    }
}
