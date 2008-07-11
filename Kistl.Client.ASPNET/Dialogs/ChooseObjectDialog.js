/// <reference name="MicrosoftAjax.js"/>
var chooseObjectDialog_Type;
var chooseObjectDialog_DataListID;
var chooseObjectDialog_Callback;

function chooseObjectDialog_OnOK()
{
    var dlg = $find('chooseObjectBehavior');
    var lst = $get('panelChooseObject_lst', dlg._popupElement);
    chooseObjectDialog_Callback(chooseObjectDialog_DataListID, 
        Sys.Serialization.JavaScriptSerializer.deserialize(lst.value));
}

function chooseObjectDialog_ChooseObject(type, dataListID, callback)
{
    chooseObjectDialog_Type = Sys.Serialization.JavaScriptSerializer.deserialize(type);
    chooseObjectDialog_DataListID = dataListID;
    chooseObjectDialog_Callback = callback;

    var dlg = $find('chooseObjectBehavior');
    var lst = $get('panelChooseObject_lst', dlg._popupElement);
    // Clear current list
    lst.options.length = 0;
    
    // Call WCF Service
    Kistl.Client.ASPNET.AJAXService.GetList(chooseObjectDialog_Type, chooseObjectDialog_ServiceCompleted_GetList);
    
    // In the meantime, show the dialog
    dlg.show();
}

function chooseObjectDialog_ServiceCompleted_GetList(result)
{
    var dlg = $find('chooseObjectBehavior');
    var lst = $get('panelChooseObject_lst', dlg._popupElement);
    lst.options.length = 0;

    for(var i = 0; i < result.length; i++)
    {
        lst.options[i] = new Option(result[i].Text, Sys.Serialization.JavaScriptSerializer.serialize(result[i]));
    }
}