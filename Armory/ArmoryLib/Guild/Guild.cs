﻿/// <summary>** BEGIN LICENSE BLOCK *****
/// Version: LGPL 3
/// 
/// Copyright 2008 David Cumps <david@cumps.be>
/// 
/// This file is part of ArmoryLib.
///
/// ArmoryLib is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Lesser General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// ArmoryLib is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
///
/// You should have received a copy of the GNU Lesser General Public License
/// along with ArmoryLib.  If not, see <http://www.gnu.org/licenses/>.
/// **** END LICENSE BLOCK ****
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArmoryLib.Exceptions;
using C = ArmoryLib.Character.Character;

namespace ArmoryLib.Guild
{
    public class Guild : IComparable<Guild>, IEquatable<Guild>
    {
        public GuildDetail DetailLoaded { get; private set; }

        #region Basic properties
        private Region _region;
        public Region Region
        {
            get
            {
                CheckDetailRequired("Region", GuildDetail.Basic);
                return _region;
            }
            private set { _region = value; }
        }

        private Faction _faction;
        public Faction Faction
        {
            get
            {
                CheckDetailRequired("Faction", GuildDetail.Basic);
                return _faction;
            }
            private set { _faction = value; }
        }

        private string _realm;
        public string Realm
        {
            get
            {
                CheckDetailRequired("Realm", GuildDetail.Basic);
                return _realm;
            }
            private set { _realm = value; }
        }

        private string _battleGroup;
        public string BattleGroup
        {
            get
            {
                CheckDetailRequired("BattleGroup", GuildDetail.Basic);
                return _battleGroup;
            }
            private set { _battleGroup = value; }
        }

        private string _name;
        public string Name
        {
            get
            {
                CheckDetailRequired("Name", GuildDetail.Basic);
                return (this._name == string.Empty) ? "(No Guild)" : this._name;
            }
            private set { _name = value; }
        }

        private string Url { get; set; }
        #endregion

        #region Roster Properties
        private int _memberCount;
        public int MemberCount
        {
            get
            {
                CheckDetailRequired("MemberCount", GuildDetail.Roster);
                return _memberCount;
            }
            private set { _memberCount = value; }
        }

        public string MemberCountText
        {
            get
            {
                CheckDetailRequired("MemberCountText", GuildDetail.Roster);
                return string.Format("{0} {1}", MemberCount, MemberCount == 1 ? "member" : "members");
            }
        }

        private Dictionary<int, List<C>> _members;
        public Dictionary<int, List<C>> Members
        {
            get
            {
                CheckDetailRequired("_members", GuildDetail.Roster);
                return _members;
            }
            private set { _members = value; }
        }

        private string NameUrl { get; set; }
        private string RealmUrl { get; set; }
        #endregion

        internal Guild(GuildDetail detailLoaded,
                     Region region,
                     Faction faction,
                     string name,
                     string realm,
                     string battleGroup,
                     string url) :
            this(detailLoaded, region, faction, name, realm, battleGroup, url, string.Empty, string.Empty, 0)
        {
        }

        internal Guild(GuildDetail detailLoaded,
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
            DetailLoaded = detailLoaded;
            Region = region;
            Faction = faction;
            Name = name;
            Realm = realm;
            BattleGroup = battleGroup;
            Url = url;
            NameUrl = nameUrl;
            RealmUrl = realmUrl;
            MemberCount = memberCount;

            Members = new Dictionary<int, List<C>>();
        }

        #region Detail Checks
        public bool IsDetailLoaded(GuildDetail checkDetail)
        {
            return DetailLoaded.ContainsDetail(checkDetail);
        }

        private void CheckDetailRequired(string propertyName, GuildDetail requiredDetail)
        {
            if (!IsDetailLoaded(requiredDetail))
            {
                throw new MissingDetailException(string.Format("{0} requires the {1} detail to be loaded. Current detail: {2}",
                                                               propertyName,
                                                               requiredDetail,
                                                               DetailLoaded));
            }
        }
        #endregion

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
