/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("Kistl.Client.ASPNET");

Kistl.Client.ASPNET.ChooseObjectDialog = function(element) {
    this._DataListID = null;
    this._Callback = null;
    this._dlg = null;
    this._lst = null;
    
    this._onOKHandler = null;
    
    Kistl.Client.ASPNET.ChooseObjectDialog.initializeBase(this, [element]);
}

// Singel instance
Kistl.Client.ASPNET.ChooseObjectDialog.instance = null;

// Static Methodcall
// TODO: do not pass the type a string - convert to object
Kistl.Client.ASPNET.ChooseObjectDialog.ChooseObject = function(typeString, dataListID, callback) {
    Kistl.Client.ASPNET.ChooseObjectDialog.instance.ChooseObject(typeString, dataListID, callback);
}

Kistl.Client.ASPNET.ChooseObjectDialog.prototype = {
    initialize: function() {
        Kistl.Client.ASPNET.ChooseObjectDialog.callBaseMethod(this, 'initialize');
        
        // initialize singelton
        // TODO: Add check for single instance!
        Kistl.Client.ASPNET.ChooseObjectDialog.instance = this;
        
        // Benutzerdefinierte Initialisierung hier hinzufügen
        this._dlg = $find('chooseObjectBehavior');
        this._lst = $get('panelChooseObject_lst', this._dlg._popupElement);
        
        this._onOKHandler = Function.createDelegate(this, this.onOK);
        this._dlg.set_OnOkScript(this._onOKHandler);
    },
    dispose: function() {        
        //Benutzerdefinierte Löschaktionen hier einfügen
        Kistl.Client.ASPNET.ChooseObjectDialog.callBaseMethod(this, 'dispose');
    },    
    onOK : function() {
        this._Callback(this._DataListID, Sys.Serialization.JavaScriptSerializer.deserialize(this._lst.value));
    },
    ChooseObject: function(typeString, dataListID, callback) {
        var type = Sys.Serialization.JavaScriptSerializer.deserialize(typeString);
        this._DataListID = dataListID;
        this._Callback = callback;

        // Clear current list
        this._lst.options.length = 0;
        
        // Call WCF Service
        Kistl.Client.ASPNET.AJAXService.GetList(type, this.ServiceCompleted_GetList);
        
        // In the meantime, show the dialog
        this._dlg.show();
    },
    ServiceCompleted_GetList: function(result) {
        this._dlg = $find('chooseObjectBehavior');
        this._lst = $get('panelChooseObject_lst', this._dlg._popupElement);
        this._lst.options.length = 0;

        for(var i = 0; i < result.length; i++)
        {
            this._lst.options[i] = new Option(result[i].Text, Sys.Serialization.JavaScriptSerializer.serialize(result[i]));
        }
    }
}

Kistl.Client.ASPNET.ChooseObjectDialog.registerClass('Kistl.Client.ASPNET.ChooseObjectDialog', Sys.UI.Control);
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
