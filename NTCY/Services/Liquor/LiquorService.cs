using AutoMapper;
using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NTCY.Database;
using NTCY.Entities;
using NTCY.Exceptions;
using NTCY.Models;
using NTCY.Business;
using NTCY.Models.LiquorDetails;
using NTCY.Models.Users;
using NTCY.Utils;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace NTCY.Services.LiquorDetails
{
    public class LiquorService : ILiquorService
    {
        //public LiquorDTO GetById(int liquorId)
        //{
        //    return getLiquor(liquorId);
        //}
        //private LiquorDTO getLiquor(int liquorId)
        //{
        //    var liquour = _context.Liquor.FirstOrDefault(m => m.LiquorId == liquorId);
        //    if (liquour == null) throw new KeyNotFoundException("Liquor Details not found");
        //    return liquour;
        //}
        public LiquorDet GetById(int liquorId)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();
            LiquorDet Det = new LiquorDet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetLiquorDetailsById";
            cmd.Parameters.AddWithValue("@LiquorId", liquorId);
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
                            //LiquorDet Det = new LiquorDet();
                            Det.LiquorId = Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorId"]);
                            Det.LiquorCatId = Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorCatId"]);
                            Det.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                            Det.LiquorName = ds.Tables[0].Rows[i]["LiquorName"].ToString();
                            Det.EffectiveDate = DateTime.Parse(ds.Tables[0].Rows[i]["EffectiveDate"].ToString()/*, CultureInfo.CreateSpecificCulture("en-IN")*/);
                            Det.PegsPerBottle = Convert.ToDouble(ds.Tables[0].Rows[i]["PegsPerBottle"].ToString());
                            Det.SoldInPegs = Convert.ToBoolean(ds.Tables[0].Rows[i]["SoldInPegs"]);
                            Det.GST = Convert.ToDouble(ds.Tables[0].Rows[i]["GST"].ToString());
                            Det.Status = Convert.ToBoolean(ds.Tables[0].Rows[i]["Status"].ToString());
                            Det.QuantityInMLBottle = Convert.ToDouble(ds.Tables[0].Rows[i]["QuantityInMLBottle"].ToString());
                            Det.SellingPriceBottle = Convert.ToDouble(ds.Tables[0].Rows[i]["SellingPriceBottle"].ToString());
                            Det.SellingPricePeg = Convert.ToDouble(ds.Tables[0].Rows[i]["SellingPricePeg"].ToString());
                        }
                    }
                }
                catch (SqlException e)
                {
                    //return list;

                }
                finally
                {
                    //
                }
            }
            return Det;
        }
        public IEnumerable<LiquorDet> GetAll()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();
            List<LiquorDet> list = new List<LiquorDet>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewAllLiquorDetails";
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
                            LiquorDet Det = new LiquorDet();
                            Det.LiquorId = Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorId"]);
                            Det.LiquorCatId = Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorCatId"]);
                            Det.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                            Det.LiquorName = ds.Tables[0].Rows[i]["LiquorName"].ToString();
                            Det.EffectiveDate = DateTime.Parse(ds.Tables[0].Rows[i]["EffectiveDate"].ToString()/*, CultureInfo.CreateSpecificCulture("en-IN")*/);
                            Det.PegsPerBottle = Convert.ToDouble(ds.Tables[0].Rows[i]["PegsPerBottle"].ToString());
                            Det.SoldInPegs = Convert.ToBoolean(ds.Tables[0].Rows[i]["SoldInPegs"]);
                            Det.GST = Convert.ToDouble(ds.Tables[0].Rows[i]["GST"].ToString());
                            Det.Status = Convert.ToBoolean(ds.Tables[0].Rows[i]["Status"].ToString());
                            Det.QuantityInMLBottle = Convert.ToDouble(ds.Tables[0].Rows[i]["QuantityInMLBottle"].ToString());
                            Det.SellingPriceBottle = Convert.ToDouble(ds.Tables[0].Rows[i]["SellingPriceBottle"].ToString());
                            Det.SellingPricePeg = Convert.ToDouble(ds.Tables[0].Rows[i]["SellingPricePeg"].ToString());
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    //return list;

                }
                finally
                {
                    //
                }
            }
            return list;
        }

        public List<LiquorCategoryDet> GetLiquorCategories()
        {
            OLEDBL oOLEDBL = new OLEDBL();
            List<LiquorCategoryDet> liquorCategoryDets = new List<LiquorCategoryDet>();

            try
            {
                DataTable dataTable = oOLEDBL.GetListByQuery("spViewAllLiquorCategory");
                if (dataTable.Rows.Count > 0)
                {
                    liquorCategoryDets = (from DataRow dr in dataTable.Rows
                                          select new LiquorCategoryDet()
                                          {
                                              LiquorCatId = Convert.ToInt32(dr["LiquorCatId"]),
                                              CategoryName = Convert.ToString(dr["CategoryName"])
                                          }).ToList();
                }
            }
            catch(Exception ex)
            {
                //Write Error Log
            }

            return liquorCategoryDets;
        }
        public void Update(int liquorId, LiquorDet liquorDet)
        {
            OLEDBL oOLEDBL = new OLEDBL();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string cnn = configuration.GetConnectionString("WebApiDatabase");
            using (SqlConnection connection = new SqlConnection(cnn))
            {
                SqlCommand scCommand = new SqlCommand("spUpdateLiquor", connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.AddWithValue("@LiquorId", liquorId);
                scCommand.Parameters.AddWithValue("@LiquorCatId", liquorDet.LiquorCatId);
                scCommand.Parameters.AddWithValue("@LiquorName", liquorDet.LiquorName);
                scCommand.Parameters.AddWithValue("@EffectiveDate", liquorDet.EffectiveDate);
                scCommand.Parameters.AddWithValue("@SoldInPegs", liquorDet.SoldInPegs);
                scCommand.Parameters.AddWithValue("@QuantityInMLBottle", liquorDet.QuantityInMLBottle);
                scCommand.Parameters.AddWithValue("@PegsPerBottle", liquorDet.PegsPerBottle);
                scCommand.Parameters.AddWithValue("@SellingPriceBottle", liquorDet.SellingPriceBottle);
                scCommand.Parameters.AddWithValue("@SellingPricePeg", liquorDet.SellingPricePeg);
                scCommand.Parameters.AddWithValue("@GST", liquorDet.GST);
                scCommand.Parameters.AddWithValue("@Status", liquorDet.Status);

                try
                {
                    if (scCommand.Connection.State == ConnectionState.Closed)
                    {
                        scCommand.Connection.Open();
                    }
                    scCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //Write Error Log
                }
                finally
                {
                    scCommand.Connection.Close();
                }
            }
        }
        public Int32 SaveLiquourDetails(LiquorDet liquorDet)
        {
            Int32 result = 0;
            OLEDBL oOLEDBL = new OLEDBL();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string cnn = configuration.GetConnectionString("WebApiDatabase");
            using (SqlConnection connection = new SqlConnection(cnn))
            {
                SqlCommand scCommand = new SqlCommand("spInsertLiquor", connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.AddWithValue("@LiquorCatId", liquorDet.LiquorCatId);
                scCommand.Parameters.AddWithValue("@LiquorName", liquorDet.LiquorName);
                scCommand.Parameters.AddWithValue("@EffectiveDate", liquorDet.EffectiveDate);
                scCommand.Parameters.AddWithValue("@SoldInPegs", liquorDet.SoldInPegs);
                scCommand.Parameters.AddWithValue("@QuantityInMLBottle", liquorDet.QuantityInMLBottle);
                scCommand.Parameters.AddWithValue("@PegsPerBottle", liquorDet.PegsPerBottle);
                scCommand.Parameters.AddWithValue("@SellingPriceBottle", liquorDet.SellingPriceBottle);
                scCommand.Parameters.AddWithValue("@SellingPricePeg", liquorDet.SellingPricePeg);
                scCommand.Parameters.AddWithValue("@GST", liquorDet.GST);
                scCommand.Parameters.AddWithValue("@Status", liquorDet.Status);

                try
                {
                    if (scCommand.Connection.State == ConnectionState.Closed)
                    {
                        scCommand.Connection.Open();
                    }
                    scCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //Write Error Log
                }
                finally
                {
                    scCommand.Connection.Close();
                }
            }
            return result;
        }

        public void Delete(int liquorId)
        {
            OLEDBL oOLEDBL = new OLEDBL();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string cnn = configuration.GetConnectionString("WebApiDatabase");
            using (SqlConnection connection = new SqlConnection(cnn))
            {
                SqlCommand scCommand = new SqlCommand("spDeleteLiquor", connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.AddWithValue("@LiquorId", liquorId);

                try
                {
                    if (scCommand.Connection.State == ConnectionState.Closed)
                    {
                        scCommand.Connection.Open();
                    }
                    scCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //Write Error Log
                }
                finally
                {
                    scCommand.Connection.Close();
                }
            }
        }

        public DataSet GetByName(string sLiquorName)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet dsLiquor = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_GetLiquor";
            cmd.Parameters.AddWithValue("@Prefix", sLiquorName);
            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dsLiquor);
                }
                catch (SqlException e)
                {
                    //return list;

                }
                finally
                {
                    //
                }
            }
            return dsLiquor;
        }
    }
}
