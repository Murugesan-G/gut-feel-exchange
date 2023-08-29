using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTCYApplication.Reports.ASPX
{
    public partial class ServicesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void TopServices_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spTopServices", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spTopServices");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/TopServicesReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("TopServicesReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

        }

        protected void BottomServices_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spBottomServices", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spBottomServices");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/BottomServicesReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("BottomServicesReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

            
        }

        protected void btnGuestCollectionRep_Click(object sender, EventArgs e)
        {
            
            DateTime d1;
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                d1=DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                d1=new DateTime(2019, 01, 01);
            }
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@Date", d1);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.CommandText="spGuestBillCollection";
                cmd.Connection=con;
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spGuestBillCollection");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/GuestBillCollection.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("GuestBillCollection", dSet.Tables[0]));
            string ReportDate = d1.ToShortDateString();
            Microsoft.Reporting.WebForms.ReportParameter[] RptParameters =
                 new Microsoft.Reporting.WebForms.ReportParameter[1];
            RptParameters[0]=new Microsoft.Reporting.WebForms.ReportParameter("ReportDate", ReportDate);
            this.ReportViewer1.ProcessingMode=ProcessingMode.Local;
            this.ReportViewer1.LocalReport.SetParameters(RptParameters);
            ReportViewer1.DataBind();
            txtFromDate.Text="";
        }
    }
}