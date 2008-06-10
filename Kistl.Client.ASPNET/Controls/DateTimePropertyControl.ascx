<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateTimePropertyControl.ascx.cs"
    Inherits="Controls_DateTimePropertyControl" %>
<div class="Control">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtDateTime"><%= ShortLabel %></asp:Label>
    <asp:TextBox ID="txtDateTime" runat="server" OnTextChanged="txtDateTime_OnTextChanged"
        CssClass="Control" />
</div>
