<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DoublePropertyControl.ascx.cs"
    Inherits="Controls_DoublePropertyControl" %>
<div class="Control">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtDouble"><%= ShortLabel %></asp:Label>
    <asp:TextBox ID="txtDouble" runat="server" OnTextChanged="txtDouble_OnTextChanged"
        CssClass="Control" />
</div>
