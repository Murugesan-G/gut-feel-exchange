using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    public class Services : ServiceInterface
    {
        private int _ServiceId;
        private string _ServiceCode;
        private string _ServiceName;
        private string _Description;
        private string _AvailabilityStatus;
        private double _PerHour;
        private double _PerHalfDay;
        private double _FullDay;
        private double _FullMonth;
        private double _Rate;
        private DateTime _Validity;
        private double _GST;


        public Double GST
        {
            get { return _GST; }
            set { _GST=value; }
        }
        public DateTime Validity
        {
            get { return _Validity; }
            set { _Validity=value; }
        }

        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId=value; }
        }
        public string ServiceCode
        {
            get { return _ServiceCode; }
            set { _ServiceCode=value; }
        }
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName=value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description=value; }
        }
        public string AvailabilityStatus
        {
            get { return _AvailabilityStatus; }
            set { _AvailabilityStatus=value; }
        }
        public double PerHour
        {
            get { return _PerHour; }
            set { _PerHour=value; }
        }
        public double PerHalfDay
        {
            get { return _PerHalfDay; }
            set { _PerHalfDay=value; }
        }
        public double FullDay
        {
            get { return _FullDay; }
            set { _FullDay=value; }
        }
        public double FullMonth
        {
            get { return _FullMonth; }
            set { _FullMonth=value; }
        }

        public double Rate
        {
            get { return _Rate; }
            set { _Rate=value; }
        }
     

        // DateTime Applicabledate { get; set; }

        public Dictionary<string, object> ServiceDictionary { get; set; }
        DBConnection db = new DBConnection();

        string BindDictionary()
        {
            ServiceDictionary=new Dictionary<string, object>();
            ServiceDictionary.Add("ServiceId", _ServiceId);
            ServiceDictionary.Add("ServiceCode", _ServiceCode);
            ServiceDictionary.Add("ServiceName", _ServiceName);
            ServiceDictionary.Add("Description", _Description);
            ServiceDictionary.Add("AvailabilityStatus", _AvailabilityStatus);
            ServiceDictionary.Add("PerHour", _PerHour);
            ServiceDictionary.Add("PerHalfDay", _PerHalfDay);
            ServiceDictionary.Add("FullDay", _FullDay);
            ServiceDictionary.Add("FullMonth", _FullMonth);
            ServiceDictionary.Add("Rate", _Rate);
            ServiceDictionary.Add("Validity", _Validity);
            ServiceDictionary.Add("GST", _GST);
            // ServiceDictionary.Add("Applicabledate", Applicabledate);

            return "Success";
        }
        public int Save()
        {
            int resultval = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;

            if (this.ServiceId == 0)
            {
                cmd.CommandText = "spInsertServiceDetails";
            }
            else
            {
                cmd.CommandText = "spUpdateService";
                cmd.Parameters.AddWithValue("@ServiceId", this.ServiceId);
            }

            cmd.Parameters.AddWithValue("@ServiceCode", this.ServiceCode);
            cmd.Parameters.AddWithValue("@ServiceName", this.ServiceName);
            cmd.Parameters.AddWithValue("@Description",this.Description);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", this.AvailabilityStatus);
            // DateTime dob = DateTime.ParseExact(this.DOB.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@PerHour", Convert.ToDouble(this.PerHour));
            cmd.Parameters.AddWithValue("@PerHalfDay", Convert.ToDouble(this.PerHalfDay));
            cmd.Parameters.AddWithValue("@FullDay", Convert.ToDouble(this.FullDay));
            cmd.Parameters.AddWithValue("@FullMonth", Convert.ToDouble(this.FullMonth));
            cmd.Parameters.AddWithValue("@Rate", Convert.ToDouble(this.Rate));
            cmd.Parameters.AddWithValue("@Validity",this.Validity);
            cmd.Parameters.AddWithValue("@GST", this.GST);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    //Check for errors using try catch 
                    resultval = cmd.ExecuteNonQuery();
                }
                //catch (Exception e)
                //{
                //    resultval=e.Message.ToString();

                //}
                finally
                {
                    db.CloseConnection();
                }

            }


            return resultval;
        }


        public string CreateService(Dictionary<string, object> ServiceIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            ServiceDictionary=ServiceIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertServiceDetails";
            cmd.Parameters.AddWithValue("@ServiceCode", ServiceDictionary["ServiceCode"]);
            cmd.Parameters.AddWithValue("@ServiceName", ServiceDictionary["ServiceName"]);
            cmd.Parameters.AddWithValue("@Description", ServiceDictionary["Description"]);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", ServiceDictionary["AvailabilityStatus"]);
            cmd.Parameters.AddWithValue("@PerHour", Convert.ToDouble(this.PerHour));
            cmd.Parameters.AddWithValue("@PerHalfDay", Convert.ToDouble(this.PerHalfDay));
            cmd.Parameters.AddWithValue("@FullDay", Convert.ToDouble(this.FullDay));
            cmd.Parameters.AddWithValue("@FullMonth",Convert.ToDouble(this.FullMonth));
            cmd.Parameters.AddWithValue("@Rate", Convert.ToDouble(this.Rate));
            cmd.Parameters.AddWithValue("@Validity",ServiceDictionary["Validity"]);
            cmd.Parameters.AddWithValue("@GST", ServiceDictionary["GST"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //Check for errors using try catch 
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


        public Dictionary<string, object> SelectService(int ServiceId)
        {
            ServiceDict service = new ServiceDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectService";
            cmd.Parameters.AddWithValue("@ServiceId", ServiceId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    if (da==null)
                    {
                        response="Data is Unavailable";
                    }
                    else
                    {
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            dict=service.BindDictionary(ds);
                        }

                    }

                }
                catch (SqlException e)
                {
                    // response=e.ToString();
                    dict.Add("Response", e.ToString());
                }
                finally
                {
                    db.CloseConnection();
                }
                return dict;

            }

        }

        public string DeleteService(int ServiceId)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteService";
            cmd.Parameters.AddWithValue("@ServiceId", ServiceId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //Check for errors using try catch 
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
        public List<Services> ViewAllServices()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Services> list = new List<Services>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewAllService";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool EOF = !reader.Read();

                    while (!EOF)
                    {
                        Services Det = new Services();
                        Det._ServiceId = (int)reader["ServiceId"];
                        Det._ServiceCode = reader["ServiceCode"].ToString();       
                        Det._ServiceName = reader["ServiceName"].ToString();
                        Det._Description = reader["Description"].ToString();
                        Det._AvailabilityStatus = reader["AvailabilityStatus"].ToString();
                        Det._PerHour = double.Parse(reader["PerHour"].ToString());
                        Det._PerHalfDay = double.Parse(reader["PerHalfDay"].ToString());
                        Det._FullDay = double.Parse(reader["FullDay"].ToString());
                        Det._FullMonth = double.Parse(reader["FullMonth"].ToString());
                        Det._Rate = double.Parse(reader["Rate"].ToString());
                        Det._Validity = (DateTime)reader["Validity"];
                        Det._GST=double.Parse((reader["GST"]).ToString());
                        list.Add(Det);
                        EOF = !reader.Read();
                    }
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }

        public List<Services> ShowAllServices()
        { 
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Services> list = new List<Services>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAllService";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool EOF = !reader.Read();

                    while (!EOF)
                    {
                        Services Det = new Services();
                        Det._ServiceCode=reader["ServiceCode"].ToString();
                        Det._ServiceName=reader["ServiceName"].ToString();
                        list.Add(Det);
                        EOF=!reader.Read();
                    }
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }

        //public List<Services> ViewAllServices()
        //{

        //    string response = string.Empty;
        //    DataSet ds = new DataSet();
        //    List<Services> list = new List<Services>();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.Clear();
        //    cmd.CommandType=CommandType.StoredProcedure;
        //    cmd.CommandText="spViewAllService";
        //    using (SqlConnection MyCon = db.OpenConnection())
        //    {
        //        cmd.Connection=MyCon;
        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(ds);
        //            for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
        //            {
        //                if (ds.Tables[0].Rows.Count>0)
        //                {
        //                    Services sub = new Services();
        //                    sub._ServiceId=Convert.ToInt32(ds.Tables[0].Rows[i]["ServiceId"]);
        //                    sub._ServiceCode=ds.Tables[0].Rows[i]["ServiceCode"].ToString();
        //                    sub._ServiceName=ds.Tables[0].Rows[i]["ServiceName"].ToString();
        //                    sub._Description=ds.Tables[0].Rows[i]["Description"].ToString();
        //                    sub._AvailabilityStatus=ds.Tables[0].Rows[i]["AvailabilityStatus"].ToString();
        //                    sub._PerHour=Convert.ToDouble(ds.Tables[0].Rows[i]["PerHour"].ToString());
        //                    sub._PerHalfDay=Convert.ToDouble(ds.Tables[0].Rows[i]["PerHalfDay"].ToString());
        //                    sub._FullDay=Convert.ToDouble(ds.Tables[0].Rows[i]["FullDay"].ToString());
        //                    sub._FullMonth=Convert.ToDouble(ds.Tables[0].Rows[i]["FullMonth"].ToString());
        //                    sub._Rate=Convert.ToDouble(ds.Tables[0].Rows[i]["Rate"].ToString());
        //                    sub._Validity= Convert.ToDateTime(ds.Tables[0].Rows[i]["Validity"].ToString());
        //                    list.Add(sub);
        //                }
        //            }

        //        }
        //        catch (SqlException e)
        //        {
        //            response=e.ToString();

        //        }
        //        finally
        //        {
        //            db.CloseConnection();
        //        }
        //        return list;
        //    }
        //}

        public string UpdateService(Dictionary<string, object> ServiceIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            ServiceDictionary=ServiceIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateService";
            cmd.Parameters.AddWithValue("@ServiceCode", ServiceDictionary["ServiceCode"]);
            cmd.Parameters.AddWithValue("@ServiceName", ServiceDictionary["ServiceName"]);
            cmd.Parameters.AddWithValue("@Description", ServiceDictionary["Description"]);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", ServiceDictionary["AvailabilityStatus"]);
            cmd.Parameters.AddWithValue("@PerHour", ServiceDictionary["PerHour"]);
            cmd.Parameters.AddWithValue("@PerHalfDay", ServiceDictionary["PerHalfDay"]);
            cmd.Parameters.AddWithValue("@FullDay", ServiceDictionary["FullDay"]);
            cmd.Parameters.AddWithValue("@FullMonth", ServiceDictionary["FullMonth"]);
            cmd.Parameters.AddWithValue("@Rate", ServiceDictionary["Rate"]);
            cmd.Parameters.AddWithValue("@ServiceId", ServiceDictionary["ServiceId"]);
            cmd.Parameters.AddWithValue("@Validity", Convert.ToDateTime(ServiceDictionary["Validity"]));
            cmd.Parameters.AddWithValue("@GST", ServiceDictionary["GST"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //Check for errors using try catch 
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

    }
}