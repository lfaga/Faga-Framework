<%@ Register TagPrefix="cc1" Namespace="Faga.Framework.Web.UI.Controls.WebControls"
Assembly="Faga.Framework.Web" %>

<%@ Page Language="c#" Inherits="Faga.Framework.Services.Administracion.Webpages.UsersGroupsPage"
CodeFile="UsersGroups.aspx.cs" %>

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
                        <td valign="top" align="center">
                          <table>
                            <tr>
                              <td style="height: 20px" align="right">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Aplicación</asp:Label>
                              </td>
                              <td style="height: 20px" align="left">
                                <asp:DropDownList ID="cboApplication" runat="server" Width="240px" CssClass="box"
                                                  AutoPostBack="True" OnSelectedIndexChanged="cboApplication_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                            </tr>
                            <tr>
                              <td style="height: 17px" align="right">
                                <asp:Label ID="lblId" runat="server" CssClass="label">Nombre</asp:Label>
                              </td>
                              <td style="height: 17px" align="left">
                                <asp:TextBox ID="txtName" runat="server" CssClass="box" Width="240px"></asp:TextBox>
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
                                                    useaccessibleheader="True" width="100%" pagesize="5" allowpaging="True" showheader="False"
                                                    title="Grupos">
                                  <SELECTEDITEMSTYLE CssClass="selected"></SELECTEDITEMSTYLE>
                                  <HEADERSTYLE CssClass="header" Width="100%"></HEADERSTYLE>
                                  <COLUMNS>
                                    <ASP:BOUNDCOLUMN SortExpression="Id" DataField="Id"></ASP:BOUNDCOLUMN>
                                    <ASP:BOUNDCOLUMN SortExpression="Name" DataField="Name"></ASP:BOUNDCOLUMN>
                                    <ASP:BOUNDCOLUMN SortExpression="Description" DataField="Description"></ASP:BOUNDCOLUMN>
                                    <ASP:TEMPLATECOLUMN>
                                      <HEADERSTYLE Width="24px"></HEADERSTYLE>
                                      <ITEMSTYLE HorizontalAlign="Center"></ITEMSTYLE>
                                      <ITEMTEMPLATE>
                                        <asp:linkbutton id="Linkbutton2" runat="server" CssClass="iconlink" causesvalidation="false" commandname="Delete">
                                          <img src="/images/icons/icon_remove.gif">
                                        </asp:linkbutton>
                                      </ITEMTEMPLATE>
                                    </ASP:TEMPLATECOLUMN>
                                  </COLUMNS>
                                  <PAGERSTYLE CssClass="footer"></PAGERSTYLE>
                                </cc1:customdatagrid>
                              </td>
                            </tr>
                            <tr>
                              <td align="right">
                                <asp:LinkButton ID="lnkAttrAdd" runat="server" CssClass="button" Width="80px" OnClick="lnkAttrAdd_Click"><img src="/images/icons/icon_add.gif">Agregar</asp:LinkButton>
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
                <SELECTEDITEMSTYLE CssClass="selected"></SELECTEDITEMSTYLE>
                <ALTERNATINGITEMSTYLE CssClass="rowimpar"></ALTERNATINGITEMSTYLE>
                <ITEMSTYLE CssClass="rowpar"></ITEMSTYLE>
                <HEADERSTYLE CssClass="header"></HEADERSTYLE>
                <COLUMNS>
                  <ASP:BOUNDCOLUMN SortExpression="Id" DataField="Id" HeaderText="Código"></ASP:BOUNDCOLUMN>
                  <ASP:BOUNDCOLUMN SortExpression="UserName" DataField="UserName" HeaderText="Nombre"></ASP:BOUNDCOLUMN>
                  <ASP:TEMPLATECOLUMN HeaderText="Editar">
                    <HEADERSTYLE Width="24px"></HEADERSTYLE>
                    <ITEMSTYLE HorizontalAlign="Center"></ITEMSTYLE>
                    <ITEMTEMPLATE>
                      <asp:linkbutton id="Linkbutton1" runat="server" CssClass="iconlink" causesvalidation="false" commandname="Select">
                        <img src="/images/icons/icon_edit.gif">
                      </asp:linkbutton>
                    </ITEMTEMPLATE>
                  </ASP:TEMPLATECOLUMN>
                </COLUMNS>
                <PAGERSTYLE CssClass="footer"></PAGERSTYLE>
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