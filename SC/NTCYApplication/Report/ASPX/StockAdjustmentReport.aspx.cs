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
    public partial class StockAdjustmentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
                DataSet dSet = new DataSet();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand("spStockAdjustmentReport", con);
                    SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                    liqAdpt.Fill(dSet, "spStockAdjustmentReport");
                }
                ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/StockAdjustmentReport.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("StockAdjustmentReportDataset", dSet.Tables[0]));
                ReportViewer1.DataBind();
            }
        }
    }
}