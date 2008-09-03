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
        public Faction Faction {
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

        public Character(CharacterDetail detailLoaded,
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
            return ((DetailLoaded & checkDetail) == checkDetail);
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
