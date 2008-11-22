<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkspaceView.ascx.cs"
    Inherits="View_WorkspaceView" %>
<div style="float: left; width: 200px; margin-right: 10px;" id="container" runat="server">
    <div>
        <adc:DataList ID="listModules" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Modules</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <a href="#" commandName="item">Select</a> <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <div>
        <adc:DataList ID="listObjectClasses" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Object Classes</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <a href="#" commandName="item">Select</a> <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <div>
        <adc:DataList ID="listInstances" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Instances</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <a href="#" commandName="item">Select</a> <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <div>
        <adc:DataList ID="listRecentObjects" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Recent Objects</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <a href="#" commandName="item">Select</a> <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
</div>
<div style="float: left; width: 600px; border: solid 1px black;" id="divObjectPlaceholder"
    runat="server">
</div>
