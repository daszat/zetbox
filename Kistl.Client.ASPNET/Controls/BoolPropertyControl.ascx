<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BoolPropertyControl.ascx.cs"
    Inherits="Controls_BoolPropertyControl" %>
<div class="Control">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="cbBool"><%= ShortLabel %></asp:Label>
    <asp:CheckBox ID="cbBool" runat="server" OnCheckedChanged="cbBool_OnCheckedChanged"
        CssClass="Control" />
</div>
