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

    public class RoomController : Controller
    {
        session ses = new session();
        public JsonResult getlist(string keysearchCodeView, string keysearchName)
        {
            List<ROOM_OBJ> li = null;
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
            ROOM_BUS bus = new ROOM_BUS();
            List<spParam> lipa = new List<spParam>();
            //Thêm điều kiện lọc theo codeview nếu có nhập
            if (keysearchCodeView != "")
            {
                lipa.Add(new spParam("CODEVIEW", System.Data.SqlDbType.VarChar, keysearchCodeView, 1));//search on codeview
            }
            //Thêm phần điều kiện lọc theo tên nếu có nhập
            if (keysearchName != "")
            {
                lipa.Add(new spParam("NAME", System.Data.SqlDbType.NVarChar, keysearchName, 1));//search on codeview
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
        public JsonResult delete(string id)
        {
            if (ses.func("ADMINDIRE") <= 0)
            {
                return Json(new { sussess = -3 }, JsonRequestBehavior.AllowGet);

            }

            int ret = 0;
            ROOM_BUS bus = new ROOM_BUS();
            ROOM_OBJ obj = bus.GetByID(new ROOM_OBJ.BusinessObjectID(id));
            //Kiểm tra đối tượng còn trên srrver hay không
            if (obj == null)
            {
                ret = -1;
            }
            
            if (ret >= 0)
            {
                obj._ID.CODE = obj.CODE;
                //xóa
                ret = bus.Delete(obj._ID);
            }
            //close connection
            bus.CloseConnection();
            return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        /// <summary>
        /// Cập nhật một bản ghi được gửi lên từ phía client
        /// </summary>
        public JsonResult update(ROOM_OBJ obj, string keysearchCodeView)
        {
            if (ses.func("ADMINDIRE") <= 0)
            {
                return Json(new { sussess = -3 }, JsonRequestBehavior.AllowGet);

            }
            ROOM_BUS bus = new ROOM_BUS();
            int ret = 0;
            int add = 0;

            ROOM_OBJ obj_temp = null;
            //kiểm tra tồn tại cho trường hợp sửa
            if (!string.IsNullOrEmpty(obj.CODE))//edit
            {
                obj_temp = bus.GetByID(new ROOM_OBJ.BusinessObjectID(obj.CODE));
                
            }
            else
            {
                obj_temp = new ROOM_OBJ();
            }

            if (ret < 0)
            {
                //đóng kết nối trước khi trả về
                bus.CloseConnection();
                //ban ghi sửa đã bị xóa
                return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);

            }
            
            //obj_temp.CODE = obj.CODE;
            obj_temp.NUMBERFLOOR = obj.NUMBERFLOOR;
            obj_temp.CODEVIEW = obj.CODEVIEW;
            obj_temp.BUILDINGCODE = obj.BUILDINGCODE;
            
            //Kiểm tra tình trạng sửa hay là thêm mới
            if (string.IsNullOrEmpty(obj.CODE))
            {
                //Thêm mới
                add = 1;
                //Sinh mã
            }
            if (add == 1)
            {

                ret = bus.Insert(obj_temp);

            }
            else
            {
                //gán _ID để xác định bản ghi sẽ được cập nhật
                obj_temp._ID.CODE = obj.CODE;
                ret = bus.Update(obj_temp);
            }

            bus.CloseConnection();

            //some thing like that
            return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);
        }
    }
}