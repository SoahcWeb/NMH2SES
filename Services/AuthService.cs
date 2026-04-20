namespace NHM.Services;

public class AuthService
{
    private bool isAuthenticated;

    public bool Login(string username, string password)
    {
        isAuthenticated =
            !string.IsNullOrWhiteSpace(username) &&
            !string.IsNullOrWhiteSpace(password);

        return isAuthenticated;
    }

    public void Logout()
    {
        isAuthenticated = false;
    }

    public bool IsAuthenticated()
    {
        return isAuthenticated;
    }
}
