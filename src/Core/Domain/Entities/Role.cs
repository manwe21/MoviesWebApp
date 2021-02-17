using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Character { get; set; }
        public int Order { get; set; }

        public int ActorId { get; set; }
        public Person Actor { get; set; }

        public int CreditId { get; set; }
        public Credit Credit { get; set; }
    }
}
        