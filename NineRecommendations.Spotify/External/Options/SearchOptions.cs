using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineRecommendations.Spotify.External.Options
{
    internal class SearchOptions
    {
        public string Genre { get; set; } = string.Empty;
        public Range Year { get; set; }
    }
}
