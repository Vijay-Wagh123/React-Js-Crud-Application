using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactCrud.Data;
using ReactCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ReactCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeController(EmployeeContext _employeeContext)
        {
            this._employeeContext = _employeeContext;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            if (_employeeContext.Employees == null)
            {
                return NotFound();
            }

            return await _employeeContext.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_employeeContext.Employees == null)
            {
                return NotFound();
            }

            var employee=await _employeeContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmploye(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            await _employeeContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> PutEmployee(int id,Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("ID mismatch");
            }
            _employeeContext.Entry(employee).State = EntityState.Modified;
            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(employee);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if (_employeeContext.Employees == null)
            {
                return NotFound();
            }

            var employee = await _employeeContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();
            return Ok();
        }

    }
}
