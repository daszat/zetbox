<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelationView.ascx.cs"
    Inherits="View_Relations_RelationView" %>
<%@ Register Src="../LabeledView.ascx" TagName="LabeledView" TagPrefix="uc1" %>
<%@ Register Src="../NullablePropertyTextBoxView.ascx" TagName="NullablePropertyTextBoxView"
    TagPrefix="uc2" %>
<%@ Register Src="RelationEndView.ascx" TagName="RelationEndView" TagPrefix="uc3" %>
<div>
    <asp:Panel ID="panelHeaderBox" runat="server" GroupingText="Summary">
        <uc1:LabeledView ID="lbvSummaryDescription" runat="server" ModelPath="PropertyModelsByName[Description]" />
        <uc1:LabeledView ID="lbvSummaryModule" runat="server" ModelPath="PropertyModelsByName[Module]" />
        <uc1:LabeledView ID="lbvSummaryStorage" runat="server" ModelPath="PropertyModelsByName[Storage]" />
    </asp:Panel>
</div>
<div>
    <table>
        <tr>
            <td>
                A
            </td>
            <td>
                <uc2:NullablePropertyTextBoxView ID="txtA" runat="server" ModelPath="A.RoleName" />
            </td>
            <td colspan="2">
                <uc2:NullablePropertyTextBoxView ID="txtVerb" runat="server" ModelPath="PropertyModelsByName[Verb]" />
            </td>
            <td>
                <uc2:NullablePropertyTextBoxView ID="txtB" runat="server" ModelPath="B.RoleName" />
            </td>
            <td>
                B
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="panelRelEndA" runat="server" GroupingText="A">
                    <uc3:RelationEndView ID="relEndA" runat="server" ModelPath="A" />
                </asp:Panel>
            </td>
            <td colspan="3">
                <asp:Panel ID="panelRelEndB" runat="server" GroupingText="B">
                    <uc3:RelationEndView ID="RelationEndView1" runat="server" ModelPath="B" />
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
