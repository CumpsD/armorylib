using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using G = ArmoryLib.Guild.Guild;

namespace ArmoryLib.Character
{
    public class Character: IComparable<Character>
    {
        private bool SearchResult { get; set; }

        // Search result properties
        public Faction Faction { get; private set; }
        public string Name { get; private set; }
        public string Realm { get; private set; }
        public string BattleGroup { get; private set; }
        public Gender Gender { get; private set; }
        public Race Race { get; private set; }
        public Class Class { get; private set; }
        public int Level { get; private set; }

        public G Guild { get; private set; }

        private string Url { get; set; }

        public Character(bool searchResult,
                         Faction faction,
                         string name,
                         string realm,
                         string battleGroup,
                         Gender gender,
                         Race race,
                         Class characterClass,
                         int level,
                         string url,
                         G guild)
        {
            SearchResult = searchResult;
            Faction = faction;
            Name = name;
            Realm = realm;
            BattleGroup = battleGroup;
            Gender = gender;
            Race = race;
            Class = characterClass;
            Level = level;
            Url = url;

            Guild = guild;
        }

        #region IComparable<Character> Members
        public int CompareTo(Character other)
        {
            if (this.Name == other.Name)
            {
                if (this.Faction == other.Faction)
                {
                    if (this.BattleGroup == other.BattleGroup)
                    {
                        return this.Realm.CompareTo(other.Realm);
                    }
                    else
                    {
                        return this.BattleGroup.CompareTo(other.BattleGroup);
                    }
                }
                else
                {
                    return ((int)this.Faction).CompareTo((int)other.Faction);
                }
            }
            else
            {
                return this.Name.ToLowerInvariant().CompareTo(other.Name.ToLowerInvariant());
            }
        }
        #endregion
    }
}
