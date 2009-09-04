/// <reference name="MicrosoftAjax.js"/>
/// <reference name="../Dialogs/ChooseObjectDialog.js"/>

//----------------------------------------------------------------
// Javascript Functions for the WorkspaceView
//----------------------------------------------------------------

Type.registerNamespace("Kistl.Client.ASPNET.View");

Kistl.Client.ASPNET.View.WorkspaceView = function(element) {

    Kistl.Client.ASPNET.View.WorkspaceView.initializeBase(this, [element]);
}

Kistl.Client.ASPNET.View.WorkspaceView.prototype = {
    // Inititalize Control
    initialize: function() {
        Kistl.Client.ASPNET.View.WorkspaceView.callBaseMethod(this, 'initialize');

    },
    // Dispose
    dispose: function() {
        Kistl.Client.ASPNET.View.WorkspaceView.callBaseMethod(this, 'dispose');
    }
}

Kistl.Client.ASPNET.View.WorkspaceView.registerClass('Kistl.Client.ASPNET.View.WorkspaceView', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();