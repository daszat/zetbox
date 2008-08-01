<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectReferencePropertyControl.ascx.cs"
    Inherits="Controls_ObjectReferencePropertyControl" %>
<div class="Control">
    <asp:Label runat="server" AssociatedControlID="cbList"><%= ShortLabel %></asp:Label>    
    <asp:DropDownList ID="cbList" runat="server" CssClass="Control" />
    <input type="button" ID="btnOpen" runat="server" value="Open" />
    <input type="button" ID="btnNew" runat="server" value="New" />
</div>
