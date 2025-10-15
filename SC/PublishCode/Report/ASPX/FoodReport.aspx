<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Pages/Master.Master" Title="Food Report" CodeBehind="FoodReport.aspx.cs" Inherits="NTCYApplication.Reports.ASPX.FoodReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">Food Report</div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title-btn">

            <%-- <a href="#" class="btn btn-primary" title="View">

                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
            </a>--%>
        </div>
    </div>


    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 workarea-outer">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 scrolling-area">
              <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 search-area" style="margin-bottom:1%;">
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 label-right">
                   From Date
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                      <asp:TextBox ID="txtFromDate" CssClass="blckthm-form-control" AutoCompleteType="Disabled" placeholder="From Date" runat="server" Width="140px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CE" runat="server" CssClass="ajax-calendar" Format="dd/MM/yyyy" PopupButtonID="txtFromDate"
                                            TargetControlID="txtFromDate">
                                        </cc1:CalendarExtender>
                                        

                               </div>
                     <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 label-right">
                   To Date
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                  <asp:TextBox ID="txttodate" CssClass="blckthm-form-control"  AutoCompleteType="Disabled" placeholder="To Date" runat="server" Width="140px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_to" runat="server" CssClass="ajax-calendar" Format="dd/MM/yyyy" PopupButtonID="txttodate"
                                            TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                </div>
                 <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                      <%--  <asp:Button type="button" value="Report" class="btn btn-primary" id="Search"  onclick="TransactionReport_Click" runat="server">Show Report <span class="glyphicon glyphicon-search"></span></asp:Button>--%>
                        <asp:Button class="btn btn-primary" ID="Search" runat="server" Text="Show Report" OnClick="TransactionReport_Click" />

                    </div>
            
            </div>


            <asp:Button class="btn btn-primary" ID="FoodDailyReport" runat="server" Text="Food Daily Report" OnClick="FoodDailyReport_Click" />
            <asp:Button class="btn btn-primary" ID="FoodWeeklyReport" runat="server" Text="Weekly Report" OnClick="FoodWeeklyReport_Click" />
            <asp:Button class="btn btn-primary" ID="FoodMonthlyReport" runat="server" Text="Monthly Report" OnClick="FoodMonthlyReport_Click" />
            <asp:Button class="btn btn-primary" ID="TopSellingFood" runat="server" Text="Top Selling Food" OnClick="TopSellingFood_Click" />
            <asp:Button class="btn btn-primary" ID="LeastSellingFood" runat="server" Text="Least Selling Food" OnClick="LeastSellingFood_Click" />
             <asp:Button class="btn btn-primary" ID="FoodProductPriceList" runat="server" Text="Product List" OnClick="FoodProductPriceList_Click" />

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color: #fff; height: 472px;">

                <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer2" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer3" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer4" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer5" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>


            </div>


        </div>
    </div>

</asp:Content>
