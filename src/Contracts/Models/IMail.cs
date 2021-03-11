namespace Contracts.Models
{
    public interface IMail
    {
        int Id { get; set; }
        string Address { get; set; }
        int MailingGroupId { get; set; }

        IMailingGroup MailingGroup { get; set; }
    }
}
