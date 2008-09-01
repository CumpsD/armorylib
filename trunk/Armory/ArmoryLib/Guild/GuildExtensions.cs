using System;
using System.Xml;
using System.Collections.Generic;
using System.Web;

using ArmoryLib.Character;
using C = ArmoryLib.Character.Character;

namespace ArmoryLib.Guild
{
    public static class GuildExtensions
    {
        public static List<Guild> SearchGuild(this Armory armory, string guildName)
        {
            // TODO: Take multiple pages into account!

            // http://eu.wowarmory.com/search.xml?searchQuery=The%20Dominion&searchType=guilds
            string searchString = string.Format("search.xml?searchQuery={0}&searchType=guilds", 
                                                HttpUtility.UrlEncode(guildName));

            XmlDocument searchResults = armory.Request(searchString);
            XmlNodeList guildNodes = searchResults.SelectNodes("/page/armorySearch/searchResults/guilds/guild");

            List<Guild> guilds = new List<Guild>();

            foreach (XmlNode guildNode in guildNodes)
            {
                // <guild battleGroup="Vindication" faction="Horde" factionId="1" name="The Dominion" realm="Sporeggar" url="r=Sporeggar&amp;n=The+Dominion&amp;p=1"/>
                Guild guild = new Guild(
                                GuildDetail.Basic,
                                armory.Region,
                                (Faction)Enum.Parse(typeof(Faction), guildNode.Attributes["factionId"].Value),
                                guildNode.Attributes["name"].Value,
                                guildNode.Attributes["realm"].Value,
                                guildNode.Attributes["battleGroup"].Value,
                                guildNode.Attributes["url"].Value);

                guilds.Add(guild);
            }

            return guilds;
        }

        // TODO: add overload to specify level of detail to load
        public static Guild LoadGuild(this Armory armory, string realmName, string guildName)
        {
            // http://eu.wowarmory.com/guild-info.xml?r=Sporeggar&n=The+Dominion&p=1
            // http://eu.wowarmory.com/guildheader.xml?r=Sporeggar&n=The+Dominion (battlegroup, members, faction)

            string searchString = string.Format("guild-info.xml?r={0}&n={1}&p=1", // TODO: Multi-page results, probably p=2, p=3, ...
                                                HttpUtility.UrlEncode(realmName), 
                                                HttpUtility.UrlEncode(guildName));

            XmlDocument searchResults = armory.Request(searchString);
            XmlNode guildDetails = searchResults.SelectSingleNode("/page/guildKey");

            if (guildDetails != null)
            {
                string searchDetailsString = string.Format("guildheader.xml?r={0}&n={1}",
                                                            HttpUtility.UrlEncode(realmName),
                                                            HttpUtility.UrlEncode(guildName));

                XmlDocument searchDetailsResults = armory.Request(searchDetailsString);
                XmlNode guildDetailsMemberCount = searchDetailsResults.SelectSingleNode("/page/guildHeader/members");
                XmlNode guildDetailsBattleGroup = searchDetailsResults.SelectSingleNode("/page/guildHeader/battleGroup");

                // <guildKey factionId="1" name="The Dominion" nameUrl="The+Dominion" realm="Sporeggar" realmUrl="Sporeggar" url="r=Sporeggar&amp;n=The+Dominion"/>
                Guild guild = new Guild(
                                GuildDetail.Basic | GuildDetail.Roster,
                                armory.Region,
                                (Faction)Enum.Parse(typeof(Faction), guildDetails.Attributes["factionId"].Value),
                                guildDetails.Attributes["name"].Value,
                                guildDetails.Attributes["realm"].Value,
                                guildDetailsBattleGroup.Attributes["value"].Value,
                                guildDetails.Attributes["url"].Value,
                                guildDetails.Attributes["nameUrl"].Value,
                                guildDetails.Attributes["realmUrl"].Value,
                                Convert.ToInt32(guildDetailsMemberCount.Attributes["value"].Value));

                XmlNodeList guildMembers = searchResults.SelectNodes("/page/guildInfo/guild/members/character");

                foreach (XmlNode guildMember in guildMembers)
                {
                    // <character class="Rogue" classId="4" gender="Female" genderId="1" level="70" name="Zoing" race="Blood Elf" raceId="10" rank="0" url="r=Sporeggar&amp;n=Zoing"/>
                    guild.Members.Add(new C(
                                            CharacterDetail.Basic,
                                            armory.Region,
                                            guild.Faction,
                                            guildMember.Attributes["name"].Value,
                                            guild.Realm,
                                            guild.BattleGroup,
                                            (Gender)Enum.Parse(typeof(Gender), guildMember.Attributes["genderId"].Value),
                                            (Race)Enum.Parse(typeof(Race), guildMember.Attributes["raceId"].Value),
                                            (Class)Enum.Parse(typeof(Class), guildMember.Attributes["classId"].Value),
                                            Convert.ToInt32(guildMember.Attributes["level"].Value),
                                            guildMember.Attributes["url"].Value,
                                            guild));
                }

                return guild;
            }
            else
            {
                return null;
            }
        }
    }
}
