using NHM.Services;
using Xunit;

namespace NHM.Tests;

public class AuthServiceTests
{
    [Fact]
    public void Login_ValidCredentials_ReturnsTrue()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        var result = authService.Login("alice", "password");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Login_ValidCredentials_AuthenticatesUser()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        authService.Login("alice", "password");

        // Assert
        Assert.True(authService.IsAuthenticated());
    }

    [Fact]
    public void Login_EmptyCredentials_ReturnsFalse()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        var result = authService.Login(string.Empty, string.Empty);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Login_EmptyCredentials_DoesNotSetCurrentUser()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        authService.Login(string.Empty, string.Empty);

        // Assert
        Assert.Null(authService.GetCurrentUser());
    }

    [Fact]
    public void Login_WrongPassword_ReturnsFalse()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        var result = authService.Login("alice", "wrong-password");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Logout_AfterLogin_UserIsNotAuthenticated()
    {
        // Arrange
        var authService = new AuthService();
        authService.Login("alice", "password");

        // Act
        authService.Logout();

        // Assert
        Assert.False(authService.IsAuthenticated());
    }

    [Fact]
    public void Logout_WithoutLogin_CurrentUserRemainsNull()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        authService.Logout();

        // Assert
        Assert.Null(authService.GetCurrentUser());
    }

    [Fact]
    public void GetCurrentUser_WhenNotLoggedIn_ReturnsNull()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        var currentUser = authService.GetCurrentUser();

        // Assert
        Assert.Null(currentUser);
    }

    [Fact]
    public void GetCurrentUser_UsernameContainsOuterSpaces_ReturnsTrimmedUsername()
    {
        // Arrange
        var authService = new AuthService();

        // Act
        authService.Login(" alice ", "password");
        var currentUser = authService.GetCurrentUser();

        // Assert
        Assert.Equal("alice", currentUser);
    }

    [Fact]
    public void Login_MultipleValidCalls_StoresMostRecentUser()
    {
        // Arrange
        var authService = new AuthService();
        authService.Login("alice", "password");

        // Act
        authService.Login("bob", "password");
        var currentUser = authService.GetCurrentUser();

        // Assert
        Assert.Equal("bob", currentUser);
    }
}
