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
    public partial class FoodReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
        }

        protected void TransactionReport_Click(object sender, EventArgs e)
        {
            DateTime fromDate, toDate;
            //DateTime toDate = Convert.ToDateTime(txttodate.Text.ToString());
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
                cmd.CommandText="spFoodTransactionReport";
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmd.Connection=con;
                con.Open();
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spFoodTransactionReport");
                con.Close();
            }

            //using (SqlConnection con = new SqlConnection(conString))
            //{
            //    SqlCommand cmd = new SqlCommand("spFoodTransactionReport", con);
            //    cmd.Parameters.AddWithValue("@FromDate", fromDate.ToShortDateString());
            //    cmd.Parameters.AddWithValue("@ToDate", toDate.ToShortDateString());
            //    SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
            //    liqAdpt.Fill(dSet, "spFoodTransactionReport");
            //}
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/FoodTransactionReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FoodTransactionReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }
        protected void FoodDailyReport_Click(object sender, EventArgs e)
        {

            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spFoodDailyReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spFoodDailyReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/FoodDailyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FoodDailyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();


        }

        protected void FoodWeeklyReport_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spFoodWeeklyReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spFoodWeeklyReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/FoodWeeklyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FoodWeeklyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }

        protected void FoodMonthlyReport_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spFoodMonthlyReport", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spFoodMonthlyReport");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/FoodMonthlyReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FoodMonthlyReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }

        protected void TopSellingFood_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spTopSellingFood", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spTopSellingFood");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/TopMovingFood.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("TopMovingFoodReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }

        protected void LeastSellingFood_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spLeastSellingFood", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spLeastSellingFood");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/BottomMovingFood.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("BottomMovingFoodReportDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }


        protected void FoodProductPriceList_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString().Trim();
            DataSet dSet = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spShowFoodProductList", con);
                SqlDataAdapter liqAdpt = new SqlDataAdapter(cmd);
                liqAdpt.Fill(dSet, "spShowFoodProductList");
            }
            ReportViewer1.LocalReport.ReportPath=Server.MapPath("~/Report/FoodProductPriceListReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FoodProducdPriceListDataset", dSet.Tables[0]));
            ReportViewer1.DataBind();
        }
    }
}