<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Pages/Master.Master" Title="SMS Report"
    CodeBehind="SMSLog.aspx.cs" Inherits="NTCYApplication.Report.ASPX.SMSLog" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">SMS Log Report</div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title-btn">
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
                </div>
            </div>
            <asp:Button class="btn btn-primary" ID="btnSMSLog" runat="server" Text="SMS Log" OnClick="btnSMSLog_Click" />
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color: #fff; height: 472px;">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>