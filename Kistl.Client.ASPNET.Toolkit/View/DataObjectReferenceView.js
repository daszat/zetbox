/// <reference name="MicrosoftAjax.js"/>
/// <reference name="../Dialogs/ChooseObjectDialog.js"/>

//----------------------------------------------------------------
// Javascript Functions for the ObjectListControl
//----------------------------------------------------------------

Type.registerNamespace("Kistl.Client.ASPNET");

Kistl.Client.ASPNET.ObjectReferencePropertyControl = function(element) {
// Private Fields
this._list = null;
    this._lnkOpen = null;
    this._type = null;

    // Handler
    this._onLnkOpenClickHandler = null;

    Kistl.Client.ASPNET.ObjectReferencePropertyControl.initializeBase(this, [element]);
}

Kistl.Client.ASPNET.ObjectReferencePropertyControl.prototype = {
    // Inititalize Control
    initialize: function() {
        Kistl.Client.ASPNET.ObjectReferencePropertyControl.callBaseMethod(this, 'initialize');

        this._onLnkOpenClickHandler = Function.createDelegate(this, this._onLnkOpenClick);
        $addHandler(this._lnkOpen, "click", this._onLnkOpenClickHandler);
        
    },
    // Dispose
    dispose: function() {
        Kistl.Client.ASPNET.ObjectReferencePropertyControl.callBaseMethod(this, 'dispose');
    },
    // public Properties
    get_List: function() {
        return this._list;
    },
    set_List: function(val) {
        this._list = val;
    },
    get_LnkOpen: function() {
        return this._lnkOpen;
    },
    set_LnkOpen: function(val) {
        this._lnkOpen = val;
    },
    get_Type: function() {
        return this._type;
    },
    set_Type: function(val) {
        this._type = val;
    },
    // Events
    _onLnkOpenClick: function() {
        var obj = Sys.Serialization.JavaScriptSerializer.deserialize(this._list.value);
        Kistl.JavascriptRenderer.showObject(obj);
    }    
}

Kistl.Client.ASPNET.ObjectReferencePropertyControl.registerClass('Kistl.Client.ASPNET.ObjectReferencePropertyControl', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();