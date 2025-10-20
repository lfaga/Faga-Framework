<%@ Page language="c#" Inherits="Faga.Framework.Services.Webpages.dialog" CodeFile="dialog.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<HEAD runat="server">
  <title>Registro Ventas</title>
  <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
  <meta name="CODE_LANGUAGE" content="C#">
  <meta name="vs_defaultClientScript" content="JavaScript">
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  <script language="javascript" type="text/javascript">
    <!--

    function OK_onClick(parentInstance) {
      window.returnValue = true;
    }

    function Cancel_onClick(parentInstance) {
      window.returnValue = false;
    }

    //-->
  </script>
</HEAD>
<body style="OVERFLOW: hidden">
<form id="Form1" method="post" runat="server">
  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" class="window">
    <thead>
    <tr>
      <th id="thTitle">
        Title
      </th>
    </tr>
    </thead>
    <tbody>
    <tr align="center">
      <td>
        <div id="tdContents" style="height: 100%; overflow: auto; overflow-x: hidden; overflow-y: auto; text-align: left; width: 100%;"></div>
      </td>
    </tr>
    <tr>
      <td height="24" align="right" class="footer">
        <a id="cmdOK" href="#" style="WIDTH: 80px" class="button" onclick="controllerObject.onOkClick();">
          <img src="/images/icons/icon_ok.gif" border="0"><span id="cmdOKText">Aceptar</span></a>
        &nbsp; <a id="cmdCancel" href="#" style="WIDTH: 80px" class="button" onclick="controllerObject.onCancelClick();">
          <img src="/images/icons/icon_cancel.gif" border="0"><span id="cmdCancelText">Cancelar</span></a>
      </td>
    </tr>
    </tbody>
  </table>
</form>
</body>
</html>