using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using NHM.Models;

namespace NHM.Services;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public TmdbService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Tmdb:ApiKey"]
            ?? throw new InvalidOperationException(
                "TMDB API key is missing. Configure Tmdb:ApiKey in wwwroot/appsettings.json.");
    }

    public async Task<IReadOnlyList<Movie>> SearchMoviesByTitleAsync(
        string title,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Array.Empty<Movie>();
        }

        var requestUri =
            $"search/movie?api_key={Uri.EscapeDataString(_apiKey)}&query={Uri.EscapeDataString(title)}";

        var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(requestUri, cancellationToken);

        return response?.Results?.ToArray() ?? Array.Empty<Movie>();
    }

    public Task<Movie?> GetMovieDetailsAsync(
        int movieId,
        CancellationToken cancellationToken = default)
    {
        if (movieId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(movieId));
        }

        var requestUri = $"movie/{movieId}?api_key={Uri.EscapeDataString(_apiKey)}";

        return _httpClient.GetFromJsonAsync<Movie>(requestUri, cancellationToken);
    }

    public async Task<IReadOnlyList<Movie>> GetPopularMoviesAsync(int page = 1)
    {
        if (page <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(page));
        }

        var requestUri =
            $"movie/popular?api_key={Uri.EscapeDataString(_apiKey)}&page={page}";

        var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(requestUri);

        return response?.Results?.ToArray() ?? Array.Empty<Movie>();
    }

    public async Task<IReadOnlyList<Movie>> GetNowPlayingMoviesAsync(int page = 1)
    {
        if (page <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(page));
        }

        var requestUri =
            $"movie/now_playing?api_key={Uri.EscapeDataString(_apiKey)}&page={page}";

        var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(requestUri);

        return response?.Results?.ToArray() ?? Array.Empty<Movie>();
    }

    public async Task<IReadOnlyList<Movie>> GetTrendingMoviesAsync()
    {
        var requestUri = $"trending/movie/day?api_key={Uri.EscapeDataString(_apiKey)}";

        var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(requestUri);

        return response?.Results?.ToArray() ?? Array.Empty<Movie>();
    }

    private sealed class TmdbSearchResponse
    {
        [JsonPropertyName("results")]
        public List<Movie>? Results { get; set; }
    }
}
