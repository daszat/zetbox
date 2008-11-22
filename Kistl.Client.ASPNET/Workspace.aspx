<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Workspace.aspx.cs" 
    Inherits="Workspace" Title="Kistl ASP.NET Prototype - Workspace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdObjects" runat="server" />
    <div>
        <uc:ChooseObjectDialog runat="server" />
    </div>
    <div id="divMainContent" runat="server"></div>
</asp:Content>

