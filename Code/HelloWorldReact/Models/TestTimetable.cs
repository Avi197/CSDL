using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using IS.Base;

namespace HelloWorldReact.Model
{
    public class Timetable
    {
        [Display(Name = "Tên môn học")]
        public string Tenmonhoc { set; get; }
        [Display(Name = "STC")]
        public int STC { set; get; }
        [Display(Name = "Lớp môn học")]
        public string Lopmonhoc { set; get; }
        [Display(Name = "Tên giảng viên")]
        public string TenGV { set; get; }
        [Display(Name = "Thứ")]
        public string Thu { set; get; }
        [Display(Name = "Tiết")]
        public string Tiet { set; get; }
        [Display(Name = "Giảng đường")]
        public string room { set; get; }
        [Display(Name = "Số sinh viên")]
        public int Sosv { set; get; }
        [Display(Name = "Số SVĐK")]
        public int Sosvdk { set; get; }
        public string ID { set; get; }
    }
    class TestTimetable
    {
        DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        public TestTimetable()
        {
        }
        public List<Timetable> getAll()
        {
            string sql = "exec inthoikhoabieu @semester=1";
            SqlCommand cm = new SqlCommand();
            List<Timetable> strlist = new List<Timetable>();
            SqlConnection con = db.getConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql,con);
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            Timetable strLH;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strLH = new Timetable();
                strLH.Tenmonhoc = dt.Rows[i]["mon hoc"].ToString();
                strLH.STC = Convert.ToInt32(dt.Rows[i]["STC"].ToString());
                strLH.Lopmonhoc = dt.Rows[i]["lop mon hoc"].ToString();
                strLH.TenGV = dt.Rows[i]["ten giang vien"].ToString();
                strLH.Thu = dt.Rows[i]["thu"].ToString();
                strLH.Tiet = dt.Rows[i]["tiet"].ToString();
                strLH.room = dt.Rows[i]["giang duong"].ToString();
                strLH.Sosv = Convert.ToInt32(dt.Rows[i]["so SV"].ToString());
                strLH.Sosvdk = Convert.ToInt32(dt.Rows[i]["so SVDK"].ToString());
                strlist.Add(strLH);
            }
            return strlist;
        }
    }
}