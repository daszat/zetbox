/// <reference name="MicrosoftAjax.js"/>
/// <reference name="../Dialogs/ChooseObjectDialog.js"/>

//----------------------------------------------------------------
// Javascript Functions for the ObjectListControl
//----------------------------------------------------------------

Type.registerNamespace("Kistl.Client.ASPNET");

Kistl.Client.ASPNET.ObjectListControl = function(element) {
    // Private Fields
    this._list = null;
    this._items = null;
    this._lnkAdd = null;
    this._type = null;
    
    // Handler
    this._onOnItemDataBoundHandler = null;
    this._onLnkAddClickHandler = null;
    this._onItemAddHandler = null;
   
    Kistl.Client.ASPNET.ObjectListControl.initializeBase(this, [element]);
}

Kistl.Client.ASPNET.ObjectListControl.prototype = {
    // Inititalize Control
    initialize: function() {
        Kistl.Client.ASPNET.ObjectListControl.callBaseMethod(this, 'initialize');
        
        this._onOnItemDataBoundHandler = Function.createDelegate(this, this._onItemDataBound);
        this._list.add_itemDataBound(this._onOnItemDataBoundHandler);
        
        this._onLnkAddClickHandler = Function.createDelegate(this, this._onLnkAddClick);
        $addHandler(this._lnkAdd, "click", this._onLnkAddClickHandler);
        this._lnkAdd.href = '#';
        
        this._onItemAddHandler = Function.createDelegate(this, this._onItemAdd);
        
        this.DataBind();
    },
    // Dispose
    dispose: function() {        
        Kistl.Client.ASPNET.ObjectListControl.callBaseMethod(this, 'dispose');
    },
    // public Properties
    get_List: function() {
        return this._list;
    },
    set_List: function(val) {
        this._list = val;
    },
    get_Items: function() {
        return this._items;
    },
    set_Items: function(val) {
        this._items = val;
    },
    get_LnkAdd: function() {
        return this._lnkAdd;
    },
    set_LnkAdd: function(val) {
        this._lnkAdd = val;
    },
    get_Type: function() {
        return this._type;
    },
    set_Type: function(val) {
        this._type = val;
    },
    // public Methods
    DataBind: function() {
        var data = Sys.Serialization.JavaScriptSerializer.deserialize(this._items.value);
        this._list.set_dataSource(data);
        this._list.dataBind();
    },
    // Events
    _onItemDataBound: function (sender, e)
    {
        var item = e.get_item();

        if (item.get_isDataItemType())
        {
            var data = item.get_dataItem();
            var txtText = item.findControl('text');
            setText(txtText, data.Text);
        }
    },
    _onLnkAddClick: function() {
        Kistl.Client.ASPNET.ChooseObjectDialog.ChooseObject(this._type, this._onItemAddHandler);
    },
    _onItemAdd: function(item) {
        var data = this._list.get_dataSource();
        data.push(item);
        this._list.dataBind();
    }
}

Kistl.Client.ASPNET.ObjectListControl.registerClass('Kistl.Client.ASPNET.ObjectListControl', Sys.UI.Control);
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


/*
function objectListControl_OnSubmit(dataListID, dataID)
{
    var dataList = $find(dataListID);
    var result = $get(dataID);
    var data = dataList.get_dataSource();
    
    result.value = Sys.Serialization.JavaScriptSerializer.serialize(data);
}

function objectListControl_OnItemDelete(sender, e)
{
    var index = e.get_item().get_itemIndex();
    var data = sender.get_dataSource();
    data.splice(index, 1);
    sender.dataBind();
}

function objectListControl_OnItemCommand(sender, e)
{
    var data = e.get_item().get_dataItem();
    Kistl.JavascriptRenderer.showObject(data);
}

*/