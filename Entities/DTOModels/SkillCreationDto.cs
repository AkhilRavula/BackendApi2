using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi2.Entities.DTOModels
{
    public class SkillCreationDto
    {
        public string? Skillname { get; set; }
        public int? Experience { get; set; }
        public string? Proficiency { get; set; }
        public int? SkillId { get; set; }
    }
}