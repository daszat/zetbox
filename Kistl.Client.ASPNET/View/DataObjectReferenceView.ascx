<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataObjectReferenceView.ascx.cs" Inherits="View_DataObjectReferenceView" %>
<div id="container" runat="server">
    <asp:Label ID="lbLabel" runat="server" AssociatedControlID="cbList" />
    <asp:DropDownList ID="cbList" runat="server" CssClass="Control" />
    <input type="button" ID="btnOpen" runat="server" value="Open" />
    <input type="button" ID="btnNew" runat="server" value="New" />
</div>
