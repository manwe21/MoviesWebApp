using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string ImagePath { get; set; }

        public List<Role> Roles { get; set; }
        public List<Employee> Jobs { get; set; }

        public Person()
        {
            Roles = new List<Role>();
            Jobs = new List<Employee>();
        }

    }
}
