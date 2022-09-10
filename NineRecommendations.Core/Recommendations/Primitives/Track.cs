namespace NineRecommendations.Core.Recommendations.Primitives
{
    public readonly struct Track
    {
        public string Name { get; }
        public string Authors { get; }
        public TimeSpan Length { get; }
        public Uri Uri { get; }
    }
}
