using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNewTemplate.Models;
using MyNewTemplate.ViewModel;

namespace MyNewTemplate.Controllers
{
    public class HomeController : Controller
    {
        newDataEntities db = new newDataEntities();
        MyModel vm = new MyModel();
        public ActionResult Index()
        {

            vm._employer = UserLogController.logEmp;
            vm._gender = db.Genders.ToList();
            vm._department = db.Departaments.ToList();
            vm._leave_status = db.Leave_status.ToList();
            vm._leave_type = db.Leave_type.ToList();
            vm._leave_app = db.Leave_App.ToList();
            vm._award = db.Awards.Where(a=>a.award_emp_id == UserLogController.logEmp.id).ToList();
            vm._holiday = db.Holidays.ToList();
            vm._attend = db.Attendences.ToList();
            vm._notice = vm._notice = (from s in db.Notice_Board
                                       orderby s.id descending
                                       select s).ToList();

            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(Leave_App leave_App)
        {
            vm._employer = UserLogController.logEmp;
            vm._gender = db.Genders.ToList();
            vm._department = db.Departaments.ToList();
            vm._leave_status = db.Leave_status.ToList();
            vm._leave_type = db.Leave_type.ToList();
            vm._leave_app = db.Leave_App.ToList();
            vm._award = db.Awards.Where(a => a.award_emp_id == UserLogController.logEmp.id).ToList();
            vm._holiday = db.Holidays.ToList();
            vm._attend = db.Attendences.ToList();
            vm._notice = (from s in db.Notice_Board
                          orderby s.id descending
                          select s).ToList();
            
      
            leave_App.leave_emp_id = vm._employer.id;
            leave_App.leave_status_id = 1;
            db.Leave_App.Add(leave_App);
            db.SaveChanges();
            return View(vm);
        }

        [HttpPost]

        public ActionResult Changepass(string oldpass,string newpass)
        {
            Employee employee = db.Employees.Find(UserLogController.logEmp.id);

            if (oldpass == employee.emp_password)
            {

                employee.emp_password = newpass;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        public ActionResult Leave()
        {
          
            vm._gender = db.Genders.ToList();
            vm._department = db.Departaments.ToList();
      
            vm._leave_type = db.Leave_type.ToList();
            vm._leave_app = db.Leave_App.ToList();
            vm._award = db.Awards.ToList();
            vm._holiday = db.Holidays.ToList();
            vm._attend = db.Attendences.ToList();
            vm._notice = db.Notice_Board.ToList();


            vm._employer = UserLogController.logEmp;
            vm._leave_status = db.Leave_status.Where(l => l.id == UserLogController.logEmp.id).ToList();
            



            return View(vm);
        }




      
    }
}