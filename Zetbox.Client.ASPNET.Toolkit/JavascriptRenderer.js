/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("Zetbox.Client.ASPNET");

Zetbox.Client.ASPNET.JavascriptRenderer = function() {
    Zetbox.Client.ASPNET.JavascriptRenderer.initializeBase(this);
}

Zetbox.Client.ASPNET.JavascriptRenderer.prototype = {
    initialize: function() {
        Zetbox.Client.ASPNET.JavascriptRenderer.callBaseMethod(this, 'initialize');
        
        // Benutzerdefinierte Initialisierung hier hinzufügen
    },
    dispose: function() {        
        //Benutzerdefinierte Löschaktionen hier einfügen
        Zetbox.Client.ASPNET.JavascriptRenderer.callBaseMethod(this, 'dispose');
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
    },
    newObject: function(type) {
        return {'ID': 0, 'Text': '', 'Type': type};
    }
}
Zetbox.Client.ASPNET.JavascriptRenderer.registerClass('Zetbox.Client.ASPNET.JavascriptRenderer', Sys.Component);

Zetbox.JavascriptRenderer = new Zetbox.Client.ASPNET.JavascriptRenderer();

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


//----------------------------------------------------------------
// Commom Zetbox Javascript Functions
//----------------------------------------------------------------
Zetbox.Client.ASPNET.JavascriptRenderer.setText = function (element, text)
{
    if (typeof element.innerText != 'undefined')
    {
        element.innerText = text;
    }
    else if (typeof element.textContent != 'undefined')
    {
        element.textContent = text;
    }
}
