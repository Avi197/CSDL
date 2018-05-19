using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IS.uni;
using IS.Base;
using IS.authen;
using IS.Sess;
using HelloWorldReact.Model;

namespace HelloWorldReact.Controllers
{
    public class HomeController : Controller
    {
        session ses = new session();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult dologin()
        {
            //Kiểm tra mật khâu cơ sở dữ liệu ở đây
            STAFF_INFO staff = new STAFF_INFO();
            staff.CODE = "ABC";
            staff.NAME = "Đã đăng nhập";
            staff.LOGTIME = DateTime.Now;
            ses.login(staff);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult timetable()
        {
            Timetableoutput bus = new Timetableoutput();
            List<Timetable> li = bus.getAll();
            return View(li);
        }
        public ActionResult Coursetime()
        {
            COURSETIME_BUS bus = new COURSETIME_BUS();
            List<COURSETIME_OBJ> li = bus.getAll();
            return View(li);
        }
        //public ActionResult Course()
        //{
        //    COURSE_BUS bus = new COURSE_BUS();
        //    List<COURSE_OBJ> li = bus.getAll();
        //    return View(li);
        //}
        //[CustomAuthentication]
        //public ActionResult Nationreact()
        //{
        //    return View();
        //}
        //public ActionResult Coursetimereact()
        //{
        //    return View();
        //}
        //[CustomAuthentication]
        //public ActionResult Province()
        //{
        //    return View();
        //}
        //[CustomAuthentication]
        //public ActionResult branch()
        //{
        //    return View();
        //}
        //[CustomAuthentication]
        //public ActionResult educationlevel()
        //{
        //    return View();
        //}

        public JsonResult GetName()
        {
            return Json(new { name = "World from server side" }, JsonRequestBehavior.AllowGet);
        }

    }
}