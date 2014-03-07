﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraktRater.TraktAPI
{
    /// <summary>
    /// List of URIs for the Trakt API
    /// </summary>
    public static class TraktURIs
    {
        const string apiKey = "5a3cf09bdce2e48c78f94f11f41b68ba";

        public const string RateEpisodes = @"http://api.trakt.tv/rate/episodes/" + apiKey;
        public const string RateMovies = @"http://api.trakt.tv/rate/movies/" + apiKey;
        public const string RateShows = @"http://api.trakt.tv/rate/shows/" + apiKey;
        public const string TestAccount = @"http://api.trakt.tv/account/test/" + apiKey;
        public const string ShowSummary = @"http://api.trakt.tv/show/summary.json/" + apiKey + "/{0}/extended";
        public const string SyncMovieLibrary = @"http://api.trakt.tv/movie/{0}/" + apiKey;
        public const string SyncEpisodeLibrary = @"http://api.trakt.tv/show/episode/{0}/" + apiKey;

		//http://trakt.tv/api-docs/movie-seen
		public const string MovieSeen=@"http://api.trakt.tv/movie/seen/" + apiKey;
		public const string MovieWatchlist=@"http://api.trakt.tv/movie/watchlist/"+ apiKey;
		public const String MovieUnseen=@"http://api.trakt.tv/movie/unseen/" + apiKey;
	
    }
}
