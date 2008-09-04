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
using System.Xml;
using System.Web;
using System.Net;

using ArmoryLib.Exceptions;

namespace ArmoryLib
{
    public class Armory
    {
        public Region Region { get; set; }
        public string UserAgent { get; set; }

        public string Url
        {
            get
            {
                switch (Region)
                {
                    case Region.USA:
                    case Region.Oceanic:
                        return "http://www.wowarmory.com/";
                    case Region.Europe:
                        return "http://eu.wowarmory.com/";
                    case Region.Korea:
                        return "http://kr.wowarmory.com/";
                    case Region.China:
                        return "http://cn.wowarmory.com/";
                    case Region.Taiwan:
                        return "http://tw.wowarmory.com/";
                }

                throw new InvalidRegionException();
            }
        }

        public string DefaultUserAgent
        {
            get
            {
                return "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.5.20404)";
            }
        }

        // Defaults to European Armory
        public Armory(): this(Region.Europe) {}

        public Armory(Region region)
        {
            Region = region;
            UserAgent = DefaultUserAgent;
        }

        internal XmlDocument Request(string command)
        {
            string armoryRequest = Url + command;
            XmlDocument armoryResponse = new XmlDocument();

            using (WebClient client = new WebClient())
            {
                client.Headers.Set("User-Agent", UserAgent);
                armoryResponse.LoadXml(client.DownloadString(armoryRequest));
            }

            return armoryResponse;
        }
    }
}
