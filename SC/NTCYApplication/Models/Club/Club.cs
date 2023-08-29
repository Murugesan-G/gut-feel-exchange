using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using NTCYApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.Club
{
    public class Club : ClubInterface
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string Address { get; set; }
        public string RegistrationNumber { get; set; }
        public string RestaurantLicense { get; set; }
        public string LiquorLicense { get; set; }
        public string GSTNumber { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public DateTime DateOfIncorporation { get; set; }
        public string About { get; set; }
        public string Amenities { get; set; }
        public byte[] Logo { get; set; }
        public string Status { get; set; }
        public string Response { get; set; }
        public Dictionary<string, object> ClubDictionary { get; set; }

        DBConnection db = new DBConnection();
        public string BindDictionary()
        {
            ClubDictionary=new Dictionary<string, object>();
            ClubDictionary.Add("ClubId", ClubId);
            ClubDictionary.Add("ClubName", ClubName);
            ClubDictionary.Add("Address", Address);
            ClubDictionary.Add("RegistrationNumber", RegistrationNumber);
            ClubDictionary.Add("RestaurantLicense", RestaurantLicense);
            ClubDictionary.Add("LiquorLicense", LiquorLicense);
            ClubDictionary.Add("GSTNumber", GSTNumber);
            ClubDictionary.Add("ContactPerson", ContactPerson);
            ClubDictionary.Add("PhoneNumber", PhoneNumber);
            ClubDictionary.Add("MobileNumber", MobileNumber);
            ClubDictionary.Add("EmailID", EmailID);
            ClubDictionary.Add("PAN", PAN);
            ClubDictionary.Add("TAN", TAN);
            ClubDictionary.Add("DateOfIncorporation", DateOfIncorporation);
            ClubDictionary.Add("Logo", Logo);
            ClubDictionary.Add("Status", Status);
            ClubDictionary.Add("Response", Response);
            ClubDictionary.Add("About", About);
            ClubDictionary.Add("Amenities", Amenities);

            return "Success";
        }

        public string CreateClubDetails(Dictionary<string, object> ClubCDictionary)
        {

            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            ClubDictionary=ClubCDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertClubDetails";
            cmd.Parameters.AddWithValue("@ClubName", ClubDictionary["ClubName"]);
            cmd.Parameters.AddWithValue("@Address", ClubDictionary["Address"]);
            cmd.Parameters.AddWithValue("@RegistrationNumber", ClubDictionary["RegistrationNumber"]);
            cmd.Parameters.AddWithValue("@LiquorLicense", ClubDictionary["LiquorLicense"]);
            cmd.Parameters.AddWithValue("@RestaurantLicense", ClubDictionary["RestaurantLicense"]);
            cmd.Parameters.AddWithValue("@GSTNumber", ClubDictionary["GSTNumber"]);
            cmd.Parameters.AddWithValue("@EmailID", ClubDictionary["EmailID"]);
            cmd.Parameters.AddWithValue("@PAN", ClubDictionary["PAN"]);
            cmd.Parameters.AddWithValue("@TAN", ClubDictionary["TAN"]);
            cmd.Parameters.AddWithValue("@MobileNumber", ClubDictionary["MobileNumber"]);
            cmd.Parameters.AddWithValue("@ContactPerson", ClubDictionary["ContactPerson"]);
            cmd.Parameters.AddWithValue("@PhoneNumber", ClubDictionary["PhoneNumber"]);
            cmd.Parameters.AddWithValue("@Amenities", ClubDictionary["Amenities"]);
            cmd.Parameters.AddWithValue("@About", ClubDictionary["About"]);
           // cmd.Parameters.AddWithValue("@Logo", ClubDictionary["Logo"]);
            cmd.Parameters.AddWithValue("@DateOfIncorporation", ClubDictionary["DateOfIncorporation"]);


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

        public List<Club> ViewClubDetails(int ClubId)
        {
            ClubDict club = new ClubDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            List<Club> clubList = new List<Club>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectClubDetails";
            cmd.Parameters.AddWithValue("@ClubId", ClubId);
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
                           
                            for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                            {
                                Club clubDetails = new Club();
                                clubDetails.ClubId=Convert.ToInt16(ds.Tables[0].Rows[i]["ClubId"]);
                                clubDetails.ClubName=ds.Tables[0].Rows[i]["ClubName"].ToString();
                                clubDetails.Address=ds.Tables[0].Rows[i]["Address"].ToString();
                                clubDetails.RegistrationNumber=ds.Tables[0].Rows[i]["RegistrationNumber"].ToString();
                                clubDetails.LiquorLicense=ds.Tables[0].Rows[i]["LiquorLicense"].ToString();
                                clubDetails.RestaurantLicense=ds.Tables[0].Rows[i]["RestaurantLicense"].ToString();
                                clubDetails.GSTNumber=ds.Tables[0].Rows[i]["GSTNumber"].ToString();
                                clubDetails.PAN=ds.Tables[0].Rows[i]["PAN"].ToString();
                                clubDetails.TAN=ds.Tables[0].Rows[i]["TAN"].ToString();
                                clubDetails.MobileNumber=ds.Tables[0].Rows[i]["MobileNumber"].ToString();
                                clubDetails.ContactPerson=ds.Tables[0].Rows[i]["ContactPerson"].ToString();
                                clubDetails.PhoneNumber=ds.Tables[0].Rows[i]["PhoneNumber"].ToString();
                                clubDetails.EmailID=ds.Tables[0].Rows[i]["EmailID"].ToString();
                                clubDetails.Amenities=ds.Tables[0].Rows[i]["Amenities"].ToString();
                                // clubDetails.Logo= ds.Tables[0].Rows[0]["Logo"].ToString());
                                clubDetails.About=ds.Tables[0].Rows[i]["About"].ToString();
                                clubDetails.DateOfIncorporation=Convert.ToDateTime(ds.Tables[0].Rows[i]["DateOfIncorporation"]);
                                clubList.Add(clubDetails);
                            }
                           
                            dict=club.BindDictionary(ds);
                        }

                    }

                }
                catch (SqlException e)
                {
                    // response=e.ToString();
                    ClubDictionary.Add("Response", e.ToString());
                }
                finally
                {
                    db.CloseConnection();
                }
                return clubList;
            }
        }

        public string UpdateClubDetails(Dictionary<string, object> ClubCDictionary)
        {
            string response;
            ClubDictionary=ClubCDictionary;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateClubDetails";
            cmd.Parameters.AddWithValue("@ClubId", ClubDictionary["ClubId"]);
            cmd.Parameters.AddWithValue("@ClubName", ClubDictionary["ClubName"]);
            cmd.Parameters.AddWithValue("@Address", ClubDictionary["Address"]);
            cmd.Parameters.AddWithValue("@RegistrationNumber", ClubDictionary["RegistrationNumber"]);
            cmd.Parameters.AddWithValue("@LiquorLicense", ClubDictionary["LiquorLicense"]);
            cmd.Parameters.AddWithValue("@RestaurantLicense", ClubDictionary["RestaurantLicense"]);
            cmd.Parameters.AddWithValue("@GSTNumber", ClubDictionary["GSTNumber"]);
            cmd.Parameters.AddWithValue("@EmailID", ClubDictionary["EmailID"]);
            cmd.Parameters.AddWithValue("@PAN", ClubDictionary["PAN"]);
            cmd.Parameters.AddWithValue("@TAN", ClubDictionary["TAN"]);
            cmd.Parameters.AddWithValue("@MobileNumber", ClubDictionary["MobileNumber"]);
            cmd.Parameters.AddWithValue("@ContactPerson", ClubDictionary["ContactPerson"]);
            cmd.Parameters.AddWithValue("@PhoneNumber", ClubDictionary["PhoneNumber"]);
            cmd.Parameters.AddWithValue("@Amenities", ClubDictionary["Amenities"]);
            cmd.Parameters.AddWithValue("@About", ClubDictionary["About"]);
            //cmd.Parameters.AddWithValue("@Logo", ClubDictionary["Logo"]);
            //  DateTime doi = DateTime.ParseExact(ClubDictionary["DateOfIncorporation"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DateOfIncorporation", Convert.ToDateTime(ClubDictionary["DateOfIncorporation"]));

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


        public Dictionary<string, object> EditClubDetails(int ClubId)
        {
            ClubDict club = new ClubDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            List<Club> clubList = new List<Club>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectClubDetails";
            cmd.Parameters.AddWithValue("@ClubId", ClubId);
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
                            dict=club.BindDictionary(ds);
                        }

                    }

                }
                catch (SqlException e)
                {
                    // response=e.ToString();
                    ClubDictionary.Add("Response", e.ToString());
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