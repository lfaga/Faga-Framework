<%@ Register TagPrefix="cc1" Namespace="Faga.Framework.Web.UI.Controls.WebControls"
Assembly="Faga.Framework.Web" %>

<%@ Page Language="c#" Inherits="Faga.Framework.Services.Administracion.Webpages.UsersPage"
CodeFile="Users.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
  <title>Applications</title>
  <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
  <meta content="C#" name="CODE_LANGUAGE">
  <meta content="JavaScript" name="vs_defaultClientScript">
  <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body>
<form id="Form1" method="post" runat="server">
  <table class="outer">
    <tr>
      <td class="container">
        <table class="window" width="100%">
          <tr>
            <td>
              <table width="100%">
                <thead>
                <tr>
                  <th>
                    <asp:Label ID="lblTitle" runat="server" CssClass="label">Usuarios</asp:Label>
                  </th>
                </tr>
                </thead>
                <tbody>
                <tr>
                  <td align="center" valign="middle">
                    <table>
                      <tr>
                        <td align="right">
                          <asp:Label ID="lblUserName" runat="server" CssClass="label">Nombre de Usuario</asp:Label>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="box"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td align="right">
                          <asp:Label ID="lblNombre" runat="server" CssClass="label">Nombre</asp:Label>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtNombre" runat="server" Width="200px" CssClass="box"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td align="right">
                          <asp:Label ID="lblApellido" runat="server" CssClass="label">Apellido</asp:Label>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtApellido" runat="server" Width="200px" CssClass="box"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td align="right">
                          <asp:Label ID="lblTel" runat="server" CssClass="label">Teléfono</asp:Label>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtTel" runat="server" Width="200px" CssClass="box"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td align="right">
                          <asp:Label ID="lblEmail" runat="server" CssClass="label">E-Mail</asp:Label>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtEmail" runat="server" Width="200px" CssClass="box"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" align="right">
                          <asp:LinkButton ID="lnkGetUserData" runat="server" CssClass="button" Width="128px"
                                          OnClick="lnkGetUserData_Click">
                            Traer datos externos
                          </asp:LinkButton>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                </tbody>
                <tfoot>
                <tr>
                  <td id="tdtoolbar" align="center">
                    <asp:LinkButton ID="lnkSearch" runat="server" SkinID="lnkSearch"/>&nbsp;
                    <asp:LinkButton ID="lnkNew" runat="server" SkinID="lnkNew"/>&nbsp;
                    <asp:LinkButton ID="lnkCopy" runat="server" SkinID="lnkCopy"/>&nbsp;
                    <asp:LinkButton ID="lnkSave" runat="server" SkinID="lnkSave"/>&nbsp;
                    <asp:LinkButton ID="lnkDelete" runat="server" SkinID="lnkDelete"/>&nbsp;
                    <asp:LinkButton ID="lnkCancel" runat="server" SkinID="lnkCancel"/>
                  </td>
                </tr>
                </tfoot>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <cc1:customdatagrid id="grdMaster" runat="server" cssclass="grid" width="100%" autogeneratecolumns="False"
                                  allowpaging="True" useaccessibleheader="True" allowsorting="True" pagesize="13">
                <SelectedItemStyle CssClass="selected"></SelectedItemStyle>
                <AlternatingItemStyle CssClass="rowimpar"></AlternatingItemStyle>
                <ItemStyle CssClass="rowpar"></ItemStyle>
                <HeaderStyle CssClass="header"></HeaderStyle>
                <Columns>
                  <asp:BoundColumn DataField="Id" SortExpression="Id" HeaderText="C&#243;digo"></asp:BoundColumn>
                  <asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="Usuario"></asp:BoundColumn>
                  <asp:BoundColumn DataField="Nombre" SortExpression="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                  <asp:BoundColumn DataField="Apellido" SortExpression="Apellido" HeaderText="Apellido"></asp:BoundColumn>
                  <asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email"></asp:BoundColumn>
                  <asp:TemplateColumn HeaderText="Editar">
                    <HeaderStyle Width="24px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                      <asp:linkbutton id="Linkbutton1" runat="server" commandname="Select" causesvalidation="false" CssClass="iconlink">
                        <img src="/images/icons/icon_edit.gif">
                      </asp:linkbutton>
                    </ItemTemplate>
                  </asp:TemplateColumn>
                </Columns>
                <PagerStyle CssClass="footer"></PagerStyle>
              </cc1:customdatagrid>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</form>
</body>
</html>