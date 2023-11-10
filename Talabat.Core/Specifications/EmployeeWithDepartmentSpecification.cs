using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class EmployeeWithDepartmentSpecification : BaseSpecifications<Employee>
    {
        public EmployeeWithDepartmentSpecification()
        {
            Includes.Add(e => e.Department);
        }

        public EmployeeWithDepartmentSpecification(int id):base(e=>e.Id==id)
        {
            Includes.Add(e=>e.Department);
        }
    }
}
