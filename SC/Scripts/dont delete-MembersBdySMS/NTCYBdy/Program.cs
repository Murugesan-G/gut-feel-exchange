using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace NTCYBdy
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ntcyDB = ConfigurationManager.ConnectionStrings["NTCY"].ConnectionString;
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(ntcyDB);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText="spGetMembersBdy";
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Connection=con;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                if (ds.Tables.Count>0)
                {
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        string memberName = ds.Tables[0].Rows[i]["MemberName"].ToString();
                        string recipient = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        string message = "Dear "+memberName+", The New Town Club wishes you a very happy birthday may god bless you with good health and prosperity.";
                        using (var webClient = new WebClient())
                        {
                            string APIKey = "SgTUwSrw2v0-Ab5OE4IzL9ZmzWWDLzJT7hWdseFcPB";
                            byte[] response = webClient.UploadValues("https://api.textlocal.in/send/", new NameValueCollection(){
                                                     {"apikey" , APIKey},
                                                     {"numbers" , recipient},
                                                     {"message" , message},
                                                     {"sender" , "NTCYNK"}});
                            string result = System.Text.Encoding.UTF8.GetString(response);
                        }
                    }
                }
            }
            catch (Exception e) { }
        }
    }
}
