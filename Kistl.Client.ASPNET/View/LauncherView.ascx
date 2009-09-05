<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LauncherView.ascx.cs"
    Inherits="View_LauncherView" %>
<div id="container" runat="server">
    <div style="float: left; width: 300px; margin-right: 10px;overflow:scroll;">
        <div id="divLoadingModules" style="display: none;">
            <strong>Modules:</strong>
            <br />
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading..." />
        </div>
        <adc:DataList ID="listModules" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Modules</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <%-- Attention! Reformating could change the attribute "commandName" to lowercase! --%>
                <a href="#" commandname="item">Select</a> <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
        <div id="divLoadingObjectClasses" style="display: none;">
            <strong>Object Classes:</strong>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading..." />
        </div>
        <adc:DataList ID="listObjectClasses" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Object Classes</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <%-- Attention! Reformating could change the attribute "commandName" to lowercase! --%>
                <a href="#" commandname="item">Select</a> <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
    <div style="margin-left: 310px;border: solid 1px black;">
        <div id="divLoadingInstances" style="display: none;">
            <strong>Instances:</strong>
            <br />
            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading..." />
        </div>
        <adc:DataList ID="listInstances" runat="server" DataKeyField="ID" CssClass="ItemTable">
            <HeaderTemplate>
                <strong>Instances</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <%-- Attention! Reformating could change the attribute "commandName" to lowercase! --%>
                <a href="#" id="lnk" target="_blank">Select</a> <span id="text"></span>
            </ItemTemplate>
        </adc:DataList>
    </div>
</div>
