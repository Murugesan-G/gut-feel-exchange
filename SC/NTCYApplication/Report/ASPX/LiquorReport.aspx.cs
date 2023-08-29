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
    public partial class LiquorReport : System.Web.UI.Page
    {
               
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TransactionReport_Click(object sender, EventArgs e)
        {
            DateTime fromDate,toDate;
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                 fromDate =DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture);
            }
            else
            {
                fromDate=new DateTime(2018, 01, 01);
            }
            if (!string.IsNullOrEmpty(txttodate.Text))
            {
                toDate=DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
               // toDate=Convert.ToDateTime(txttodate.Text);
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
                cmd.CommandText="spLiquorTransactionReport";
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmd.Connection=con;
                con.Open();
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spLiquorTransactionReport");
                con.Close();
            }
                   
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorTransactionReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorTransactionReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }

        protected void LiquorDailyReport_DW_Click(object sender, EventArgs e)
        {
            DateTime reportDate;
            if (!string.IsNullOrEmpty(txtRptDate.Text))
            {
                reportDate = DateTime.ParseExact(txtRptDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                reportDate = DateTime.Now;
            }

            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                //SqlCommand cmd = new SqlCommand("spLiquorDailyReport", con);
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spLiquorDailyReport_DateWise";
                cmd.Parameters.AddWithValue("@RptDate", reportDate);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spLiquorDailyReport_DateWise");
            }
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/LiquorDailyReport_DW.rdlc");
            // ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorDailyCollectionReportDataset_DW", dSet.Tables[0]));
            //ReportViewer1.DataBind();


            //  string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet1 = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                //SqlCommand cmd = new SqlCommand("spLiquorDailyReportTotal", con);
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spLiquorDailyReportTotal_DateWise";
                cmd.Parameters.AddWithValue("@RptDate", reportDate);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet1, "spLiquorDailyReportTotal_DateWise");
            }
            // ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorDailyReport.rdlc");
            //ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorDailyCollectionReportDataset_DW2", dSet1.Tables[0]));
            ReportViewer1.DataBind();


        }

        protected void LiquorDailyReport_Click(object sender, EventArgs e)
        {

            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spLiquorDailyReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spLiquorDailyReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorDailyReport.rdlc");
           // ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorDailyCollectionReportDataset", dSet.Tables[0]));
            //ReportViewer1.DataBind();


          //  string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet1 = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spLiquorDailyReportTotal", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet1, "spLiquorDailyReportTotal");
            }
            // ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorDailyReport.rdlc");
            //ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorDailyCollectionReportDataset2", dSet1.Tables[0]));
            ReportViewer1.DataBind();


        }

        protected void LiquorWeeklyReport_Click(object sender, EventArgs e)
        {

            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spLiquorWeeklyReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spLiquorWeeklyReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorWeeklyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorWeeklyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

           
        }

        protected void LiquorMonthlyReport_Click(object sender, EventArgs e)
        {
            
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spLiquorMonthlyReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spLiquorMonthlyReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorMonthlyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorMonthlyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

           
        }

        protected void TopSellingLiquor_Click(object sender, EventArgs e)
        {

            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spTopSellingLiquor", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spTopSellingLiquor");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/TopMovingLiquorReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("TopMovingLiquorReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

           
        }

        protected void LeastSellingLiquor_Click(object sender, EventArgs e)
        {

            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spLeastSellingLiquor", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spLeastSellingLiquor");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/BottomMovingLiquorReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("BottomMovingLiquorReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();

           
        }

        protected void LiquorMonthlyReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Month = 0;
            int.TryParse(ddlLiquorMonthlyReport.SelectedValue.ToString(),out Month);
            if(Month==0)
            {
                return;
            }
            if (Month>0)
            {
                string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
                DataSet dSet = new DataSet();
                DataSet dSet1 = new DataSet();
                DataSet dSet2 = new DataSet();
                using (SqlConnection con = new SqlConnection(conString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.Clear();
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.CommandText="spLiquorMonthlyReport";
                    cmd.Parameters.AddWithValue("@Month", Month);                   
                    cmd.Connection=con;
                    con.Open();
                    SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                    liqAdpt.Fill(dSet, "spLiquorMonthlyReport");
                    con.Close();
                }
                using (SqlConnection con = new SqlConnection(conString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.Clear();
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.CommandText="spLiquorMonthlyReportOpeningBalance";
                    cmd.Parameters.AddWithValue("@Month", Month);
                    cmd.Connection=con;
                    con.Open();
                    SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                    liqAdpt.Fill(dSet1, "spLiquorMonthlyReportOpeningBalance");
                    con.Close();
                }
                using (SqlConnection con = new SqlConnection(conString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.Clear();
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.CommandText="spLiquorMonthlyReportClsBalance";
                    cmd.Parameters.AddWithValue("@Month", Month);
                    cmd.Connection=con;
                    con.Open();
                    SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                    liqAdpt.Fill(dSet2, "spLiquorMonthlyReportClsBalance");
                    con.Close();
                }


                string date = ddlLiquorMonthlyReport.SelectedItem.Text.ToString()+"-"+DateTime.Now.ToString("yyyy");
                ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/LiquorMonthlyReport.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorMonthlyReportDataset", dSet.Tables[0]));
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorMonthlyReportOpeningBalance", dSet1.Tables[0]));
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("LiquorMonthlyReportClsBalance", dSet2.Tables[0]));
                ReportViewer1.DataBind();
                Microsoft.Reporting.WebForms.ReportParameter[] RptParameters = new Microsoft.Reporting.WebForms.ReportParameter[1];
                RptParameters[0]=new Microsoft.Reporting.WebForms.ReportParameter("ReportDate", date.ToString());
                this.ReportViewer1.LocalReport.SetParameters(RptParameters);
            }
              
        }
    }
}