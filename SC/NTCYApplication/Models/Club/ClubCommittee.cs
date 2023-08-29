
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
    public class ClubCommittee : IClubCommmittee
    {
        int _CommitteeId { get; set; }
        int _ClubId { get; set; }
        string _Chairman { get; set; }
        string _President { get; set; }
        string _VicePresident { get; set; }
        string _Secretary { get; set; }
        string _Treasurer { get; set; }
        string _CommitteeMembers { get; set; }
        string _Status { get; set; }
        DateTime _StartDate { get; set; }
        DateTime _EndDate { get; set; }


        public int CommitteeId
        {
            get { return _CommitteeId; }
            set { _CommitteeId = value; }
        }
        public int ClubId
        {
            get { return _ClubId; }
            set { _ClubId = value; }
        }
        public string Chairman
        {
            get { return _Chairman; }
            set { _Chairman = value; }
        }
        public string President
        {
            get { return _President; }
            set { _President = value; }
        }
        public string VicePresident
        {
            get { return _VicePresident; }
            set { _VicePresident = value; }
        }
        public string Secretary
        {
            get { return _Secretary; }
            set { _Secretary = value; }
        }
        public string Treasurer
        {
            get { return _Treasurer; }
            set { _Treasurer = value; }
        }
        public string CommitteeMembers
        {
            get { return _CommitteeMembers; }
            set { _CommitteeMembers = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }


        Dictionary<string, object> ClubComitteeDictionary = new Dictionary<string, object>();
        public string BindDictionary()
        {
            ClubComitteeDictionary.Add("CommitteeId", _CommitteeId);
            ClubComitteeDictionary.Add("ClubId", _ClubId);
            ClubComitteeDictionary.Add("Chairman", _Chairman);
            ClubComitteeDictionary.Add("President", _President);
            ClubComitteeDictionary.Add("VicePresident", _VicePresident);
            ClubComitteeDictionary.Add("Secretary", _Secretary);
            ClubComitteeDictionary.Add("Treasurer", _Treasurer);
            ClubComitteeDictionary.Add("CommitteeMembers", _CommitteeMembers);
            ClubComitteeDictionary.Add("Status", _Status);
            ClubComitteeDictionary.Add("StartDate", _StartDate);
            ClubComitteeDictionary.Add("EndDate", _EndDate);

            return "Success";
        }

        DBConnection db = new DBConnection();

        public string CreateCommittee(Dictionary<string, object> ClubComitteeDictionary)
        { 
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear(); 
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertClubCommitteeDetails";
            cmd.Parameters.AddWithValue("@ClubId", ClubComitteeDictionary["ClubId"]);
            cmd.Parameters.AddWithValue("@Chairman", ClubComitteeDictionary["Chairman"]);
            cmd.Parameters.AddWithValue("@President", ClubComitteeDictionary["President"]);
            cmd.Parameters.AddWithValue("@VicePresident", ClubComitteeDictionary["VicePresident"]);
            cmd.Parameters.AddWithValue("@Secretary", ClubComitteeDictionary["Secretary"]);
            cmd.Parameters.AddWithValue("@Treasurer", ClubComitteeDictionary["Treasurer"]);
            cmd.Parameters.AddWithValue("@CommitteeMembers", ClubComitteeDictionary["CommitteeMembers"]);
            cmd.Parameters.AddWithValue("@Status", ClubComitteeDictionary["Status"]);
            DateTime startDate = DateTime.ParseExact(ClubComitteeDictionary["StartDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            DateTime endDate = DateTime.ParseExact(ClubComitteeDictionary["EndDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

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

        public string UpdateCommittee(Dictionary<string, object> ClubComitteeDictionary)
        {
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateClubCommitteeDetails";
            cmd.Parameters.AddWithValue("@CommitteeId", ClubComitteeDictionary["CommitteeId"]);
            cmd.Parameters.AddWithValue("@ClubId", ClubComitteeDictionary["ClubId"]);
            cmd.Parameters.AddWithValue("@Chairman", ClubComitteeDictionary["Chairman"]);
            cmd.Parameters.AddWithValue("@President", ClubComitteeDictionary["President"]);
            cmd.Parameters.AddWithValue("@VicePresident", ClubComitteeDictionary["VicePresident"]);
            cmd.Parameters.AddWithValue("@Secretary", ClubComitteeDictionary["Secretary"]);
            cmd.Parameters.AddWithValue("@Treasurer", ClubComitteeDictionary["Treasurer"]);
            cmd.Parameters.AddWithValue("@CommitteeMembers", ClubComitteeDictionary["CommitteeMembers"]);
            cmd.Parameters.AddWithValue("@Status", ClubComitteeDictionary["Status"]);
            cmd.Parameters.AddWithValue("@StartDate",Convert.ToDateTime(ClubComitteeDictionary["StartDate"]));
            cmd.Parameters.AddWithValue("@EndDate",Convert.ToDateTime(ClubComitteeDictionary["EndDate"]));

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

        public List<ClubCommittee> ViewCommiteeMembers()
        {
            string CommitteeMembers = string.Empty;
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<ClubCommittee> list = new List<ClubCommittee>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAllClubCommittee";
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
                            ClubCommittee cc = new ClubCommittee();

                            cc._ClubId=Convert.ToInt32(ds.Tables[0].Rows[i]["ClubId"]);
                            cc._CommitteeId=Convert.ToInt32(ds.Tables[0].Rows[i]["CommitteeId"]);
                            string Chairman = ds.Tables[0].Rows[i]["Chairman"].ToString();
                            string President = ds.Tables[0].Rows[i]["President"].ToString()/*.Split(',')*/;
                            string VicePresident = ds.Tables[0].Rows[i]["VicePresident"].ToString();
                            string Secretary = ds.Tables[0].Rows[i]["Secretary"].ToString();
                            string Treasurer = ds.Tables[0].Rows[i]["Treasurer"].ToString();
                            //string[] committeeMembers=ds.Tables[0].Rows[i]["CommitteeMembers"].ToString().Split(',');
                            //for(int j=0; j<committeeMembers.Length; j++)
                            //{

                            //    CommitteeMembers+=committeeMembers[j].ToString().Trim()+",";

                            //}
                            cc._Chairman=Chairman;
                            //cc._StartDate = Convert.ToDateTime(Chairman[1].ToString().Trim());
                            // cc._EndDate = Convert.ToDateTime(Chairman[2].ToString().Trim());
                            cc._President=President/*[0].ToString().Trim()*/;
                            cc._VicePresident=VicePresident;
                            cc._Secretary=Secretary;
                            cc._Treasurer=Treasurer;
                            // cc._CommitteeMembers=CommitteeMembers.ToString().TrimEnd(',');
                            cc._CommitteeMembers=ds.Tables[0].Rows[i]["CommitteeMembers"].ToString();
                            cc._StartDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["StartDate"].ToString());
                            cc._EndDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["EndDate"].ToString());
                            cc._Status=ds.Tables[0].Rows[i]["Status"].ToString();

                            list.Add(cc);
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

        public List<Member> ShowMembers()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Member> list = new List<Member>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText= "spView_AllMembers";
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
                            Member member = new Member();

                            member.MemberId=(ds.Tables[0].Rows[i]["MemberId"].ToString());
                            member.MemberName=ds.Tables[0].Rows[i]["MemberName"].ToString();
                            list.Add(member);
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

        //public string DeleteCommittee(int? CommitteeId)
        //{
        //    string response = string.Empty;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.Clear();
        //    cmd.CommandType=CommandType.StoredProcedure;
        //    cmd.CommandText="spDeleteCommittee";
        //    cmd.Parameters.AddWithValue("@CommitteeId", CommitteeId);

        //    try
        //    {
        //        SqlConnection MyCon = db.OpenConnection();
        //        cmd.Connection=MyCon;
        //        //Check for errors using try catch 
        //        response=cmd.ExecuteNonQuery().ToString();
        //    }
        //    catch (SqlException e)
        //    {
        //        response=e.ToString();
        //    }
        //    db.CloseConnection();
        //    return response;
        //}
         

        public Dictionary<string, object> SelectCommittee(int? CommitteeId)
        {
            ClubCommitteeDict committeeDict = new ClubCommitteeDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectCommittee";
            cmd.Parameters.AddWithValue("@CommitteeId", CommitteeId);
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
                            dict=committeeDict.BindDictionary(ds);
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

        public string CheckCommitteeTenure()
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spCheckCommiteeTenure";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    response=(string)cmd.ExecuteScalar();
                    //response = da.ToString();

                }
                catch (SqlException e)
                {
                    // response=e.ToString();
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