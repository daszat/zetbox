<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StringPropertyControl.ascx.cs"
    Inherits="Controls_StringPropertyControl" %>
<div>
    <strong><label for="txtString">
        <%= ShortLabel %></label></strong>
    <asp:TextBox ID="txtString" runat="server" OnTextChanged="txtString_OnTextChanged" />
</div>
