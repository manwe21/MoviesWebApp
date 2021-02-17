using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Domain.Entities
{
    public class Person : BaseEntity
    {
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
