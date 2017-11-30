using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjaxTableData
{
    public class EmployeeDBContext: DbContext
    {
        public EmployeeDBContext() : base("EmployeeConnectionString")
        {

        }
        public DbSet<Employees> Employees { set; get; }
    }
}
