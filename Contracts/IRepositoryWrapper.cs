using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi2.Contracts
{
    public interface IRepositoryWrapper 
    {
        IEmployeeRepository employee { get; }
        ISkillRepository skill { get; }

        IUserRepository user { get; }
        Task save();
    }
}