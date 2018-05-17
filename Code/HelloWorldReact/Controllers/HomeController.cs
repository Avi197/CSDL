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
            staff.NAME="Đã đăng nhập";
            staff.LOGTIME = DateTime.Now;
            ses.login(staff);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult TestTKB()
        {
            //using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=Northwind;Integrated Security=SSPI"))
            //{
            //    conn.Open();

            //    // 1.  create a command object identifying the stored procedure
            //    SqlCommand cmd = new SqlCommand("inthoikhoabieu", conn);

            //    // 2. set the command object so it knows to execute a stored procedure
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    // 3. add parameter to command, which will be passed to the stored procedure
            //    cmd.Parameters.Add(new SqlParameter("@semester", 1));

            //    // execute the command
            //    using (SqlDataReader rdr = cmd.ExecuteReader())
            //    {
            //        // iterate through results, printing each to console
            //    }
            //}
            //db.Database.ExecuteSqlCommand("dbo.sp_docsstatus", false);
            ////Display the report
            //return View(db.NeededDocsReport.ToList());
            TestTimetable bus = new TestTimetable();
            List<Timetable> li = bus.getAll();
            return View(li);
        }
        public ActionResult subjects()
        {
            SUBJECTS_BUS bus = new SUBJECTS_BUS();
            List<SUBJECTS_OBJ> li = bus.getAll();
            return View(li);
        }
        public ActionResult Coursetime()
        {
            COURSETIME_BUS bus = new COURSETIME_BUS();
            List<COURSETIME_OBJ> li = bus.getAll();
            return View(li);
        }
        [CustomAuthentication]
        public ActionResult Nationreact()
        {
            return View();
        }
        public ActionResult Coursetimereact()
        {
            return View();
        }
        [CustomAuthentication]
        public ActionResult Province()
        {
            return View();
        }
        [CustomAuthentication]
        public ActionResult branch()
        {
            return View();
        }
        [CustomAuthentication]
        public ActionResult educationlevel()
        {
            return View();
        }

        public JsonResult GetName()
        {
            return Json(new { name = "World from server side" }, JsonRequestBehavior.AllowGet);
        }
        
	}
}