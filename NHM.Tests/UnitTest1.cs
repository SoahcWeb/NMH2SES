using NHM.Services;
using Xunit;

namespace NHM.Tests;

public class AuthServiceTests
{
    [Fact]
    public void Login_WithValidCredentials_AuthenticatesUser()
    {
        var authService = new AuthService();

        var loginSucceeded = authService.Login(" alice ", "password");

        Assert.True(loginSucceeded);
        Assert.True(authService.IsAuthenticated());
        Assert.Equal("alice", authService.GetCurrentUser());
    }

    [Fact]
    public void Logout_ClearsAuthenticationState()
    {
        var authService = new AuthService();
        authService.Login("alice", "password");

        authService.Logout();

        Assert.False(authService.IsAuthenticated());
        Assert.Null(authService.GetCurrentUser());
    }
}
