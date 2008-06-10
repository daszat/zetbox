<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectPanel.ascx.cs" Inherits="Controls_ObjectPanel" %>
<div>
    <h1><%= Value.ToString() %></h1>
    <div id="divChildren" runat="server" style="border: solid 2px black;">
        <%-- Children goes here --%>
    </div>
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="OnSave" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="javascript:window.location = default.aspx;" />
</div>