<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Pages/Master.Master" Title="Service Report" CodeBehind="ServicesReport.aspx.cs" Inherits="NTCYApplication.Reports.ASPX.ServicesReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">Service Report</div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title-btn">

            <%--   <a href="#" class="btn btn-primary" title="View">

                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
            </a>--%>
        </div>
    </div>


    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 workarea-outer">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 scrolling-area">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 search-area" style="margin-bottom: 1%;">
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 label-right">
                    Enter Date
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                    <asp:TextBox ID="txtFromDate" CssClass="blckthm-form-control" placeholder="Date" AutoCompleteType="Disabled" runat="server" Width="140px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDate_CE" runat="server" CssClass="ajax-calendar" Format="dd/MM/yyyy" PopupButtonID="txtFromDate"
                        TargetControlID="txtFromDate">
                    </cc1:CalendarExtender>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                    <asp:Button ID="btnGuestCollectionRep" OnClientClick="javascript:return validateDate();" runat="server" 
                        Text="Guest Bill Collection" class="btn btn-primary" OnClick="btnGuestCollectionRep_Click" />
                </div>
            </div>



            <asp:Button ID="TopServices" runat="server" Text="Top Services" class="btn btn-primary" OnClick="TopServices_Click" />
            <asp:Button ID="BottomServices" runat="server" Text="Bottom Services" class="btn btn-primary" OnClick="BottomServices_Click" />
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color: #fff; height: 535px;">

                <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer2" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer3" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>

            </div>
        </div>
    </div>


    <script>

        function validateDate() {

            var btnsearch = document.getElementById("<%=txtFromDate.ClientID%>");
             if (btnsearch.value == null || btnsearch.value == "") {
                 alert("Please Select Date");
                 btnsearch.focus();
                 return false;
             }
         }
    </script>
</asp:Content>



