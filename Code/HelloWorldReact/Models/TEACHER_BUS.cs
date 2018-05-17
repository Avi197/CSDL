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
    public class TEACHER_BUS
    {
        DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        public TEACHER_BUS()
        {
        }
        public  TEACHER_OBJ createObject()
        {
            TEACHER_OBJ obj = new TEACHER_OBJ();
            return obj;
        }
        public  TEACHER_OBJ createNull()
        {
            return null;
        }
        public List<TEACHER_OBJ> getAll(params spParam[] listFilter)
        {
            List<TEACHER_OBJ> lidata = new List<uni.TEACHER_OBJ>();
            string sql = "SELECT * FROM TEACHER";
            string swhere = "";
            SqlCommand cm = new SqlCommand();
            foreach (var item in listFilter)
            {
                if (swhere != "")
                {
                    swhere += " AND ";
                }
                if (item.data == null)
                {
                    //cm.Parameters.Add("@" + f.Name, st);
                    //cm.Parameters["@" + f.Name].Value = DBNull.Value;
                    swhere += "[" + item.name + "]" + " is null";
                }
                else
                {
                    if (item.searchtype == 0)
                    {
                        swhere += "[" + item.name + "]= @" + item.name;
                        cm.Parameters.Add(new SqlParameter("@" + item.name, item.data));
                    }
                    else
                    {
                        swhere += "[" + item.name + "] LIKE @" + item.name;
                        cm.Parameters.Add(new SqlParameter("@" + item.name,"%"+  item.data +"%"));
                    }
                }
            }
            if(swhere!="")
            {
                sql += " WHERE " + swhere;
            }
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            int ret= db.getCommand(ref ds, "Tmp",  cm);
            if(ret<0)
            {
                return null;
            }
            else
            {
                foreach(DataRow dr in ds.Tables["Tmp"].Rows)
                {
                    TEACHER_OBJ obj = new TEACHER_OBJ();

                    Type myTableObject = typeof(TEACHER_OBJ);
                    System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                    Type myObjectType = typeof(TEACHER_OBJ.BusinessObjectID);
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
                            TEACHER_OBJ.BusinessObjectID objid;
                            objid = (TEACHER_OBJ.BusinessObjectID)info.GetValue(obj, null);
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
                    lidata.Add( obj);
                }
            }
            return lidata;
        }
        public TEACHER_OBJ GetByID(TEACHER_OBJ.BusinessObjectID id)
        {
            List<TEACHER_OBJ> li = getAll(new spParam("CODE", SqlDbType.VarChar, id.CODE,0));
            if(li== null || li.Count==0)
            {
                return null;
            }
            return li[0];
        }
        public string genNextCode(TEACHER_OBJ obj)
        {
            //Phải viết lại theo mô hình nào đó
            Random rnd = new Random();
            int i = rnd.Next(int.MaxValue);
            return (i % 10000000000).ToString();
        }
        //public int Insert(TEACHER_OBJ obj)
        //{
        //    int ret = 0;
        //    DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        //    string sql = "INSERT INTO TEACHER(code,codeview, name, note, edituser,edittime,lock, lockdate) VALUES(@code,@codeview, @name, @note, @edituser,@edittime,@lock, @lockdate)";
        //    SqlCommand com = new SqlCommand();
        //    com.CommandText = sql;
        //    com.CommandType = CommandType.Text;
        //    com.Parameters.Add("@code", SqlDbType.VarChar).Value = obj.CODE ;
        //    com.Parameters.Add("@timestart", SqlDbType.VarChar).Value = obj.TIMESTART;
        //    com.Parameters.Add("@timeend", SqlDbType.VarChar).Value = obj.TIMEEND;
        //    com.Parameters.Add("@dayinweek", SqlDbType.VarChar).Value = obj.DAYINWEEK;
        //    com.Parameters.Add("@codeview", SqlDbType.VarChar).Value = obj.CODEVIEW;
        //    //com.Parameters.Add("@name", SqlDbType.NVarChar).Value = obj.NAME;
        //    //com.Parameters.Add("@note", SqlDbType.NVarChar).Value = obj.NOTE;
        //    //com.Parameters.Add("@edituser", SqlDbType.VarChar).Value = obj.EDITUSER;
        //    //com.Parameters.Add("@edittime", SqlDbType.DateTime).Value = obj.EDITTIME;
        //    //com.Parameters.Add("@lock", SqlDbType.Int).Value = obj.LOCK;
        //    //com.Parameters.Add("@lockdate", SqlDbType.DateTime).Value = obj.LOCKDATE;
        //    ret = db.doCommand(ref com);
        //    return ret;
        //}
        //public int Update(TEACHER_OBJ obj)
        //{
        //    int ret = 0;
        //    DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        //    string sql = @"UPDATE TEACHER SET 
        //            code=@code
        //            ,codeview=@codeview
        //            , name=@name
        //            , note=@note
        //            , edituser=@edituser
        //            ,edittime=@edittime
        //            ,lock=@lock
        //            , lockdate=@lockdate
        //            WHERE code=@code_key
        //        ";
        //    SqlCommand com = new SqlCommand();
        //    com.CommandText = sql;
        //    com.CommandType = CommandType.Text;
        //    com.Parameters.Add("@code", SqlDbType.VarChar).Value = obj.CODE;
        //    com.Parameters.Add("@code", SqlDbType.VarChar).Value = obj.CODE;
        //    com.Parameters.Add("@timestart", SqlDbType.VarChar).Value = obj.TIMESTART;
        //    com.Parameters.Add("@timeend", SqlDbType.VarChar).Value = obj.TIMEEND;
        //    com.Parameters.Add("@dayinweek", SqlDbType.VarChar).Value = obj.DAYINWEEK;
        //    com.Parameters.Add("@codeview", SqlDbType.VarChar).Value = obj.CODEVIEW;
        //    com.Parameters.Add("@codeview", SqlDbType.VarChar).Value = obj.CODEVIEW;
        //    //com.Parameters.Add("@name", SqlDbType.NVarChar).Value = obj.NAME;
        //    //com.Parameters.Add("@note", SqlDbType.NVarChar).Value = obj.NOTE;
        //    //com.Parameters.Add("@edituser", SqlDbType.VarChar).Value = obj.EDITUSER;
        //    //com.Parameters.Add("@edittime", SqlDbType.DateTime).Value = obj.EDITTIME;
        //    //com.Parameters.Add("@lock", SqlDbType.Int).Value = obj.LOCK;
        //    //com.Parameters.Add("@lockdate", SqlDbType.DateTime).Value = obj.LOCKDATE;
        //    com.Parameters.Add("@code_key", SqlDbType.VarChar).Value = obj._ID.CODE;
        //    ret = db.doCommand(ref com);
        //    return ret;
        //}
        public int Delete(TEACHER_OBJ.BusinessObjectID obj)
        {
            int ret = 0;
            DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
            string sql = @"DELETE FROM TEACHER  WHERE code=@code_key
                ";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Parameters.Add("@code_key", SqlDbType.VarChar).Value = obj.CODE;
            ret = db.doCommand(ref com);
            return ret;
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

