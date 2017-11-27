using MvcAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcAjax.Controllers
{
    public class HomeController : Controller
    {
        NorthwindEntities1 db = new NorthwindEntities1();
        List<EmpModel> ListEmp = new List<EmpModel>() {
            new EmpModel()
            {
                ID = 1,
                Name = "Nguyen Van A",
                Salary = 200,
                Status = true
            },
            new EmpModel()
            {
                ID = 2,
                Name = "Nguyen Van B",
                Salary = 3000000,
                Status = true
            },
            new EmpModel()
            {
                ID = 3,
                Name = "Nguyen Van C",
                Salary = 4000000,
                Status = true
            },
            new EmpModel()
            {
                ID = 4,
                Name = "Nguyen Van D",
                Salary = 3500000,
                Status = true
            },
            new EmpModel()
            {
                ID = 5,
                Name = "Nguyen Van E",
                Salary = 3000000,
                Status = true
            },
            new EmpModel()
            {
                ID = 6,
                Name = "Nguyen Van F",
                Salary = 4000000,
                Status = true
            },
             new EmpModel()
            {
                ID = 7,
                Name = "Nguyen Van G",
                Salary = 3000000,
                Status = true
            },
            new EmpModel()
            {
                ID = 8,
                Name = "Nguyen Van H",
                Salary = 4050000,
                Status = true
            }
        };

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadDataFromData()
        {
            //var ListEmp = from Employee in db.Employees select new
            //{
            //    EmployeeID = Employee.EmployeeID,
            //    LastName = Employee.LastName,
            //    FirstName = Employee.FirstName,
            //    Title = Employee.Title,
            //    Sex = Employee.TitleOfCourtesy,
            //    BirthDate = Employee.BirthDate.ToString(),
            //    HireDate = Employee.HireDate.ToString()                 
            //};
            //var model = db.Employees
            //ListEmp = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);
            //int totalRow = ListEmp.Count;

            return Json(new
            {
                data = ListEmp,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadData(int page, int pageSize)
        {
            var model = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = ListEmp.Count;
            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            EmpModel emp = serializer.Deserialize<EmpModel>(model);

            //save db
            var entity = ListEmp.Single(x => x.ID == emp.ID);
            entity.Salary = emp.Salary;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult UpdateName(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Employee emp = serializer.Deserialize<Employee>(model);

            //save db
            var entity = db.Employees.Single(x => x.EmployeeID == emp.EmployeeID);
            entity.FirstName = emp.FirstName;
            entity.LastName = emp.LastName;
            //db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }
    }
}