using NineRecommendations.Core.Recommendations.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineRecommendations.Spotify.External.Primitives
{
    public class TracksResult
    {
        public IEnumerable<Track> Tracks { get; init; } = Enumerable.Empty<Track>();
    }
}
