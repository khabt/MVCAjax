using AjaxTableData;
using MvcAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcAjax.Controllers
{
    public class HomeController : Controller
    {
        #region database
        //NorthwindEntities1 db = new NorthwindEntities1();
        private EmployeeDBContext _context;
        public HomeController()
        {
            _context = new EmployeeDBContext();
        }
        #region data hardcode
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
        #endregion
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadDataFromData(int page, int pageSize)
        {
            //var model = db.Employees.Skip((page - 1) * pageSize).Take(pageSize);
            //int totalRow = db.Employees.Count();

            //List<Employee> ListEmp = (from Employee in db.Employees
            //                          select Employee).ToList();
            //int totalRow = ListEmp.Count;
            //var model = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);
            //ListEmp = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);

            //ListEmp = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);
            //int totalRow = ;

            //var ListEmp = db.Employees.Skip((page - 1) * pageSize).Take(pageSize);
            //ListEmp.len;

            var model = _context.Employees.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = _context.Employees.Count();

            return Json(new
            {
                data = model,
                status = true,
                total = totalRow
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadData(string name, string status, int page, int pageSize)
        {
            //var model = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);
            //int totalRow = ListEmp.Count;
            IQueryable<Employees> model = _context.Employees;

            if (!string.IsNullOrEmpty(name)) {
                model = model.Where(x => x.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(status)) {
                var statusBool = bool.Parse(status);
                model = model.Where(x => x.Status == statusBool);
            }
            int totalRow = model.Count();

            model = model.OrderByDescending(x =>x.ID).
                Skip((page - 1) * pageSize)
                .Take(pageSize);
            

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
            Employees emp = serializer.Deserialize<Employees>(model);

            //save db
            var entity = _context.Employees.Find(emp.ID);
            entity.Salary = emp.Salary;
            _context.SaveChanges();
            return Json(new
            {
                status = true
            });
        }

        //[HttpPost]
        //public JsonResult UpdateName(string model)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    Employee emp = serializer.Deserialize<Employee>(model);

        //    //save db
        //    var entity = db.Employees.Single(x => x.EmployeeID == emp.EmployeeID);
        //    entity.FirstName = emp.FirstName;
        //    entity.LastName = emp.LastName;
        //    //db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    return Json(new
        //    {
        //        status = true
        //    });
        //}

        [HttpPost]
        public JsonResult SaveData(string StrEmployee)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Employees emp = serializer.Deserialize<Employees>(StrEmployee);

            bool status = false;
            string message = string.Empty;
            //add new db
            if (emp.ID == 0)
            {
                emp.CreateDate = DateTime.Now;
                _context.Employees.Add(emp);
                try
                {
                    _context.SaveChanges();
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }
            }
            else
            {
                //save db
                var entity = _context.Employees.Find(emp.ID);
                entity.Name = emp.Name;
                entity.Salary = emp.Salary;
                entity.Status = emp.Status;
                try
                {
                    _context.SaveChanges();
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }

            }

            return Json(new
            {
                status = status,
                message = message
            });
        }

        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            //var model = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);
            //int totalRow = ListEmp.Count;

            var model = _context.Employees.Find(id);

            return Json(new
            {
                data = model,                
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            //var model = ListEmp.Skip((page - 1) * pageSize).Take(pageSize);
            //int totalRow = ListEmp.Count;

            var model = _context.Employees.Find(id);
            _context.Employees.Remove(model);
            try
            {
                _context.SaveChanges();
                return Json(new
                {                    
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
            
        }
    }
}