namespace Contracts.Dto
{
    public class RetrieveMailDto
    {

        public RetrieveMailDto(int id, int mailingGroupId, string address)
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
