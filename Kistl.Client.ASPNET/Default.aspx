<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Unbenannte Seite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: left; width: 300px;">
        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_OnRefresh" Text="Refresh" />
        <asp:TreeView ID="tree" runat="server" ShowExpandCollapse="true" ShowLines="true"
            OnTreeNodeExpanded="tree_OnTreeNodeExpanded"
            OnSelectedNodeChanged="tree_OnSelectedNodeChanged">
        </asp:TreeView>
    </div>
    <div style="float: left;">
        <asp:Repeater ID="repItems" runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal ID="litName" runat="server" Text='<%# Container.DataItem.ToString() %>' />
                    </td>
                    <td>
                        <asp:HyperLink ID="lnkShow" runat="server" NavigateUrl='<%# "ObjectPage.aspx?Type=" + Eval("Type") + "&ID=" + Eval("ID") %>'>Show</asp:HyperLink>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
