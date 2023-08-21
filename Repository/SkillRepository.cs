using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi2.Contracts;
using BackendApi2.Entities;
using BackendApi2.Entities.Models;

namespace BackendApi2.Repository
{
    public class SkillRepository : RepositoryBase<Skill>,ISkillRepository
    {
        public SkillRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void DeleteSkill(Skill skill)
        {
           Delete(skill);
        }

        public Skill GetSkillsByid(int skillId)
        {
            return FindByCondition(c=>c.SkillId.Equals(skillId)).First();
        }
    }
}