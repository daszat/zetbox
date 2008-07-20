<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectListControl.ascx.cs" Inherits="Controls_ObjectListControl" %>
<%@ Import Namespace="Kistl.Client.ASPNET.Toolkit" %>
<div class="Control" id="container" runat="server">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="divItems"><%= ShortLabel %></asp:Label>
    <div style="float: left;" id="divItems" runat="server">
        <div>
            <asp:LinkButton ID="lnkNew" runat="server" Text="New"></asp:LinkButton>
            <a ID="lnkAdd" runat="server">Add</a>
        </div>
        <adc:DataList ID="lstItems" runat="server" DataKeyField="ID">
            <ItemTemplate>
                <a href="#" commandName="item">Open</a>
                <a href="#" commandName="delete">Remove</a>
                <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <asp:HiddenField ID="hdItems" runat="server" />
    <div style="clear: left; height=0px;">
    </div>
</div>