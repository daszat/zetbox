<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ObjectPage.aspx.cs" Inherits="ObjectPage" Title="Unbenannte Seite" %>

<%@ Register TagPrefix="uc" TagName="ChooseObjectDialog" Src="~/Dialogs/ChooseObjectDialog.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdObjects" runat="server" />
    <div>
        <uc:ChooseObjectDialog runat="server" />
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <ajaxToolkit:TabContainer ID="tabObjects" runat="server">
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
