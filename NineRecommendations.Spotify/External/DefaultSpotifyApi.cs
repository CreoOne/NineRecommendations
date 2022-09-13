using NineRecommendations.Spotify.External.Models;
using NineRecommendations.Spotify.External.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NineRecommendations.Spotify.External
{
    public class DefaultSpotifyApi : ISpotifyApi
    {
        private IHttpClientFactory HttpClientFactory { get; }
        private SpotifyOptions SpotifyOptions { get; }

        public DefaultSpotifyApi(IHttpClientFactory httpClientFactory, SpotifyOptions spotifyOptions)
        {
            HttpClientFactory = httpClientFactory;
            SpotifyOptions = spotifyOptions;
        }

        public async Task<RootObject?> CallSearchAsync(SearchOptions searchOptions)
        {
            var httpClient = await CreateHttpClient();
            var uri = await ConstructUriAsync(searchOptions);
            var result = await httpClient.GetAsync(uri);

            var text = await result.Content.ReadAsStringAsync();

            using var content = await result.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<RootObject>(content);
        }

        private async Task<string> ConstructUriAsync(SearchOptions searchOptions)
        {
            var uri = new StringBuilder("/v1/search?");
            var parameters = new Dictionary<string, string> 
            {
                { "type", "track" }
            };

            var query = new StringBuilder();
            query.Append("genre:");
            query.Append(searchOptions.Genre);
            query.Append(' ');

            query.Append("year:");
            query.Append(searchOptions.Year.Start);
            query.Append('-');
            query.Append(searchOptions.Year.End);

            if(!string.IsNullOrEmpty(searchOptions.Unique))
            {
                query.Append(' ');
                query.Append("tag:");
                query.Append(searchOptions.Unique);
            }

            var queryString = query.ToString();

            if (!string.IsNullOrEmpty(queryString))
                parameters.Add("q", queryString);

            var formUrlEncoded = new FormUrlEncodedContent(parameters);
            uri.Append(await formUrlEncoded.ReadAsStringAsync());
            return uri.ToString();
        }

        private async Task<HttpClient> CreateHttpClient()
        {
            var authToken = await GetAuthorizationTokenAsync(SpotifyOptions);

            var httpClient = HttpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authToken.TokenType, authToken.Token);

            return httpClient;
        }

        private readonly HttpContent AuthPostContent = new FormUrlEncodedContent(new Dictionary<string, string> { { "grant_type", "client_credentials" } });

        private async Task<AccessToken> GetAuthorizationTokenAsync(SpotifyOptions spotifyOptions)
        {
            var httpClient = HttpClientFactory.CreateClient();
            var concatenated = string.Concat(spotifyOptions.ClientId, ":", spotifyOptions.ClientSecret);
            var base64EncodedSecrets = Convert.ToBase64String(Encoding.ASCII.GetBytes(concatenated));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedSecrets);    
            var response = await httpClient.PostAsync("https://accounts.spotify.com/api/token", AuthPostContent);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<AccessToken>(responseStream) ?? throw new InvalidOperationException(); // this is not the proper way of handling errors of this kind
        }
    }
}
