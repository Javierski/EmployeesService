namespace Application.Interfaces
{
    public interface IAuthService
    {
        string ValidateUser(string username, string role);
    }
}
