using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = ArmoryLib.Character.Character;

namespace ArmoryLib.Guild
{
    public class Guild : IComparable<Guild>, IEquatable<Guild>
    {
        private bool SearchResult { get; set; }

        // Search result properties
        public Region Region { get; private set; }
        public Faction Faction { get; private set; }
        public string Realm { get; private set; }
        public string BattleGroup { get; private set; }

        private string _name;
        public string Name { 
            get { return (this._name == string.Empty) ? "(No Guild)" : this._name; }
            private set { _name = value; }
        }

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

        public List<C> Members { get; private set; }

        private string NameUrl { get; set; }
        private string RealmUrl { get; set; }

        public Guild(bool searchResult,
                     Region region,
                     Faction faction,
                     string name,
                     string realm,
                     string battleGroup,
                     string url) :
            this(searchResult,
                 region,
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
                     Region region,
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
            Region = region;
            Faction = faction;
            Name = name;
            Realm = realm;
            BattleGroup = battleGroup;
            Url = url;
            NameUrl = nameUrl;
            RealmUrl = realmUrl;
            MemberCount = memberCount;

            Members = new List<C>();
        }

        #region IComparable<Guild> Members
        public int CompareTo(Guild other)
        {
            if (this.Region == other.Region)
            {
                // Converting because WoW keeps guild name casing, as opposed to character names
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
            else
            {
                return ((int)this.Region).CompareTo((int)other.Region);
            }
        }
        #endregion

        #region IEquatable<Guild> Members
        public bool Equals(Guild other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                return (other.Region == this.Region) &&
                       (other.Name == this.Name) &&
                       (other.Realm == this.Realm) &&
                       (other.Faction == this.Faction);
            }
        }
        #endregion

        #region Equality Support
        public override int GetHashCode()
        {
            return string.Format("{0}|{1}", this.Name, this.Realm).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return base.Equals(obj);

            if (!(obj is Guild))
            {
                throw new InvalidCastException("The 'obj' argument is not a Guild object.");
            }
            else
            {
                return Equals(obj as Guild);
            }
        }

        public static bool operator ==(Guild a, Guild b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if (object.ReferenceEquals(a, null)) return false;
            if (object.ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Guild a, Guild b)
        {
            return !(a == b);
        }
        #endregion
    }
}
