using System;

namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for HtmlDeviceSettings.
  /// </summary>
  [Serializable]
  public class HtmlDeviceSettings : DeviceSettings
  {
    //The bookmark ID to jump to in the report.
    public string BookmarkId;

    //Indicates whether to show or hide the report document map. The default value of this parameter is true. 
    public string DocMap;

    //The document map ID to scroll to in the report. 
    public string DocMapId;

    //The number of the last page to use in the search. For example, a value of 5 indicates that the last page to be searched is page 5 of the report. The default value is the number of the current page. Use this setting in conjunction with the StartFind setting. 
    public string EndFind;

    //The number of the page to display if a search or a document map selection fails. The default value is the number of the current page. 
    public string FallbackPage;

    //The text to search for in the report. The default value of this parameter is an empty string. 
    public string FindString;

    //Gets a particular icon for the HTML Viewer user interface. 
    public string GetImage;

    //Indicates whether an HTML fragment is created in place of a full HTML document. An HTML fragment includes the report content in a TABLE element and omits the HTML and BODY elements.
    public string HtmlFragment;

    //The icon of a particular rendering extension. 
    public string Icon;

    //Indicates whether JavaScript is supported in the rendered report. 
    public string JavaScript;

    //The target for hyperlinks in the report. You can target a window or frame by providing the name of the window, like LinkTarget=window_name, or you can target a new window using LinkTarget=_blank. Other valid target names include _self, _parent, and _top. 
    public string LinkTarget;

    //Indicates whether to show or hide the parameters area of the toolbar. If you set this parameter to a value of true, the parameters area of the toolbar is displayed. The default value of this parameter is true. 
    public string Parameters;

    //The page number of the report to render. A value of 0 indicates that all sections of the report are rendered. The default value is 1. 
    public string Section;

    //The number of the page on which to begin the search. The default value is the number of the current page. Use this setting in conjunction with the EndFind setting. 
    public string StartFind;

    //The path used for prefixing the value of the src attribute of the IMG element in the HTML report returned by the report server. By default, the report server provides the path. You can use this setting to specify a root path for the images in a report (for example, http://myserver/resources/companyimages). 
    public string StreamRoot;

    //Indicates whether styles and scripts are created as a separate stream instead of in the document.
    public string StyleStream;

    //Indicates whether to show or hide the toolbar. If the value of this parameter is false, all remaining options (except the document map) are ignored. If you omit this parameter, the toolbar is automatically displayed for rendering formats that support it.
    public string Toolbar;

    //The short name of the browser type (for example, "IE5") as defined in browscap.ini. 
    public string Type;

    //The report zoom value as an integer percentage or a string constant. Standard string values include Page Width and Whole Page. This parameter is ignored by versions of Microsoft Internet Explorer earlier than Internet Explorer 5.0 and all non-Microsoft browsers.
    public string Zoom;
  }
}