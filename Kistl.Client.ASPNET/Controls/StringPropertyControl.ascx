<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StringPropertyControl.ascx.cs"
    Inherits="Controls_StringPropertyControl" %>
<div class="Control">
    <asp:Label runat="server" AssociatedControlID="txtString"><%= ShortLabel %></asp:Label>
    <asp:TextBox ID="txtString" runat="server" CssClass="Control" />
</div>
