using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _employeeRepo;
        public EmployeeController(IGenericRepository<Employee> EmployeeRepo)
        {
            _employeeRepo = EmployeeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartmentSpecification();
            var employees = await _employeeRepo.GetAllWithSpecAsync(spec);
            return Ok(employees);
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult <Employee>> GetEmployeeById(int id)
        {
            var spec = new EmployeeWithDepartmentSpecification(id);
            var employee = _employeeRepo.GetByIdWithSpecAsync(spec);
            return Ok(employee);
        }

    }
}
