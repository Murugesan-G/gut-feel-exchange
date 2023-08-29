using NTCYApplication.Interfaces;
using NTCYApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Room
{
    public class Room : IRoom
    {
         int _RoomId { get; set; } 
         string _RoomName { get; set; }
         int _RoomNo { get; set; }
         string _Status { get; set; } 
         int _Charges { get; set; }
        double _GST { get; set; }
        string _RoomAllocated { get; set; }

        public double GST
        {
            get { return _GST; }
            set { _GST = value; }
        }
        public int RoomId 
        {
            get { return _RoomId; }
            set { _RoomId=value; }
        }

        public string RoomName
        {
            get { return _RoomName; }
            set { _RoomName=value; }
        }
        public int RoomNo
        {
            get { return _RoomNo; }
            set { _RoomNo=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }
        public int Charges
        {
            get { return _Charges; }
            set { _Charges=value; }
        }


        public string RoomAllocated
        {
            get { return _RoomAllocated; } 
            set { _RoomAllocated=value; }
        }
        Dictionary<string, object> RoomDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            RoomDictionary.Add("RoomId", _RoomId);
            RoomDictionary.Add("RoomName", _RoomName);
            RoomDictionary.Add("RoomNo", _RoomNo);
            RoomDictionary.Add("Charges", _Charges);
            RoomDictionary.Add("Status", _Status);
            RoomDictionary.Add("GST", _GST);
            RoomDictionary.Add("RoomAllocated", _RoomAllocated);
            return "Success";
        }

        DBConnection db = new DBConnection();

        public string CreateRoom(Dictionary<string, object> RoomsDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            RoomDictionary=RoomsDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertRoom";
            cmd.Parameters.AddWithValue("@RoomName", RoomDictionary["RoomName"]);
            cmd.Parameters.AddWithValue("@RoomNo", RoomDictionary["RoomNo"]);
            cmd.Parameters.AddWithValue("@Charges", RoomDictionary["Charges"]);
            cmd.Parameters.AddWithValue("@Status", RoomDictionary["Status"]);
            cmd.Parameters.AddWithValue("@GST", RoomDictionary["GST"]);
            cmd.Parameters.AddWithValue("@RoomAllocated", RoomDictionary["RoomAllocated"]);
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

        public string EditRoom(Dictionary<string, object> RoomsDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            RoomDictionary=RoomsDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateRoom";
            cmd.Parameters.AddWithValue("@RoomId", RoomDictionary["RoomId"]);
            cmd.Parameters.AddWithValue("@RoomName", RoomDictionary["RoomName"]);
            cmd.Parameters.AddWithValue("@RoomNo", RoomDictionary["RoomNo"]);
            cmd.Parameters.AddWithValue("@Charges", RoomDictionary["Charges"]);
            cmd.Parameters.AddWithValue("@Status", RoomDictionary["Status"]);
            cmd.Parameters.AddWithValue("@GST", RoomDictionary["GST"]);
            cmd.Parameters.AddWithValue("@RoomAllocated", RoomDictionary["RoomAllocated"]);
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

        public string DeleteRoom(int? RoomId)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteRoom"; 
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
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

        public List<Room> ViewAllRoom()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Room> list = new List<Room>();
            SqlCommand cmd=new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewRoom";
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
                            Room room = new Room();
                            room._RoomId=Convert.ToInt32(ds.Tables[0].Rows[i]["RoomId"]);
                            room._RoomName=ds.Tables[0].Rows[i]["RoomName"].ToString();
                            room._RoomNo=Convert.ToInt32(ds.Tables[0].Rows[i]["RoomNo"].ToString());
                            room._Charges=Convert.ToInt32(ds.Tables[0].Rows[i]["Charges"].ToString());
                            room._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                            room._GST=Convert.ToDouble(ds.Tables[0].Rows[i]["GST"].ToString());
                            room._RoomAllocated=ds.Tables[0].Rows[i]["RoomAllocated"].ToString();
                            list.Add(room);
                        }

                    }
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                    //   RoomDictionary.Add("Response", e.ToString());
                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }

        public Dictionary<string, object> SelectRoom(int? RoomId)
        {

           // SubscriptionDict subscription = new SubscriptionDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectRoom";
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
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
                            dict.Add("RoomId", ds.Tables[0].Rows[0]["RoomId"].ToString());
                            dict.Add("RoomName", ds.Tables[0].Rows[0]["RoomName"].ToString());
                            dict.Add("RoomNo", ds.Tables[0].Rows[0]["RoomNo"].ToString());
                            dict.Add("Charges", ds.Tables[0].Rows[0]["Charges"].ToString());
                            dict.Add("Status", ds.Tables[0].Rows[0]["Status"].ToString());
                            dict.Add("GST", ds.Tables[0].Rows[0]["GST"].ToString());
                            dict.Add("RoomAllocated", ds.Tables[0].Rows[0]["RoomAllocated"].ToString());

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
    }
}