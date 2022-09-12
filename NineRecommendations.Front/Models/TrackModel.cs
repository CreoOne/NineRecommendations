namespace NineRecommendations.Front.Models
{
    public class TrackModel
    {
        public string Name { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public Uri? Uri { get; set; }
    }
}
