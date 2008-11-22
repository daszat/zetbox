/// <reference name="MicrosoftAjax.js"/>
/// <reference name="../Dialogs/ChooseObjectDialog.js"/>

//----------------------------------------------------------------
// Javascript Functions for the WorkspaceView
//----------------------------------------------------------------

Type.registerNamespace("Kistl.Client.ASPNET.View");

Kistl.Client.ASPNET.View.WorkspaceView = function(element) {
    // Private Fields
    this._listModules = null;
    this._listObjectClasses = null;
    this._listInstances = null;
    this._listRecentObjects = null;

    // Handler
    this._onOnItemDataBoundHandler = null;

    this._onServiceCompleted_GetModulesHandler = null;
    this._onServiceCompleted_GetObjectClassesHandler = null;
    
    this._onOnItemCommand_ListModulesHandler = null;

    Kistl.Client.ASPNET.View.WorkspaceView.initializeBase(this, [element]);
}

Kistl.Client.ASPNET.View.WorkspaceView.prototype = {
    // Inititalize Control
    initialize: function() {
        Kistl.Client.ASPNET.View.WorkspaceView.callBaseMethod(this, 'initialize');

        // Init Handler
        this._onServiceCompleted_GetModulesHandler = Function.createDelegate(this, this._onServiceCompleted_GetModules);
        this._onServiceCompleted_GetObjectClassesHandler = Function.createDelegate(this, this._onServiceCompleted_GetObjectClasses);

        this._onOnItemDataBoundHandler = Function.createDelegate(this, this._onItemDataBound);

        this._listModules.add_itemDataBound(this._onOnItemDataBoundHandler);
        this._listObjectClasses.add_itemDataBound(this._onOnItemDataBoundHandler);
        this._listInstances.add_itemDataBound(this._onOnItemDataBoundHandler);
        this._listRecentObjects.add_itemDataBound(this._onOnItemDataBoundHandler);

        this._onOnItemCommand_ListModulesHandler = Function.createDelegate(this, this._onOnItemCommand_ListModules);
        this._listModules.add_itemCommand(this._onOnItemCommand_ListModulesHandler);

        this.BindModules();
    },
    // Dispose
    dispose: function() {
        Kistl.Client.ASPNET.View.WorkspaceView.callBaseMethod(this, 'dispose');
    },
    // public Properties
    get_ListModules: function() {
        return this._listModules;
    },
    set_ListModules: function(val) {
        this._listModules = val;
    },
    get_ListObjectClasses: function() {
        return this._listObjectClasses;
    },
    set_ListObjectClasses: function(val) {
        this._listObjectClasses = val;
    },
    get_ListInstances: function() {
        return this._listInstances;
    },
    set_ListInstances: function(val) {
        this._listInstances = val;
    },
    get_ListRecentObjects: function() {
        return this._listRecentObjects;
    },
    set_ListRecentObjects: function(val) {
        this._listRecentObjects = val;
    },
    // Methods
    BindModules: function() {
        Kistl.Client.ASPNET.WorkspaceModelService.GetModules(this._onServiceCompleted_GetModulesHandler);
    },
    _onItemDataBound: function(sender, e) {
        var item = e.get_item();

        if (item.get_isDataItemType()) {
            var data = item.get_dataItem();
            var txtText = item.findControl('text');
            Kistl.Client.ASPNET.JavascriptRenderer.setText(txtText, data.Text);
        }
    },
    // EventHandler
    _onServiceCompleted_GetModules: function(result) {
        this._listModules.set_dataSource(result);
        this._listModules.dataBind();
    },
    _onServiceCompleted_GetObjectClasses: function(result) {
        this._listObjectClasses.set_dataSource(result);
        this._listObjectClasses.dataBind();
    },
    _onOnItemCommand_ListModules: function(sender, e) {
        var data = e.get_item().get_dataItem();
        Kistl.Client.ASPNET.WorkspaceModelService.GetObjectClasses(data.ID, this._onServiceCompleted_GetObjectClassesHandler); 
    }
}

Kistl.Client.ASPNET.View.WorkspaceView.registerClass('Kistl.Client.ASPNET.View.WorkspaceView', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();