using NineRecommendations.Spotify.External.Models;
using NineRecommendations.Spotify.External.Options;

namespace NineRecommendations.Spotify.External
{
    public interface ISpotifyApi
    {
        Task<RootObject?> CallSearchAsync(SearchOptions searchOptions);
    }
}
