﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TraktRater;
using TraktRater.Extensions;
using TraktRater.TraktAPI;
using TraktRater.TraktAPI.DataStructures;
using TraktRater.Web;

namespace TraktRater.TraktAPI
{
    /// <summary>
    /// List of Sync Modes
    /// </summary>
    public enum TraktSyncModes
    {
        library,
        seen,
        unlibrary,
        unseen,
        watchlist,
        unwatchlist
    }

    /// <summary>
    /// Object that communicates with the Trakt API
    /// </summary>
    public static class TraktAPI
    {
        /// <summary>
        /// Tests account details can login to trakt.tv
        /// </summary>
        /// <param name="data">Object containing username/password</param>
        /// <returns>The response from trakt</returns>
        public static TraktResponse TestAccount(TraktAuthentication data)
        {
            string response = TraktWeb.Transmit(TraktURIs.TestAccount, data.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        /// <summary>
        /// Rates a list of episodes on trakt
        /// </summary>
        /// <param name="data">The object containing the list of episodes to be rated</param>       
        /// <returns>The response from trakt</returns>
        public static TraktRatingsResponse RateEpisodes(TraktRateEpisodes data)
        {
            // check that we have everything we need
            if (data == null || data.Episodes.Count == 0)
                return null;

            // serialize data to JSON and send to server
            string response = TraktWeb.Transmit(TraktURIs.RateEpisodes, data.ToJSON());
      
            // return success or failure
            return response.FromJSON<TraktRatingsResponse>();
        }

        /// <summary>
        /// Rates a list of shows on trakt
        /// </summary>
        /// <param name="data">The object containing the list of shows to be rated</param>       
        /// <returns>The response from trakt</returns>
        public static TraktRatingsResponse RateShows(TraktRateShows data)
        {
            // check that we have everything we need
            if (data == null || data.Shows.Count == 0)
                return null;

            // serialize data to JSON and send to server
            string response = TraktWeb.Transmit(TraktURIs.RateShows, data.ToJSON());

            // return success or failure
            return response.FromJSON<TraktRatingsResponse>();
        }
        
        /// <summary>
        /// Rates a list of movies on trakt
        /// </summary>
        /// <param name="data">The object containing the list of movies to be rated</param>       
        /// <returns>The response from trakt</returns>
        public static TraktRatingsResponse RateMovies(TraktRateMovies data)
        {
            // check that we have everything we need
            if (data == null || data.Movies.Count == 0)
                return null;

            // serialize data to JSON and send to server
            string response = TraktWeb.Transmit(TraktURIs.RateMovies, data.ToJSON());

            // return success or failure
            return response.FromJSON<TraktRatingsResponse>();
        }

        public static TraktShowSummary GetShowSummary(string slug)
        {
            // check that we have everything we need
            if (string.IsNullOrEmpty(slug))
                return null;
            
            string fileCache = string.Format(TraktCache.cShowInfoFileCache, slug);
            string response = TraktCache.GetFromCache(fileCache, 1);
            if (string.IsNullOrEmpty(response))
            {
                // serialize data to JSON and send to server
                response = TraktWeb.Transmit(string.Format(TraktURIs.ShowSummary, slug), string.Empty);
                TraktCache.CacheResponse(response, fileCache);
                if (response.FromJSON<TraktShowSummary>() == null)
                {
                    TraktCache.DeleteFromCache(fileCache);
                    return null;
                }
            }
            // return success or failure
            return response.FromJSON<TraktShowSummary>();
        }

        /// <summary>
        /// Sends movie sync data to Trakt
        /// </summary>
        /// <param name="syncData">The sync data to send</param>
        /// <param name="mode">The sync mode to use</param>
        /// <returns>The response from trakt</returns>
        public static TraktMovieSyncResponse SyncMovieLibrary(TraktMovieSync syncData, TraktSyncModes mode)
        {
            // check that we have everything we need
            if (syncData == null || syncData.MovieList.Count == 0)
                return null;

            // serialize data to JSON and send to server
            string response = TraktWeb.Transmit(string.Format(TraktURIs.SyncMovieLibrary, mode.ToString()), syncData.ToJSON());

            // return success or failure
            return response.FromJSON<TraktMovieSyncResponse>();
        }

        /// <summary>
        /// Sends episode sync data to Trakt
        /// </summary>
        /// <param name="syncData">The sync data to send</param>
        /// <param name="mode">The sync mode to use</param>
        public static TraktResponse SyncEpisodeLibrary(TraktEpisodeSync syncData, TraktSyncModes mode)
        {
            // check that we have everything we need
            if (syncData == null || string.IsNullOrEmpty(syncData.SeriesID))
                return null;

            // serialize data to JSON and send to server
            string response = TraktWeb.Transmit(string.Format(TraktURIs.SyncEpisodeLibrary, mode.ToString()), syncData.ToJSON());

            // return success or failure
            return response.FromJSON<TraktResponse>();
        }
    }
}