using System;
using System.Collections.Generic;

namespace BackendApi2.Entities.Models
{
    public partial class Employee
    {
       public Employee()
        {
            Skills = new HashSet<Skill>();
        }

        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Confirmemail { get; set; }
        
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
