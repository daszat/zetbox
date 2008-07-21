<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BoolPropertyControl.ascx.cs"
    Inherits="Controls_BoolPropertyControl" %>
<div class="Control">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="cbBool"><%= ShortLabel %></asp:Label>
    <asp:CheckBox ID="cbBool" runat="server" CssClass="Control" />
</div>
