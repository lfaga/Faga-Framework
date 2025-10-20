<%@ Register TagPrefix="cc1" Namespace="Faga.Framework.Web.UI.Controls.WebControls"
Assembly="Faga.Framework.Web" %>

<%@ Page Language="c#" Inherits="Faga.Framework.Services.Administracion.ApplicationsPage"
CodeFile="Applications.aspx.cs" %>

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
                    <asp:Label ID="lblTitle" runat="server" CssClass="label">Aplicaciones</asp:Label>
                  </th>
                </tr>
                </thead>
                <tbody>
                <tr>
                  <td align="center" valign="middle">
                    <table>
                      <tr>
                        <td style="height: 17px" align="right">
                          <asp:Label ID="lblId" runat="server" CssClass="label">Código</asp:Label>
                        </td>
                        <td style="height: 17px" align="left">
                          <asp:TextBox ID="txtId" runat="server" CssClass="box_disabled" Width="240px" ReadOnly="true"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="height: 15px" align="right">
                          <asp:Label ID="lblName" runat="server" CssClass="label">Nombre</asp:Label>
                        </td>
                        <td style="height: 15px" align="left">
                          <asp:TextBox ID="txtName" runat="server" CssClass="box" Width="240px"></asp:TextBox>
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
                  <asp:BoundColumn DataField="Id" SortExpression="Id" HeaderText="C&#243;digo"></asp:BoundColumn>
                  <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nombre"></asp:BoundColumn>
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