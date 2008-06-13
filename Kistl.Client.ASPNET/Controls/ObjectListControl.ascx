<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectListControl.ascx.cs"
    Inherits="Controls_ObjectListControl" %>
<div class="Control">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="repItems"><%= ShortLabel %></asp:Label>
    <div style="float:left;">
        <div>
            <asp:LinkButton ID="lnkNew" runat="server" Text="New"></asp:LinkButton>
            <asp:LinkButton ID="lnkAdd" runat="server" Text="Add"></asp:LinkButton>
        </div>
        <table class="tblList">
            <tr>
                <th>
                    Bezeichnung
                </th>
            </tr>
            <asp:Repeater ID="repItems" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.DataItem.ToString() %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div style="clear:left;height=0px;"></div>
</div>
