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
    public class CardTable : CardTableInterface
    {
        private int _TableNo;
        private string _TableName;
        private string _Status;
        public int TableNo
        {
            get { return _TableNo; }
            set { _TableNo=value; }
        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }
        public Dictionary<string, object> CardTableDictionary { get; set; }
        DBConnection db = new DBConnection();

        public string BindDictionary()
        {
            CardTableDictionary=new Dictionary<string, object>();
            CardTableDictionary.Add("TableNo", _TableNo);
            CardTableDictionary.Add("TableName", _TableName);
            CardTableDictionary.Add("Status", _Status);
            return "success";
        }

        public string CreateCardTableDetails(Dictionary<string, object> CardTableIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            CardTableDictionary=CardTableIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertCardTable";
            //cmd.Parameters.AddWithValue("@TableNo", CardTableDictionary["TableNo"]);
            cmd.Parameters.AddWithValue("@TableName", CardTableDictionary["TableName"]);
            cmd.Parameters.AddWithValue("@Status", CardTableDictionary["Status"]);
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

        public Dictionary<string, object> SelectCardTable(int TableNo)
        {
            CardTableDict card = new CardTableDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewCardTable";
            cmd.Parameters.AddWithValue("@TableNo", TableNo);
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
                    // response=e.ToString();
                    CardTableDictionary.Add("Response", e.ToString());
                }
                finally
                {
                    db.CloseConnection();
                }
                return dict;
            }
        }

        public string UpdateCardTable(Dictionary<string, object> CardTableIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            CardTableDictionary=CardTableIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateCardTable";
            cmd.Parameters.AddWithValue("@TableNo", CardTableDictionary["TableNo"]);
            cmd.Parameters.AddWithValue("@TableName", CardTableDictionary["TableName"]);
            cmd.Parameters.AddWithValue("@Status", CardTableDictionary["Status"]);
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



        public List<CardTable> ViewAllCardTables()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<CardTable> list = new List<CardTable>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAllCardTable";

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
                            CardTable sub = new CardTable();
                            sub._TableNo=Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"]);
                            sub._TableName=ds.Tables[0].Rows[i]["TableName"].ToString();
                            sub._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                            list.Add(sub);
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

        public string DeleteCardTable(int TableNo)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteCardTable";
            cmd.Parameters.AddWithValue("@TableNo", TableNo);
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