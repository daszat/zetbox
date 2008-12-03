<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataObjectFullView.ascx.cs"
    Inherits="View_DataObjectFullView" %>
<h1>
    <asp:Literal ID="litTitle" runat="server" /></h1>
<asp:Repeater ID="repProperties" runat="server">
    <ItemTemplate>
        <div id="divPlaceHolder" runat="server" class="Control">
        </div>
    </ItemTemplate>
</asp:Repeater>
