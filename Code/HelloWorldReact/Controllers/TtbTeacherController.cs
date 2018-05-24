using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IS.uni;
using IS.Sess;
using IS.Base;

namespace HelloWorldReact.Controllers
{

    public class TtbTeacherController : Controller
    {
        session ses = new session();
        public JsonResult getlist(string keysearchCodeView, string keysearchName)
        {
            List<TTBTEACHER_OBJ> li = null;
            //Không trả về dữ liệu khi chưa đăng nhập
            if (ses.func("ADMINDIRE") <= 0)
            {
                return Json(new
                {
                    data = li,//Danh sách
                    total = 0,//số lượng trang
                    parent = "",//đơn vị cấp trên
                    startindex = 1, //bắt đầu số trang
                    ret = -1//error
                }, JsonRequestBehavior.AllowGet);

            }
            //Khai báo lấy dữ liệu
            TTBTEACHER_BUS bus = new TTBTEACHER_BUS();
            List<spParam> lipa = new List<spParam>();
            if (keysearchCodeView != "")
            {
                lipa.Add(new spParam("CODEVIEW", System.Data.SqlDbType.VarChar, keysearchCodeView, 1));
            }
            if (keysearchName != "")
            {
                lipa.Add(new spParam("NAME", System.Data.SqlDbType.VarChar, keysearchName, 1));
            }
            int countpage = 0;
            //order by theorder, with pagesize and the page
            li = bus.getAll(lipa.ToArray());
            return Json(new
            {
                data = li,//Danh sách
                total = countpage,//số lượng trang
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);
        }
        
    }
}