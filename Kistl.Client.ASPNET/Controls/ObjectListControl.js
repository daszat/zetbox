//----------------------------------------------------------------
// Javascript Functions for the ObjectListControl
//----------------------------------------------------------------

function objectListControl_OnSubmit(dataListID, dataID)
{
    var dataList = $find(dataListID);
    var result = $get(dataID);
    var data = dataList.get_dataSource();
    
    result.value = Sys.Serialization.JavaScriptSerializer.serialize(data);
}

function objectListControl_DataBind(dataListID, dataID)
{
    var dataList = $find(dataListID);
    var data = Sys.Serialization.JavaScriptSerializer.deserialize($get(dataID).value);

    if(dataList != null)
    {
        dataList.set_dataSource(data);
        dataList.dataBind();
    }
}

function objectListControl_OnItemDataBound(sender, e)
{
    var item = e.get_item();

    if (item.get_isDataItemType())
    {
        var data = item.get_dataItem();
        var txtText = item.findControl('text');
        setText(txtText, data.Text);
    }
}

function objectListControl_OnItemDelete(sender, e)
{
    var index = e.get_item().get_itemIndex();
    var data = sender.get_dataSource();
    data.splice(index, 1);
    sender.dataBind();
}
