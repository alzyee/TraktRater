using System.Xml;
using System; 
using TraktRater.Web;
using TraktRater.Settings;
using TraktRater.TraktAPI;



namespace TraktRater.Sites
{
	public class Listal
	{
		public static Boolean ListalTest (string XmlFile)
		{
			XmlDocument xmlFile = new XmlDocument ();
			xmlFile.Load (XmlFile);
			XmlNamespaceManager nsmgr = new XmlNamespaceManager (xmlFile.NameTable);
		
			XmlNodeList xnList = xmlFile.SelectNodes ("/rss/channel/item", nsmgr);
			foreach (XmlNode xn in xnList) {
				//The title and description should not be needed since it gives the IMDB ID -alzyee
				string title = xn ["title"].InnerText;
				//string description = xn ["description"].InnerText;
				string DateAdded = xn ["pubDate"].InnerText;
				string rating = xn ["listal:rating"].InnerText;
				string imdbNumber = xn ["listal:imdb"].InnerText;
				string ownedWanted = xn ["listal:listtype"].InnerText;


				//#*# This time was wrong by 11 hours for EST so I added 11*3600 seconds. -aLzyEE
				//#*# I later found out that trakt lists the time the movie ended so 11 hours is probably wrong -aLzyEE
				TimeSpan ts = (Convert.ToDateTime(DateAdded) - new DateTime(1970,1,1,0,0,0));
				double unixTime = ts.TotalSeconds +11*3600;

				//Create Upload String
				//#*# remove after being implemented execute load app settings because in aLzyEE's version this is done before the form loads. -aLzyEE
				AppSettings.Load();
				string UserName = AppSettings.TraktUsername;
				string Password = AppSettings.TraktPassword;
				string TransString="";


				//Clear seen from library first (I went through and clicked a seen on a bunch of movies and I would like the right time stamp.) -alzyee
//				TransString = "{\"password\":\"" + Password + "\",\"username\":\"" + UserName +
//					"\",\"movies\":[{\"imdb_id\":\"tt" + imdbNumber + "\"}]}";
//				Console.WriteLine (TraktWeb.Transmit (TraktURIs.MovieUnseen, TransString));
			
				if (ownedWanted=="owned")
				{
//
					TransString= "{\"password\":\"" + Password + "\",\"username\":\"" + UserName +
						"\",\"movies\":[{\"imdb_id\":\"tt" + imdbNumber + "\",\"last_played\":"+unixTime+"}]}";
					Console.WriteLine (TraktWeb.Transmit (TraktURIs.MovieSeen, TransString));
					//I imagine there are faster + cleaner ways to deal with strings... but they can't be that much faster and I am not familiar with them. -alzyee
					if (int.Parse (rating)>0)
					{
						TransString= "{\"password\":\"" + Password + "\",\"username\":\"" + UserName +
							"\",\"movies\":[{\"imdb_id\":\"tt" + imdbNumber + "\",\"rating\":" + int.Parse (rating) + "}]}";
						
						Console.WriteLine (TraktWeb.Transmit (TraktURIs.RateMovies, TransString));
					}
				}
				else
				{ 
					if (ownedWanted=="wanted")
					{

					TransString = "{\"password\":\"" + Password + "\",\"username\":\"" + UserName +
						"\",\"movies\":[{\"imdb_id\":\"tt" + imdbNumber + "\"}]}";
						Console.WriteLine (TraktWeb.Transmit (TraktURIs.MovieWatchlist, TransString));
					}
				}

			}
			return true;
		}

	}

}
