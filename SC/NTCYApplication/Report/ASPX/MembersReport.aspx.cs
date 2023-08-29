using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTCYApplication.Report.ASPX
{
    public partial class MembersReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void MemAddress_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spMemberAddressReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spMemberAddressReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/MembersAddress.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MembersAddressReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

        }
        protected void MemContact_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spViewAllMembers", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spViewAllMembers");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/MembersContact.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MembersContactReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

        }
        protected void MemDetails_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spViewAllMembers", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spViewAllMembers");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/MembersDetails.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MembersDetailReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

        }
        protected void ddlMembersBdy_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = 0;
            DataSet dSet = new DataSet();
            int.TryParse(ddlMembersBdy.SelectedValue.ToString(), out month);
            if (month>0)
            {
                string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.Clear();
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.CommandText="spGetMembersBdyMonthlyWise";
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Connection=con;
                    con.Open();
                    SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                    liqAdpt.Fill(dSet, "spGetMembersBdyMonthlyWise");
                    con.Close();
                }
                ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/MembersBdyMonthlyWise.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MembersDetailReportDataset", dSet.Tables[0]));
                ReportViewer1.DataBind();
                Microsoft.Reporting.WebForms.ReportParameter[] RptParameters = new Microsoft.Reporting.WebForms.ReportParameter[1];
                RptParameters[0]=new Microsoft.Reporting.WebForms.ReportParameter("month", ddlMembersBdy.SelectedItem.Text);
                this.ReportViewer1.LocalReport.SetParameters(RptParameters);
            }
        }
    }
}