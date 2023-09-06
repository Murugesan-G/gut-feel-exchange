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
    public class Member : IMember
    {
        private int _MemId;
        private string _MemberId;
        private int _ClubId;
        private string _MembershipNo;
        private string _MemberName;
        private DateTime _DOB;
        private string _Gender;
        private string _Address;
        private string _MobileNo;
        private string _AltMobileNo;
        private string _EmailId;
        private string _ProximityCardNo;
        private string _Guests;
        private string _GuestCards;
        private string _AmenitiesInterested;
        private string _MembershipType;
        private DateTime _MemberShipStartDate;
        private string _MemberShipStatus;
        private double _InitialMembershipAmount;
        private DateTime _MemberSince;
        private DateTime _MembershipValidity;
        private double _LastSubscriptionPaid;
        private double _SubscriptionAmountPaid;
        private string _SpouseName;
        private string _FathersName;
        private string _Child1sName;
        private string _Child2sName;
        private string _Alive;
        private string _Qualification;
        private string _MaritalStatus;
        private string _Profession;
        private DateTime _DOBOfChild1;
        private DateTime _DOBOfChild2;
        private DateTime _DOBOfSpouse;
        private DateTime _DOBOfFather;
        private string _Hobbies;
        private double _Balance;
        private string _PaymentStatus;
        private string _Status;
        private string _MemberType;
        private string _Salutation;

        public int MemId
        {
            get { return _MemId; }
            set { _MemId = value; }
        }
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        public int ClubId
        {
            get { return _ClubId; }
            set { _ClubId = value; }
        }
        public string MembershipNo
        {
            get { return _MembershipNo; }
            set { _MembershipNo = value; }
        }
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string AltMobileNo
        {
            get { return _AltMobileNo; }
            set { _AltMobileNo = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string ProximityCardNo
        {
            get { return _ProximityCardNo; }
            set { _ProximityCardNo = value; }
        }
        public string Guests
        {
            get { return _Guests; }
            set { _Guests = value; }
        }
        public string GuestCards
        {
            get { return _GuestCards; }
            set { _GuestCards = value; }
        }
        public string AmenitiesInterested
        {
            get { return _AmenitiesInterested; }
            set { _AmenitiesInterested = value; }
        }
        public string MembershipType
        {
            get { return _MembershipType; }
            set { _MembershipType = value; }
        }
        public DateTime MemberShipStartDate
        {
            get { return _MemberShipStartDate; }
            set { _MemberShipStartDate = value; }
        }
        public string MemberShipStatus
        {
            get { return _MemberShipStatus; }
            set { _MemberShipStatus = value; }
        }
        public double InitialMembershipAmount
        {
            get { return _InitialMembershipAmount; }
            set { _InitialMembershipAmount = value; }
        }
        public DateTime MemberSince
        {
            get { return _MemberSince; }
            set { _MemberSince = value; }
        }
        public DateTime MembershipValidity
        {
            get { return _MembershipValidity; }
            set { _MembershipValidity = value; }
        }
        public double LastSubscriptionPaid
        {
            get { return _LastSubscriptionPaid; }
            set { _LastSubscriptionPaid = value; }
        }
        public double SubscriptionAmountPaid
        {
            get { return _SubscriptionAmountPaid; }
            set { _SubscriptionAmountPaid = value; }
        }
        public string SpouseName
        {
            get { return _SpouseName; }
            set { _SpouseName = value; }
        }
        public string FathersName
        {
            get { return _FathersName; }
            set { _FathersName = value; }
        }
        public string Child1sName
        {
            get { return _Child1sName; }
            set { _Child1sName = value; }
        }
        public string Child2sName
        {
            get { return _Child2sName; }
            set { _Child2sName = value; }
        }
        public string Alive
        {
            get { return _Alive; }
            set { _Alive = value; }
        }
        public string Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; }
        }
        public string MaritalStatus
        {
            get { return _MaritalStatus; }
            set { _MaritalStatus = value; }
        }
        public string Profession
        {
            get { return _Profession; }
            set { _Profession = value; }
        }
        public DateTime DOBOfChild1
        {
            get { return _DOBOfChild1; }
            set { _DOBOfChild1 = value; }
        }
        public DateTime DOBOfChild2
        {
            get { return _DOBOfChild2; }
            set { _DOBOfChild2 = value; }
        }
        public DateTime DOBOfSpouse
        {
            get { return _DOBOfSpouse; }
            set { _DOBOfSpouse = value; }
        }
        public DateTime DOBOfFather
        {
            get { return _DOBOfFather; }
            set { _DOBOfFather = value; }
        }
        public string Hobbies
        {
            get { return _Hobbies; }
            set { _Hobbies = value; }
        }
        public double Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }
        public string PaymentStatus
        {
            get { return _PaymentStatus; }
            set { _PaymentStatus = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string MemberType
        {
            get { return _MemberType; }
            set { _MemberType = value; }
        }
        public string Salutation
        {
            get { return _Salutation; }
            set { _Salutation = value; }
        }

        Dictionary<string, object> MemberDictionary = new Dictionary<string, object>();
        public string BindDictionary()
        {
            MemberDictionary.Add("MemberId", _MemberId);
            MemberDictionary.Add("ClubId", _ClubId);
            MemberDictionary.Add("MembershipNo", _MembershipNo);
            MemberDictionary.Add("MemberName", _MemberName);
            MemberDictionary.Add("Address", _Address);
            MemberDictionary.Add("DOB", _DOB);
            MemberDictionary.Add("Gender", _Gender);
            MemberDictionary.Add("MobileNo", _MobileNo);
            MemberDictionary.Add("AltMobileNo", _AltMobileNo);
            MemberDictionary.Add("EmailId", _EmailId);
            MemberDictionary.Add("ProximityCardNo", _ProximityCardNo);
            MemberDictionary.Add("Guests", _Guests);
            MemberDictionary.Add("GuestCards", _GuestCards);
            MemberDictionary.Add("AmenitiesInterested", _AmenitiesInterested);
            MemberDictionary.Add("MembershipType", _MembershipType);
            MemberDictionary.Add("MemberShipStartDate", _MemberShipStartDate);
            MemberDictionary.Add("MemberShipStatus", _MemberShipStatus);
            MemberDictionary.Add("MemberSince", _MemberSince);
            MemberDictionary.Add("InitialMembershipAmount", _InitialMembershipAmount);
            MemberDictionary.Add("MembershipValidity", _MembershipValidity);
            MemberDictionary.Add("LastSubscriptionPaid", _LastSubscriptionPaid);
            MemberDictionary.Add("SubscriptionAmountPaid", _SubscriptionAmountPaid);
            MemberDictionary.Add("SpouseName", _SpouseName);
            MemberDictionary.Add("FathersName", _FathersName);
            MemberDictionary.Add("Child1sName", _Child1sName);
            MemberDictionary.Add("Child2sName", _Child2sName);
            MemberDictionary.Add("Alive", _Alive);
            MemberDictionary.Add("Qualification", _Qualification);
            MemberDictionary.Add("MaritalStatus", _MaritalStatus);
            MemberDictionary.Add("Profession", _Profession);
            MemberDictionary.Add("DOBOfChild1", _DOBOfChild1);
            MemberDictionary.Add("DOBOfChild2", _DOBOfChild2);
            MemberDictionary.Add("DOBOfSpouse", _DOBOfSpouse);
            MemberDictionary.Add("DOBOfFather", _DOBOfFather);
            MemberDictionary.Add("Hobbies", _Hobbies);
            MemberDictionary.Add("Balance", _Balance);
            MemberDictionary.Add("PaymentStatus", _PaymentStatus);
            MemberDictionary.Add("MemberType", _MemberType);
            MemberDictionary.Add("Salutation", _Salutation);
            return "Success";
        }

        DBConnection db = new DBConnection();

        public int Save()
        {
            int resultval = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;

            //Change this after the completion of data entry of members

            if (this.MemId == 0)
            {
                cmd.CommandText = "spInsertMemberDetails";
            }
            else
            {
                cmd.CommandText = "spUpdateMemberDetails";
                cmd.Parameters.AddWithValue("@MemId", this.MemId);
            }
            cmd.Parameters.AddWithValue("@MemberId", this.MemberId);
            cmd.Parameters.AddWithValue("@MembershipNo", this.MembershipNo);
            cmd.Parameters.AddWithValue("@ClubId", this.ClubId);
            cmd.Parameters.AddWithValue("@MemberName", this.MemberName);
            cmd.Parameters.AddWithValue("@Address", this.Address);
            // DateTime dob = DateTime.ParseExact(this.DOB.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOB", this.DOB);
            cmd.Parameters.AddWithValue("@Gender", this.Gender);
            cmd.Parameters.AddWithValue("@MobileNo", this.MobileNo);
            cmd.Parameters.AddWithValue("@AltMobileNo", this.AltMobileNo);
            cmd.Parameters.AddWithValue("@EmailId", this.EmailId);
            cmd.Parameters.AddWithValue("@ProximityCardNo", this.ProximityCardNo);
            cmd.Parameters.AddWithValue("@Guests", this.Guests);
            cmd.Parameters.AddWithValue("@GuestCards", this.GuestCards);
            cmd.Parameters.AddWithValue("@AmenitiesInterested", this.AmenitiesInterested);
            cmd.Parameters.AddWithValue("@MembershipType", this.MembershipType);
            //DateTime memberShipStartDate = DateTime.ParseExact(this.MemberShipStartDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MemberShipStartDate", this.MemberShipStartDate);
            //DateTime memberSince = DateTime.ParseExact(this.MemberSince.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MemberSince", this.MemberSince);
            cmd.Parameters.AddWithValue("@MemberShipStatus", this.MemberShipStatus);
            cmd.Parameters.AddWithValue("@InitialMembershipAmount", Convert.ToDouble(this.InitialMembershipAmount));
            //DateTime membershipValidity = DateTime.ParseExact(this.MembershipValidity.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MembershipValidity", this.MembershipValidity);
            cmd.Parameters.AddWithValue("@LastSubscriptionPaid", Convert.ToDouble(this.LastSubscriptionPaid));
            cmd.Parameters.AddWithValue("@SubscriptionAmountPaid", Convert.ToDouble(this.SubscriptionAmountPaid));
            cmd.Parameters.AddWithValue("@SpouseName", this.SpouseName);
            cmd.Parameters.AddWithValue("@FathersName", this.FathersName);
            cmd.Parameters.AddWithValue("@Child1sName", this.Child1sName);
            cmd.Parameters.AddWithValue("@Child2sName", this.Child2sName);
            cmd.Parameters.AddWithValue("@Alive", this.Alive);
            cmd.Parameters.AddWithValue("@Qualification", this.Qualification);
            cmd.Parameters.AddWithValue("@MaritalStatus", this.MaritalStatus);
            cmd.Parameters.AddWithValue("@Profession", this.Profession);
            //DateTime dobofchild1 = DateTime.ParseExact(this.DOBOfChild1.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfChild1", this.DOBOfChild1);
            //DateTime dobofchild2 = DateTime.ParseExact(this.DOBOfChild2.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfChild2", this.DOBOfChild2);
            //DateTime dobofspouse = DateTime.ParseExact(this.DOBOfSpouse.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfSpouse", this.DOBOfSpouse);
            //DateTime doboffather = DateTime.ParseExact(this.DOBOfFather.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfFather", this.DOBOfFather);
            cmd.Parameters.AddWithValue("@Hobbies", this.Hobbies);
            cmd.Parameters.AddWithValue("@Balance", this.Balance);
            cmd.Parameters.AddWithValue("@PaymentStatus", this.PaymentStatus);
            cmd.Parameters.AddWithValue("@MemberType", this.MemberType);
            cmd.Parameters.AddWithValue("@Salutation", this.Salutation);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    //Check for errors using try catch 
                    resultval = cmd.ExecuteNonQuery();
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

        public string CreateMember(Dictionary<string, string> MemberDictionary)
        {
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertMemberDetails";
            cmd.Parameters.AddWithValue("@MembershipNo", MemberDictionary["MembershipNo"]);
            cmd.Parameters.AddWithValue("@ClubId", int.Parse(MemberDictionary["ClubId"]));
            cmd.Parameters.AddWithValue("@MemberName", MemberDictionary["MemberName"]);
            cmd.Parameters.AddWithValue("@Address", MemberDictionary["Address"]);
            // DateTime dob = DateTime.ParseExact(MemberDictionary["DOB"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(MemberDictionary["DOB"].ToString()));
            cmd.Parameters.AddWithValue("@Gender", MemberDictionary["Gender"]);
            cmd.Parameters.AddWithValue("@MobileNo", MemberDictionary["MobileNo"]);
            cmd.Parameters.AddWithValue("@AltMobileNo", MemberDictionary["AltMobileNo"]);
            cmd.Parameters.AddWithValue("@EmailId", MemberDictionary["EmailId"]);
            cmd.Parameters.AddWithValue("@ProximityCardNo", MemberDictionary["ProximityCardNo"]);
            cmd.Parameters.AddWithValue("@Guests", MemberDictionary["Guests"]);
            cmd.Parameters.AddWithValue("@GuestCards", MemberDictionary["GuestCards"]);
            cmd.Parameters.AddWithValue("@AmenitiesInterested", MemberDictionary["AmenitiesInterested"]);
            cmd.Parameters.AddWithValue("@MembershipType", MemberDictionary["MembershipType"]);
            //DateTime memberShipStartDate = DateTime.ParseExact(MemberDictionary["MemberShipStartDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MemberShipStartDate", Convert.ToDateTime(MemberDictionary["MemberShipStartDate"].ToString()));
            //DateTime memberSince = DateTime.ParseExact(MemberDictionary["MemberSince"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MemberSince", Convert.ToDateTime(MemberDictionary["MemberSince"].ToString()));
            cmd.Parameters.AddWithValue("@MemberShipStatus", MemberDictionary["MemberShipStatus"]);
            cmd.Parameters.AddWithValue("@InitialMembershipAmount", Convert.ToDouble(MemberDictionary["InitialMembershipAmount"]));
            //DateTime membershipValidity = DateTime.ParseExact(MemberDictionary["MembershipValidity"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MembershipValidity", Convert.ToDateTime(MemberDictionary["MembershipValidity"].ToString()));
            cmd.Parameters.AddWithValue("@LastSubscriptionPaid", Convert.ToDouble(MemberDictionary["LastSubscriptionPaid"]));
            cmd.Parameters.AddWithValue("@SubscriptionAmountPaid", Convert.ToDouble(MemberDictionary["SubscriptionAmountPaid"]));
            cmd.Parameters.AddWithValue("@SpouseName", MemberDictionary["SpouseName"]);
            cmd.Parameters.AddWithValue("@FathersName", MemberDictionary["FathersName"]);
            cmd.Parameters.AddWithValue("@Child1sName", MemberDictionary["Child1sName"]);
            cmd.Parameters.AddWithValue("@Child2sName", MemberDictionary["Child2sName"]);
            cmd.Parameters.AddWithValue("@Alive", MemberDictionary["Alive"]);
            cmd.Parameters.AddWithValue("@Qualification", MemberDictionary["Qualification"]);
            cmd.Parameters.AddWithValue("@MaritalStatus", MemberDictionary["MaritalStatus"]);
            cmd.Parameters.AddWithValue("@Profession", MemberDictionary["Profession"]);
            //DateTime dobofchild1 = DateTime.ParseExact(MemberDictionary["DOBOfChild1"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfChild1", Convert.ToDateTime(MemberDictionary["DOBOfChild1"].ToString()));
            //DateTime dobofchild2 = DateTime.ParseExact(MemberDictionary["DOBOfChild2"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfChild2", Convert.ToDateTime(MemberDictionary["DOBOfChild2"].ToString()));
            //DateTime dobofspouse = DateTime.ParseExact(MemberDictionary["DOBOfSpouse"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfSpouse", Convert.ToDateTime(MemberDictionary["DOBOfSpouse"].ToString()));
            //DateTime doboffather = DateTime.ParseExact(MemberDictionary["DOBOfFather"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfFather", Convert.ToDateTime(MemberDictionary["DOBOfFather"].ToString()));
            cmd.Parameters.AddWithValue("@Hobbies", MemberDictionary["Hobbies"]);
            cmd.Parameters.AddWithValue("@Balance", Convert.ToDouble(MemberDictionary["Balance"]));
            cmd.Parameters.AddWithValue("@PaymentStatus", MemberDictionary["PaymentStatus"]);
            cmd.Parameters.AddWithValue("@MemberType", MemberDictionary["MemberType"]);
            cmd.Parameters.AddWithValue("@Salutation", MemberDictionary["Salutation"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    //Check for errors using try catch 
                    response = cmd.ExecuteNonQuery().ToString();
                }
                catch (Exception e)
                {
                    response = e.Message.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public string UpdateMember(Dictionary<string, string> MemberDictionary)
        {
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spUpdateMemberDetails";
            cmd.Parameters.AddWithValue("@MemberId", int.Parse(MemberDictionary["MemberId"]));
            cmd.Parameters.AddWithValue("@MembershipNo", MemberDictionary["MembershipNo"]);
            cmd.Parameters.AddWithValue("@ClubId", MemberDictionary["ClubId"]);
            cmd.Parameters.AddWithValue("@MemberName", MemberDictionary["MemberName"]);
            cmd.Parameters.AddWithValue("@Address", MemberDictionary["Address"]);
            // DateTime dob = DateTime.ParseExact(MemberDictionary["DOB"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(MemberDictionary["DOB"].ToString()));
            cmd.Parameters.AddWithValue("@Gender", MemberDictionary["Gender"]);
            cmd.Parameters.AddWithValue("@MobileNo", MemberDictionary["MobileNo"]);
            cmd.Parameters.AddWithValue("@AltMobileNo", MemberDictionary["AltMobileNo"]);
            cmd.Parameters.AddWithValue("@EmailId", MemberDictionary["EmailId"]);
            cmd.Parameters.AddWithValue("@ProximityCardNo", MemberDictionary["ProximityCardNo"]);
            cmd.Parameters.AddWithValue("@Guests", MemberDictionary["Guests"]);
            cmd.Parameters.AddWithValue("@GuestCards", MemberDictionary["GuestCards"]);
            cmd.Parameters.AddWithValue("@AmenitiesInterested", MemberDictionary["AmenitiesInterested"]);
            cmd.Parameters.AddWithValue("@MembershipType", MemberDictionary["MembershipType"]);
            //DateTime memberShipStartDate = DateTime.ParseExact(MemberDictionary["MemberShipStartDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MemberShipStartDate", Convert.ToDateTime(MemberDictionary["MemberShipStartDate"].ToString()));
            //DateTime memberSince = DateTime.ParseExact(MemberDictionary["MemberSince"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MemberSince", Convert.ToDateTime(MemberDictionary["MemberSince"].ToString()));
            cmd.Parameters.AddWithValue("@MemberShipStatus", MemberDictionary["MemberShipStatus"]);
            cmd.Parameters.AddWithValue("@InitialMembershipAmount", Convert.ToDouble(MemberDictionary["InitialMembershipAmount"]));
            //DateTime membershipValidity = DateTime.ParseExact(MemberDictionary["MembershipValidity"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@MembershipValidity", Convert.ToDateTime(MemberDictionary["MembershipValidity"].ToString()));
            cmd.Parameters.AddWithValue("@LastSubscriptionPaid", Convert.ToDouble(MemberDictionary["LastSubscriptionPaid"]));
            cmd.Parameters.AddWithValue("@SubscriptionAmountPaid", Convert.ToDouble(MemberDictionary["SubscriptionAmountPaid"]));
            cmd.Parameters.AddWithValue("@SpouseName", MemberDictionary["SpouseName"]);
            cmd.Parameters.AddWithValue("@FathersName", MemberDictionary["FathersName"]);
            cmd.Parameters.AddWithValue("@Child1sName", MemberDictionary["Child1sName"]);
            cmd.Parameters.AddWithValue("@Child2sName", MemberDictionary["Child2sName"]);
            cmd.Parameters.AddWithValue("@Alive", MemberDictionary["Alive"]);
            cmd.Parameters.AddWithValue("@Qualification", MemberDictionary["Qualification"]);
            cmd.Parameters.AddWithValue("@MaritalStatus", MemberDictionary["MaritalStatus"]);
            cmd.Parameters.AddWithValue("@Profession", MemberDictionary["Profession"]);
            //DateTime dobofchild1 = DateTime.ParseExact(MemberDictionary["DOBOfChild1"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfChild1", Convert.ToDateTime(MemberDictionary["DOBOfChild1"].ToString()));
            //DateTime dobofchild2 = DateTime.ParseExact(MemberDictionary["DOBOfChild2"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfChild2", Convert.ToDateTime(MemberDictionary["DOBOfChild2"].ToString()));
            //DateTime dobofspouse = DateTime.ParseExact(MemberDictionary["DOBOfSpouse"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfSpouse", Convert.ToDateTime(MemberDictionary["DOBOfSpouse"].ToString()));
            //DateTime doboffather = DateTime.ParseExact(MemberDictionary["DOBOfFather"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOBOfFather", Convert.ToDateTime(MemberDictionary["DOBOfFather"].ToString()));
            cmd.Parameters.AddWithValue("@Hobbies", MemberDictionary["Hobbies"]);
            cmd.Parameters.AddWithValue("@Balance", Convert.ToDouble(MemberDictionary["Balance"]));
            cmd.Parameters.AddWithValue("@PaymentStatus", MemberDictionary["PaymentStatus"]);
            cmd.Parameters.AddWithValue("@MemberType", MemberDictionary["MemberType"]);
            cmd.Parameters.AddWithValue("@Salutation", MemberDictionary["Salutation"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    //Check for errors using try catch 
                    response = cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public Dictionary<string, object> ViewMemberDetails(int MemId)
        {
            string ErrorString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewMember";
            cmd.Parameters.AddWithValue("@MemId", MemId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    if (da == null)
                    {
                        ErrorString = "Data is Unavailable";
                    }
                    else
                    {
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MemberDictionary = new Dictionary<string, object>();
                            MemberDictionary.Add("MemId", ds.Tables[0].Rows[0]["MemberId"].ToString());
                            MemberDictionary.Add("MemberId", ds.Tables[0].Rows[0]["MemberId"].ToString());
                            MemberDictionary.Add("MembershipNo", ds.Tables[0].Rows[0]["MembershipNo"].ToString());
                            MemberDictionary.Add("ClubId", ds.Tables[0].Rows[0]["ClubId"].ToString());
                            MemberDictionary.Add("MemberName", ds.Tables[0].Rows[0]["MemberName"].ToString());
                            MemberDictionary.Add("MemberSince", ds.Tables[0].Rows[0]["MemberSince"].ToString());
                            MemberDictionary.Add("DOB", ds.Tables[0].Rows[0]["DOB"].ToString());
                            MemberDictionary.Add("Gender", ds.Tables[0].Rows[0]["Gender"].ToString());
                            MemberDictionary.Add("Address", ds.Tables[0].Rows[0]["Address"].ToString());
                            MemberDictionary.Add("MobileNo", ds.Tables[0].Rows[0]["MobileNo"].ToString());
                            MemberDictionary.Add("AltMobileNo", ds.Tables[0].Rows[0]["AltMobileNo"].ToString());
                            MemberDictionary.Add("EmailId", ds.Tables[0].Rows[0]["EmailId"].ToString());
                            MemberDictionary.Add("ProximityCardNo", ds.Tables[0].Rows[0]["ProximityCardNo"].ToString());
                            MemberDictionary.Add("Guests", ds.Tables[0].Rows[0]["Guests"].ToString());
                            MemberDictionary.Add("GuestCards", ds.Tables[0].Rows[0]["GuestCards"].ToString());
                            MemberDictionary.Add("AmenitiesInterested", ds.Tables[0].Rows[0]["AmenitiesInterested"].ToString());
                            MemberDictionary.Add("MembershipType", ds.Tables[0].Rows[0]["MembershipType"].ToString());
                            MemberDictionary.Add("MemberShipStartDate", ds.Tables[0].Rows[0]["MemberShipStartDate"].ToString());
                            MemberDictionary.Add("MemberShipStatus", ds.Tables[0].Rows[0]["MemberShipStatus"].ToString());
                            MemberDictionary.Add("InitialMembershipAmount", ds.Tables[0].Rows[0]["InitialMembershipAmount"].ToString());
                            MemberDictionary.Add("MembershipValidity", ds.Tables[0].Rows[0]["MembershipValidity"].ToString());
                            MemberDictionary.Add("LastSubscriptionPaid", ds.Tables[0].Rows[0]["LastSubscriptionPaid"].ToString());
                            MemberDictionary.Add("SubscriptionAmountPaid", ds.Tables[0].Rows[0]["SubscriptionAmountPaid"].ToString());
                            MemberDictionary.Add("SpouseName", ds.Tables[0].Rows[0]["SpouseName"].ToString());
                            MemberDictionary.Add("FathersName", ds.Tables[0].Rows[0]["FathersName"].ToString());
                            MemberDictionary.Add("Child1sName", ds.Tables[0].Rows[0]["Child1sName"].ToString());
                            MemberDictionary.Add("Child2sName", ds.Tables[0].Rows[0]["Child2sName"].ToString());
                            MemberDictionary.Add("Alive", ds.Tables[0].Rows[0]["Alive"].ToString());
                            MemberDictionary.Add("Qualification", ds.Tables[0].Rows[0]["Qualification"].ToString());
                            MemberDictionary.Add("MaritalStatus", ds.Tables[0].Rows[0]["MaritalStatus"].ToString());
                            MemberDictionary.Add("Profession", ds.Tables[0].Rows[0]["Profession"].ToString());
                            MemberDictionary.Add("DOBOfChild1", ds.Tables[0].Rows[0]["DOBOfChild1"].ToString());
                            MemberDictionary.Add("DOBOfChild2", ds.Tables[0].Rows[0]["DOBOfChild2"].ToString());
                            MemberDictionary.Add("DOBOfSpouse", ds.Tables[0].Rows[0]["DOBOfSpouse"].ToString());
                            MemberDictionary.Add("DOBOfFather", ds.Tables[0].Rows[0]["DOBOfFather"].ToString());
                            MemberDictionary.Add("Hobbies", ds.Tables[0].Rows[0]["Hobbies"].ToString());
                            MemberDictionary.Add("Balance", ds.Tables[0].Rows[0]["Balance"].ToString());
                            MemberDictionary.Add("PaymentStatus", ds.Tables[0].Rows[0]["PaymentStatus"].ToString());
                            MemberDictionary.Add("MemberType", ds.Tables[0].Rows[0]["MemberType"].ToString());
                            MemberDictionary.Add("Salutation", ds.Tables[0].Rows[0]["Salutation"].ToString());
                        }

                    }

                }
                catch (SqlException e)
                {
                    ErrorString = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return MemberDictionary;
            }
        }

        public List<Member> ViewAllMembers()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Member> list = new List<Member>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spView_AllMembers";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {

                    SqlDataReader reader = cmd.ExecuteReader();
                    bool EOF = !reader.Read();

                    while (!EOF)
                    {
                        Member sub = new Member();
                        sub._MemId = (int)reader["MemId"];
                        // sub._MemId=int.Parse(ds.Tables[0].Rows[i]["MemId"].ToString());
                        sub._MemberId = reader["MemberId"].ToString();
                        // sub._MemberId=(ds.Tables[0].Rows[i]["MemberId"].ToString());
                        sub._MembershipNo = reader["MembershipNo"].ToString();
                        // sub._MembershipNo=ds.Tables[0].Rows[i]["MembershipNo"].ToString();
                        sub._ClubId = (int)reader["ClubId"];
                        // sub._ClubId=Convert.ToInt32(ds.Tables[0].Rows[i]["ClubId"].ToString());
                        sub._MemberName = reader["MemberName"].ToString();
                        // sub._MemberName=ds.Tables[0].Rows[i]["MemberName"].ToString();
                        sub._DOB = (DateTime)reader["DOB"];
                        // sub._DOB=Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"].ToString());
                        sub._Gender = reader["Gender"].ToString();
                        // sub._Gender=ds.Tables[0].Rows[i]["Gender"].ToString();
                        sub._Address = reader["Address"].ToString();
                        // sub._Address=ds.Tables[0].Rows[i]["Address"].ToString();
                        sub._MobileNo = reader["MobileNo"].ToString();
                        // sub._MobileNo=ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        sub._AltMobileNo = reader["AltMobileNo"].ToString();
                        //sub._AltMobileNo=ds.Tables[0].Rows[i]["AltMobileNo"].ToString();
                        sub._EmailId = reader["EmailId"].ToString();
                        //sub._EmailId=ds.Tables[0].Rows[i]["EmailId"].ToString();
                        sub._ProximityCardNo = reader["ProximityCardNo"].ToString();
                        // sub._ProximityCardNo=ds.Tables[0].Rows[i]["ProximityCardNo"].ToString();
                        sub._Guests = reader["Guests"].ToString();
                        //   sub._Guests=ds.Tables[0].Rows[i]["Guests"].ToString();
                        sub._GuestCards = reader["GuestCards"].ToString();
                        // sub._GuestCards=ds.Tables[0].Rows[i]["GuestCards"].ToString();
                        sub._AmenitiesInterested = reader["AmenitiesInterested"].ToString();
                        //sub._AmenitiesInterested=ds.Tables[0].Rows[i]["AmenitiesInterested"].ToString();
                        sub._MembershipType = reader["MembershipType"].ToString();
                        //  sub._MembershipType=ds.Tables[0].Rows[i]["MembershipType"].ToString();
                        sub._MemberSince = (DateTime)reader["MemberSince"];
                        // sub._MemberSince=Convert.ToDateTime(ds.Tables[0].Rows[i]["MemberSince"].ToString());
                        sub._MemberShipStartDate = (DateTime)reader["MemberShipStartDate"];
                        //   sub._MemberShipStartDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["MemberShipStartDate"].ToString());
                        sub._MemberShipStatus = reader["MemberShipStatus"].ToString();
                        //  sub._MemberShipStatus=ds.Tables[0].Rows[i]["MemberShipStatus"].ToString();
                        sub._InitialMembershipAmount = (double)reader["InitialMembershipAmount"];
                        // sub._InitialMembershipAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["InitialMembershipAmount"].ToString());
                        sub._MembershipValidity = (DateTime)reader["MembershipValidity"];
                        //sub._MembershipValidity=Convert.ToDateTime(ds.Tables[0].Rows[i]["MembershipValidity"].ToString());
                        sub._LastSubscriptionPaid = (Double)reader["LastSubscriptionPaid"];
                        // sub._LastSubscriptionPaid=Convert.ToDouble(ds.Tables[0].Rows[i]["LastSubscriptionPaid"].ToString());
                        sub._SubscriptionAmountPaid = (Double)reader["SubscriptionAmountPaid"];
                        // sub._SubscriptionAmountPaid=Convert.ToDouble(ds.Tables[0].Rows[i]["SubscriptionAmountPaid"].ToString());
                        sub._SpouseName = reader["SpouseName"].ToString();
                        // sub._SpouseName=ds.Tables[0].Rows[i]["SpouseName"].ToString();
                        sub._FathersName = reader["FathersName"].ToString();
                        // sub._FathersName=ds.Tables[0].Rows[i]["FathersName"].ToString();
                        sub._Child1sName = reader["Child1sName"].ToString();
                        // sub._Child1sName=ds.Tables[0].Rows[i]["Child1sName"].ToString();
                        sub._Child2sName = reader["Child2sName"].ToString();
                        // sub._Child2sName=ds.Tables[0].Rows[i]["Child2sName"].ToString();
                        sub._Alive = reader["Alive"].ToString();
                        //sub._Alive=ds.Tables[0].Rows[i]["Alive"].ToString();
                        sub._Qualification = reader["Qualification"].ToString();
                        //sub._Qualification=ds.Tables[0].Rows[i]["Qualification"].ToString();
                        sub._MaritalStatus = reader["MaritalStatus"].ToString();
                        // sub._MaritalStatus=ds.Tables[0].Rows[i]["MaritalStatus"].ToString();
                        sub._Profession = reader["Profession"].ToString();
                        // sub._Profession=ds.Tables[0].Rows[i]["Profession"].ToString();
                        sub._DOBOfChild1 = (DateTime)reader["DOBOfChild1"];
                        // sub._DOBOfChild1=Convert.ToDateTime(ds.Tables[0].Rows[i]["DOBOfChild1"].ToString());
                        sub._DOBOfChild2 = (DateTime)reader["DOBOfChild2"];
                        //sub._DOBOfChild2=Convert.ToDateTime(ds.Tables[0].Rows[i]["DOBOfChild2"].ToString());
                        sub._DOBOfSpouse = (DateTime)reader["DOBOfSpouse"];
                        //sub._DOBOfSpouse=Convert.ToDateTime(ds.Tables[0].Rows[i]["DOBOfSpouse"].ToString());
                        sub._DOBOfFather = (DateTime)reader["DOBOfFather"];
                        //sub._DOBOfFather=Convert.ToDateTime(ds.Tables[0].Rows[i]["DOBOfFather"].ToString());
                        sub._Hobbies = reader["Hobbies"].ToString();
                        //sub._Hobbies=ds.Tables[0].Rows[i]["Hobbies"].ToString();
                        sub._Balance = (double)reader["Balance"];
                        // sub._Balance=Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"].ToString());
                        sub._PaymentStatus = reader["PaymentStatus"].ToString();
                        // sub._PaymentStatus=ds.Tables[0].Rows[i]["PaymentStatus"].ToString();
                        sub._MemberType = reader["MemberType"].ToString();
                        sub._Salutation = reader["Salutation"].ToString();
                        list.Add(sub);
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

        public string DeleteMember(int MemId)
        {
            string count;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteMember";
            cmd.Parameters.AddWithValue("@MemId", MemId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    count = cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    count = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return count;
            }
        }

        public List<Subscription> ShowMemberShipTypes()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Subscription> list = new List<Subscription>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewAllSubscription";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Subscription sp = new Subscription();
                            sp.SubscriptionId = Convert.ToInt32(ds.Tables[0].Rows[i]["SubscriptionId"]);
                            sp.SubscriptionType = ds.Tables[0].Rows[i]["SubscriptionType"].ToString();
                            sp.SubscriptionRate = Convert.ToDouble(ds.Tables[0].Rows[i]["SubscriptionRate"].ToString());

                            //dict=subscription.BindDictionary(ds);
                            list.Add(sp);
                        }
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


        public string GenerateSubscriptionBill(Dictionary<string, object> MemberDictionary)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> SelectMember(int? MemId)
        {
            SubscriptionDict member = new SubscriptionDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewMember";
            cmd.Parameters.AddWithValue("@MemId", MemId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    if (da == null)
                    {
                        response = "Data is Unavailable";
                    }
                    else
                    {
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dict = member.BindDictionary(ds);
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



        //list of members while searching in the kittycollection
        public DataSet SearchMembers(string Prefix)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SearchMember";
            cmd.Parameters.AddWithValue("MemberName", Prefix);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (SqlException e)
                {
                    response = e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return ds;
            }
        }

        public DataSet GetMembers(string Prefix)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetMembers";
            cmd.Parameters.AddWithValue("@Prefix", Prefix);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                }
                catch (SqlException e)
                {
                    response = e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return ds;
            }
        }

        public DataSet GetMember(string sMemNo)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetMember";
            cmd.Parameters.AddWithValue("@MembershipNo", sMemNo);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                }
                catch (SqlException e)
                {
                    response = e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return ds;
            }
        }

        public List<Member> ViewAllPendingSubscriptions()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Member> list = new List<Member>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spView_AllMembers";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[i]["PaymentStatus"].ToString() == "UnPaid")
                            {
                                Member sub = new Member();
                                sub._MemberId = (ds.Tables[0].Rows[i]["MemberId"].ToString());
                                sub._MembershipNo = ds.Tables[0].Rows[i]["MembershipNo"].ToString();
                                sub._MemberName = ds.Tables[0].Rows[i]["MemberName"].ToString();
                                sub._MembershipType = ds.Tables[0].Rows[i]["MembershipType"].ToString();
                                sub._MemberShipStartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["MemberShipStartDate"].ToString());
                                sub._MembershipValidity = Convert.ToDateTime(ds.Tables[0].Rows[i]["MembershipValidity"].ToString());
                                sub._MemberShipStatus = ds.Tables[0].Rows[i]["MemberShipStatus"].ToString();
                                sub._SubscriptionAmountPaid = Convert.ToDouble(ds.Tables[0].Rows[i]["SubscriptionAmountPaid"].ToString());
                                sub._PaymentStatus = ds.Tables[0].Rows[i]["PaymentStatus"].ToString();
                                list.Add(sub);
                            }

                        }
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

        public string GetMemberMobileNo(string mNo)
        {
            string mobileNo = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetMemberMobileNo";
            cmd.Parameters.AddWithValue("@MembershipNo", mNo);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    mobileNo = cmd.ExecuteScalar().ToString();
                }
                catch (SqlException e)
                {
                    mobileNo = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
            }
            return mobileNo;
        }

        public DataSet GetMemberDue(string mNo)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetMemberDue";
            cmd.Parameters.AddWithValue("@MembershipNo", mNo);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (SqlException e)
                {

                }
                finally
                {
                    db.CloseConnection();
                }
            }
            return ds;
        }

        public int SaveSMSLog(string MNo, string MobNo, float Amount, string Status, string Batch_Id)
        {
            int res = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveSMSLog";
            cmd.Parameters.AddWithValue("@MembershipNo", MNo);
            cmd.Parameters.AddWithValue("@MobileNo", MobNo);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@Batch_Id", Batch_Id);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {

                    res = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {

                }
                finally
                {
                    db.CloseConnection();
                }
            }
            return res;
        }
    }
}