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
        public class BusinessObjectID
        {
            public BusinessObjectID() { }
            private System.String _CODE;

            public BusinessObjectID(System.String mCODE)
            {
                _CODE = mCODE;

            }

            public System.String CODE
            {
                get { return _CODE; }
                set { _CODE = value; }
            }


            public override bool Equals(object obj)
            {
                if (obj == this) return true;
                if (obj == null) return false;

                BusinessObjectID that = obj as BusinessObjectID;
                if (that == null)
                {
                    return false;
                }
                else
                {
                    if (this.CODE != that.CODE) return false;

                    return true;
                }

            }


            public override int GetHashCode()
            {
                return CODE.GetHashCode();
            }

        }
        public BusinessObjectID _ID;
        //main object
        protected string _codeP = "{yyMMdd}{CCCC}";
        protected string _codePattern
        {
            get { return _codeP; }
            set { _codeP = value; }
        }

        //##fieldList##
        public static System.String pre() { return "PRE"; }
        public static System.String suf() { return "SUF"; }

        public Timetable()
        {
            _ID = new BusinessObjectID();
        }

        public Timetable(BusinessObjectID id)
        {
            _ID = new BusinessObjectID();
            _ID = id;
        }

        [Display(Name = "Tên môn học")]
        public string monhoc { set; get; }
        [Display(Name = "STC")]
        public int stc { set; get; }
        [Display(Name = "Lớp môn học")]
        public string lopmonhoc { set; get; }
        [Display(Name = "Tên giảng viên")]
        public string tengiangvien { set; get; }
        [Display(Name = "Thứ")]
        public string thu { set; get; }
        [Display(Name = "Tiết")]
        public string tiet { set; get; }
        [Display(Name = "Giảng đường")]
        public string giangduong { set; get; }
        [Display(Name = "Số sinh viên")]
        public int sosv { set; get; }
        [Display(Name = "Số SVĐK")]
        public int sosvdk { set; get; }
        public string ID { set; get; }
    }
    class Timetableoutput
    {
        DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        public Timetableoutput()
        {
        }
        public List<Timetable> getAll()
        {
            string sql = "exec inthoikhoabieu @semester=1";
            SqlCommand cm = new SqlCommand();
            List<Timetable> lidata = new List<Timetable>();            
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            int ret = db.getCommand(ref ds, "Tmp", cm);
            if (ret < 0)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in ds.Tables["Tmp"].Rows)
                {
                    Timetable obj = new Timetable();

                    Type myTableObject = typeof(Timetable);
                    System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                    Type myObjectType = typeof(Timetable.BusinessObjectID);
                    System.Reflection.PropertyInfo[] fieldInfo = myObjectType.GetProperties();

                    //set object value
                    foreach (System.Reflection.PropertyInfo info in selectFieldInfo)
                    {
                        if (info.Name != "_ID")
                        {
                            if (dr.Table.Columns.Contains(info.Name))
                            {
                                if (!dr.IsNull(info.Name))
                                {
                                    info.SetValue(obj, dr[info.Name], null);
                                }
                            }
                        }
                        else
                        {
                            //set id value
                            Timetable.BusinessObjectID objid;
                            objid = (Timetable.BusinessObjectID)info.GetValue(obj, null);
                            foreach (System.Reflection.PropertyInfo info1 in fieldInfo)
                            {
                                if (dr.Table.Columns.Contains(info1.Name))
                                {
                                    info1.SetValue(objid, dr[info1.Name], null);
                                }
                            }
                            info.SetValue(obj, objid, null);
                        }
                    }
                    lidata.Add(obj);
                }
            }
            return lidata;
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;
//using System.Data.SqlClient;
//using System.Data;

//namespace HelloWorldReact.Model
//{
//    public class QLlichhoc
//    {
//        [Display(Name = "Tên môn học")]
//        public string Tenmonhoc { set; get; }
//        [Display(Name = "STC")]
//        public int STC { set; get; }
//        [Display(Name = "Lớp môn học")]
//        public string Lopmonhoc { set; get; }
//        [Display(Name = "Tên giảng viên")]
//        public string TenGV { set; get; }
//        [Display(Name = "Thứ")]
//        public string Thu { set; get; }
//        [Display(Name = "Tiết")]
//        public string Tiet { set; get; }
//        [Display(Name = "Giảng đường")]
//        public string room { set; get; }
//        [Display(Name = "Số sinh viên")]
//        public int Sosv { set; get; }
//        [Display(Name = "Số SVĐK")]
//        public int Sosvdk { set; get; }
//        public string ID { set; get; }
//    }
//    class Danhsachlop
//    {
//        DBConnection db;
//        public Danhsachlop()
//        {
//            db = new DBConnection();
//        }
//        public List<QLlichhoc> getlop(string ID, string Thu, string Ten, string room, string start, string end)
//        {
//            string sqlselect = "SELECT dbo.subjects.name AS 'Môn học',dbo.subjects.numbercredit AS 'STC',course.code AS 'Lớp môn học',dbo.teacher.name AS 'Tên giảng viên',dbo.coursetime.dayinweek AS 'Thứ',dbo.coursetime.codeview AS 'Tiết',dbo.courseschedule.roomcode AS 'Giảng đường',dbo.course.maxstudent AS 'Số sv',dbo.course.signedstudent AS 'Số svđk' FROM dbo.department,dbo.teacher,dbo.subjects,dbo.course,dbo.coursetime,dbo.room,dbo.courseschedule,dbo.teacercourse,dbo.detailsubjecteducationprogram,dbo.educationprogram,dbo.MAJOR,dbo.faculty  WHERE dbo.department.code=dbo.teacher.codedepartment AND dbo.teacher.code=dbo.teacercourse.teachercode AND dbo.teacercourse.coursecode=dbo.course.code AND dbo.courseschedule.coursecode=dbo.course.code AND dbo.subjects.code=dbo.course.subjectcode AND dbo.subjects.code=dbo.detailsubjecteducationprogram.subjectcode AND dbo.detailsubjecteducationprogram.educationprogramcode=dbo.educationprogram.code AND dbo.educationprogram.majorcode=dbo.MAJOR.code AND dbo.MAJOR.facultycode=dbo.faculty.code AND dbo.faculty.code=dbo.department.codefaculty AND dbo.coursetime.code=dbo.courseschedule.coursetimecode AND dbo.courseschedule.roomcode=dbo.room.code";
//            string sqlwhere = "";
//            if (ID != "")
//            {
//                sqlwhere = sqlwhere + " AND dbo.faculty.code='" + ID + "'";
//            }
//            if (Thu != "")
//            {
//                sqlwhere = sqlwhere + "AND dbo.coursetime.dayinweek='" + Thu + "'";
//            }
//            if (Ten != "")
//            {
//                sqlwhere = sqlwhere + "AND dbo.subjects.name=N'" + Ten + "'";
//            }
//            if (room != "")
//            {
//                sqlwhere = sqlwhere + "AND dbo.courseschedule.roomcode='" + room + "'";
//            }
//            if (start != "")
//            {
//                sqlwhere = sqlwhere + "AND dbo.coursetime.timestart='" + start + "'";
//            }
//            if (end != "")
//            {
//                sqlwhere = sqlwhere + "AND dbo.coursetime.timeend='" + end + "'";
//            }
//            string sql = sqlselect + sqlwhere;
//            List<QLlichhoc> strlist = new List<QLlichhoc>();
//            SqlConnection con = db.getConnection();
//            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
//            DataTable dt = new DataTable();
//            con.Open();
//            cmd.Fill(dt);
//            cmd.Dispose();
//            con.Close();
//            QLlichhoc strLH;
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                strLH = new QLlichhoc();
//                strLH.Tenmonhoc = dt.Rows[i]["Môn học"].ToString();
//                strLH.STC = Convert.ToInt32(dt.Rows[i]["STC"].ToString());
//                strLH.Lopmonhoc = dt.Rows[i]["Lớp môn học"].ToString();
//                strLH.TenGV = dt.Rows[i]["Tên giảng viên"].ToString();
//                strLH.Thu = dt.Rows[i]["Thứ"].ToString();
//                strLH.Tiet = dt.Rows[i]["Tiết"].ToString();
//                strLH.room = dt.Rows[i]["Giảng đường"].ToString();
//                strLH.Sosv = Convert.ToInt32(dt.Rows[i]["Số sv"].ToString());
//                strLH.Sosvdk = Convert.ToInt32(dt.Rows[i]["Số svđk"].ToString());
//                strlist.Add(strLH);
//            }
//            return strlist;
//        }
//        public void AddLichhoc(QLlichhoc strLH)
//        {

//        }
//    }
//}
