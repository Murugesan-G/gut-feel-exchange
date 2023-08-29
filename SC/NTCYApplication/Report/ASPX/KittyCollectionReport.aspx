<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Pages/Master.Master" Title="Liquor Report" CodeBehind="KittyCollectionReport.aspx.cs" Inherits="NTCYApplication.Reports.ASPX.KittyCollectionReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

 
   <asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">KittyCollection Current Date Report</div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title-btn">

            <!--  <a href="#" class="btn btn-primary" title="View">

                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
            </a>-->
        </div>
    </div>


    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 workarea-outer">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 scrolling-area">




                <asp:Button ID="KittyCurDate" runat="server" Text="Kitty Collection Today's Report" OnClick="KittyCurDate_Click" class="btn btn-primary" />
                <asp:Button ID="KittyWeeklyRep" runat="server" OnClick="WeeklyReport_Click" Text="Weekly Report" class="btn btn-primary" />
                <asp:Button ID="KittyMonthlyRep" runat="server" OnClick="MonthlyReport_Click" Text="Monthly Report" class="btn btn-primary" />
             <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color: #fff; height: 535px;">   
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
              <%--  <rsweb:ReportViewer ID="ReportViewer2" runat="server" CssClass="Reportstyle" Height="420px" Width="480px"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer3" runat="server" CssClass="Reportstyle" Height="420px" Width="480px"></rsweb:ReportViewer>
              --%>
            </div>


        </div>
    </div>

    </asp:Content>