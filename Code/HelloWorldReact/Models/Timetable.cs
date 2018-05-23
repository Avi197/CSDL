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