<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataObjectListView.ascx.cs" Inherits="View_DataObjectListView" %>
<div id="container" runat="server">
    <asp:Label ID="lbLabel" runat="server" AssociatedControlID="divItems" /></asp:Label>
    <div style="float: left;max-height:200px; overflow: auto;" id="divItems" runat="server">
        <div>
            <a ID="lnkNew" runat="server">New</a>
            <a ID="lnkAdd" runat="server">Add</a>
        </div>
        <adc:DataList ID="lstItems" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Objects</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <%-- Attention! Reformating could change the attribute "commandName" to lowercase! --%>
                <a href="#" commandName="item">Open</a>
                <a href="#" commandName="delete">Remove</a>
                <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <asp:HiddenField ID="hdItems" runat="server" />
    <div style="clear: left; height:0px;">
    </div>
</div>