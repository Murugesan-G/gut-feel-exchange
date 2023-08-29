<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master_Pages/Master.Master" Title="Stock Adjustment Report" CodeBehind="StockAdjustmentReport.aspx.cs" Inherits="NTCYApplication.Reports.ASPX.StockAdjustmentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

 
   <asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">Stock Adjustment Report</div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title-btn">

            <a href="#" class="btn btn-primary" title="View">

                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
            </a>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 workarea-outer">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 scrolling-area">
       <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color:#fff; height:564px;">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>
 