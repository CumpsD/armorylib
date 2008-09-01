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
            // http://eu.wowarmory.com/search.xml?searchQuery=The%20Dominion&searchType=guilds
            string searchString = string.Format("search.xml?searchQuery={0}&searchType=guilds", 
                                                HttpUtility.UrlEncode(guildName));

            XmlDocument searchResults = armory.Request(searchString);
            XmlNodeList guildNodes = searchResults.SelectNodes("/page/armorySearch/searchResults/guilds/guild");

            List<Guild> guilds = new List<Guild>();

            foreach (XmlNode guildNode in guildNodes)
            {
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
            string searchString = string.Format("guild-info.xml?r={0}&n={1}&p=1", 
                                                HttpUtility.UrlEncode(realmName), 
                                                HttpUtility.UrlEncode(guildName));

            XmlDocument searchResults = armory.Request(searchString);
            XmlNode guildDetails = searchResults.SelectSingleNode("/page/guildKey");
            XmlNode memberDetails = searchResults.SelectSingleNode("/page/guildInfo/guild/members");

            if ((guildDetails != null) && (memberDetails != null))
            {

                // TODO: Apparently to figure out the battlegroup, we'll have to load 1 member...

                Guild guild = new Guild(
                                false,
                                (Faction)Enum.Parse(typeof(Faction), guildDetails.Attributes["factionId"].Value),
                                guildDetails.Attributes["name"].Value,
                                guildDetails.Attributes["realm"].Value,
                                string.Empty,
                                guildDetails.Attributes["url"].Value,
                                guildDetails.Attributes["nameUrl"].Value,
                                guildDetails.Attributes["realmUrl"].Value,
                                Convert.ToInt32(memberDetails.Attributes["memberCount"].Value));

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
