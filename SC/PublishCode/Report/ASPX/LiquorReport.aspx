<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Pages/Master.Master" Title="Liquor Report" CodeBehind="LiquorReport.aspx.cs" Inherits="NTCYApplication.Reports.ASPX.LiquorReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <script type="text/javascript">
       $(document).ready(function () {           
          var btnId = '<%= ddlLiquorMonthlyReport.ClientID %>'; 
           $('#' + '<%= ddlLiquorMonthlyReport.ClientID %> > option').each(function () {
               var Cdate = new Date();
               if (this.value != 0)
               {
                   this.text = this.text.trim() + "-" + Cdate.getFullYear();
               }              
           });         
       })
   <%--  $('#' + '<%= ddlLiquorMonthlyReport.ClientID %>').on('change',function () {
           alert("here");
           return false;
       })--%>
     
   </script>
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">Liquor Report</div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title-btn">

            <%-- <a href="#" class="btn btn-primary" title="View">

                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
            </a>--%>
        </div>
    </div>


    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 workarea-outer">

        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 scrolling-area">

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 search-area" style="margin-bottom: 1%;">
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 label-right">
                    From Date
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                    <asp:TextBox ID="txtFromDate" CssClass="blckthm-form-control" placeholder="From Date" AutoCompleteType="Disabled" runat="server" Width="140px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDate_CE" runat="server" CssClass="ajax-calendar" Format="dd/MM/yyyy" PopupButtonID="txtFromDate"
                        TargetControlID="txtFromDate">
                    </cc1:CalendarExtender>


                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 label-right">
                    To Date
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                    <asp:TextBox ID="txttodate" CssClass="blckthm-form-control" placeholder="To Date" AutoCompleteType="Disabled" runat="server" Width="140px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDate_to" runat="server" CssClass="ajax-calendar" Format="dd/MM/yyyy" PopupButtonID="txttodate"
                        TargetControlID="txttodate">
                    </cc1:CalendarExtender>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">

                    <asp:Button class="btn btn-primary" ID="Search" runat="server" Text="Show Report" OnClick="TransactionReport_Click" />

                </div>

            </div>

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 search-area" style="margin-bottom: 1%;">
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 label-right">
                    Select Date
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                    <asp:TextBox ID="txtRptDate" CssClass="blckthm-form-control" placeholder="From Date" AutoCompleteType="Disabled" runat="server" Width="140px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtRpt_Date" runat="server" CssClass="ajax-calendar" Format="dd/MM/yyyy" PopupButtonID="txtRptDate"
                        TargetControlID="txtRptDate">
                    </cc1:CalendarExtender>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                    <asp:Button class="btn btn-primary" ID="btn_DailyReport" runat="server" Text="Daily Report" OnClick="LiquorDailyReport_DW_Click" />
                </div>
            </div>



            <asp:Button class="btn btn-primary" ID="LiquorDailyReport" runat="server" Text="Liquor Today's Report" OnClick="LiquorDailyReport_Click" />
        <asp:Button class="btn btn-primary" ID="LiquorWeeklyReport" runat="server" Text="Weekly Report" OnClick="LiquorWeeklyReport_Click" />
        <%--<asp:Button class="btn btn-primary" ID="LiquorMonthlyReport" runat="server" Text="Monthly Report" OnClick="LiquorMonthlyReport_Click" />--%>
            <asp:DropDownList Height="33px" AutoPostBack="true" class="btn btn-primary" ID="ddlLiquorMonthlyReport" runat="server" OnSelectedIndexChanged="LiquorMonthlyReport_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="Monthly Report"></asp:ListItem>
                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                <asp:ListItem Value="6" Text="June"></asp:ListItem>
                <asp:ListItem Value="7" Text="July"></asp:ListItem>
                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
            </asp:DropDownList>
        <asp:Button  class="btn btn-primary" ID="TopSellingLiquor" runat="server" Text="Top Selling Liquor" OnClick="TopSellingLiquor_Click" />
        <asp:Button class="btn btn-primary" ID="LeastSellingLiquor" runat="server" Text="Least Selling Liquor" OnClick="LeastSellingLiquor_Click" />

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color: #fff; height: 472px;">

                   <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                 <%--  <rsweb:ReportViewer ID="ReportViewer2" runat="server"></rsweb:ReportViewer>
                   <rsweb:ReportViewer ID="ReportViewer3" runat="server"></rsweb:ReportViewer>
                   <rsweb:ReportViewer ID="ReportViewer4" runat="server"></rsweb:ReportViewer>
                   <rsweb:ReportViewer ID="ReportViewer5" runat="server"></rsweb:ReportViewer>--%>
         </div>
        </div>
    </div>
 
   </asp:Content>