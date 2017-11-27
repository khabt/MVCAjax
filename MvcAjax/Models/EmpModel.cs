using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAjax.Models
{
    public class EmpModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public bool Status { set; get; }
    }
}