<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IntPropertyControl.ascx.cs"
    Inherits="Controls_IntPropertyControl" %>
<div class="Control">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtInt"><%= ShortLabel %></asp:Label>
    <asp:TextBox ID="txtInt" runat="server" OnTextChanged="txtInt_OnTextChanged" CssClass="Control" />
</div>
