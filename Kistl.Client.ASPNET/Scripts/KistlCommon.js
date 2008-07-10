//----------------------------------------------------------------
// Commom Kistl Javascript Functions
//----------------------------------------------------------------

function setText(element, text)
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
