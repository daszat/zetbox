<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataObjectFullView.ascx.cs"
    Inherits="View_DataObjectFullView" %>
    <div style="float:left;">
    <asp:LinkButton ID="btnClose" runat="server" Text="X" />
    </div>
<h1>
    
    <asp:Literal ID="litTitle" runat="server" /></h1>
<asp:Repeater ID="repProperties" runat="server">
    <ItemTemplate>
        <div id="divPlaceHolder" runat="server" class="Control">
        </div>
    </ItemTemplate>
</asp:Repeater>
