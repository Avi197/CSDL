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
    public class COURSETIME_BUS
    {
        DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
        public COURSETIME_BUS()
        {
        }
        public  COURSETIME_OBJ createObject()
        {
            COURSETIME_OBJ obj = new COURSETIME_OBJ();
            return obj;
        }
        public  COURSETIME_OBJ createNull()
        {
            return null;
        }
        public List<COURSETIME_OBJ> getAll(params spParam[] listFilter)
        {
            List<COURSETIME_OBJ> lidata = new List<uni.COURSETIME_OBJ>();
            string sql = "SELECT * FROM coursetime";
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
                    COURSETIME_OBJ obj = new COURSETIME_OBJ();

                    Type myTableObject = typeof(COURSETIME_OBJ);
                    System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                    Type myObjectType = typeof(COURSETIME_OBJ.BusinessObjectID);
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
                            COURSETIME_OBJ.BusinessObjectID objid;
                            objid = (COURSETIME_OBJ.BusinessObjectID)info.GetValue(obj, null);
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
        public COURSETIME_OBJ GetByID(COURSETIME_OBJ.BusinessObjectID id)
        {
            List<COURSETIME_OBJ> li = getAll(new spParam("CODE", SqlDbType.VarChar, id.CODE,0));
            if(li== null || li.Count==0)
            {
                return null;
            }
            return li[0];
        }
        public string genNextCode(COURSETIME_OBJ obj)
        {
            //Phải viết lại theo mô hình nào đó
            Random rnd = new Random();
            int i = rnd.Next(int.MaxValue);
            return (i % 10000000000).ToString();
        }
        public int Insert(COURSETIME_OBJ obj)
        {
            int ret = 0;
            DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
            string sql = "exec addcoursetime @start=@timestart @end=@timeend @day=@dayinweek";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            //com.Parameters.Add("@code", SqlDbType.VarChar).Value = obj.CODE ;
            com.Parameters.Add("@timestart", SqlDbType.VarChar).Value = obj.TIMESTART;
            com.Parameters.Add("@timeend", SqlDbType.VarChar).Value = obj.TIMEEND;
            com.Parameters.Add("@dayinweek", SqlDbType.VarChar).Value = obj.DAYINWEEK;
            //com.Parameters.Add("@codeview", SqlDbType.VarChar).Value = obj.CODEVIEW;
            ret = db.doCommand(ref com);
            return ret;
        }
        public int Update(COURSETIME_OBJ obj)
        {
            int ret = 0;
            DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
            string sql = @"UPDATE COURSETIME SET 
                    code=@code,
                    ,timestart=@timestart
                    ,timeend=@timeend
                    ,dayinweek=@dayinweek
                    ,codeview=@codeview
                    WHERE code=@code_key
                ";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Parameters.Add("@code", SqlDbType.VarChar).Value = obj.CODE;
            com.Parameters.Add("@timestart", SqlDbType.VarChar).Value = obj.TIMESTART;
            com.Parameters.Add("@timeend", SqlDbType.VarChar).Value = obj.TIMEEND;
            com.Parameters.Add("@dayinweek", SqlDbType.VarChar).Value = obj.DAYINWEEK;
            com.Parameters.Add("@codeview", SqlDbType.VarChar).Value = obj.CODEVIEW;
            com.Parameters.Add("@code_key", SqlDbType.VarChar).Value = obj._ID.CODE;
            ret = db.doCommand(ref com);
            return ret;
        }
        public int Delete(COURSETIME_OBJ.BusinessObjectID obj)
        {
            int ret = 0;
            DBBase db = new DBBase(ConfigurationSettings.AppSettings["connectionString"].ToString());
            string sql = @"DELETE FROM COURSETIME  WHERE code=@code_key
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

