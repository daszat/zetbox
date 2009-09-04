<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkspaceView.ascx.cs"
    Inherits="View_WorkspaceView" %>
<asp:HiddenField ID="hdObjects" runat="server" />
<div id="container" runat="server">
    <div style="float: left; width: 300px; margin-right: 10px; cursor: pointer">
        <asp:Repeater ID="repObjects" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:Literal ID="litText" runat="server" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div id="containerObjects" runat="server" style="margin-left: 310px; min-height: 100px;
        border: solid 1px black;" />
</div>
