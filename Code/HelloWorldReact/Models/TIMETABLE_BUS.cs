//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Data.SqlClient;
//using IS.Base;
//using IS.Config;
//using System.Configuration;
//namespace IS.uni
//{
//    public class TIMETABLE_BUS
//    {
//        DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());

//        public TIMETABLE_BUS()
//        {
//        }
//        public TIMETABLE_OBJ createObject()
//        {
//            TIMETABLE_OBJ obj = new TIMETABLE_OBJ();
//            return obj;
//        }
//        public TIMETABLE_OBJ createNull()
//        {
//            return null;
//        }


//        //public List<TIMETABLE_OBJ> getAll(params spParam[] listFilter)
//        DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
//        public TIMETABLE_BUS()
//        {
//        }
//        public List<Timetable> getAll()
//        {
//            string sql = "exec inthoikhoabieu @semester=1";
//            SqlCommand cm = new SqlCommand();
//            List<Timetable> lidata = new List<Timetable>();
//            //SqlConnection con = new SqlConnection();
//            //SqlDataAdapter cmd = new SqlDataAdapter(sql,conn);
//            cm.CommandText = sql;
//            cm.CommandType = CommandType.Text;
//            //DataTable dt = new DataTable();
//            //Timetable strLH;

//            //for (int i = 0; i < dt.Rows.Count; i++)
//            //{
//            //    strLH = new Timetable();
//            //    strLH.Tenmonhoc = dt.Rows[i]["mon hoc"].ToString();
//            //    strLH.STC = Convert.ToInt32(dt.Rows[i]["STC"].ToString());
//            //    strLH.Lopmonhoc = dt.Rows[i]["lop mon hoc"].ToString();
//            //    strLH.TenGV = dt.Rows[i]["ten giang vien"].ToString();
//            //    strLH.Thu = dt.Rows[i]["thu"].ToString();
//            //    strLH.Tiet = dt.Rows[i]["tiet"].ToString();
//            //    strLH.room = dt.Rows[i]["giang duong"].ToString();
//            //    strLH.Sosv = Convert.ToInt32(dt.Rows[i]["so SV"].ToString());
//            //    strLH.Sosvdk = Convert.ToInt32(dt.Rows[i]["so SVDK"].ToString());
//            //    strlist.Add(strLH);
//            //}
//            //return strlist;
//            DataSet ds = new DataSet();
//            int ret = db.getCommand(ref ds, "Tmp", cm);
//            if (ret < 0)
//            {
//                return null;
//            }
//            else
//            {
//                foreach (DataRow dr in ds.Tables["Tmp"].Rows)
//                {
//                    Timetable obj = new Timetable();

//                    Type myTableObject = typeof(Timetable);
//                    System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

//                    //Type myObjectType = typeof(Timetable.BusinessObjectID);
//                    //System.Reflection.PropertyInfo[] fieldInfo = myObjectType.GetProperties();

//                    //set object value
//                    foreach (System.Reflection.PropertyInfo info in selectFieldInfo)
//                    {
//                        //if (info.Name != "_ID")
//                        //{
//                        if (dr.Table.Columns.Contains(info.Name))
//                        {
//                            if (!dr.IsNull(info.Name))
//                            {
//                                info.SetValue(obj, dr[info.Name], null);
//                            }
//                        }
//                        //}
//                        //else
//                        //{
//                        //    //set id value
//                        //    Timetable.BusinessObjectID objid;
//                        //    objid = (TEACHER_OBJ.BusinessObjectID)info.GetValue(obj, null);
//                        //    foreach (System.Reflection.PropertyInfo info1 in fieldInfo)
//                        //    {
//                        //        if (dr.Table.Columns.Contains(info1.Name))
//                        //        {
//                        //            info1.SetValue(objid, dr[info1.Name], null);
//                        //        }
//                        //    }
//                        //    info.SetValue(obj, objid, null);
//                        //}
//                    }
//                    lidata.Add(obj);
//                }
//            }
//            return lidata;
//        }

//    public int Open()
//    {
//        return db.Open();
//    }
//    public void CloseConnection()
//    {
//        db.Close();
//    }
//}

//}

