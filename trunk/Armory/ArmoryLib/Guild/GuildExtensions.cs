using System;
using System.Xml;
using System.Collections.Generic;
using System.Web;

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
                                true,
                                (Faction)Enum.Parse(typeof(Faction), guildNode.Attributes["factionId"].Value),
                                guildNode.Attributes["name"].Value,
                                guildNode.Attributes["realm"].Value,
                                guildNode.Attributes["battleGroup"].Value,
                                guildNode.Attributes["url"].Value);

                guilds.Add(guild);
            }

            return guilds;
        }

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

                Guild guild = new Guild(
                                false,
                                (Faction)Enum.Parse(typeof(Faction), guildDetails.Attributes["factionId"].Value),
                                guildDetails.Attributes["name"].Value,
                                guildDetails.Attributes["realm"].Value,
                                guildDetailsBattleGroup.Attributes["value"].Value,
                                guildDetails.Attributes["url"].Value,
                                guildDetails.Attributes["nameUrl"].Value,
                                guildDetails.Attributes["realmUrl"].Value,
                                Convert.ToInt32(guildDetailsMemberCount.Attributes["value"].Value));

                // TODO: Load guild members from search result
                // Check if the xml for <members> is the same as the results for character search

                return guild;
            }
            else
            {
                return null;
            }
        }
    }
}
