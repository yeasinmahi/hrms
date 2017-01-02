function CancelForm()
{
   var yn = window.confirm("This will cancel all the changes. Do you want to continue?");

   if( yn == 1)
   {
      window.document.aspnetForm.reset();
      __doPostBack('ctl00$mnuPageToolbar', 'CANCEL');
   }
}

function Confirm(message, command)
{
   var yn = window.confirm(message);

   if( yn == 1)
   {
      __doPostBack('ctl00$mnuPageToolbar', command);
   }
}

function ConfirmPostBack(message, control)
{
   var yn = window.confirm(message);

   if( yn == 1)
   {
    var id =  control.id.toString();
   
    while(id.search('_') > -1)
    {
        id = id.replace('_', '$');
    }
    
    __doPostBack(id, true);
   }
}

function DeleteSelected()
{
   var yn = window.confirm("This will delete selected record(s). Do you want to continue?");

   if( yn == 1)
   {
      __doPostBack('ctl00$mnuPageToolbar', 'DELETE');
   }
}

function DeleteSingleRecord()
{
   var yn = window.confirm("This record will be deleted. Do you want to continue?");

   if( yn == 1)
   {
      __doPostBack('ctl00$mnuPageToolbar', 'DELETE');
   }
}

function GridSelectAll(spanChk)
{
   // Added as ASPX uses SPAN for checkbox
   var oItem = spanChk.children;
   var theBox = (spanChk.type == "checkbox") ?  spanChk : spanChk.children.item[0];
   xState = theBox.checked;
   elm = theBox.form.elements;

   for(i = 0; i < elm.length; i ++ )
   {
      if(elm[i].type == "checkbox" && elm[i].id != theBox.id)
      {
         // elm[i].click();
         if(elm[i].checked != xState)
         {
            elm[i].click();
            // elm[i].checked = xState;
         }
      }
   }
}

function Transfer(url)
{
   document.location = url;
}
