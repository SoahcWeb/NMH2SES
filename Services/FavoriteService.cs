using System.Text.Json;
using NHM.Models;

namespace NHM.Services;

public class FavoriteService
{
    private const string FileName = "favorites_movies.json";

    private readonly AuthService _authService;

    // utilisateur -> liste de films
    private readonly Dictionary<string, List<Movie>> _favoritesByUser = new();

    public FavoriteService(AuthService authService)
    {
        _authService = authService;
        Load();
    }

    private string? CurrentUser => _authService.GetCurrentUser();

    public List<Movie> GetAll()
    {
        if (CurrentUser == null)
            return new List<Movie>();

        if (!_favoritesByUser.ContainsKey(CurrentUser))
            _favoritesByUser[CurrentUser] = new List<Movie>();

        return _favoritesByUser[CurrentUser];
    }

    public void Add(Movie movie)
    {
        if (CurrentUser == null)
            return;

        if (!_favoritesByUser.ContainsKey(CurrentUser))
            _favoritesByUser[CurrentUser] = new List<Movie>();

        var userFavorites = _favoritesByUser[CurrentUser];

        if (userFavorites.Any(x => x.Id == movie.Id))
            return;

        userFavorites.Add(movie);
        Save();
    }

    public void Remove(int id)
    {
        if (CurrentUser == null)
            return;

        if (!_favoritesByUser.ContainsKey(CurrentUser))
            return;

        var userFavorites = _favoritesByUser[CurrentUser];

        var movie = userFavorites.FirstOrDefault(x => x.Id == id);

        if (movie != null)
        {
            userFavorites.Remove(movie);
            Save();
        }
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_favoritesByUser);
        File.WriteAllText(FileName, json);
    }

    private void Load()
    {
        if (!File.Exists(FileName))
            return;

        var json = File.ReadAllText(FileName);
        var data = JsonSerializer.Deserialize<Dictionary<string, List<Movie>>>(json);

        if (data != null)
        {
            foreach (var item in data)
            {
                _favoritesByUser[item.Key] = item.Value;
            }
        }
    }
}