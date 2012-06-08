<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelationEndView.ascx.cs"
    Inherits="View_Relations_RelationEndView" %>
<%@ Register Src="../LabeledView.ascx" TagName="LabeledView" TagPrefix="uc1" %>
<div>
    <uc1:LabeledView ID="LabeledView2" runat="server" ModelPath="PropertyModelsByName[Type]" />
    <uc1:LabeledView ID="LabeledView3" runat="server" ModelPath="PropertyModelsByName[Multiplicity]" />
    <uc1:LabeledView ID="LabeledView4" runat="server" ModelPath="PropertyModelsByName[HasPersistentOrder]" />
    <uc1:LabeledView ID="LabeledView1" runat="server" ModelPath="PropertyModelsByName[Navigator]" />
</div>
