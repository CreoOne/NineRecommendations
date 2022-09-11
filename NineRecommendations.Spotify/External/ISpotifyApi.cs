﻿using NineRecommendations.Spotify.External.Options;
using NineRecommendations.Spotify.External.Primitives;

namespace NineRecommendations.Spotify.External
{
    public interface ISpotifyApi
    {
        Task<TracksResult> CallTracksAsync(IEnumerable<string> ids);
        Task<SearchResult> CallSearchAsync(SearchOptions searchOptions);
    }
}
