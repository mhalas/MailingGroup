namespace Contracts.Dto
{
    public class RetrieveMailingGroupDto
    {
        public RetrieveMailingGroupDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
