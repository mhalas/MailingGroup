namespace Contracts.Dto
{
    public class RetrieveEmailAddressDto
    {

        public RetrieveEmailAddressDto(int id, int mailingGroupId, string address)
        {
            Id = id;
            MailingGroupId = mailingGroupId;
            Address = address;
        }

        public int Id { get; }

        public int MailingGroupId { get; }

        public string Address { get; }
    }
}
