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

    public class TimetableController : Controller
    {
        session ses = new session();
        public JsonResult getlist(string searchSemester)
        {
            List<PROVINCE_OBJ> li = null;
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
            PROVINCE_BUS bus = new PROVINCE_BUS();
            List<spParam> lipa = new List<spParam>();
            //Thêm điều kiện lọc theo semester nếu có nhập
            if (searchSemester != "")
            {
                lipa.Add(new spParam("semester", System.Data.SqlDbType.Int, searchSemester, 1));//search on semester
            }

            int countpage = 0;
            //order by theorder, with pagesize and the page
            li = bus.getAll(lipa.ToArray());
            bus.CloseConnection();
            //Chỉ số đầu tiên của trang hiện tại (đã trừ -1)
            //Trả về client
            return Json(new
            {
                data = li,//Danh sách
                total = countpage,//số lượng trang
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);
        }

    }
}