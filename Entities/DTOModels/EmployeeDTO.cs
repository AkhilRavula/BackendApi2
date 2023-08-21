using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi2.Entities.DTOModels
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Confirmemail { get; set; }     

        public List<SkillDTO> Skills { get; set; } =  null!;
    }
}