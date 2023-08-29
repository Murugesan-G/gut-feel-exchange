using Microsoft.Reporting.WebForms;
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
    public partial class PendingBillsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void PendingBillsReport1_Click(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
                DataSet dSet = new DataSet();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand("spPendingBillsReport", con);
                    SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                    liqAdpt.Fill(dSet, "spPendingBillsReport");
                }
                ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/PendingBillsReport.rdlc");
              ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PendingBillsReportDataset", dSet.Tables[0]));
                ReportViewer1.DataBind();
            //}

        }

        protected void PendingBillsCurDate_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spPendingBillsReportCurDate", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spPendingBillsReportCurDate");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/PendingBillsReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PendingBillsReportDataset", dSet.Tables[0]));
            string ReportDate=DateTime.Now.ToShortDateString();
            Microsoft.Reporting.WebForms.ReportParameter[] RptParameters =
                 new Microsoft.Reporting.WebForms.ReportParameter[1];
            RptParameters[0]=new Microsoft.Reporting.WebForms.ReportParameter("ReportDate", ReportDate);
            this.ReportViewer1.ProcessingMode=ProcessingMode.Local;
            this.ReportViewer1.LocalReport.SetParameters(RptParameters);
            ReportViewer1.DataBind();
        }

        protected void GuestPendingBillRep_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spPendingBillsReportGuest", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spPendingBillsReportGuest");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/PendingBillsReporGuest.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PendingBillsReportDataset", dSet.Tables[0]));
            string ReportDate = DateTime.Now.ToShortDateString();
            Microsoft.Reporting.WebForms.ReportParameter[] RptParameters =
                 new Microsoft.Reporting.WebForms.ReportParameter[1];
            RptParameters[0]=new Microsoft.Reporting.WebForms.ReportParameter("ReportDate", ReportDate);
            this.ReportViewer1.ProcessingMode=ProcessingMode.Local;
            this.ReportViewer1.LocalReport.SetParameters(RptParameters);
            ReportViewer1.DataBind();
        }
    }
}