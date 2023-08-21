using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi2.Entities.Models;

namespace BackendApi2.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetEmployees();

        Employee GetEmployeeById(int employeeId);

        void UpdateEmployeeById(Employee employee);

        void CreateEmployee(Employee employee);

        void DeleteEmployeeById(Employee employee);
    }
}