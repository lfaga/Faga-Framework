<%@ Page Language="c#" Inherits="Faga.Framework.Services.Administracion.Webpages.ParamsPage"
CodeFile="Params.aspx.cs" %>

<%@ Register TagPrefix="cc1" Namespace="Faga.Framework.Web.UI.Controls.WebControls"
Assembly="Faga.Framework.Web" %>
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
                    <asp:Label ID="lblTitle" runat="server" CssClass="label">Permisos de Grupos</asp:Label>
                  </th>
                </tr>
                </thead>
                <tbody>
                <tr>
                  <td align="center" valign="middle">
                    <table>
                      <tr>
                        <td valign="top" align="center">
                          <table>
                            <tr>
                              <td align="right">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Aplicación</asp:Label>
                              </td>
                              <td align="left">
                                <asp:DropDownList ID="cboApplication" runat="server" Width="240px" CssClass="box"
                                                  AutoPostBack="True">
                                </asp:DropDownList>
                              </td>
                            </tr>
                            <tr>
                              <td align="right">
                                <asp:Label ID="lblId" runat="server" CssClass="label">Grupo</asp:Label>
                              </td>
                              <td align="left">
                                <asp:DropDownList ID="cboGroup" runat="server" Width="240px" CssClass="box">
                                </asp:DropDownList>
                              </td>
                            </tr>
                            <tr>
                              <td align="right">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Permiso</asp:Label>
                              </td>
                              <td align="left">
                                <asp:DropDownList ID="cboPermission" runat="server" Width="240px" CssClass="box">
                                </asp:DropDownList>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td valign="top" align="center">
                          <table cellpadding="0" cellspacing="0" id="tblDetail" runat="server">
                            <tr>
                              <td>
                                <cc1:customdatagrid id="grdDetail" runat="server" cssclass="grid" autogeneratecolumns="False"
                                                    useaccessibleheader="True" width="100%" pagesize="5" allowpaging="True" title="Parámetros"
                                                    height="100%" allowsorting="True">
                                  <SelectedItemStyle CssClass="selected"></SelectedItemStyle>
                                  <HeaderStyle Width="100%" CssClass="header"></HeaderStyle>
                                  <Columns>
                                    <asp:BoundColumn DataField="key" SortExpression="key" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="value" SortExpression="value" HeaderText="Valor"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                      <HeaderStyle Width="24px"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                      <ItemTemplate>
                                        <asp:linkbutton id="Linkbutton2" runat="server" commandname="Delete" causesvalidation="false" CssClass="iconlink">
                                          <img src="/images/icons/icon_remove.gif">
                                        </asp:linkbutton>
                                      </ItemTemplate>
                                    </asp:TemplateColumn>
                                  </Columns>
                                  <PagerStyle CssClass="footer"></PagerStyle>
                                </cc1:customdatagrid>
                              </td>
                            </tr>
                            <tr>
                              <td align="right">
                                <asp:LinkButton ID="lnkAttrAdd" runat="server" CssClass="button" Width="80px"><img src="/images/icons/icon_add.gif">Agregar</asp:LinkButton>
                              </td>
                            </tr>
                          </table>
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
                                  allowpaging="True" useaccessibleheader="True" allowsorting="True" pagesize="15">
                <SelectedItemStyle CssClass="selected"></SelectedItemStyle>
                <AlternatingItemStyle CssClass="rowimpar"></AlternatingItemStyle>
                <ItemStyle CssClass="rowpar"></ItemStyle>
                <HeaderStyle CssClass="header"></HeaderStyle>
                <Columns>
                  <asp:BoundColumn DataField="Group" SortExpression="Group" HeaderText="Grupo"></asp:BoundColumn>
                  <asp:BoundColumn DataField="Permission" SortExpression="Permission" HeaderText="Permiso"></asp:BoundColumn>
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