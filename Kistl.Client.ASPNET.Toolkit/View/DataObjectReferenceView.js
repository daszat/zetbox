﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="../Dialogs/ChooseObjectDialog.js"/>

//----------------------------------------------------------------
// Javascript Functions for the ObjectListControl
//----------------------------------------------------------------

Type.registerNamespace("Kistl.Client.ASPNET");

Kistl.Client.ASPNET.ObjectReferencePropertyControl = function(element) {
    // Private Fields
    this._lnkOpen = null;
    this._valueCtrl = null;
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
    get_LnkOpen: function() {
        return this._lnkOpen;
    },
    set_LnkOpen: function(val) {
        this._lnkOpen = val;
    },
    get_ValueCtrl: function() {
        return this._valueCtrl;
    },
    set_ValueCtrl: function(val) {
        this._valueCtrl = val;
    },
    get_Value: function() {
        if (this._valueCtrl.value == '') return null;
        return Sys.Serialization.JavaScriptSerializer.deserialize(this._valueCtrl.value);
    },
    set_Value: function(val) {
        if (val == null) {
            this._valueCtrl.value = '';
        } else {
            this._valueCtrl.value = Sys.Serialization.JavaScriptSerializer.serialize(val);
        }
    },
    get_Type: function() {
        return this._type;
    },
    set_Type: function(val) {
        this._type = val;
    },
    // Events
    _onLnkOpenClick: function() {
        var obj = this.get_Value();
        if (obj == null) return;
        Kistl.JavascriptRenderer.showObject(obj);
    }
}

Kistl.Client.ASPNET.ObjectReferencePropertyControl.registerClass('Kistl.Client.ASPNET.ObjectReferencePropertyControl', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();