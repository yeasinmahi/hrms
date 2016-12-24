var isProcessRunning = false;
var currentUrl = 0;
var fileName = '';

// pagemethod url
var pageUrl = 'DataImport.aspx/HandleDataImport';

// command list
var commands = new Array();
commands[0] = 'INIT';
commands[1] = 'RESTORE';
commands[2] = 'IMPPROG';
commands[3] = 'IMPACC';
commands[4] = 'IMPSTCK';
commands[5] = 'FINALIZE';

// checkbox list
var chkBoxes = new Array();
chkBoxes[0] = 'ctl00_ContentPlaceHolder1_chkInit';
chkBoxes[1] = 'ctl00_ContentPlaceHolder1_chkRestore';
chkBoxes[2] = 'ctl00_ContentPlaceHolder1_chkImportProgramms';
chkBoxes[3] = 'ctl00_ContentPlaceHolder1_chkImportAccounts';
chkBoxes[4] = 'ctl00_ContentPlaceHolder1_chkImportStock';
chkBoxes[5] = 'ctl00_ContentPlaceHolder1_chkFinalize';

// status message list
var statusList = new Array();
statusList[0] = 'Initializing...';
statusList[1] = 'Restoring database...';
statusList[2] = 'Importing programs data...';
statusList[3] = 'Importing accounts data...';
statusList[4] = 'Importing stock data...';
statusList[5] = 'Finalizing...';

// initialize
$(document).ready(function(){
    $('#progressBar').progressBar({barImage: '../Images/progressbg_green.gif', showText: false});
    $("#progressBar").hide();
});

function StartImport(sender)
{
    if(isProcessRunning)
        return false;

    // get file name
    //fileName = $('#ctl00_ContentPlaceHolder1_txtFileName').val();   
    fileName = document.getElementById(sender.id.replace("btnImport", "txtFileName")).value;
    
    // escape file name
    fileName = escape(fileName);

    if(fileName.trim().length > 0)
    {
        if(!isProcessRunning)
        {
            // initialize async calls
            FormatBeforeRequest(sender);  
            
            // update status
            UpdateStatus(statusList[currentUrl], 0);
            
            // first call
            ProcessImport(pageUrl, commands[currentUrl], fileName);
        }
    }
    else
    {
        UpdateStatus('Incorrect file name!!', 0);
    }
}

function FormatBeforeRequest(sender)
{
    // initialize async calls
    currentUrl = 0;
    isProcessRunning = true;
    
    $('#' + sender.id + '').attr('disabled', 'true');
    $('input[type=checkbox]').attr('checked', false);
    $("#progressBar").fadeIn();
    
    UpdateStatus("", 0);
}

function ProcessImport(url, command, args)
{
    // send a request with parameters
    SendRequest(url, 'command', command, 'args', args);
}

function FormatAfterResponse(msg, progress)
{
    // finialize async calls
    isProcessRunning = false;
    UpdateStatus(msg, progress);
    
    //$('#btnImport').removeAttr('disabled');
}

function UpdateStatus(msg, progress)
{
    // update text status
    $('#ctl00_ContentPlaceHolder1_lblStatus').text(msg);
    
    // update progressbar
    if(progress != -1)
        $("#progressBar").progressBar(progress);
}

function SendRequest(url)
{
    // core function for async calls
    var args = '';

    // register arguments
    if(arguments.length > 1)
    {
        for(var i=1; i<arguments.length; i++)
        {
            if(args.length != 0)
                args += ', ';

            args += '"' + arguments[i] + '": "'  + arguments[++i] + '"';
        }
    }

    // start async call (using json)
    $.ajax({
        type: 'POST',
        url: url,
        cache: false,
        data: '{' + args + '}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: OnSuccess,
        fail: OnFail,
        error: OnError 
    });
}

OnSuccess = function(response)
{
    if(isProcessRunning)
    {
        // valid response
        
        // update checkbox
        $('input[id=' + chkBoxes[currentUrl] + ']').attr('checked', true);
        
        if(currentUrl < commands.length - 1)
        {
            // send subsequent requests
            currentUrl++;
            UpdateStatus(statusList[currentUrl], ((100 / (commands.length + 1)) * currentUrl));
            ProcessImport(pageUrl, commands[currentUrl], fileName);
        }
        else
        {
            // all responses ok
            FormatAfterResponse('Data imported successfully.', 100);
        }
    }
}

OnFail = function(response)
{
    // request failed
    if(isProcessRunning)
        FormatAfterResponse('An error occurred!!', -1);
}

OnError = function(XMLHttpRequest, textStatus, errorThrown)
{
    // exception handler
    if(isProcessRunning)
    {
        // format json response to handle exception
        var err = eval('(' + XMLHttpRequest.responseText + ')');
        
        if(err.Message == 'Response is not available in this context.')
        {
            FormatAfterResponse('Authentication error!!', -1);
        }
        else
        {
            // show exception
            FormatAfterResponse(err.Message, -1);
        }
    }
}