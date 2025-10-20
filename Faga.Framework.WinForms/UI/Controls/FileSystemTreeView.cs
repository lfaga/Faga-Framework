using System;
using System.IO;
using System.Windows.Forms;

namespace Faga.Framework.WinForms.UI.Controls
{
  public class FileSystemTreeView : TreeView
  {
    public DirectoryInfo SelectedFolder
    {
      get
      {
        if ((SelectedNode != null)
            && !string.IsNullOrEmpty(SelectedNode.Tag.ToString()))
        {
          return new DirectoryInfo(SelectedNode.Tag.ToString());
        }
        return null;
      }
    }

    public event EventHandler<GenericEventArgs<DirectoryInfo>> AfterFolderSelected;


    protected override void OnCreateControl()
    {
      base.OnCreateControl();

      Nodes.Clear();

      if (!DesignMode)
      {
        foreach (var drive in DriveInfo.GetDrives())
        {
          if (drive.DriveType == DriveType.Fixed)
          {
            var tnd = new TreeNode(drive.Name, 0, 0) {Tag = drive.RootDirectory.FullName};
            Nodes.Add(tnd);

            SubFoldersAdd(tnd, drive.RootDirectory);
          }
        }
      }
    }

    protected override void OnAfterExpand(TreeViewEventArgs e)
    {
      foreach (TreeNode node in e.Node.Nodes)
      {
        var folder = new DirectoryInfo(node.Tag.ToString());
        if (node.Nodes.Count == 0)
        {
          SubFoldersAdd(node, folder);
        }
      }
      base.OnAfterExpand(e);
    }

    protected override void OnAfterSelect(TreeViewEventArgs e)
    {
      if (AfterFolderSelected != null)
      {
        AfterFolderSelected(this, new GenericEventArgs<DirectoryInfo>(
          new DirectoryInfo(e.Node.Tag.ToString())
          ));
      }
      base.OnAfterSelect(e);
    }

    private static void SubFoldersAdd(TreeNode parentNode, DirectoryInfo parentFolder)
    {
      try
      {
        foreach (var folder in parentFolder.GetDirectories())
        {
          var tnf = new TreeNode(folder.Name, 1, 2) {Tag = folder.FullName};
          parentNode.Nodes.Add(tnf);
        }
      }
      catch (UnauthorizedAccessException)
      {
      }
    }
  }
}