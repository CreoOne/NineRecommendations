namespace NineRecommendations.Spotify.External
{
    public class SearchQueryBuilder
    {
        private List<string> Types { get; } = new();

        private List<string> Tags { get; } = new();

        public SearchQueryBuilder SetGenre(string? genre)
        {
            if(!string.IsNullOrWhiteSpace(genre))
                Tags.Add($"genre:{genre}");

            return this;
        }

        public SearchQueryBuilder SetYears(Range years)
        {
            Tags.Add($"year:{years.Start}-{years.End}");

            return this;
        }

        public SearchQueryBuilder SetTag(string? tag)
        {
            if (!string.IsNullOrWhiteSpace(tag))
                Tags.Add($"tag:{tag}");

            return this;
        }

        public SearchQueryBuilder AddType(string? type)
        {
            if (!string.IsNullOrWhiteSpace(type))
                Types.Add(type);

            return this;
        }

        public async Task<string> Build()
        {
            var parameters = new Dictionary<string, string>();

            if (Types.Count > 0)
                parameters.Add("type", string.Join(',', Types));

            if (Tags.Count > 0)
                parameters.Add("q", string.Join(' ', Tags));

            var formUrlEncoded = new FormUrlEncodedContent(parameters);

            return string.Concat("/v1/search?", await formUrlEncoded.ReadAsStringAsync());
        }
    }
}
