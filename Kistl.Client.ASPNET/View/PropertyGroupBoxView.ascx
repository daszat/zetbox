<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PropertyGroupBoxView.ascx.cs" Inherits="View_PropertyGroupBoxView" %>
<%@ Register src="LabeledView.ascx" tagname="LabeledView" tagprefix="uc1" %>
<asp:Panel ID="panel" runat="server">
    <asp:Repeater ID="repProperties" runat="server">
        <ItemTemplate>
            <uc1:LabeledView ID="lbView" runat="server" />            
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>

