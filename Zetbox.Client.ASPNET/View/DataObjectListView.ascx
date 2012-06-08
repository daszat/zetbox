<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataObjectListView.ascx.cs"
    Inherits="View_DataObjectListView" %>
<div id="container" runat="server">
    <div>
        <a id="lnkNew" runat="server">New</a> <a id="lnkAdd" runat="server">Add</a>
    </div>
    <div style="max-height: 200px; overflow: auto;">
        <adc:DataList ID="lstItems" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Objects</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <%-- Attention! Reformating could change the attribute "commandName" to lowercase! --%>
                <a href="#" commandname="item">Open</a> <a href="#" commandname="delete">Remove</a>
                <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <asp:HiddenField ID="hdItems" runat="server" />
</div>
