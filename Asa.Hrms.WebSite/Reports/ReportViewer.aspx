<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report Viewer - ASA</title>
</head>
<body>
    <form id="frmLayout" runat="server" style="width:800px; height:600px">         
        <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="False" 
        DisplayPage="True" 
        EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" 
        HasCrystalLogo = "False" OnAfterRender="crViewer_AfterRender" 
        HasViewList="False" DisplayGroupTree="False" />
      </form>
</body>
</html>
