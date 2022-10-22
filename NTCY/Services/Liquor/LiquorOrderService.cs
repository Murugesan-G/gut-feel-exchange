using Microsoft.Data.SqlClient;
using NTCY.Models.Foods;
using NTCY.Models.LiquorDetails;
using System.Data;

namespace NTCY.Services.LiquorOrderS
{
    public class LiquorOrderService : ILiquorOrderService
    {
        public List<Models.LiquorDetails.Liquor> ViewAllLiquorDetails(string type)
        {
            throw new NotImplementedException();
        }

        public List<LiquorOrder> ViewAllLiquorOrder()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");

            string response = string.Empty;
            DataSet ds = new DataSet();
            List<LiquorOrder> list = new List<LiquorOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetAllLiquorOrderDetails";
            using (SqlConnection MyCon = new SqlConnection(cnn))
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
                            LiquorOrder Det = new LiquorOrder();
                            Det.OrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det.SubOrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"]);
                            Det.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                            Det.MembershipNo = Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det.TableNo = Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"]);
                            Det.MemberName = Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det.FoodName = Convert.ToString(ds.Tables[0].Rows[i]["FoodName"]);
                            Det.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                            Det.Status = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    //
                }
                return list;
            }
        }

        public List<LiquorOrder> ViewCompletedOrders()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");

            string response = string.Empty;
            DataSet ds = new DataSet();
            List<LiquorOrder> list = new List<LiquorOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[spViewCompletedLiquorOrder]";
            using (SqlConnection MyCon = new SqlConnection(cnn))
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
                            LiquorOrder Det = new LiquorOrder();
                            Det.OrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det.SubOrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"]);
                            Det.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                            Det.MembershipNo = Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det.TableNo = Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"]);
                            Det.MemberName = Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det.FoodName = Convert.ToString(ds.Tables[0].Rows[i]["FoodName"]);
                            Det.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                            Det.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                            Det.Type = Convert.ToString(ds.Tables[0].Rows[i]["Type"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    //
                }
                return list;
            }
        }
    }
}
