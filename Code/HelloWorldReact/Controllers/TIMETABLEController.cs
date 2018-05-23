//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using IS.uni;
//using IS.Sess;
//using IS.Base;
//using System.Data;
//using System.Data.SqlTypes;
//using System.Data.SqlClient;

//namespace HelloWorldReact.Controllers
//{

//    public class TIMETABLEController : Controller
//    {

//        session ses = new session();

//        //using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=Northwind;Integrated Security=SSPI"))
//        //{
//        //    conn.Open();

//        //    // 1.  create a command object identifying the stored procedure
//        //    SqlCommand cmd = new SqlCommand("inthoikhoabieu", conn);

//        //    // 2. set the command object so it knows to execute a stored procedure
//        //    cmd.CommandType = CommandType.StoredProcedure;

//        //    // 3. add parameter to command, which will be passed to the stored procedure
//        //    cmd.Parameters.Add(new SqlParameter("@semester", 1));

//        //    // execute the command
//        //    using (SqlDataReader rdr = cmd.ExecuteReader())
//        //    {
//        //        // iterate through results, printing each to console
//        //    }
//        //}
//        //}
//        public JsonResult getlist(string keysearchCodeView, string keysearchName)
//        {
//            List<TIMETABLE_OBJ> li = null;
//            //Không trả về dữ liệu khi chưa đăng nhập
//            if (ses.func("ADMINDIRE") <= 0)
//            {
//                return Json(new
//                {
//                    data = li,//Danh sách
//                    total = 0,//số lượng trang
//                    parent = "",//đơn vị cấp trên
//                    startindex = 1, //bắt đầu số trang
//                    ret = -1//error
//                }, JsonRequestBehavior.AllowGet);

//            }
//            //Khai báo lấy dữ liệu
//            TIMETABLE_BUS bus = new TIMETABLE_BUS();
//            List<spParam> lipa = new List<spParam>();
//            //Thêm điều kiện lọc theo codeview nếu có nhập
//            //if (keysearchCodeView != "")
//            //{
//            //    lipa.Add(new spParam("CODEVIEW", System.Data.SqlDbType.VarChar, keysearchCodeView,1));//search on codeview
//            //}
//            ////Thêm phần điều kiện lọc theo tên nếu có nhập
//            //if (keysearchName != "")
//            //{
//            //    lipa.Add(new spParam("NAME", System.Data.SqlDbType.NVarChar, keysearchName,1));//search on codeview
//            //}
//            //Lọc đơn vị cấp trên; '' sẽ là không co đơn vị cấp trên
//            //lipa.Add(new fieldpara("UNIVERSITYCODE", ses.gUNIVERSITYCODE, 0));
//            //lipa.Add(new fieldpara("LANGUAGECODE", ses.getLang(), 0));
//            int countpage = 0;
//            //order by theorder, with pagesize and the page
//            li = bus.getAll(lipa.ToArray());
//            bus.CloseConnection();
//            //Chỉ số đầu tiên của trang hiện tại (đã trừ -1)
//            //Trả về client
//            return Json(new
//            {
//                data = li,//Danh sách
//                total = countpage,//số lượng trang
//                ret = 0//ok
//            }, JsonRequestBehavior.AllowGet);
//        }
//        //public JsonResult delete(string id)
//        //{
//        //    if (ses.func("ADMINDIRE") <= 0)
//        //    {
//        //        return Json(new { sussess = -3 }, JsonRequestBehavior.AllowGet);

//        //    }

//        //    int ret = 0;
//        //    TIMETABLE_BUS bus = new TIMETABLE_BUS();
//        //    TIMETABLE_OBJ obj = bus.GetByID(new TIMETABLE_OBJ.BusinessObjectID(id));
//        //    //Kiểm tra đối tượng còn trên srrver hay không
//        //    if (obj == null)
//        //    {
//        //        ret = -1;
//        //    }
//        //    //     Kiểm tra thuộc đơn vị triển khai

//        //    //if (ret >= 0)
//        //    //{
//        //    //    STUDENT_BUS bus_news = new STUDENT_BUS();
//        //    //    //check children
//        //    //    ret = bus_news.checkCode(null, new fieldpara("RELIGIONCODE", id));
//        //    //    bus_news.CloseConnection();
//        //    //    //exist children
//        //    //    if (ret > 0)
//        //    //    {
//        //    //        ret = -2;
//        //    //    }
//        //    //}
//        //    if (ret >= 0)
//        //    {
//        //        obj._ID.CODE = obj.CODE;
//        //        //xóa
//        //        ret = bus.Delete(obj._ID);
//        //    }
//        //    //close connection
//        //    bus.CloseConnection();
//        //    return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);
//        //}
//        //[HttpPost]
//        //// <summary>
//        //// Cập nhật một bản ghi được gửi lên từ phía client
//        //// </summary>
//        //public JsonResult update(TIMETABLE_OBJ obj, string keysearchCodeView, string keysearchName)
//        //{
//        //    if (ses.func("ADMINDIRE") <= 0)
//        //    {
//        //        return Json(new { sussess = -3 }, JsonRequestBehavior.AllowGet);

//        //    }
//        //    TIMETABLE_BUS bus = new TIMETABLE_BUS();
//        //    int ret = 0;
//        //    int add = 0;

//        //    TIMETABLE_OBJ obj_temp = null;
//        //kiểm tra tồn tại cho trường hợp sửa
//        //if (!string.IsNullOrEmpty(obj.CODE))//edit
//        //{
//        //    obj_temp = bus.GetByID(new TIMETABLE_OBJ.BusinessObjectID(obj.CODE));
//        //    //if(obj_temp == null || obj_temp.UNIVERSITYCODE!=ses.gUNIVERSITYCODE)
//        //    //{
//        //    //    ret=-4;
//        //    //}
//        //}
//        //else
//        //{
//        //    obj_temp = new TIMETABLE_OBJ();
//        //}

//        //if (ret < 0)
//        //{
//        //    //đóng kết nối trước khi trả về
//        //    bus.CloseConnection();
//        //    //ban ghi sửa đã bị xóa
//        //    return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);

//        //}
//        //hết kiểm tra tồn tại bản ghi
//        //obj_temp.EDITTIME = DateTime.Now;//Thời điểm sủa bản ghi
//        //obj_temp.EDITUSER = ses.loginCode;//Người sửa bản ghi

//        //obj_temp.CODEVIEW = obj.CODEVIEW;
//        //obj_temp.NAME = obj.NAME;
//        //obj_temp.NOTE = obj.NOTE;
//        //obj_temp.LOCK = obj.LOCK;
//        //obj_temp.THEORDER = obj.THEORDER;
//        //obj_temp.COMPARELEVEL = obj.COMPARELEVEL;
//        //obj_temp.THETYPE = "TIMETABLE";
//        //obj_temp.WHOIS = "";
//        //obj_temp.UNIVERSITYCODE = obj.UNIVERSITYCODE;
//        //obj_temp.LANG = obj.LANG;
//        //Kiểm tra tình trạng sửa hay là thêm mới
//        //    if (string.IsNullOrEmpty(obj.CODE))
//        //    {
//        //        //Thêm mới
//        //        add = 1;
//        //        //Sinh mã
//        //        obj_temp.CODE = bus.genNextCode(obj);
//        //        obj_temp.LOCK = 0;
//        //        obj_temp.LOCKDATE = DateTime.Now;
//        //    }
//        //    if (add == 1)
//        //    {

//        //        ret = bus.Insert(obj_temp);

//        //    }
//        //    else
//        //    {
//        //        //gán _ID để xác định bản ghi sẽ được cập nhật
//        //        obj_temp._ID.CODE = obj.CODE;
//        //        if (obj_temp.LOCKDATE < SqlDateTime.MinValue.Value)
//        //        {
//        //            obj_temp.LOCKDATE = SqlDateTime.MinValue.Value;
//        //        }
//        //        ret = bus.Update(obj_temp);
//        //    }

//        //    bus.CloseConnection();

//        //    //some thing like that
//        //    return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);
//        //}
//    }
//}