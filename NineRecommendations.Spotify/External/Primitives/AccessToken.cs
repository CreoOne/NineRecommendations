namespace NineRecommendations.Spotify.External.Primitives
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "For ease of use property names are same as JSON object returned from spotify api")]
    internal sealed class AccessToken
    {
        public string access_token { get; set; } = string.Empty;
        public string token_type { get; set; } = string.Empty;
        public int expires_in { get; set; }
    }
}