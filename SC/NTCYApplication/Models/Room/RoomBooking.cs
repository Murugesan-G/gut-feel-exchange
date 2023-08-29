//using NTCYApplication.Dictionaries;
//using NTCYApplication.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;

//namespace NTCYApplication.Models.Room
//{
//    public class RoomBooking : IRoomBooking
//    {
//        private int _BookingId { get; set; }
//        private string _MemberId { get; set; }
//        private string _RoomNo { get; set; }
//        private double _Charges { get; set; }
//        private DateTime _FromDate { get; set; }
//        private DateTime _ToDate { get; set; }
//        private string _FromTime { get; set; }
//        private string _ToTime { get; set; }
//        private string _AvailabilityStatus { get; set; }

//        public int BookingId
//        {
//            get { return _BookingId; }
//            set { _BookingId=value; }
//        }

//        public string MemberId
//        {
//            get { return _MemberId; }
//            set { _MemberId=value; }
//        }

//        public string RoomNo
//        {
//            get { return _RoomNo; }
//            set { _RoomNo=value; }
//        }

//        public double Charges
//        {
//            get { return _Charges; }
//            set { _Charges=value; }
//        }

//        public DateTime FromDate
//        {
//            get { return _FromDate; }
//            set { _FromDate=value; }
//        }

//        public DateTime ToDate
//        {
//            get { return _ToDate; }
//            set { _ToDate=value; }
//        }

//        public string FromTime
//        {
//            get { return _FromTime; }
//            set { _FromTime=value; }
//        }

//        public string ToTime
//        {
//            get { return _ToTime; }
//            set { _ToTime=value; }
//        }
//        public string AvailabilityStatus
//        {
//            get { return _AvailabilityStatus; }
//            set { _AvailabilityStatus=value; }
//        }



//        Dictionary<string, object> RoomBookingDictionary = new Dictionary<string, object>();
//        public string BindDictionary()
//        {
//            RoomBookingDictionary.Add("BookingId", _BookingId);
//            RoomBookingDictionary.Add("MemberId", _MemberId);
//            RoomBookingDictionary.Add("RoomNo", _RoomNo);
//            RoomBookingDictionary.Add("Charges", _Charges);
//            RoomBookingDictionary.Add("FromDate", _FromDate);
//            RoomBookingDictionary.Add("ToDate", _ToDate);
//            RoomBookingDictionary.Add("FromTime", _FromTime);
//            RoomBookingDictionary.Add("ToTime", _ToTime);
//            RoomBookingDictionary.Add("AvailabilityStatus", _AvailabilityStatus);
//            return "Success";
//        }

//        DBConnection db = new DBConnection();
//        public int Save()
//        {
//            int resultval = 0;
//            SqlCommand cmd = new SqlCommand();
//            cmd.Parameters.Clear();
//            cmd.CommandType=CommandType.StoredProcedure;

//            if (this.BookingId==0)
//            {
//                cmd.CommandText="spInsertRoomBooking";
//            }
//            else
//            {
//                cmd.CommandText="spUpdateRoomBookingDetails";
//                cmd.Parameters.AddWithValue("@BookingId", this.BookingId);
//            }

//            cmd.Parameters.AddWithValue("@MemberId", this.MemberId);
//            cmd.Parameters.AddWithValue("@RoomNo", this.RoomNo);
//            cmd.Parameters.AddWithValue("@Charges", Convert.ToDouble(this.Charges));
//            cmd.Parameters.AddWithValue("@FromDate", this.FromDate);
//            // DateTime dob = DateTime.ParseExact(this.DOB.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
//            cmd.Parameters.AddWithValue("@ToDate", this.ToDate);
//            cmd.Parameters.AddWithValue("@FromTime", this.FromTime);
//            cmd.Parameters.AddWithValue("@ToTime", this.ToTime);
//            cmd.Parameters.AddWithValue("@AvailabilityStatus", this.AvailabilityStatus);
//            using (SqlConnection MyCon = db.OpenConnection())
//            {
//                cmd.Connection=MyCon;
//                try
//                {
//                    //Check for errors using try catch 
//                    resultval=cmd.ExecuteNonQuery();
//                }
//                //catch (Exception e)
//                //{
//                //    resultval=e.Message.ToString();

//                //}
//                finally
//                {
//                    db.CloseConnection();
//                }

//            }


//            return resultval;
//        }

//        public string AllocateRoom(Dictionary<string, object> RoomBookingIDictionary)
//        {
//            RoomBookingDictionary=RoomBookingIDictionary;
//            string response;
//            SqlCommand cmd = new SqlCommand();
//            cmd.Parameters.Clear();
//            cmd.CommandType=CommandType.StoredProcedure;
//            cmd.CommandText="spInsertRoomBooking";
//            cmd.Parameters.AddWithValue("@MemberId", RoomBookingDictionary["MemberId"]);
//            cmd.Parameters.AddWithValue("@RoomNo", RoomBookingDictionary["RoomNo"]);
//            cmd.Parameters.AddWithValue("@Charges", RoomBookingDictionary["Charges"]);
//            DateTime fromDate = Convert.ToDateTime(RoomBookingDictionary["FromDate"]);
//            cmd.Parameters.AddWithValue("@FromDate", fromDate);
//            DateTime toDate = Convert.ToDateTime(RoomBookingDictionary["ToDate"]);
//            cmd.Parameters.AddWithValue("@ToDate", toDate);
//            cmd.Parameters.AddWithValue("@FromTime", RoomBookingDictionary["FromTime"]);
//            cmd.Parameters.AddWithValue("@ToTime", RoomBookingDictionary["ToTime"]);
//            cmd.Parameters.AddWithValue("@AvailabilityStatus", RoomBookingDictionary["AvailabilityStatus"]);

//            SqlConnection MyCon = db.OpenConnection();
//            cmd.Connection=MyCon;
//            try
//            {
//                //Check for errors using try catch 
//                response=cmd.ExecuteNonQuery().ToString();
//            }
//            catch (SqlException e)
//            {
//                response=e.ToString();
//            }
//            finally
//            {
//                db.CloseConnection();
//            }
//            return response;
//        }

//        public string UnallocateRoom(Dictionary<string, object> RoomBookingIDictionary)
//        {
//            RoomBookingDictionary=RoomBookingIDictionary;
//            string response;
//            SqlCommand cmd = new SqlCommand();
//            cmd.Parameters.Clear();
//            cmd.CommandType=CommandType.StoredProcedure;
//            cmd.CommandText="";
//            cmd.Parameters.AddWithValue("@MemberId", RoomBookingDictionary["MemberId"]);
//            cmd.Parameters.AddWithValue("@RoomNo", RoomBookingDictionary["RoomNo"]);
//            cmd.Parameters.AddWithValue("@Charges", RoomBookingDictionary["Charges"]);
//            cmd.Parameters.AddWithValue("@FromDate", RoomBookingDictionary["FromDate"]);
//            cmd.Parameters.AddWithValue("@ToDate", RoomBookingDictionary["ToDate"]);
//            cmd.Parameters.AddWithValue("@FromTime", RoomBookingDictionary["FromTime"]);
//            cmd.Parameters.AddWithValue("@ToTime", RoomBookingDictionary["FromTime"]);
//            cmd.Parameters.AddWithValue("@AvailabilityStatus", RoomBookingDictionary["AvailabilityStatus"]);

//            SqlConnection MyCon = db.OpenConnection();
//            cmd.Connection=MyCon;
//            try
//            {
//                //Check for errors using try catch 
//                response=cmd.ExecuteNonQuery().ToString();
//            }
//            catch (SqlException e)
//            {
//                response=e.ToString();
//            }
//            finally
//            {
//                db.CloseConnection();
//            }
//            return response;
//        }

//        public Dictionary<string, object> ViewAvailableRooms()
//        {
//            throw new NotImplementedException();
//        }

//        public string DeleteRoomBooking(int BookingId, string RoomNo)
//        {
//            string response = string.Empty;
//            DataSet ds = new DataSet();
//            SqlCommand cmd = new SqlCommand();
//            cmd.Parameters.Clear();
//            cmd.CommandType=CommandType.StoredProcedure;
//            cmd.CommandText="spDeleteRoomBooking";
//            cmd.Parameters.AddWithValue("@BookingId", BookingId);
//            cmd.Parameters.AddWithValue("@RoomName", RoomNo);
//            using (SqlConnection MyCon = db.OpenConnection())
//            {
//                cmd.Connection=MyCon;
//                try
//                {

//                    //Check for errors using try catch 
//                    response=cmd.ExecuteNonQuery().ToString();
//                }
//                catch (SqlException e)
//                {
//                    response=e.ToString();
//                }
//                finally
//                {
//                    db.CloseConnection();
//                }
//                return response;
//            }
//        }

//        public List<RoomBooking> ViewAllRoomBooking()
//        {
//            string response = string.Empty;
//            DataSet ds = new DataSet();
//            List<RoomBooking> list = new List<RoomBooking>();
//            SqlCommand cmd = new SqlCommand();
//            cmd.Parameters.Clear();
//            cmd.CommandType=CommandType.StoredProcedure;
//            cmd.CommandText="spViewAllRoomBookings";
//            using (SqlConnection MyCon = db.OpenConnection())
//            {
//                cmd.Connection=MyCon;
//                try
//                {
//                    SqlDataReader reader = cmd.ExecuteReader();

//                    bool EOF = !reader.Read();

//                    while (!EOF)
//                    {
//                        RoomBooking Det = new RoomBooking();
//                        Det._BookingId=(int)reader["BookingId"];
//                        Det._MemberId=reader["MemberId"].ToString();
//                        Det._RoomNo=reader["RoomNo"].ToString();
//                        Det._Charges=(double)reader["Charges"];
//                        Det._AvailabilityStatus=reader["AvailabilityStatus"].ToString();
//                        Det._FromDate=(DateTime)reader["FromDate"];
//                        //Det._ToDate=(DateTime)reader["ToDate"];
//                        //Det._FromTime=reader["FromTime"].ToString();
//                        //Det._ToTime=reader["ToTime"].ToString();
//                        list.Add(Det);
//                        EOF=!reader.Read();
//                    }
//                }
//                catch (SqlException e)
//                {
//                    response=e.ToString();
//                }
//                finally
//                {
//                    db.CloseConnection();
//                }
//                return list;
//            }
//        }


//        //public List<RoomBooking> ViewAllRoomBooking()
//        //{

//        //    string response = string.Empty;
//        //    DataSet ds = new DataSet();
//        //    List<RoomBooking> list = new List<RoomBooking>();
//        //    SqlCommand cmd = new SqlCommand();
//        //    cmd.Parameters.Clear();
//        //    cmd.CommandType=CommandType.StoredProcedure;
//        //    cmd.CommandText="spViewAllRoomBookings";
//        //    using (SqlConnection MyCon = db.OpenConnection())
//        //    {
//        //        cmd.Connection=MyCon;
//        //        try
//        //        {

//        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
//        //            da.Fill(ds);
//        //            for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
//        //            {
//        //                if (ds.Tables[0].Rows.Count>0)
//        //                {
//        //                    RoomBooking Det = new RoomBooking();
//        //                    Det._BookingId=Convert.ToInt32(ds.Tables[0].Rows[i]["BookingId"].ToString());
//        //                    Det._MemberId=ds.Tables[0].Rows[i]["MemberId"].ToString();
//        //                    Det._RoomNo=ds.Tables[0].Rows[i]["RoomNo"].ToString();
//        //                    Det._Charges=Convert.ToInt32(ds.Tables[0].Rows[i]["Charges"].ToString());
//        //                    Det._FromDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"].ToString());
//        //                    Det._ToDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"].ToString());
//        //                    Det._FromTime= ds.Tables[0].Rows[i]["FromTime"].ToString();
//        //                    Det._ToTime= ds.Tables[0].Rows[i]["ToTime"].ToString();
//        //                    Det._AvailabilityStatus=ds.Tables[0].Rows[i]["AvailabilityStatus"].ToString();

//        //                    list.Add(Det);
//        //                }
//        //            }
//        //        }
//        //        catch (SqlException e)
//        //        {
//        //            response=e.ToString();

//        //        }
//        //        finally
//        //        {
//        //            db.CloseConnection();
//        //        }
//        //        return list;
//        //    }
//        //}

//        public string UpdateRoomBookingDetails(Dictionary<string, object> RoomBookingIDict)
//        {

//            string response;
//            SqlCommand cmd = new SqlCommand();
//            RoomBookingDictionary=RoomBookingIDict;
//            cmd.Parameters.Clear();
//            cmd.CommandType=CommandType.StoredProcedure;
//            cmd.CommandText="spUpdateRoomBookingDetails";
//            cmd.Parameters.AddWithValue("@BookingId", RoomBookingDictionary["BookingId"]);
//            cmd.Parameters.AddWithValue("@MemberId", RoomBookingDictionary["MemberId"]);
//            cmd.Parameters.AddWithValue("@RoomNo", RoomBookingDictionary["RoomNo"]);
//            cmd.Parameters.AddWithValue("@Charges", RoomBookingDictionary["Charges"]);
//            cmd.Parameters.AddWithValue("@FromDate", RoomBookingDictionary["FromDate"]);
//            cmd.Parameters.AddWithValue("@ToDate", RoomBookingDictionary["ToDate"]);
//            cmd.Parameters.AddWithValue("@FromTime", RoomBookingDictionary["FromTime"]);
//            cmd.Parameters.AddWithValue("@ToTime", RoomBookingDictionary["ToTime"]);
//            cmd.Parameters.AddWithValue("@AvailabilityStatus", RoomBookingDictionary["AvailabilityStatus"]);

//            using (SqlConnection MyCon = db.OpenConnection())
//            {
//                cmd.Connection=MyCon;
//                try
//                {
//                    //Check for errors using try catch 
//                    response=cmd.ExecuteNonQuery().ToString();
//                }
//                catch (SqlException e)
//                {
//                    response=e.ToString();
//                }
//                finally
//                {
//                    db.CloseConnection();
//                }
//                return response;
//            }
//        }

//        public Dictionary<string, object> ViewRoomBooking(int BookingId)
//        {
//            Dictionary<string, object> dict = new Dictionary<string, object>();
//            RoomBookingDict RoomBooking = new RoomBookingDict();
//            string ErrorString;
//            DataSet ds = new DataSet();
//            SqlCommand cmd = new SqlCommand();
//            cmd.Parameters.Clear();
//            cmd.CommandType=CommandType.StoredProcedure;
//            cmd.CommandText="spSelectRoomBooking";
//            cmd.Parameters.AddWithValue("@BookingId", BookingId);
//            using (SqlConnection MyCon = db.OpenConnection())
//            {
//                cmd.Connection=MyCon;
//                try
//                {

//                    SqlDataAdapter da = new SqlDataAdapter(cmd);
//                    if (da==null)
//                    {
//                        ErrorString="Data is Unavailable";
//                    }
//                    else
//                    {
//                        da.Fill(ds);
//                        if (ds.Tables[0].Rows.Count>0)
//                        {
//                            dict=RoomBooking.BindDictionary(ds);
//                        }

//                    }

//                }
//                catch (SqlException e)
//                {
//                    ErrorString=e.ToString();
//                }
//                finally
//                {
//                    db.CloseConnection();
//                }
//                return dict;

//            }
//        }
//    }
//}




using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Room
{
    public class RoomBooking : IRoomBooking
    {
        private int _BookingId { get; set; }
        private string _MemberId { get; set; }
        private string _RoomNo { get; set; }
        private double _Charges { get; set; }
        private DateTime _FromDate { get; set; }
        private DateTime _ToDate { get; set; }
        private string _FromTime { get; set; }
        private string _ToTime { get; set; }
        private string _AvailabilityStatus { get; set; }
        private string _ExtraBed { get; set; }

        public string ExtraBed
        {
            get { return _ExtraBed; }
            set { _ExtraBed=value; }
        }
        public int BookingId
        {
            get { return _BookingId; }
            set { _BookingId=value; }
        }

        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId=value; }
        }

        public string RoomNo
        {
            get { return _RoomNo; }
            set { _RoomNo=value; }
        }

        public double Charges
        {
            get { return _Charges; }
            set { _Charges=value; }
        }

        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate=value; }
        }

        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate=value; }
        }

        public string FromTime
        {
            get { return _FromTime; }
            set { _FromTime=value; }
        }

        public string ToTime
        {
            get { return _ToTime; }
            set { _ToTime=value; }
        }
        public string AvailabilityStatus
        {
            get { return _AvailabilityStatus; }
            set { _AvailabilityStatus=value; }
        }



        Dictionary<string, object> RoomBookingDictionary = new Dictionary<string, object>();
        public string BindDictionary()
        {
            RoomBookingDictionary.Add("BookingId", _BookingId);
            RoomBookingDictionary.Add("MemberId", _MemberId);
            RoomBookingDictionary.Add("RoomNo", _RoomNo);
            RoomBookingDictionary.Add("Charges", _Charges);
            RoomBookingDictionary.Add("FromDate", _FromDate);
            RoomBookingDictionary.Add("ToDate", _ToDate);
            RoomBookingDictionary.Add("FromTime", _FromTime);
            RoomBookingDictionary.Add("ToTime", _ToTime);
            RoomBookingDictionary.Add("AvailabilityStatus", _AvailabilityStatus);
            RoomBookingDictionary.Add("ExtraBed", _ExtraBed);
            return "Success";
        }

        DBConnection db = new DBConnection();
        public int Save()
        {
            int resultval = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;

            if (this.BookingId==0)
            {
                cmd.CommandText="spInsertRoomBooking";
            }
            else
            {
                cmd.CommandText="spUpdateRoomBookingDetails";
                cmd.Parameters.AddWithValue("@BookingId", this.BookingId);
            }

            cmd.Parameters.AddWithValue("@MemberId", this.MemberId);
            cmd.Parameters.AddWithValue("@RoomNo", this.RoomNo);
            cmd.Parameters.AddWithValue("@Charges", Convert.ToDouble(this.Charges));
            cmd.Parameters.AddWithValue("@FromDate", this.FromDate);
            //DateTime dob = DateTime.ParseExact(this.DOB.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@ToDate", this.ToDate);
            cmd.Parameters.AddWithValue("@FromTime", this.FromTime);
            cmd.Parameters.AddWithValue("@ToTime", this.ToTime);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", this.AvailabilityStatus);
            cmd.Parameters.AddWithValue("@ExtraBed", this.ExtraBed);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //Check for errors using try catch
                    resultval=cmd.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                //resultval=e.Message.ToString();

                            }
                            finally
                            {
                                db.CloseConnection();
                            }

                }


            return resultval;
            }

        public string AllocateRoom(Dictionary<string, object> RoomBookingIDictionary)
        {
            RoomBookingDictionary=RoomBookingIDictionary;
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertRoomBooking";
            cmd.Parameters.AddWithValue("@MemberId", RoomBookingDictionary["MemberId"]);
            cmd.Parameters.AddWithValue("@RoomNo", RoomBookingDictionary["RoomNo"]);
            cmd.Parameters.AddWithValue("@Charges", RoomBookingDictionary["Charges"]);
            DateTime fromDate = Convert.ToDateTime(RoomBookingDictionary["FromDate"]);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            DateTime toDate = Convert.ToDateTime(RoomBookingDictionary["ToDate"]);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.Parameters.AddWithValue("@FromTime", RoomBookingDictionary["FromTime"]);
            cmd.Parameters.AddWithValue("@ToTime", RoomBookingDictionary["ToTime"]);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", RoomBookingDictionary["AvailabilityStatus"]);
            cmd.Parameters.AddWithValue("@ExtraBed", RoomBookingDictionary["ExtraBed"]);

            SqlConnection MyCon = db.OpenConnection();
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

        public string UnallocateRoom(Dictionary<string, object> RoomBookingIDictionary)
        {
            RoomBookingDictionary=RoomBookingIDictionary;
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="";
            cmd.Parameters.AddWithValue("@MemberId", RoomBookingDictionary["MemberId"]);
            cmd.Parameters.AddWithValue("@RoomNo", RoomBookingDictionary["RoomNo"]);
            cmd.Parameters.AddWithValue("@Charges", RoomBookingDictionary["Charges"]);
            cmd.Parameters.AddWithValue("@FromDate", RoomBookingDictionary["FromDate"]);
            cmd.Parameters.AddWithValue("@ToDate", RoomBookingDictionary["ToDate"]);
            cmd.Parameters.AddWithValue("@FromTime", RoomBookingDictionary["FromTime"]);
            cmd.Parameters.AddWithValue("@ToTime", RoomBookingDictionary["FromTime"]);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", RoomBookingDictionary["AvailabilityStatus"]);
            cmd.Parameters.AddWithValue("@ExtraBed", RoomBookingDictionary["ExtraBed"]);
            SqlConnection MyCon = db.OpenConnection();
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

        public Dictionary<string, object> ViewAvailableRooms()
        {
            throw new NotImplementedException();
        }

        public string DeleteRoomBooking(int BookingId, string RoomNo)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteRoomBooking";
            cmd.Parameters.AddWithValue("@BookingId", BookingId);
            cmd.Parameters.AddWithValue("@RoomName", RoomNo);
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

        public List<RoomBooking> ViewAllRoomBooking()
        {

            string response = string.Empty;
            DataSet ds = new DataSet();
            List<RoomBooking> list = new List<RoomBooking>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAllRoomBookings";
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
                            RoomBooking Det = new RoomBooking();
                            Det._BookingId=Convert.ToInt32(ds.Tables[0].Rows[i]["BookingId"].ToString());
                            Det._MemberId=ds.Tables[0].Rows[i]["MemberId"].ToString();
                            Det._RoomNo=ds.Tables[0].Rows[i]["RoomNo"].ToString();
                            Det._Charges=Convert.ToInt32(ds.Tables[0].Rows[i]["Charges"].ToString());
                            Det._FromDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"].ToString());
                            Det._ToDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"].ToString());
                            Det._FromTime=ds.Tables[0].Rows[i]["FromTime"].ToString();
                            Det._ToTime=ds.Tables[0].Rows[i]["ToTime"].ToString();
                            Det._AvailabilityStatus=ds.Tables[0].Rows[i]["AvailabilityStatus"].ToString();
                            //Det._ExtraBed=ds.Tables[0].Rows[i]["ExtraBed"].ToString();
                            list.Add(Det);
                        }
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

        public string UpdateRoomBookingDetails(Dictionary<string, object> RoomBookingIDict)
        {

            string response;
            SqlCommand cmd = new SqlCommand();
            RoomBookingDictionary=RoomBookingIDict;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateRoomBookingDetails";
            cmd.Parameters.AddWithValue("@BookingId", RoomBookingDictionary["BookingId"]);
            cmd.Parameters.AddWithValue("@MemberId", RoomBookingDictionary["MemberId"]);
            cmd.Parameters.AddWithValue("@RoomNo", RoomBookingDictionary["RoomNo"]);
            cmd.Parameters.AddWithValue("@Charges", RoomBookingDictionary["Charges"]);
            cmd.Parameters.AddWithValue("@FromDate", RoomBookingDictionary["FromDate"]);
            cmd.Parameters.AddWithValue("@ToDate", RoomBookingDictionary["ToDate"]);
            cmd.Parameters.AddWithValue("@FromTime", RoomBookingDictionary["FromTime"]);
            cmd.Parameters.AddWithValue("@ToTime", RoomBookingDictionary["ToTime"]);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", RoomBookingDictionary["AvailabilityStatus"]);
            cmd.Parameters.AddWithValue("@ExtraBed", RoomBookingDictionary["ExtraBed"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                   // Check for errors using try catch
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

        public Dictionary<string, object> ViewRoomBooking(int BookingId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            RoomBookingDict RoomBooking = new RoomBookingDict();
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectRoomBooking";
            cmd.Parameters.AddWithValue("@BookingId", BookingId);
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
                            dict=RoomBooking.BindDictionary(ds);
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