/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("Kistl.Client.ASPNET");

Kistl.Client.ASPNET.JavascriptRenderer = function() {
    Kistl.Client.ASPNET.JavascriptRenderer.initializeBase(this);
}

Kistl.Client.ASPNET.JavascriptRenderer.prototype = {
    initialize: function() {
        Kistl.Client.ASPNET.JavascriptRenderer.callBaseMethod(this, 'initialize');
        
        // Benutzerdefinierte Initialisierung hier hinzufügen
    },
    dispose: function() {        
        //Benutzerdefinierte Löschaktionen hier einfügen
        Kistl.Client.ASPNET.JavascriptRenderer.callBaseMethod(this, 'dispose');
    },
    doPostBack: function(action, argument) {
        $get('__JavascriptRenderer_Action').value = action;
        $get('__JavascriptRenderer_Argument').value = Sys.Serialization.JavaScriptSerializer.serialize(argument);
        __JavascriptRenderer_PostBack();
    },
    sayHello: function() {
        this.doPostBack('SayHello', '');
    },
    showObject: function(objectMoniker) {
        this.doPostBack('ShowObject', objectMoniker);
    }
}
Kistl.Client.ASPNET.JavascriptRenderer.registerClass('Kistl.Client.ASPNET.JavascriptRenderer', Sys.Component);

Kistl.JavascriptRenderer = new Kistl.Client.ASPNET.JavascriptRenderer();

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
