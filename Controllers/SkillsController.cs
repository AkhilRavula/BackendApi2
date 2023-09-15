using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackendApi2.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi2.Controllers
{
    [ApiController]
    [Route("api/Employees/[controller]")]
    public class SkillsController : ControllerBase
    {

         private IRepositoryWrapper _repositoryWrapper;

        private IMapper _mapper;
        public SkillsController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;

            _mapper = mapper;
        }

         [HttpDelete("{skillsId:int}")]
         public async Task<IActionResult> DeleteSkills(int skillsId)
        {
           var skill= await _repositoryWrapper.skill.GetSkillsByid(skillsId);
            _repositoryWrapper.skill.DeleteSkill(skill);

            await _repositoryWrapper.save();

            return NoContent();
        }
    }
}