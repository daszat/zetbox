<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectListControl.ascx.cs"
    Inherits="Controls_ObjectListControl" %>
<div class="Control">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="divItems"><%= ShortLabel %></asp:Label>
    <div style="float: left;" id="divItems" runat="server">
        <div>
            <asp:LinkButton ID="lnkNew" runat="server" Text="New"></asp:LinkButton>
            <asp:LinkButton ID="lnkAdd" runat="server" Text="Add"></asp:LinkButton>
        </div>
        <adc:DataList ID="lstItems" runat="server" 
            DataKeyField="ID"
            ItemDataBoundEvent="objectListControl_OnItemDataBound"
            DeleteCommandEvent="objectListControl_OnItemDelete">
            <ItemTemplate>
                <a href="#" commandName="delete">Remove</a>
                <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <asp:HiddenField ID="hdItems" runat="server" />
    <div style="clear: left; height=0px;">
    </div>
</div>
