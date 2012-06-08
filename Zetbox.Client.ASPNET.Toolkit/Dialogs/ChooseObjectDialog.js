/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("Zetbox.Client.ASPNET");

Zetbox.Client.ASPNET.ChooseObjectDialog = function(element) {
    // Private Fields
    this._Callback = null;
    this._dlg = null;
    this._lst = null;

    // Event Handler    
    this._onOKHandler = null;
    this._onServiceCompleted_GetListHandler = null;
    
    Zetbox.Client.ASPNET.ChooseObjectDialog.initializeBase(this, [element]);
}

// Singel instance of Choose Object Dialog
Zetbox.Client.ASPNET.ChooseObjectDialog.instance = null;

// Public static ChooseObject method
// TODO: do not pass the type a string - convert to object
Zetbox.Client.ASPNET.ChooseObjectDialog.ChooseObject = function(type, callback) {
    Zetbox.Client.ASPNET.ChooseObjectDialog.instance.ChooseObject(type, callback);
}

Zetbox.Client.ASPNET.ChooseObjectDialog.prototype = {
    // Inititalize Control
    initialize: function() {
        Zetbox.Client.ASPNET.ChooseObjectDialog.callBaseMethod(this, 'initialize');
        
        // initialize singelton
        // TODO: Add check for single instance!
        Zetbox.Client.ASPNET.ChooseObjectDialog.instance = this;
        
        this._dlg = $find('chooseObjectBehavior');
        this._lst = $get('panelChooseObject_lst', this._dlg._popupElement);
        
        this._onOKHandler = Function.createDelegate(this, this._onOK);
        this._onServiceCompleted_GetListHandler = Function.createDelegate(this, this._onServiceCompleted_GetList);

        this._dlg.set_OnOkScript(this._onOKHandler);
    },
    // Dispose
    dispose: function() {        
        Zetbox.Client.ASPNET.ChooseObjectDialog.callBaseMethod(this, 'dispose');
    },    
    // Public ChooseObject method
    ChooseObject: function(type, callback) {
        this._Callback = callback;

        // Clear current list
        this._lst.options.length = 0;
        
        // Call WCF Service
        Zetbox.Client.ASPNET.AJAXService.GetList(type, this._onServiceCompleted_GetListHandler);
        
        // In the meantime, show the dialog
        this._dlg.show();
    },
    // On OK handler
    _onOK: function() {
        this._Callback(Sys.Serialization.JavaScriptSerializer.deserialize(this._lst.value));
    },
    // On Service Completed handler
    _onServiceCompleted_GetList: function(result) {
        this._lst.options.length = 0;

        for(var i = 0; i < result.length; i++)
        {
            this._lst.options[i] = new Option(result[i].Text, Sys.Serialization.JavaScriptSerializer.serialize(result[i]));
        }
    }
}

Zetbox.Client.ASPNET.ChooseObjectDialog.registerClass('Zetbox.Client.ASPNET.ChooseObjectDialog', Sys.UI.Control);
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
