using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi2.Contracts;
using BackendApi2.Entities;

namespace BackendApi2.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private RepositoryContext _repositoryContext;
        private IEmployeeRepository? _employeeRepository;
        private ISkillRepository? _skillRepository;
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IEmployeeRepository employee
        {
            get{
                if(_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                }
               return _employeeRepository;
            
            }
        }

        public ISkillRepository skill {
            get{

                 if(_skillRepository == null)
                {
                    _skillRepository = new SkillRepository(_repositoryContext);
                }
               return _skillRepository;
            }
        }

        public void save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}