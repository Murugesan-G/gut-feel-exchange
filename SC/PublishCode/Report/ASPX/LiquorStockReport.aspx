<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Pages/Master.Master" Title="Liquor Stock Report" CodeBehind="LiquorStockReport.aspx.cs" Inherits="NTCYApplication.Reports.ASPX.LiquorStockReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
      <asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
         <script>       
             function validateDate() {

                 var btnsearch = document.getElementById("<%=txtFromDate.ClientID%>");
                 if (btnsearch.value == null || btnsearch.value=="") {
                     alert("Please Select Date");
                     btnsearch.focus();
                     return false;
                 }                                 
             }
         </script>
 
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">Liquor Stock Report</div>
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
                    Enter Date
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                    <asp:TextBox ID="txtFromDate" CssClass="blckthm-form-control" placeholder="Date" AutoCompleteType="Disabled" runat="server" Width="140px"></asp:TextBox>
                         <cc1:CalendarExtender ID="txtDate_CE" runat="server" CssClass="ajax-calendar" Format="dd/MM/yyyy" PopupButtonID="txtFromDate"
                        TargetControlID="txtFromDate">
                    </cc1:CalendarExtender>                    
                </div>
             
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 textbox-area">
                  <%--  OnClientClick="return validateDate();"--%>
                    <asp:Button class="btn btn-primary" ID="Search" OnClientClick="javascript:return validateDate();" runat="server" Text="Show Report"  OnClick="Search_Click" />
                </div>
            </div>


         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color: #fff; height: 569px;">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
            </div>


        </div>
    </div>      
</asp:Content>

     