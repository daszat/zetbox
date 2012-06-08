/// <reference name="MicrosoftAjax.js"/>
/// <reference name="../Dialogs/ChooseObjectDialog.js"/>

//----------------------------------------------------------------
// Javascript Functions for the LauncherView
//----------------------------------------------------------------

Type.registerNamespace("Zetbox.Client.ASPNET.View");

Zetbox.Client.ASPNET.View.LauncherView = function(element) {
    // Private Fields
    this._listModules = null;
    this._listObjectClasses = null;
    this._listInstances = null;
    this._tabObjectsControl = null;

    // Handler
    this._onOnItemDataBoundHandler = null;
    this._listInstances_onOnItemDataBoundHandler = null;

    // Service Event Handler
    this._onServiceCompleted_GetModulesHandler = null;
    this._onServiceCompleted_GetObjectClassesHandler = null;
    this._onServiceCompleted_GetInstancesHandler = null;

    // DataList ItemCommand EventHandler
    this._onOnItemCommand_ListModulesHandler = null;
    this._onOnItemCommand_ListObjectClassesHandler = null;

    Zetbox.Client.ASPNET.View.LauncherView.initializeBase(this, [element]);
}

Zetbox.Client.ASPNET.View.LauncherView.prototype = {
    // Inititalize Control
    initialize: function() {
        Zetbox.Client.ASPNET.View.LauncherView.callBaseMethod(this, 'initialize');

        // Item BoundHandler
        this._onOnItemDataBoundHandler = Function.createDelegate(this, this._onItemDataBound);
        this._listInstances_onOnItemDataBoundHandler = Function.createDelegate(this, this._listInstances_onItemDataBound);
        this._listModules.add_itemDataBound(this._onOnItemDataBoundHandler);
        this._listObjectClasses.add_itemDataBound(this._onOnItemDataBoundHandler);
        this._listInstances.add_itemDataBound(this._listInstances_onOnItemDataBoundHandler);

        // Service Event Handler
        this._onServiceCompleted_GetModulesHandler = Function.createDelegate(this, this._onServiceCompleted_GetModules);
        this._onServiceCompleted_GetObjectClassesHandler = Function.createDelegate(this, this._onServiceCompleted_GetObjectClasses);
        this._onServiceCompleted_GetInstancesHandler = Function.createDelegate(this, this._onServiceCompleted_GetInstances);

        // DataList ItemCommand EventHandler
        this._onOnItemCommand_ListModulesHandler = Function.createDelegate(this, this._onOnItemCommand_ListModules);
        this._listModules.add_itemCommand(this._onOnItemCommand_ListModulesHandler);

        this._onOnItemCommand_ListObjectClassesHandler = Function.createDelegate(this, this._onOnItemCommand_ListObjectClasses);
        this._listObjectClasses.add_itemCommand(this._onOnItemCommand_ListObjectClassesHandler);

        this.BindModules();
    },
    // Dispose
    dispose: function() {
        Zetbox.Client.ASPNET.View.LauncherView.callBaseMethod(this, 'dispose');
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
    // Methods
    BindModules: function() {
        $get("divLoadingModules").style.display = "block";
        Zetbox.Client.ASPNET.WorkspaceModelService.GetModules(this._onServiceCompleted_GetModulesHandler);
    },
    // DataBound EventHandler
    _onItemDataBound: function(sender, e) {
        var item = e.get_item();

        if (item.get_isDataItemType()) {
            var data = item.get_dataItem();
            var txtText = item.findControl('text');
            Zetbox.Client.ASPNET.JavascriptRenderer.setText(txtText, data.Text);
        }
    },
    _listInstances_onItemDataBound: function(sender, e) {
        var item = e.get_item();

        if (item.get_isDataItemType()) {
            var data = item.get_dataItem();
            var txtText = item.findControl('text');
            var lnk = item.findControl('lnk');
            Zetbox.Client.ASPNET.JavascriptRenderer.setText(txtText, data.Text);
            lnk.href = 'Workspace.aspx?type=' + data.Type.TypeName + '&ID=' + data.ID;
        }
    },
    // Service EventHandler
    _onServiceCompleted_GetModules: function(result) {
        this._listModules.set_dataSource(result);
        this._listModules.dataBind();
        $get("divLoadingModules").style.display = "none";
    },
    _onServiceCompleted_GetObjectClasses: function(result) {
        this._listObjectClasses.set_dataSource(result);
        this._listObjectClasses.dataBind();
        $get("divLoadingObjectClasses").style.display = "none";
    },
    _onServiceCompleted_GetInstances: function(result) {
        this._listInstances.set_dataSource(result);
        this._listInstances.dataBind();
        $get("divLoadingInstances").style.display = "none";
    },
    // DataList ItemCommand EventHandler
    _onOnItemCommand_ListModules: function(sender, e) {
        $get("divLoadingObjectClasses").style.display = "block";
        this._listObjectClasses.set_dataSource(null);
        this._listObjectClasses.dataBind();
        this._listInstances.set_dataSource(null);
        this._listInstances.dataBind();
        var data = e.get_item().get_dataItem();
        Zetbox.Client.ASPNET.WorkspaceModelService.GetObjectClasses(data.ID, this._onServiceCompleted_GetObjectClassesHandler);
    },
    _onOnItemCommand_ListObjectClasses: function(sender, e) {
        $get("divLoadingInstances").style.display = "block";
        this._listInstances.set_dataSource(null);
        this._listInstances.dataBind();
        var data = e.get_item().get_dataItem();
        Zetbox.Client.ASPNET.WorkspaceModelService.GetInstances(data.ID, this._onServiceCompleted_GetInstancesHandler);
    }
}

Zetbox.Client.ASPNET.View.LauncherView.registerClass('Zetbox.Client.ASPNET.View.LauncherView', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();