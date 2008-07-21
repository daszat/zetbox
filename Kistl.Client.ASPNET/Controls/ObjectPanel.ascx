<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectPanel.ascx.cs" Inherits="Controls_ObjectPanel" %>
<div>
    <h1>
        <%= Value.ToString() %></h1>
    <div id="divChildren" runat="server" class="ObjectPanelChildren">
        <%-- Children goes here --%>
    </div>
    <div style="clear: left;">
        <asp:Button ID="btnSave" runat="server" Text="Save" />
        <input id="btnCancel" type="button" value="Cancel" onclick="javascript:window.location = 'default.aspx'" />
    </div>
</div>
