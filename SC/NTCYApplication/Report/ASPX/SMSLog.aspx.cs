using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace NTCYApplication.Report.ASPX
{
    public partial class SMSLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSMSLog_Click(object sender, EventArgs e)
        {
            DateTime fromDate, toDate;
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                fromDate=DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                fromDate=new DateTime(2018, 01, 01);
            }
            if (!string.IsNullOrEmpty(txttodate.Text))
            {
                toDate=DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                toDate=DateTime.Now;
            }
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.CommandText="spGetSMSLogReport";
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmd.Connection=con;
                con.Open();
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spGetSMSLogReport");
                con.Close();
            }

            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/SMSLogReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("SMSLogReport", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }
    }
}