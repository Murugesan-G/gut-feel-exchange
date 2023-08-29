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
    public partial class LiquorStockReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //{
            //    LoadLiquorStockReport();
            //}
        }

        private void LoadLiquorStockReport(DateTime date)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
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
                SqlCommand cmd = new SqlCommand();// "spLiquorStockReport", con);
                cmd.Parameters.AddWithValue("@Date", d1);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.CommandText="spLiquorStockReport";
                cmd.Connection=con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dSet);
                //SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);              
                //liqAdpt.Fill(dSet, "spLiquorStockReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorStockReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorstockReportDatasets", dSet.Tables[0]));
            ReportViewer1.DataBind();
            Microsoft.Reporting.WebForms.ReportParameter[] RptParameters = new Microsoft.Reporting.WebForms.ReportParameter[1];
            RptParameters[0]=new Microsoft.Reporting.WebForms.ReportParameter("ReportDate", txtFromDate.Text.ToString());
            this.ReportViewer1.LocalReport.SetParameters(RptParameters);
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            DateTime.TryParse(txtFromDate.Text.Trim(), out date);
            LoadLiquorStockReport(date);
        }
    }
}