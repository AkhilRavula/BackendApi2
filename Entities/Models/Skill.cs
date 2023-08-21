using System;
using System.Collections.Generic;

namespace BackendApi2.Entities.Models
{
    public partial class Skill
    {
      public int SkillId { get; set; }
        public string? Skillname { get; set; }
        public int? Experience { get; set; }
        public string? Proficiency { get; set; }
        public int EmpId { get; set; }

        public virtual Employee Emp { get; set; } = null!;
    }
}
