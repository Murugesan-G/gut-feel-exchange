using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    public class Employees : iEmployee
    {
        private int _EmployeeId;
        private string _Empid;
        private string _EmployeeName;
        private string _EmployeeCategory;
        private string _Gender;
        private string _PhoneNo;
        private string _Address;
        private string _EmailId;
        //  private int _DepartmentId;
        private DateTime _JoiningDate;
        private DateTime _LeavingDate;
        private string _Status;
        private DateTime _DOB;
        public int EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId=value; }
        }
        public string Empid
        {
            get { return _Empid; }
            set { _Empid=value; }
        }
        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName=value; }
        }
        public string EmployeeCategory
        {
            get { return _EmployeeCategory; }
            set { _EmployeeCategory=value; }
        }

        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB=value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender=value; }
        }
        public string PhoneNo
        {
            get { return _PhoneNo; }
            set { _PhoneNo=value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address=value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId=value; }
        }
        //public int DepartmentId
        //{
        //    get { return _DepartmentId; }
        //    set { _DepartmentId=value; }
        //}
        public DateTime JoiningDate
        {
            get { return _JoiningDate; }
            set { _JoiningDate=value; }
        }
        public DateTime LeavingDate
        {
            get { return _LeavingDate; }
            set { _LeavingDate=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }

        Dictionary<string, object> EmployeeDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            EmployeeDictionary.Add("EmployeeId", _EmployeeId);
            EmployeeDictionary.Add("Empid", _Empid);
            EmployeeDictionary.Add("EmployeeCategory", _EmployeeCategory);
            EmployeeDictionary.Add("EmployeeName", _EmployeeName);
            EmployeeDictionary.Add("DOB", _DOB);
            EmployeeDictionary.Add("Gender", _Gender);
            EmployeeDictionary.Add("PhoneNo", _PhoneNo);
            EmployeeDictionary.Add("Address", _Address);
            EmployeeDictionary.Add("EmailId", _EmailId);
            //  EmployeeDictionary.Add("DepartmentId", _DepartmentId);
            EmployeeDictionary.Add("JoiningDate", _JoiningDate);
            EmployeeDictionary.Add("LeavingDate", _LeavingDate);
            EmployeeDictionary.Add("Status", _Status);

            return "Success";
        }

        DBConnection db = new DBConnection();
        public string CreateEmployee(Dictionary<string, object> EmployeesIDictionary)
        {

            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            EmployeeDictionary=EmployeesIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertEmployee";
            cmd.Parameters.AddWithValue("@Empid", EmployeeDictionary["Empid"]);
            cmd.Parameters.AddWithValue("@EmployeeCategory", EmployeeDictionary["EmployeeCategory"]);
            cmd.Parameters.AddWithValue("@EmployeeName", EmployeeDictionary["EmployeeName"]);
            DateTime dob = DateTime.ParseExact(EmployeeDictionary["DOB"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOB", dob);
            cmd.Parameters.AddWithValue("@Gender", EmployeeDictionary["Gender"]);
            cmd.Parameters.AddWithValue("@PhoneNo", EmployeeDictionary["PhoneNo"]);
            cmd.Parameters.AddWithValue("@Address", EmployeeDictionary["Address"]);
            cmd.Parameters.AddWithValue("@EmailId", EmployeeDictionary["EmailId"]);
            //   cmd.Parameters.AddWithValue("@DepartmentId", Convert.ToInt32(EmployeeDictionary["DepartmentId"]));
            DateTime joiningDate = DateTime.ParseExact(EmployeeDictionary["JoiningDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@JoiningDate", joiningDate);

            if (EmployeeDictionary["LeavingDate"]==null||EmployeeDictionary["LeavingDate"].ToString()=="")
            {
                cmd.Parameters.AddWithValue("@LeavingDate", DBNull.Value);
            }
            else
            {
                DateTime leavingDate = DateTime.ParseExact(EmployeeDictionary["LeavingDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("@LeavingDate", leavingDate);
            }


            cmd.Parameters.AddWithValue("@Status", EmployeeDictionary["Status"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    response=cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public string UpdateEmployee(Dictionary<string, object> EmployeesIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            EmployeeDictionary=EmployeesIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateEmployee";
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeDictionary["EmployeeId"]);
            cmd.Parameters.AddWithValue("@Empid", EmployeeDictionary["Empid"]);
            cmd.Parameters.AddWithValue("@EmployeeCategory", EmployeeDictionary["EmployeeCategory"]);
            cmd.Parameters.AddWithValue("@EmployeeName", EmployeeDictionary["EmployeeName"]);
            //DateTime dob = DateTime.ParseExact(EmployeeDictionary["DOB"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(EmployeeDictionary["DOB"]));
            cmd.Parameters.AddWithValue("@Gender", EmployeeDictionary["Gender"]);
            cmd.Parameters.AddWithValue("@PhoneNo", EmployeeDictionary["PhoneNo"]);
            cmd.Parameters.AddWithValue("@Address", EmployeeDictionary["Address"]);
            cmd.Parameters.AddWithValue("@EmailId", EmployeeDictionary["EmailId"]);
            //   cmd.Parameters.AddWithValue("@DepartmentId", Convert.ToInt32(EmployeeDictionary["DepartmentId"]));
            // DateTime joiningDate = DateTime.ParseExact(EmployeeDictionary["JoiningDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (EmployeeDictionary["JoiningDate"]==null||EmployeeDictionary["JoiningDate"].ToString()=="")
            {
                cmd.Parameters.AddWithValue("@JoiningDate", DBNull.Value);

            }
            else
            {
                cmd.Parameters.AddWithValue("@JoiningDate", Convert.ToDateTime(EmployeeDictionary["JoiningDate"]));

            }
            //DateTime leavingDate = DateTime.ParseExact(EmployeeDictionary["LeavingDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (EmployeeDictionary["LeavingDate"]==null||EmployeeDictionary["LeavingDate"].ToString()=="")
            {
                cmd.Parameters.AddWithValue("@LeavingDate", DBNull.Value);

            }
            else
            {
                cmd.Parameters.AddWithValue("@LeavingDate", Convert.ToDateTime(EmployeeDictionary["LeavingDate"]));

            }
            cmd.Parameters.AddWithValue("@Status", EmployeeDictionary["Status"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    response=cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public string DeleteEmployee(int EmployeeId)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteEmployee";
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    response=cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public List<Employees> ViewAllEmployee()
        {
            List<Employees> list = new List<Employees>();
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAllEmployee";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            Employees sub = new Employees();
                            sub._EmployeeId=Convert.ToInt32(ds.Tables[0].Rows[i]["EmployeeID"]);
                            sub._Empid=ds.Tables[0].Rows[i]["Empid"].ToString();
                            sub._EmployeeName=ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                            sub._EmployeeCategory=ds.Tables[0].Rows[i]["EmployeeCategory"].ToString();
                            sub._DOB=Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"].ToString());
                            sub._Gender=ds.Tables[0].Rows[i]["Gender"].ToString();
                            sub._PhoneNo=ds.Tables[0].Rows[i]["PhoneNo"].ToString();
                            sub._Address=ds.Tables[0].Rows[i]["Address"].ToString();
                            sub._EmailId=ds.Tables[0].Rows[i]["EmailId"].ToString();
                            //   sub._DepartmentId=Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"].ToString());
                            sub._JoiningDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["JoiningDate"].ToString());
                            var leavingdatedb = ds.Tables[0].Rows[i]["LeavingDate"].ToString();
                            if (leavingdatedb==""||leavingdatedb==null)
                            {

                            }
                            else
                            {
                                sub._LeavingDate=Convert.ToDateTime(leavingdatedb);

                            }

                            sub._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                            list.Add(sub);
                        }
                    }

                }
                catch (SqlException e)
                {
                    ErrorString=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }

        public Dictionary<string, object> SelectEmployee(int EmployeeId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            EmployeeDict Employee = new EmployeeDict();
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectEmployee";
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    if (da==null)
                    {
                        ErrorString="Data is Unavailable";
                    }
                    else
                    {
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            dict=Employee.BindDictionary(ds);
                        }

                    }

                }
                catch (SqlException e)
                {
                    ErrorString=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return dict;
            }
        }
    }
}