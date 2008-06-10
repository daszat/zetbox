<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectListControl.ascx.cs"
    Inherits="Controls_ObjectListControl" %>
<div>
    <strong>
    <label for="repItems">
        <%= ShortLabel %></label></strong>
    <asp:Repeater ID="repItems" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Container.DataItem.ToString() %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
