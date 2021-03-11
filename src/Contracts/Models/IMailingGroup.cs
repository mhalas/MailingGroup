namespace Contracts.Models
{
    public interface IMailingGroup
    {
        int Id { get; set; }
        string Name { get; set; }
        int SystemUserId { get; set; }

        ISystemUser SystemUser { get; set; }
    }
}
