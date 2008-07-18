/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("Kistl.Client.ASPNET");

Kistl.Client.ASPNET.ChooseObjectDialog = function(element) {
    // Private Fields
    this._Callback = null;
    this._dlg = null;
    this._lst = null;

    // Event Handler    
    this._onOKHandler = null;
    this._onServiceCompleted_GetListHandler = null;
    
    Kistl.Client.ASPNET.ChooseObjectDialog.initializeBase(this, [element]);
}

// Singel instance of Choose Object Dialog
Kistl.Client.ASPNET.ChooseObjectDialog.instance = null;

// Public static ChooseObject method
// TODO: do not pass the type a string - convert to object
Kistl.Client.ASPNET.ChooseObjectDialog.ChooseObject = function(type, callback) {
    Kistl.Client.ASPNET.ChooseObjectDialog.instance.ChooseObject(type, callback);
}

Kistl.Client.ASPNET.ChooseObjectDialog.prototype = {
    // Inititalize Control
    initialize: function() {
        Kistl.Client.ASPNET.ChooseObjectDialog.callBaseMethod(this, 'initialize');
        
        // initialize singelton
        // TODO: Add check for single instance!
        Kistl.Client.ASPNET.ChooseObjectDialog.instance = this;
        
        this._dlg = $find('chooseObjectBehavior');
        this._lst = $get('panelChooseObject_lst', this._dlg._popupElement);
        
        this._onOKHandler = Function.createDelegate(this, this._onOK);
        this._onServiceCompleted_GetListHandler = Function.createDelegate(this, this._onServiceCompleted_GetList);

        this._dlg.set_OnOkScript(this._onOKHandler);
    },
    // Dispose
    dispose: function() {        
        Kistl.Client.ASPNET.ChooseObjectDialog.callBaseMethod(this, 'dispose');
    },    
    // Public ChooseObject method
    ChooseObject: function(type, callback) {
        this._Callback = callback;

        // Clear current list
        this._lst.options.length = 0;
        
        // Call WCF Service
        Kistl.Client.ASPNET.AJAXService.GetList(type, this._onServiceCompleted_GetListHandler);
        
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

Kistl.Client.ASPNET.ChooseObjectDialog.registerClass('Kistl.Client.ASPNET.ChooseObjectDialog', Sys.UI.Control);
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
