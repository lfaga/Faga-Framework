<%@ Page language="c#" Inherits="Faga.Framework.Services.Administracion.Dialogs.SeleccionarPermiso" CodeFile="SeleccionarPermiso.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD runat="server">
  <title>seleccionarAtributo</title>
  <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
  <meta name="CODE_LANGUAGE" content="C#">
  <meta name="vs_defaultClientScript" content="JavaScript">
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  <script language="javascript" type="text/javascript">
    <!--

    function OK_onClick(parentInstance) {
      var cbo = document.getElementById("cboPermiso");

      window.returnValue = "permission=" + cbo.value.toString();
    }

    //-->
  </script>
</HEAD>
<body style="OVERFLOW: hidden">
<form id="Form1" method="post" runat="server">
  <table class="window" width="100%" height="100%">
    <thead>
    <tr>
      <th colspan="2">
        Seleccione un Permiso
      </th>
    </tr>
    </thead>
    <tbody>
    <tr>
      <td>
        <asp:label id="lblAtributo" runat="server" cssclass="label">Permiso</asp:label>
      </td>
      <td>
        <asp:dropdownlist id="cboPermiso" runat="server" width="200px" cssclass="box"></asp:dropdownlist>
      </td>
    </tr>
    </tbody>
    <tfoot>
    <tr>
      <td height="24" align="right" colspan="2">
        <a id="cmdOK" href="#" style="WIDTH: 80px" class="button" onclick="controllerObject.onOkClick();">
          <img src="/images/icons/icon_ok.gif" border="0"><span id="cmdOKText">Aceptar</span></a>
        &nbsp; <a id="cmdCancel" href="#" style="WIDTH: 80px" class="button" onclick="controllerObject.onCancelClick();">
          <img src="/images/icons/icon_cancel.gif" border="0"><span id="cmdCancelText">Cancelar</span></a>
      </td>
    </tr>
    </tfoot>
  </table>
</form>
</body>
</HTML>