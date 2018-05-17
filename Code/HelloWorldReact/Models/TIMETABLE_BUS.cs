using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IS.Base;
using IS.Config;
using System.Configuration;
namespace IS.uni
{
    public class TIMETABLE_BUS
    {
        DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());

        public TIMETABLE_BUS()
        {
        }
        public TIMETABLE_OBJ createObject()
        {
            TIMETABLE_OBJ obj = new TIMETABLE_OBJ();
            return obj;
        }
        public TIMETABLE_OBJ createNull()
        {
            return null;
        }


        //public List<TIMETABLE_OBJ> getAll(params spParam[] listFilter)
        public List<TIMETABLE_OBJ> getAll(string ID, string Thu, string Ten, string room, string start, string end)
        {
            string sqlselect = "SELECT dbo.subjects.name AS 'Môn học',dbo.subjects.numbercredit AS 'STC',course.code AS 'Lớp môn học',dbo.teacher.name AS 'Tên giảng viên',dbo.coursetime.dayinweek AS 'Thứ',dbo.coursetime.codeview AS 'Tiết',dbo.courseschedule.roomcode AS 'Giảng đường',dbo.course.maxstudent AS 'Số sv',dbo.course.signedstudent AS 'Số svđk' FROM dbo.department,dbo.teacher,dbo.subjects,dbo.course,dbo.coursetime,dbo.room,dbo.courseschedule,dbo.teacercourse,dbo.detailsubjecteducationprogram,dbo.educationprogram,dbo.MAJOR,dbo.faculty  WHERE dbo.department.code=dbo.teacher.codedepartment AND dbo.teacher.code=dbo.teacercourse.teachercode AND dbo.teacercourse.coursecode=dbo.course.code AND dbo.courseschedule.coursecode=dbo.course.code AND dbo.subjects.code=dbo.course.subjectcode AND dbo.subjects.code=dbo.detailsubjecteducationprogram.subjectcode AND dbo.detailsubjecteducationprogram.educationprogramcode=dbo.educationprogram.code AND dbo.educationprogram.majorcode=dbo.MAJOR.code AND dbo.MAJOR.facultycode=dbo.faculty.code AND dbo.faculty.code=dbo.department.codefaculty AND dbo.coursetime.code=dbo.courseschedule.coursetimecode AND dbo.courseschedule.roomcode=dbo.room.code";
            string sqlwhere = "";
            if (ID != "")
            {
                sqlwhere = sqlwhere + " AND dbo.faculty.code='" + ID + "'";
            }
            if (Thu != "")
            {
                sqlwhere = sqlwhere + "AND dbo.coursetime.dayinweek='" + Thu + "'";
            }
            if (Ten != "")
            {
                sqlwhere = sqlwhere + "AND dbo.subjects.name=N'" + Ten + "'";
            }
            if (room != "")
            {
                sqlwhere = sqlwhere + "AND dbo.courseschedule.roomcode='" + room + "'";
            }
            if (start != "")
            {
                sqlwhere = sqlwhere + "AND dbo.coursetime.timestart='" + start + "'";
            }
            if (end != "")
            {
                sqlwhere = sqlwhere + "AND dbo.coursetime.timeend='" + end + "'";
            }
            string sql = sqlselect + sqlwhere;
            List<TIMETABLE_OBJ> strlist = new List<TIMETABLE_OBJ>();
            //SqlConnection con = db.getConnection();
            //SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            //con.Open();
            //cmd.Fill(dt);
            //cmd.Dispose();
            //con.Close();
            TIMETABLE_OBJ strLH;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strLH = new TIMETABLE_OBJ();
                strLH.Tenmonhoc = dt.Rows[i]["Môn học"].ToString();
                strLH.STC = Convert.ToInt32(dt.Rows[i]["STC"].ToString());
                strLH.Lopmonhoc = dt.Rows[i]["Lớp môn học"].ToString();
                strLH.TenGV = dt.Rows[i]["Tên giảng viên"].ToString();
                strLH.Thu = dt.Rows[i]["Thứ"].ToString();
                strLH.Tiet = dt.Rows[i]["Tiết"].ToString();
                strLH.room = dt.Rows[i]["Giảng đường"].ToString();
                strLH.Sosv = Convert.ToInt32(dt.Rows[i]["Số sv"].ToString());
                strLH.Sosvdk = Convert.ToInt32(dt.Rows[i]["Số svđk"].ToString());
                strlist.Add(strLH);
            }
            return strlist;
        }        //public TIMETABLE_OBJ GetByID(TIMETABLE_OBJ.BusinessObjectID id)
                 //{
                 //    List<TIMETABLE_OBJ> li = getAll(new spParam("CODE", SqlDbType.VarChar, id.CODE, 0));
                 //    if (li == null || li.Count == 0)
                 //    {
                 //        return null;
                 //    }
                 //    return li[0];
                 //}
        public string genNextCode(TIMETABLE_OBJ obj)
        {
            //Phải viết lại theo mô hình nào đó
            Random rnd = new Random();
            int i = rnd.Next(int.MaxValue);
            return (i % 10000000000).ToString();
        }
        //public int Insert(TIMETABLE_OBJ obj)
        //{
        //    int ret = 0;
        //    DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        //    string sql = "INSERT INTO TIMETABLE(code,codeview, name, note, edituser,edittime,lock, lockdate, theorder, thetype, comparelevel, whois) VALUES(@code,@codeview, @name, @note, @edituser,@edittime,@lock, @lockdate, @theorder, @thetype, @comparelevel, @whois)";
        //    SqlCommand com = new SqlCommand();
        //    com.CommandText = sql;
        //    com.CommandType = CommandType.Text;
        //    com.Parameters.Add("@code", SqlDbType.VarChar).Value = obj.CODE ;
        //    com.Parameters.Add("@codeview", SqlDbType.VarChar).Value = obj.CODEVIEW;
        //    com.Parameters.Add("@name", SqlDbType.NVarChar).Value = obj.NAME;
        //    com.Parameters.Add("@note", SqlDbType.NVarChar).Value = obj.NOTE;
        //    com.Parameters.Add("@edituser", SqlDbType.VarChar).Value = obj.EDITUSER;
        //    com.Parameters.Add("@edittime", SqlDbType.DateTime).Value = obj.EDITTIME;
        //    com.Parameters.Add("@lock", SqlDbType.Int).Value = obj.LOCK;
        //    com.Parameters.Add("@lockdate", SqlDbType.DateTime).Value = obj.LOCKDATE;
        //    com.Parameters.Add("@theorder", SqlDbType.Int).Value = obj.THEORDER;
        //    com.Parameters.Add("@thetype", SqlDbType.VarChar).Value = obj.THETYPE;
        //    com.Parameters.Add("@comparelevel", SqlDbType.Int).Value = obj.COMPARELEVEL;
        //    com.Parameters.Add("@whois", SqlDbType.VarChar).Value = obj.WHOIS;
        //    ret = db.doCommand(ref com);
        //    return ret;
        //}
        //public int Update(TIMETABLE_OBJ obj)
        //{
        //    int ret = 0;
        //    DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        //    string sql = @"UPDATE TIMETABLE SET 
        //            code=@code
        //            ,codeview=@codeview
        //            , name=@name
        //            , note=@note
        //            , edituser=@edituser
        //            ,edittime=@edittime
        //            ,lock=@lock
        //            , lockdate=@lockdate
        //            , theorder=@theorder
        //            , comparelevel=@comparelevel
        //            , whois=@whois
        //            WHERE code=@code_key
        //        ";
        //    SqlCommand com = new SqlCommand();
        //    com.CommandText = sql;
        //    com.CommandType = CommandType.Text;
        //    com.Parameters.Add("@code", SqlDbType.VarChar).Value = obj.CODE;
        //    com.Parameters.Add("@codeview", SqlDbType.VarChar).Value = obj.CODEVIEW;
        //    com.Parameters.Add("@name", SqlDbType.NVarChar).Value = obj.NAME;
        //    com.Parameters.Add("@note", SqlDbType.NVarChar).Value = obj.NOTE;
        //    com.Parameters.Add("@edituser", SqlDbType.VarChar).Value = obj.EDITUSER;
        //    com.Parameters.Add("@edittime", SqlDbType.DateTime).Value = obj.EDITTIME;
        //    com.Parameters.Add("@lock", SqlDbType.Int).Value = obj.LOCK;
        //    com.Parameters.Add("@lockdate", SqlDbType.DateTime).Value = obj.LOCKDATE;
        //    com.Parameters.Add("@code_key", SqlDbType.VarChar).Value = obj._ID.CODE;
        //    com.Parameters.Add("@theorder", SqlDbType.Int).Value = obj.THEORDER;
        //    com.Parameters.Add("@comparelevel", SqlDbType.Int).Value = obj.COMPARELEVEL;
        //    com.Parameters.Add("@whois", SqlDbType.VarChar).Value = obj.COMPARELEVEL;
        //    ret = db.doCommand(ref com);
        //    return ret;
        //}
        //public int Delete(TIMETABLE_OBJ.BusinessObjectID obj)
        //{
        //    int ret = 0;
        //    DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        //    string sql = @"DELETE FROM TIMETABLE  WHERE code=@code_key
        //        ";
        //    SqlCommand com = new SqlCommand();
        //    com.CommandText = sql;
        //    com.CommandType = CommandType.Text;
        //    com.Parameters.Add("@code_key", SqlDbType.VarChar).Value = obj.CODE;
        //    ret = db.doCommand(ref com);
        //    return ret;
        //}
        public int Open()
        {
            return db.Open();
        }
        public void CloseConnection()
        {
            db.Close();
        }
    }

}

