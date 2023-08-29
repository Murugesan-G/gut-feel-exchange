using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTCYApplication.Reports.ASPX
{
    public partial class KittyCollectionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void KittyCurDate_Click(object sender, EventArgs e)
        {

            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("GetKittyCollectionCurDate", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "GetKittyCollectionCurDate");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/KittyCollectionDailyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("KittyDailyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

            
        }

        protected void WeeklyReport_Click(object sender, EventArgs e)
        {
             
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("GetKittyCollectionCurWeek", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "GetKittyCollectionCurWeek");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/KittyCollectionWeeklyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("KittyWeeklyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
            
        }

        protected void MonthlyReport_Click(object sender, EventArgs e)
        {
             
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("GetKittyCollectionCurMonth", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "GetKittyCollectionCurMonth");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/KittyCollectionMonthlyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FoodMonthlyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

        }
    }
}