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
        public List<TIMETABLE_OBJ> getAll(params spParam[] listFilter)
        {
            List<TIMETABLE_OBJ> lidata = new List<TIMETABLE_OBJ>();
            string sql = "exec inthoikhoabieu";
            string smt = "";
            SqlCommand cm = new SqlCommand();
            if (smt != "")
            {
                sql += smt;
            }
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
                    TIMETABLE_OBJ obj = new TIMETABLE_OBJ();

                    Type myTableObject = typeof(TIMETABLE_OBJ);
                    System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

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
                    }
                    lidata.Add(obj);
                }
            }
            return lidata;
        }
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

