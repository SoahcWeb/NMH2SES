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

// Auth service (IMPORTANT AVANT FavoriteService)
builder.Services.AddScoped<AuthService>();

// Services existants
builder.Services.AddScoped<TmdbService>();
builder.Services.AddScoped<FavoriteService>();

await builder.Build().RunAsync();