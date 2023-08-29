using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class RoomBookingDict 
    {
        public Dictionary<string, object> RoomBookingDictionary { get; set; }
        //DataSet ds = new DataSet();

        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            RoomBookingDictionary = new Dictionary<string, object>();
            RoomBookingDictionary.Add("BookingId", ds.Tables[0].Rows[0]["BookingId"].ToString());
            RoomBookingDictionary.Add("MemberId", ds.Tables[0].Rows[0]["MemberId"].ToString());
            RoomBookingDictionary.Add("RoomNo", ds.Tables[0].Rows[0]["RoomNo"].ToString());
            RoomBookingDictionary.Add("Charges", ds.Tables[0].Rows[0]["Charges"].ToString());
            RoomBookingDictionary.Add("FromDate", ds.Tables[0].Rows[0]["FromDate"].ToString());
            RoomBookingDictionary.Add("ToDate", ds.Tables[0].Rows[0]["ToDate"].ToString());
            RoomBookingDictionary.Add("FromTime", ds.Tables[0].Rows[0]["FromTime"].ToString());
            RoomBookingDictionary.Add("ToTime", ds.Tables[0].Rows[0]["ToTime"].ToString());
            RoomBookingDictionary.Add("AvailabilityStatus", ds.Tables[0].Rows[0]["AvailabilityStatus"].ToString());
         

            return RoomBookingDictionary;
        }
    }
}