namespace Contracts.ApiRequests.SystemUser
{
    public interface ICreateSystemUserRequest
    {
        string Username { get; }
        string Password { get; }
    }
}
