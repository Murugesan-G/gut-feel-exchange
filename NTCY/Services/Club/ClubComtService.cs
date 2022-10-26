using Microsoft.Data.SqlClient;
using NTCY.Business;
using NTCY.Entities;
using NTCY.Models;
using NTCY.Models.Club;
using NTCY.Models.LiquorDetails;
using NTCY.Models.Users;
using System.Data;

namespace NTCY.Services.Club
{
    public class ClubComtService : IClubCommitteeService
    {
        public void Add(ClubCommittee clubComitee)
        {
            throw new NotImplementedException();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int comiteeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClubCommDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ClubCommitteeDet GetById(int committeeId)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();
            ClubCommitteeDet Det = new ClubCommitteeDet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewClubCommitteeById";
            cmd.Parameters.AddWithValue("@CommitteeId", committeeId);
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
                            Det.CommitteeId = Convert.ToInt32(ds.Tables[0].Rows[i]["CommitteeId"]);
                            Det.ClubId = Convert.ToInt32(ds.Tables[0].Rows[i]["ClubId"]);
                            Det.ClubName = ds.Tables[0].Rows[i]["ClubName"].ToString();
                            Det.Chairman = ds.Tables[0].Rows[i]["Chairman"].ToString();
                            Det.President = ds.Tables[0].Rows[i]["President"].ToString();
                            Det.VicePresident = ds.Tables[0].Rows[i]["VicePresident"].ToString();
                            Det.Secretary = ds.Tables[0].Rows[i]["Secretary"].ToString();
                            Det.Treasurer = ds.Tables[0].Rows[i]["Treasurer"].ToString();
                            Det.CommitteeMembers = ds.Tables[0].Rows[i]["CommitteeMembers"].ToString();
                            Det.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                            Det.StartDate = DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString());
                            Det.EndDate = DateTime.Parse(ds.Tables[0].Rows[i]["EndDate"].ToString());
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

        public List<ClubDetails> GetClubDetails()
        {
            OLEDBL oOLEDBL = new OLEDBL();
            List<ClubDetails> clubDetails = new List<ClubDetails>();

            try
            {
                DataTable dataTable = oOLEDBL.GetListByQuery("spViewAllClub");
                if (dataTable.Rows.Count > 0)
                {
                    clubDetails = (from DataRow dr in dataTable.Rows
                                   select new ClubDetails()
                                   {
                                       ClubId = Convert.ToInt32(dr["ClubId"]),
                                       ClubName = Convert.ToString(dr["ClubName"])
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                //Write Error Log
            }

            return clubDetails;
        }

        public void Update(int comiteeId, ClubCommittee clubComitee)
        {
            throw new NotImplementedException();
        }

        ClubCommitteeDTO IClubCommitteeService.GetById(int comiteeId)
        {
            throw new NotImplementedException();
        }
    }
}
