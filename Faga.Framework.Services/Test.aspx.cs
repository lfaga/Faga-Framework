using System;
using Faga.Framework.Web.UI.Controls.WebControls;
using Faga.Framework.Web.UI.Templates;

public partial class Test : PageTemplate
{
  protected MorphingControl mctl;

  protected void Page_Load(object sender, EventArgs e)
  {
    mctl = new MorphingControl(typeof (string));
    pnlContainer.Controls.Add(mctl);
  }

  protected void btnTest_Click(object sender, EventArgs e)
  {
    lblTest.Text = mctl.Value.ToString();
  }
}