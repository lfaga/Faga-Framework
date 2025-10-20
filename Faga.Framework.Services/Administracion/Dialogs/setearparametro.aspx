<%@ Page language="c#" Inherits="Faga.Framework.Services.Administracion.Dialogs.SetearParametro" CodeFile="SetearParametro.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD runat="server">
  <title>seleccionarAtributo</title>
  <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
  <meta content="C#" name="CODE_LANGUAGE">
  <meta content="JavaScript" name="vs_defaultClientScript">
  <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  <script language="javascript" type="text/javascript">
    <!--

    function OK_onClick(parentInstance) {
      var cbo = document.getElementById("cboKey");
      var txt = document.getElementById("txtValue");
      window.returnValue = "key=" + cbo.value.toString()
        + ";value=" + txt.value;
    }

    //-->
  </script>
</HEAD>
<body style="OVERFLOW: hidden">
<form id="Form1" method="post" runat="server">
  <table class="window" height="100%" width="100%">
    <thead>
    <tr>
      <th colSpan="2">
        Agregar Parámetro
      </th>
    </tr>
    </thead>
    <tbody>
    <tr>
      <td>
        <asp:label id="lblAtributo" runat="server" cssclass="label">Nombre</asp:label>
      </td>
      <td>
        <asp:DropDownList id="cboKey" runat="server" Width="200px" CssClass="box"></asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td>
        <asp:label id="Label1" runat="server" cssclass="label">Valor</asp:label>
      </td>
      <td>
        <asp:textbox id="txtValue" runat="server" CssClass="box" Width="200px"></asp:textbox>
      </td>
    </tr>
    </tbody>
    <tfoot>
    <tr>
      <td align="right" colSpan="2" height="24">
        <A class="button" id="cmdOK" style="WIDTH: 80px" onclick="controllerObject.onOkClick();"
           href="#">
          <IMG src="/images/icons/icon_ok.gif" border="0"><span id="cmdOKText">Aceptar</span></A>
        &nbsp; <A class="button" id="cmdCancel" style="WIDTH: 80px" onclick="controllerObject.onCancelClick();"
                  href="#">
          <IMG src="/images/icons/icon_cancel.gif" border="0"><span id="cmdCancelText">Cancelar</span></A>
      </td>
    </tr>
    </tfoot>
  </table>
</form>
</body>
</HTML>