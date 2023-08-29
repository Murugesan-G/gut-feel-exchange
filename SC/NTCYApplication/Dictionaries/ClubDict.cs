using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTCYApplication.Dictionaries
{
    public class ClubDict
    {
        public Dictionary<string, object> ClubDictionary { get; set; }
        DataSet ds = new DataSet();
        public Dictionary<string, object> BindDictionary(DataSet ds)
        {
            ClubDictionary=new Dictionary<string, object>();
            ClubDictionary.Add("ClubId", ds.Tables[0].Rows[0]["ClubId"].ToString());
            ClubDictionary.Add("ClubName", ds.Tables[0].Rows[0]["ClubName"].ToString());
            ClubDictionary.Add("Address", ds.Tables[0].Rows[0]["Address"].ToString());
            ClubDictionary.Add("RegistrationNumber", ds.Tables[0].Rows[0]["RegistrationNumber"].ToString());
            ClubDictionary.Add("LiquorLicense", ds.Tables[0].Rows[0]["LiquorLicense"].ToString());
            ClubDictionary.Add("RestaurantLicense", ds.Tables[0].Rows[0]["RestaurantLicense"].ToString());
            ClubDictionary.Add("GSTNumber", ds.Tables[0].Rows[0]["GSTNumber"].ToString());
            ClubDictionary.Add("PAN", ds.Tables[0].Rows[0]["PAN"].ToString());
            ClubDictionary.Add("TAN", ds.Tables[0].Rows[0]["TAN"].ToString());
            ClubDictionary.Add("MobileNumber", ds.Tables[0].Rows[0]["MobileNumber"].ToString());
            ClubDictionary.Add("ContactPerson", ds.Tables[0].Rows[0]["ContactPerson"].ToString());
            ClubDictionary.Add("PhoneNumber", ds.Tables[0].Rows[0]["PhoneNumber"].ToString());
            ClubDictionary.Add("EmailID", ds.Tables[0].Rows[0]["EmailID"].ToString());
            ClubDictionary.Add("Amenities", ds.Tables[0].Rows[0]["Amenities"].ToString());
            ClubDictionary.Add("Logo", ds.Tables[0].Rows[0]["Logo"].ToString());
            ClubDictionary.Add("About", ds.Tables[0].Rows[0]["About"].ToString());
            ClubDictionary.Add("DateOfIncorporation", ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString());

            return ClubDictionary;
        }
    }
}