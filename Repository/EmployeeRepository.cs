
using BackendApi2.Contracts;
using BackendApi2.Entities;
using BackendApi2.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi2.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await FindALLwithRelatedEntities("Skills").OrderBy(c=>c.Id).ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int Empid)
        {
            return await FindByConditionWithRelatedEntities("Skills",c=>c.Id.Equals(Empid)).FirstOrDefaultAsync();
        }

        public void UpdateEmployeeById(Employee employee)
        {
            Update(employee);
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        public void DeleteEmployeeById(Employee employee)
        {
            Delete(employee);
        }
    }
}