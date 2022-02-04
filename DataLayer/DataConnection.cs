using System.Transactions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.VisualBasic;

namespace AspNetCoreWebAPI.DataLayer
{
    public class DataConnection
    {

        private SqlConnection cn;
        private SqlDataAdapter da;
        private SqlCommand cmd;

        private DataTable dt;
<<<<<<< HEAD
        const string sConnString = "Server=DESKTOP-GTQGN41;Database=BookStall;Trusted_Connection=True;";
=======
        const string sConnString = "Server=DESKTOP-BTJA4IB;Database=BookStall;Trusted_Connection=True;";
>>>>>>> 5611c30c7aaf42e8290b28a4c408ba7c2459177b
        public long Save(string TableName,
                         object ObjItem,
                         bool IsReturnID,
                         ref long ReturnID)
        {
            int result = 0;
            SqlCommand Command = null/* TODO Change to default(_) if this is not a reference type */;
            SqlDataAdapter DataAdaptor = null/* TODO Change to default(_) if this is not a reference type */;
            DataTable DTTable = null/* TODO Change to default(_) if this is not a reference type */;
            Type ObjType = null;
            FieldInfo ObjFieldInfo = null;
            string StrFields = "";
            string StrValues = "";
            // bool StartTransaction = false;
            // SqlTransaction Transaction;
            Exception Exec = null;
            try
            {
                cn = getcn();
                Command = new SqlCommand("SELECT * FROM " + TableName + " WHERE 1=2", cn); // TO GET THE TABLE STRUCTURE
                                                                                           // if (StartTransaction == true)
                                                                                           //     Command.Transaction = Transaction;
                Command.CommandTimeout = 90000;
                DataAdaptor = new SqlDataAdapter(Command);
                DTTable = new DataTable();

                DataAdaptor.Fill(DTTable);
                ObjType = ObjItem.GetType();

                StrFields = " (";
                StrValues = " values (";

                foreach (DataColumn DtColumn in DTTable.Columns)
                {
                    ObjFieldInfo = ObjType.GetField(DtColumn.ColumnName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (ObjFieldInfo != null)
                    {
                        if ((ObjFieldInfo.GetValue(ObjItem)) != null)
                        {
                            if (DtColumn.ColumnName.Substring(DtColumn.ColumnName.ToString().Length - 3, 3) != "_ID")
                            {
                                StrFields = StrFields + DtColumn.ColumnName + ",";
                                switch (DtColumn.DataType.ToString())
                                {
                                    case "System.Int16":
                                    case "System.Int32":
                                    case "System.Int64":
                                    case "System.Decimal":
                                    case "System.Double":
                                    case "System.Single":
                                    case "System.Byte":
                                    case "System.UInt64":
                                        {
                                            StrValues = StrValues + ObjFieldInfo.GetValue(ObjItem) + ",";
                                            break;
                                        }

                                    case "System.String":
                                        {
                                            StrValues = StrValues + "'" + ObjFieldInfo.GetValue(ObjItem).ToString().Replace("'", "`").Replace(@"\\", @"\\\\").Replace(@"\", @"\\") + "',";
                                            break;
                                        }

                                    case "System.DateTime":
                                        {
                                            StrValues = StrValues + "STR_TO_DATE('" + ObjFieldInfo.GetValue(ObjItem) + "','%m/%d/%Y %h:%i:%s %p'),";
                                            break;
                                        }

                                    case "System.Boolean":
                                        {
                                            StrValues = StrValues + System.Convert.ToString(ObjFieldInfo.GetValue(ObjItem)) + ",";
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }

                StrFields = Strings.Left(StrFields, StrFields.Length - 1) + ")";
                StrValues = Strings.Left(StrValues, StrValues.Length - 1) + ")";

                Command.CommandText = "INSERT INTO " + TableName + StrFields + StrValues;
                result = Command.ExecuteNonQuery();
                if (IsReturnID == true)
                {
                    if (result > 0)
                    {
                        Command.CommandText = "SELECT SCOPE_IDENTITY()";
                        result = Convert.ToInt32(Command.ExecuteScalar());
                        ReturnID = result;
                    }
                }
                return result;
            }
            catch (SqlException ex)
            {
                if (Command != null)
                {
                    Exec = new Exception(Command.CommandText + " : " + ex.Message + "-" + ex.StackTrace);
                    throw Exec;
                }
                else
                {
                    throw ex;
                }

            }

            catch (Exception ex)
            {
                Command = null/* TODO Change to default(_) if this is not a reference type */;
                DataAdaptor = null/* TODO Change to default(_) if this is not a reference type */;
                DTTable = null/* TODO Change to default(_) if this is not a reference type */;
                ObjType = null;
                ObjFieldInfo = null;
                StrFields = "";
                StrValues = "";
                throw ex;
            }
            finally
            {
                closecn();
            }
        }

        public long SaveByProperty(string TableName,
                                object ObjItem,
                                bool IsReturnID,
                                ref long ReturnID)
        {
            int result = 0, colcount = 0;
            
            SqlCommand Command = null/* TODO Change to default(_) if this is not a reference type */;
            SqlDataAdapter DataAdaptor = null/* TODO Change to default(_) if this is not a reference type */;
            DataTable DTTable = null/* TODO Change to default(_) if this is not a reference type */;
            Type ObjType = null;
            PropertyInfo ObjPropertyInfo = null;
            string StrFields = "";
            string StrValues = "";
            // bool StartTransaction = false;
            // SqlTransaction Transaction;
            Exception Exec = null;
            try
            {

                cn = getcn();
                
                Command = new SqlCommand("SELECT * FROM " + TableName + " WHERE 1=2", cn); // TO GET THE TABLE STRUCTURE
                                                                                           // if (StartTransaction == true)
                                                                                           //     Command.Transaction = Transaction;
                Command.CommandTimeout = 90000;
                DataAdaptor = new SqlDataAdapter(Command);
                DTTable = new DataTable();

                DataAdaptor.Fill(DTTable);
                ObjType = ObjItem.GetType();

                StrFields = " (";
                StrValues = " values (";

                foreach (DataColumn DtColumn in DTTable.Columns)
                {
                    colcount++;
                    if (colcount != 1)
                    {
                        ObjPropertyInfo = ObjType.GetProperty(DtColumn.ColumnName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        if (ObjPropertyInfo != null)
                        {
                            if ((ObjPropertyInfo.GetValue(ObjItem)) != null)
                            {
                                if (DtColumn.ColumnName.Substring(DtColumn.ColumnName.ToString().Length - 3, 3) != "_ID")
                                {
                                    StrFields = StrFields + DtColumn.ColumnName + ",";
                                    switch (DtColumn.DataType.ToString())
                                    {
                                        case "System.Byte[]":
                                            {
                                                StrValues = StrValues + "@" + DtColumn.ColumnName + ",";
                                                Command.Parameters.Add("@" + DtColumn.ColumnName, SqlDbType.VarBinary).Value = ObjPropertyInfo.GetValue(ObjItem, null);
                                                break;
                                            }
                                        case "System.Int16":
                                        case "System.Int32":
                                        case "System.Int64":
                                        case "System.Decimal":
                                        case "System.Double":
                                        case "System.Single":
                                        case "System.UInt64":
                                        case "System.Byte":
                                            {
                                                StrValues = StrValues + ObjPropertyInfo.GetValue(ObjItem, null).ToString() + ",";
                                                break;
                                            }

                                        case "System.String":
                                            {
                                                StrValues = StrValues + "'" + ObjPropertyInfo.GetValue(ObjItem, null).ToString().Replace("'", "`").Replace(@"\\", @"\\\\").Replace(@"\", @"\\") + "',";
                                                break;
                                            }

                                        case "System.DateTime":
                                            {
                                                StrValues = StrValues + "STR_TO_DATE('" + ObjPropertyInfo.GetValue(ObjItem, null) + "','%m/%d/%Y %h:%i:%s %p'),";
                                                break;
                                            }

                                        case "System.Boolean":
                                            {
                                                StrValues = StrValues + System.Convert.ToString(ObjPropertyInfo.GetValue(ObjItem)) + ",";
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }

                StrFields = Strings.Left(StrFields, StrFields.Length - 1) + ")";
                StrValues = Strings.Left(StrValues, StrValues.Length - 1) + ")";

                Command.CommandText = "INSERT INTO " + TableName + StrFields + StrValues;
                result = Command.ExecuteNonQuery();
                /* if (IsReturnID == true)
                {
                    if (result > 0)
                    {
                        Command.CommandText = "SELECT SCOPE_IDENTITY()";
                        var res = Command.ExecuteScalar();
                        result = Convert.ToInt32(res);
                        ReturnID = result;
                       
                    }
                } */
                return result;
            }
            catch (SqlException ex)
            {
                if (Command != null)
                {
                    Exec = new Exception(Command.CommandText + " : " + ex.Message + "-" + ex.StackTrace);
                    throw Exec;
                }
                else
                {
                    throw ex;
                }

            }

            catch (Exception ex)
            {
                Command = null/* TODO Change to default(_) if this is not a reference type */;
                DataAdaptor = null/* TODO Change to default(_) if this is not a reference type */;
                DTTable = null/* TODO Change to default(_) if this is not a reference type */;
                ObjType = null;
                ObjPropertyInfo = null;
                StrFields = "";
                StrValues = "";
                throw ex;
            }
            finally
            {
                
                closecn();                
            }
        }


        private SqlConnection getcn()
        {
            cn = new SqlConnection(sConnString);
            cn.Open();
            return cn;
        }
        public void closecn()
        {
            cn.Close();
            cn.Dispose();
        }
        public String SetDatabase(String sql)
        {
            String retmsg = "";
            try
            {
                //cmd = new SqlCommand(sql, getcn());
                cmd = new SqlCommand(sql, getcn());

                cmd.ExecuteNonQuery();
                retmsg = "updatesuccessful";
            }
            catch (Exception e)
            {
                retmsg = e.ToString();
            }
            finally
            {
                closecn();
            }
            return retmsg;

        }
        public String SetDatabase(SqlCommand sqlcmd,
                                  String flag)
        {
            String retmsg = "";
            try
            {
                sqlcmd.Connection = getcn();
                sqlcmd.ExecuteNonQuery();
                retmsg = "updatesuccessful";
            }
            catch (Exception e)
            {
                retmsg = e.ToString();
            }
            finally
            {
                closecn();
            }
            return retmsg;

        }
        public DataTable GetData(String Sql)
        {
            dt = new DataTable();
            String retmsg = "";
            try
            {
                //cmd = new SqlCommand(sql, getcn());
                cmd = new SqlCommand(Sql, getcn());
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                retmsg = e.ToString();
            }
            finally
            {
                closecn();
            }
            return dt;
        }
    }

}