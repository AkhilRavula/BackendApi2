
using BackendApi2.Contracts;
using BackendApi2.Entities;
using BackendApi2.Entities.Models;

namespace BackendApi2.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return FindALLwithRelatedEntities("Skills").OrderBy(c=>c.Id).ToList();
        }

        public Employee GetEmployeeById(int Empid)
        {
            return FindByConditionWithRelatedEntities("Skills",c=>c.Id.Equals(Empid)).First();
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