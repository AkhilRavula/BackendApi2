using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi2.Entities.Models;
using BackendApi2.Repository;

namespace BackendApi2.Contracts
{
    public interface ISkillRepository : IRepositoryBase<Skill>
    {
        void DeleteSkill(Skill skill);  

        Skill GetSkillsByid(int skillId);
    }
}