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
    public partial class MonthlySubscriptionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadReport();
                  
            }
        }

        private void LoadReport()
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spSubscriptionReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spSubscriptionReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/SubscriptionReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MonthlySubscriptionReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }
    }
}