/// <reference name="MicrosoftAjax.js"/>
var chooseObjectDialog_Type;
var chooseObjectDialog_DataListID;
var chooseObjectDialog_Callback;

function chooseObjectDialog_OnOK()
{
    chooseObjectDialog_Callback(chooseObjectDialog_DataListID, '');
}

function chooseObjectDialog_ChooseObject(type, dataListID, callback)
{
    chooseObjectDialog_Type = Sys.Serialization.JavaScriptSerializer.deserialize(type);
    chooseObjectDialog_DataListID = dataListID;
    chooseObjectDialog_Callback = callback;

    var dlg = $find('chooseObjectBehavior');
    var lst = $get('lst', dlg._popupElement);
    lst.options.length = 0;
    lst.options[0] = new Option(chooseObjectDialog_Type.TypeName);
    dlg.show();
}