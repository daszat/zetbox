<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ObjectPage.aspx.cs" Inherits="ObjectPage" Title="Unbenannte Seite" %>
<%@ Register TagPrefix="uc" TagName="ChooseObjectDialog" Src="~/Dialogs/ChooseObjectDialog.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <uc:ChooseObjectDialog runat="server" />
    </div>
    <div id="divMainPanel" runat="server">
        <%-- Controls goes here --%>
    </div>
</asp:Content>

