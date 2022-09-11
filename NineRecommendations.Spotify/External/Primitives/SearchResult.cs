using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineRecommendations.Spotify.External.Primitives
{
    public class SearchResult
    {
        public IEnumerable<string> TrackIds { get; init; } = Enumerable.Empty<string>();
    }
}
