using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackendApi2.Contracts;
using BackendApi2.Entities.DTOModels;
using BackendApi2.Entities.Models;
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
        public IActionResult GetALLEmployees()
        {
            try
            {

                IEnumerable<Employee> emps = _repositoryWrapper.employee.GetEmployees();
                var _emps = _mapper.Map<IEnumerable<EmployeeDTO>>(emps);
                return Ok(_emps);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeById(int id)
        {
            try
            {
                var employee = _repositoryWrapper.employee.GetEmployeeById(id);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeForCreateDto employeeDTO)
        {
            try
            {
                if (employeeDTO is null)
                    return BadRequest();

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var employee = _mapper.Map<Employee>(employeeDTO);
                _repositoryWrapper.employee.CreateEmployee(employee);
                _repositoryWrapper.save();
                return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employeeDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployeeId(int id, [FromBody] EmployeeForCreateDto employee)
        {
            try
            {


                if (employee is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _employee = _repositoryWrapper.employee.GetEmployeeById(id);

                if (_employee is null)
                {
                    return NotFound();
                }
                _mapper.Map(employee, _employee);

                _repositoryWrapper.employee.UpdateEmployeeById(_employee);

                _repositoryWrapper.save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployeById(int id)
        {
            var emp = _repositoryWrapper.employee.GetEmployeeById(id);
            if (emp is null)
                return NotFound();
            _repositoryWrapper.employee.DeleteEmployeeById(emp);
            _repositoryWrapper.save();
            return NoContent();
        }


    }
}