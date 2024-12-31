using Microsoft.EntityFrameworkCore;
using ReactCrud.Models;

namespace ReactCrud.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext>options):base(options)
        {

            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
