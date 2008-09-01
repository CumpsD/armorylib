using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using G = ArmoryLib.Guild.Guild;

namespace ArmoryLib.Character
{
    public class Character : IComparable<Character>, IEquatable<Character>
    {
        // TODO: Decorate properties to indicate under which detail-level they are available.
        public CharacterDetail DetailLoaded { get; private set; }

        // Search result properties
        public Region Region { get; private set; }
        public Faction Faction { get; private set; }
        public string Name { get; private set; }
        public string Realm { get; private set; }
        public string BattleGroup { get; private set; }
        public Gender Gender { get; private set; }
        public Race Race { get; private set; }
        public Class Class { get; private set; }
        public int Level { get; private set; }

        public G Guild { get; private set; }

        public string BeImbaUrl
        {
            get
            {
                return string.Format("http://be.imba.hu/?zone={0}&realm={1}&character={2}", 
                                     this.Region.RegionAbbreviation(), 
                                     this.Realm, 
                                     this.Name);
            }
        }

        private string Url { get; set; }

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

        public bool IsDetailLoaded(CharacterDetail checkDetail)
        {
            return ((DetailLoaded & checkDetail) == checkDetail);
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
