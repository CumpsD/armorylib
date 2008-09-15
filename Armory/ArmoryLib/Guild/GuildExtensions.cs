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
                    int rank = Convert.ToInt32(guildMember.Attributes["rank"].Value);
                    if (!guild.Members.ContainsKey(rank))
                    {
                        guild.Members.Add(rank, new List<C>());
                    }

                    guild.Members[rank].Add(new C(
                                                  armory,
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
