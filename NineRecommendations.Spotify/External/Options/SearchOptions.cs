using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineRecommendations.Spotify.External.Options
{
    public class SearchOptions
    {
        public string? Genre { get; set; }
        public Range Year { get; set; } = new Range(1900, DateTime.UtcNow.Year);
    }
}
