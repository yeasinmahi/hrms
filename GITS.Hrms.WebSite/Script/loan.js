/// <reference path="jquery-1.8.2-vsdoc.js" />
$(document).ready(function() {
$("#ctl00_ContentsPlaceHolder1_txtNumberOfInst").keyup(function() {
        alert("gdfgfdh");
        var instAmt = 0;
        var loanAmt = $("#ctl00_ContentsPlaceHolder1_txtLoanAmount").val();
        var inst = $(this).val();
        instAmt = loanAmt / inst;
        $("#ctl00_ContentsPlaceHolder1_txtInstAmount").val() = instAmt;
    });
});