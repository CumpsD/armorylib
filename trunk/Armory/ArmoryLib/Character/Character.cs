/// <summary>** BEGIN LICENSE BLOCK *****
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
using G = ArmoryLib.Guild.Guild;

namespace ArmoryLib.Character
{
    public class Character : IComparable<Character>, IEquatable<Character>
    {
        public CharacterDetail DetailLoaded { get; private set; }

        #region Basic Properties
        private Region _region;
        public Region Region
        {
            get
            {
                CheckDetailRequired("Region", CharacterDetail.Basic);
                return _region;
            }
            private set { _region = value; }
        }

        private Faction _faction;
        public Faction Faction
        {
            get
            {
                CheckDetailRequired("Faction", CharacterDetail.Basic);
                return _faction;
            }
            private set { _faction = value; }
        }

        private string _name;
        public string Name
        {
            get
            {
                CheckDetailRequired("Name", CharacterDetail.Basic);
                return _name;
            }
            private set { _name = value; }
        }

        private string _realm;
        public string Realm
        {
            get
            {
                CheckDetailRequired("Realm", CharacterDetail.Basic);
                return _realm;
            }
            private set { _realm = value; }
        }

        private string _battleGroup;
        public string BattleGroup
        {
            get
            {
                CheckDetailRequired("BattleGroup", CharacterDetail.Basic);
                return _battleGroup;
            }
            private set { _battleGroup = value; }
        }

        private Gender _gender;
        public Gender Gender
        {
            get
            {
                CheckDetailRequired("Gender", CharacterDetail.Basic);
                return _gender;
            }
            private set { _gender = value; }
        }

        private Race _race;
        public Race Race
        {
            get
            {
                CheckDetailRequired("Race", CharacterDetail.Basic);
                return _race;
            }
            private set { _race = value; }
        }

        private Class _class;
        public Class Class
        {
            get
            {
                CheckDetailRequired("Class", CharacterDetail.Basic);
                return _class;
            }
            private set { _class = value; }
        }

        private int _level;
        public int Level
        {
            get
            {
                CheckDetailRequired("Level", CharacterDetail.Basic);
                return _level;
            }
            private set { _level = value; }
        }

        private G _guild;
        public G Guild
        {
            get
            {
                CheckDetailRequired("Guild", CharacterDetail.Basic);
                return _guild;
            }
            private set { _guild = value; }
        }

        public string BeImbaUrl
        {
            get
            {
                CheckDetailRequired("BeImbaUrl", CharacterDetail.Basic);
                return string.Format("http://be.imba.hu/?zone={0}&realm={1}&character={2}",
                                     this.Region.RegionAbbreviation(),
                                     this.Realm,
                                     this.Name);
            }
        }

        private string Url { get; set; }
        #endregion

        #region CharacterSheet Properties
        private TalentSpec _talentSpec;
        public TalentSpec TalentSpec
        {
            get
            {
                CheckDetailRequired("TalentSpec", CharacterDetail.CharacterSheet);
                return _talentSpec;
            }
            internal set { _talentSpec = value; }
        }

        private PvpInfo _pvpInfo;
        public PvpInfo PvpInfo
        {
            get
            {
                CheckDetailRequired("PvpInfo", CharacterDetail.CharacterSheet);
                return _pvpInfo;
            }
            internal set { _pvpInfo = value; }
        }

        private Stats _stats;
        public Stats Stats
        {
            get
            {
                CheckDetailRequired("Stats", CharacterDetail.CharacterSheet);
                return _stats;
            }
            internal set { _stats = value; }
        }
        #endregion

        internal Character(CharacterDetail detailLoaded,
                         Region region,
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
            DetailLoaded = detailLoaded;
            Region = region;
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

        #region Detail Checks
        public bool IsDetailLoaded(CharacterDetail checkDetail)
        {
            return DetailLoaded.ContainsDetail(checkDetail);
        }

        private void CheckDetailRequired(string propertyName, CharacterDetail requiredDetail)
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

        internal void LoadedDetail(CharacterDetail characterDetail)
        {
            DetailLoaded = DetailLoaded | characterDetail;
        }

        #region IComparable<Character> Members
        public int CompareTo(Character other)
        {
            if (this.Region == other.Region)
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
            else
            {
                return ((int)this.Region).CompareTo((int)other.Region);
            }
        }
        #endregion

        #region IEquatable<Character> Members
        public bool Equals(Character other)
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

            if (!(obj is Character))
            {
                throw new InvalidCastException("The 'obj' argument is not a Character object.");
            }
            else
            {
                return Equals(obj as Character);
            }
        }

        public static bool operator ==(Character a, Character b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if (object.ReferenceEquals(a, null)) return false;
            if (object.ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Character a, Character b)
        {
            return !(a == b);
        }
        #endregion
    }
}
