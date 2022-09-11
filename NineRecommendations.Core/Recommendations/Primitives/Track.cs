namespace NineRecommendations.Core.Recommendations.Primitives
{
    public readonly struct Track
    {
        public string Name { get; }
        public string Authors { get; }
        public TimeSpan Duration { get; }
        public Uri Uri { get; }

        public Track(string name, string authors, TimeSpan duration, Uri uri)
        {
            Name = name;
            Authors = authors;
            Duration = duration;
            Uri = uri;
        }
    }
}
