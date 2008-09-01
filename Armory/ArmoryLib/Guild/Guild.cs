using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Guild
{
    public class Guild : IComparable<Guild>
    {
        private bool SearchResult { get; set; }

        // Search result properties
        public Faction Faction { get; private set; }
        public string Name { get; private set; }
        public string Realm { get; private set; }
        public string BattleGroup { get; private set; }

        private string Url { get; set; }

        // Detailed properties
        public int MemberCount { get; private set; }

        public string MemberCountText
        {
            get
            {
                return string.Format("{0} {1}", MemberCount, MemberCount == 1 ? "member" : "members");
            }
        }

        private string NameUrl { get; set; }
        private string RealmUrl { get; set; }

        public Guild(bool searchResult,
                     Faction faction,
                     string name,
                     string realm,
                     string battleGroup,
                     string url) :
            this(searchResult,
                 faction,
                 name,
                 realm,
                 battleGroup,
                 url,
                 string.Empty,
                 string.Empty,
                 0)
        {
        }

        public Guild(bool searchResult,
                     Faction faction,
                     string name,
                     string realm,
                     string battleGroup,
                     string url,
                     string nameUrl,
                     string realmUrl,
                     int memberCount)
        {
            SearchResult = searchResult;
            Faction = faction;
            Name = name;
            Realm = realm;
            BattleGroup = battleGroup;
            Url = url;
            NameUrl = nameUrl;
            RealmUrl = realmUrl;
            MemberCount = memberCount;
        }

        #region IComparable<Guild> Members

        public int CompareTo(Guild other)
        {
            if (this.Name.ToLowerInvariant() == other.Name.ToLowerInvariant())
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
