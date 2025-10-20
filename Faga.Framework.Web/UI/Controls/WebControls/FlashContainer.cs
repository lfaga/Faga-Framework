using System;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Faga.Framework.Web.UI.Controls.WebControls
{
  public delegate void WccFlashCommandEventHandler(object source, string command, string arguments);


  [DefaultProperty("Src")]
  [ToolboxData("<{0}:FlashContainer runat=server></{0}:FlashContainer>")]
  public class FlashContainer : WebControl
  {
    protected HtmlGenericControl Emb;
    protected HtmlInputHidden HidEventArgument;
    protected HtmlInputHidden HidEventTarget;
    protected HtmlGenericControl Obj;
    protected HtmlGenericControl Scp;

    public FlashContainer()
    {
      Obj = new HtmlGenericControl("OBJECT");
      Emb = new HtmlGenericControl("EMBED");
      Scp = new HtmlGenericControl("SCRIPT");
      HidEventTarget = new HtmlInputHidden();
      HidEventArgument = new HtmlInputHidden();

      Load += FlashContainer_Load;
    }

    public event WccFlashCommandEventHandler WccFlashCommand;


    private void FlashContainer_Load(object sender, EventArgs e)
    {
      var etName = "__" + ClientID + @"Obj_EVENTTARGET";
      var eaName = "__" + ClientID + @"Obj_EVENTARGUMENT";

      if (Context.Request.Form[etName] != null)
      {
        string et = Context.Request.Form[etName], ea;

        if (Context.Request.Form[eaName] != null)
        {
          ea = Context.Request.Form[eaName];
        }
        else
        {
          ea = "";
        }

        if (WccFlashCommand != null)
        {
          WccFlashCommand(this, et, ea);
        }
      }

      AddParam(Obj, "Movie", _src);
      AddParam(Obj, "Src", _src);
      Emb.Attributes["src"] = _src;
    }


    protected override void Render(HtmlTextWriter output)
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      preRender();

      base.Render(htm);

      output.Write(str.ToString());
    }


    private void preRender()
    {
      Obj.Attributes["codeBase"] =
        "http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0";
      Obj.Attributes["height"] = "400";
      Obj.Attributes["width"] = "550";
      Obj.Attributes["classid"] = "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000";

      AddParam(Obj, "_cx", "14552");
      AddParam(Obj, "_cy", "10583");
      AddParam(Obj, "WMode", "Window");
      AddParam(Obj, "Play", "-1");
      AddParam(Obj, "Loop", "-1");
      AddParam(Obj, "Quality", "High");
      AddParam(Obj, "SAlign", "");
      AddParam(Obj, "Menu", "-1");
      AddParam(Obj, "Base", "");
      AddParam(Obj, "AllowScriptAccess", "always");
      AddParam(Obj, "Scale", "ShowAll");
      AddParam(Obj, "DeviceFont", "0");
      AddParam(Obj, "EmbedMovie", "0");
      AddParam(Obj, "BGColor", "");
      AddParam(Obj, "SWRemote", "");
      AddParam(Obj, "MovieData", "");
      AddParam(Obj, "SeamlessTabbing", "1");

      Emb.Attributes["quality"] = "high";
      Emb.Attributes["width"] = "550";
      Emb.Attributes["height"] = "400";
      Emb.Attributes["name"] = ClientID + "Obj";
      Emb.Attributes["type"] = "application/x-shockwave-flash";
      Emb.Attributes["pluginspage"] = "http://www.macromedia.com/go/getflashplayer";
      Emb.Attributes["swLiveConnect"] = "true";

      Obj.ID = ClientID + "Obj";

      Obj.Controls.Add(Emb);

      Controls.Add(Obj);

      Scp.Attributes["language"] = "javascript";

      Scp.InnerHtml =
        @"<!--
				var InternetExplorer = navigator.appName.indexOf('Microsoft') != -1;
	
				if (navigator.appName &&
						navigator.appName.indexOf('Microsoft') != -1 && 
						navigator.userAgent.indexOf('Windows') != -1 &&
						navigator.userAgent.indexOf('Windows 3.1') == -1) {
					var vs = '<SCRIPT LANGUAGE=VBScript\> \n' +
						'on error resume next \n' +
						'Sub " +
        ClientID + @"Obj_FSCommand(ByVal command, ByVal args)\n' +
						' call " + ClientID +
        @"Obj_DoFSCommand(command, args)\n' +
						'end sub\n' + 
						'<\/SCRIPT\> \n';
							
					document.write(vs);
				}

				function " +
        ClientID +
        @"Obj_DoFSCommand(command, args) {
					switch (command)
					{
						case 'rollOver':
							break;
						case 'click':
							" +
        ClientID +
        @"Obj_CmdPost(command, args);
							break;
						default:
							alert(command);
							break;
					}
				}
				
				function " +
        ClientID +
        @"Obj_CmdPost(command, args) {
					var theform;
					if (window.navigator.appName.toLowerCase().indexOf('microsoft') > -1)
						theform = document.Form1;
					else
						theform = document.forms['Form1'];

					theform.__" +
        ClientID + @"Obj_EVENTTARGET.value = command;
					theform.__" + ClientID +
        @"Obj_EVENTARGUMENT.value = args;
					theform.submit();
				}
			";

      Controls.Add(Scp);

      HidEventTarget.ID = "__" + ClientID + @"Obj_EVENTTARGET";
      Controls.Add(HidEventTarget);

      HidEventArgument.ID = "__" + ClientID + @"Obj_EVENTARGUMENT";
      Controls.Add(HidEventArgument);
    }


    private static void AddParam(HtmlGenericControl obj, string pName, string pValue)
    {
      var pm = new HtmlGenericControl("PARAM");

      pm.Attributes["NAME"] = pName;
      pm.Attributes["VALUE"] = pValue;

      obj.Controls.Add(pm);
    }

    #region Propiedades

    private string _src = "";
    private string _targetFrameName = "";
    private bool _targetIsIFrame;

    public string Src
    {
      get
      {
        try
        {
          _src = ViewState[ClientID + "_Src"].ToString();
        }
        catch
        {
          _src = "";
        }
        return _src;
      }
      set
      {
        _src = value;
        ViewState[ClientID + "_Src"] = _src;
      }
    }

    public string TargetFrameName
    {
      get
      {
        try
        {
          _targetFrameName = ViewState[ClientID + "_targetFrameName"].ToString();
        }
        catch
        {
          _targetFrameName = "";
        }
        return _targetFrameName;
      }
      set
      {
        _targetFrameName = value;
        ViewState[ClientID + "_targetFrameName"] = _targetFrameName;
      }
    }

    public bool TargetIsIFrame
    {
      get
      {
        try
        {
          _targetIsIFrame = Convert.ToBoolean(ViewState[ClientID + "_targetIsIFrame"].ToString());
        }
        catch
        {
          _targetIsIFrame = false;
        }
        return _targetIsIFrame;
      }
      set
      {
        _targetIsIFrame = value;
        ViewState[ClientID + "_targetIsIFrame"] = _targetIsIFrame.ToString();
      }
    }

    #endregion
  }
}