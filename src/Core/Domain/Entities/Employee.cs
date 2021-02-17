using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class Employee : BaseEntity
    {   
        public string Job { get; set; }
        public string Department { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int CreditId { get; set; }
        public Credit Credit { get; set; }
    
    }
}
