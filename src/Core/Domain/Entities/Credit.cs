using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Entities
{
    [Table("Credits")]
    public class Credit : BaseEntity
    {
        public List<Role> Cast { get; set; }
        public List<Employee> Crew { get; set; }

        public Movie Movie { get; set; }
    }
}
