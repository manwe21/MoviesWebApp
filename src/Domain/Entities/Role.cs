using Domain.Common;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }

        public int ActorId { get; set; }
        public Person Actor { get; set; }

        public int CreditId { get; set; }
        public Credit Credit { get; set; }
    }
}
        