<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelationView.ascx.cs"
    Inherits="View_Relations_RelationView" %>
<%@ Register src="../LabeledView.ascx" tagname="LabeledView" tagprefix="uc1" %>
<div>
    <asp:Panel ID="panelHeaderBox" runat="server" GroupingText="Summary">
        <uc1:LabeledView ID="LabeledView1" runat="server" ModelPath="PropertyModelsByName[Description]" />
        <uc1:LabeledView ID="LabeledView2" runat="server" ModelPath="PropertyModelsByName[Module]" />
        <uc1:LabeledView ID="LabeledView3" runat="server" ModelPath="PropertyModelsByName[Storage]" />
    </asp:Panel>
</div>
