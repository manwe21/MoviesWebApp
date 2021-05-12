using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    [Table("Credits")]
    public class Credit : BaseEntity
    {
        public int Id { get; set; }
        public List<Role> Cast { get; set; }
        public List<Employee> Crew { get; set; }

        public Movie Movie { get; set; }
    }
}
