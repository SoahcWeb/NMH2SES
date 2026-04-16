using System.Text.Json;
using NHM.Models;

namespace NHM.Services;

public class FavoriteService
{
    private const string FileName = "favorites_movies.json";

    private readonly List<Movie> _favorites = new();

    public void Add(Movie movie)
    {
        if (_favorites.Any(x => x.Id == movie.Id))
            return;

        _favorites.Add(movie);
        Save();
    }

    public void Remove(int id)
    {
        var movie = _favorites.FirstOrDefault(x => x.Id == id);

        if (movie != null)
        {
            _favorites.Remove(movie);
            Save();
        }
    }

    public List<Movie> GetAll()
    {
        return _favorites;
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_favorites);
        File.WriteAllText(FileName, json);
    }
}