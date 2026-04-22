namespace NHM.Services;

public class AuthService
{
    private string? currentUser;

    // Login simple simulé
    public bool Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            currentUser = null;
            return false;
        }

        currentUser = username.Trim();
        return true;
    }

    // Déconnexion
    public void Logout()
    {
        currentUser = null;
    }

    // Vérifie si un utilisateur est connecté
    public bool IsAuthenticated()
    {
        return currentUser != null;
    }

    // Retourne l'utilisateur connecté
    public string? GetCurrentUser()
    {
        return currentUser;
    }
}
