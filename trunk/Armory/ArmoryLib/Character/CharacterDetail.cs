using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    [Flags]
    public enum CharacterDetail
    {
        None = 0,
        Basic = 1,
        CharacterSheet = 2,
        Reputation = 4,
        Skills = 8,
        Talents = 16,
        Arena = 32
    }

    internal static class CharacterDetailExtensions
    {
        internal static bool ContainsDetail(this CharacterDetail searchedDetail, CharacterDetail detailToSearch)
        {
            return ((searchedDetail & detailToSearch) == detailToSearch);
        }
    }
}
