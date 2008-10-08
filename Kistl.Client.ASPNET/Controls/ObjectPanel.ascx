<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectPanel.ascx.cs" Inherits="Controls_ObjectPanel" %>
<div class="ObjectPanelTitle">
    <h1>
        <asp:Literal ID="litTitle" runat="server" /></h1>
</div>
<div id="divChildren" runat="server" class="ObjectPanelChildren">
    <%-- Children goes here --%>
</div>
<div style="clear: both;">
</div>
<div>
    <asp:Button ID="btnSave" runat="server" Text="Save" />
    <input id="btnCancel" type="button" value="Cancel" onclick="javascript:window.location = 'default.aspx'" />
</div>
