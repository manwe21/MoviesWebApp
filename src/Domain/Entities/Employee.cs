using Domain.Common;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {   
        public int Id { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int CreditId { get; set; }
        public Credit Credit { get; set; }
    
    }
}
