<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChooseObjectDialog.ascx.cs" Inherits="Dialogs_ChooseObjectDialog" %>
<ajaxToolkit:ModalPopupExtender runat="server"
    BehaviorID="chooseObjectBehavior"
    TargetControlID="lnkOpenPopup"
    PopupControlID="panelChooseObject"
    BackgroundCssClass="ModalBackground"
    OkControlID="btnOK"
    OnOkScript="chooseObjectDialog_OnOK()" 
    CancelControlID="btnCancel" 
    DropShadow="true" />
<asp:LinkButton ID="lnkOpenPopup" runat="server" Text="Choose Object" style="display:none"/> 
<asp:Panel ID="panelChooseObject" runat="server" style="display:none;">
    <div class="Dialog">
        <h2>Choose Object</h2>
        <div>
            <select id="panelChooseObject_lst" size="5" >
            </select>
        </div>
        <div>
            <asp:Button ID="btnOK" runat="server" Text="OK" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
    </div>
</asp:Panel>