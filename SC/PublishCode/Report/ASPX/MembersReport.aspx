<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MembersReport.aspx.cs" Inherits="NTCYApplication.Report.ASPX.MembersReport" MasterPageFile="~/Master_Pages/Master.Master" Title="User Report"%>



<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 page-title-outer">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title">Members Report</div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 page-title-btn">

            <%-- <a href="#" class="btn btn-primary" title="View">

                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
            </a>--%>
        </div>
    </div>


    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 workarea-outer">




        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 scrolling-area">

            <asp:Button ID="MemAddress" runat="server" Text="Address" OnClick="MemAddress_Click" CssClass="btn btn-primary"/>          
            <asp:Button ID="MemContact" runat="server" Text="Contact" OnClick="MemContact_Click" class="btn btn-primary" />
            <asp:Button ID="MemDetails" runat="server" Text="Details" OnClick="MemDetails_Click" class="btn btn-primary"  />
             <asp:DropDownList Height="33px" AutoPostBack="true" class="btn btn-primary" ID="ddlMembersBdy" runat="server" OnSelectedIndexChanged="ddlMembersBdy_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="Birthday"></asp:ListItem>
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
<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-outer" style="overflow: auto; background-color: #fff; height: 536px;">

                <rsweb:ReportViewer ID="ReportViewer1" runat="server" AutoDataBind="true" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer2" runat="server" AutoDataBind="true" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
                <rsweb:ReportViewer ID="ReportViewer3" runat="server" AutoDataBind="true" CssClass="Reportstyle" Height="100%" Width="100%"></rsweb:ReportViewer>
               
            </div>
        </div>
    </div>

</asp:Content>
