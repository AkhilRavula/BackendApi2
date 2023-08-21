using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi2.Entities.DTOModels
{
    public class EmployeeForCreateDto
    {
        public string Fullname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Confirmemail { get; set; }     

        public List<SkillCreationDto> Skills { get; set; } =  null!;
    }
}