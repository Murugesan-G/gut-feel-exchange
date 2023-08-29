using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.CardGame
{
    public class CardRoomGame : ICardGame
    {
        int _GameId { get; set; }
        string _UserId { get; set; }
        int _TableNo { get; set; }
        int _NoofMembers { get; set; }
        string _Winner { get; set; }
        string _Game { get; set; }
        double _AmountPaid { get; set; }
        string _Status { get; set; }
        DateTime _Date { get; set; }

        public int GameId
        {
            get { return _GameId; }
            set { _GameId=value; }
        }
        public string UserId
        {
            get { return _UserId; }
            set { _UserId=value; }
        }
        public int TableNo
        {
            get { return _TableNo; }
            set { _TableNo=value; }
        }

        public int NoofMembers
        {
            get { return _NoofMembers; }
            set { _NoofMembers=value; }
        }
        public string Winner
        {
            get { return _Winner; }
            set { _Winner=value; }
        }

        public string Game
        {
            get { return _Game; }
            set { _Game=value; }
        }
        public double AmountPaid
        {
            get { return _AmountPaid; }
            set { _AmountPaid=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }
        public DateTime Date
        {
            get { return _Date; }
            set { _Date=value; }
        }

        public Dictionary<string, object> CardGameDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            CardGameDictionary.Add("GameId", _GameId);
            CardGameDictionary.Add("UserId", _UserId);
            CardGameDictionary.Add("TableNo", _TableNo);
            CardGameDictionary.Add("NoofMembers", _NoofMembers);
            CardGameDictionary.Add("Winner", _Winner);
            CardGameDictionary.Add("Game", _Game);
            CardGameDictionary.Add("AmountPaid", _AmountPaid);
            CardGameDictionary.Add("Status", _Status);
            CardGameDictionary.Add("Date", _Date);

            return "Success";
        }

        DBConnection db = new DBConnection();

        public string CreateKittyCollection(Dictionary<string, object> CardIGameDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            CardGameDictionary=CardIGameDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertCardRoomGame";
            cmd.Parameters.AddWithValue("@UserId", CardGameDictionary["UserId"]);
            cmd.Parameters.AddWithValue("@TableNo", CardGameDictionary["TableNo"]);
            cmd.Parameters.AddWithValue("@Winner", CardGameDictionary["Winner"]);
            cmd.Parameters.AddWithValue("@AmountPaid", CardGameDictionary["AmountPaid"]);
            cmd.Parameters.AddWithValue("@Game", CardGameDictionary["Game"]);
            cmd.Parameters.AddWithValue("@Status", CardGameDictionary["Status"]);
            cmd.Parameters.AddWithValue("@Date", CardGameDictionary["Date"]);

            SqlConnection MyCon = db.OpenConnection();
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

        public string EditKittyCollection(Dictionary<string, object> CardIGameDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            CardGameDictionary=CardIGameDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateCardRoomGame";
            cmd.Parameters.AddWithValue("@GameId", CardGameDictionary["GameId"]);
            cmd.Parameters.AddWithValue("@UserId", CardGameDictionary["UserId"]);
            cmd.Parameters.AddWithValue("@AmountPaid", CardGameDictionary["AmountPaid"]);
            cmd.Parameters.AddWithValue("@Status", CardGameDictionary["Status"]);
            cmd.Parameters.AddWithValue("@Date", CardGameDictionary["Date"]);

            SqlConnection MyCon = db.OpenConnection();
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

        public Dictionary<string, object> ViewKittYCollection(int GameId)
        {
            CardGameDict card = new CardGameDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewCardRoomGame";
            cmd.Parameters.AddWithValue("@GameId", GameId);
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
                            dict=card.BindDictionary(ds);
                        }

                    }

                }
                catch (SqlException e)
                {
                    CardGameDictionary.Add("Response", e.ToString());
                }
                finally
                {
                    db.CloseConnection();
                }
                return dict;
            }
        }

        public List<CardRoomGame> ViewAllKittyCollection(string Status)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<CardRoomGame> list = new List<CardRoomGame>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetAllCardGame";
            cmd.Parameters.AddWithValue("@Status", Status);
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
                            CardRoomGame crg = new CardRoomGame();
                            crg._GameId=Convert.ToInt32(ds.Tables[0].Rows[i]["GameId"].ToString());
                            crg._UserId=ds.Tables[0].Rows[i]["UserId"].ToString();
                            crg._TableNo=Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            crg._Winner=ds.Tables[0].Rows[i]["Winner"].ToString();
                            crg._Game=ds.Tables[0].Rows[i]["Game"].ToString();
                            crg._AmountPaid=Convert.ToDouble(ds.Tables[0].Rows[i]["AmountPaid"]);

                            list.Add(crg);
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

        public string DeleteCardGame(int GameId)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteCardRoomGame";
            cmd.Parameters.AddWithValue("@GameId", GameId);

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

        public List<CardRoomGame> ViewPaidKittyCollection(string Status)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<CardRoomGame> list = new List<CardRoomGame>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetAllCardGame";
            cmd.Parameters.AddWithValue("@Status", Status);
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
                            CardRoomGame crg = new CardRoomGame();
                            crg._GameId=Convert.ToInt32(ds.Tables[0].Rows[i]["GameId"].ToString());
                            crg._UserId=ds.Tables[0].Rows[i]["UserId"].ToString();
                            crg._TableNo=Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            crg._Winner=ds.Tables[0].Rows[i]["Winner"].ToString();
                            crg._Game=ds.Tables[0].Rows[i]["Game"].ToString();
                            crg._AmountPaid=Convert.ToDouble(ds.Tables[0].Rows[i]["AmountPaid"].ToString());
                            crg._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                            crg._Date=Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString());

                            list.Add(crg);
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

        public List<CardRoomGame> SearchMemberById(string MemberName, string Status)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<CardRoomGame> list = new List<CardRoomGame>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSearchCardGameWinner";
            cmd.Parameters.AddWithValue("@MemberName", MemberName);
            cmd.Parameters.AddWithValue("@Status", Status);
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
                            CardRoomGame crg = new CardRoomGame();
                            crg._GameId=Convert.ToInt32(ds.Tables[0].Rows[i]["GameId"].ToString());
                            crg._UserId=ds.Tables[0].Rows[i]["UserId"].ToString();
                            crg._TableNo=Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            crg._Winner=ds.Tables[0].Rows[i]["Winner"].ToString();
                            crg._Game=ds.Tables[0].Rows[i]["Game"].ToString();
                            crg._AmountPaid=Convert.ToDouble(ds.Tables[0].Rows[i]["AmountPaid"].ToString());
                            crg._Status=ds.Tables[0].Rows[0]["Status"].ToString();
                            list.Add(crg);
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
    }
}