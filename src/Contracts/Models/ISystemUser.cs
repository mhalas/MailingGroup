namespace Contracts.Models
{
    public interface ISystemUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        byte[] Salt { get; set; }
    }
}
