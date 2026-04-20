using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NHM;
using NHM.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient pour TMDB
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://api.themoviedb.org/3/")
});

// Services existants
builder.Services.AddScoped<TmdbService>();
builder.Services.AddScoped<FavoriteService>();

// Auth service (simulation login)
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();