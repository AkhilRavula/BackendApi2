using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackendApi2.Contracts;
using BackendApi2.Entities.DTOModels;
using BackendApi2.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {

        private IRepositoryWrapper _repositoryWrapper;

        private IMapper _mapper;
        public EmployeesController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;

            _mapper = mapper;
        }


        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetALLEmployees()
        {
            IEnumerable<Employee> emps = await _repositoryWrapper.employee.GetEmployees();
            var _emps = _mapper.Map<IEnumerable<EmployeeDTO>>(emps);
            return Ok(_emps);
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeById(int id)
        {

            var employee = await _repositoryWrapper.employee.GetEmployeeById(id);
            if (employee is null)
            {
                return NotFound();
            }
            else
            {
                var empdto = _mapper.Map<EmployeeDTO>(employee);
                return Ok(empdto);
            }

        }


        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeForCreateDto employeeDTO)
        {

            if (employeeDTO is null)
                return BadRequest("Employee information is in correct");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = _mapper.Map<Employee>(employeeDTO);
            _repositoryWrapper.employee.CreateEmployee(employee);
            _repositoryWrapper.save();
            return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employeeDTO);


        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployeeId(int id, [FromBody] EmployeeForCreateDto employee)
        {
            if (employee is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var _employee = await _repositoryWrapper.employee.GetEmployeeById(id);

            if (_employee is null)
            {
                return NotFound();
            }
            _mapper.Map(employee, _employee);

            _repositoryWrapper.employee.UpdateEmployeeById(_employee);

            await _repositoryWrapper.save();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployeById(int id)
        {
            var emp = await _repositoryWrapper.employee.GetEmployeeById(id);
            if (emp is null)
                return NotFound();
            _repositoryWrapper.employee.DeleteEmployeeById(emp);
            await _repositoryWrapper.save();
            return NoContent();
        }


    }
}