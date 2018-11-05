namespace Sitecore.Support.XA.Foundation.Multisite.Dialogs
{
  using Sitecore;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.Shell.Framework;
  using Sitecore.Web.UI.HtmlControls;
  using Sitecore.Web.UI.Sheer;
  using Sitecore.XA.Foundation.Multisite.Extensions;
  using System.Linq;

  public class InsertImageForm : Sitecore.XA.Foundation.Multisite.Dialogs.InsertImageForm
  {
    private Item GetCurrentItem(Message message)
    {
      //change the call of the base method to make inheritance and code change easier and more reliable
      return this.GetCurrentItem(DataContext, message, DataContext.Language);
    }

    #region Added code from MediaDialogFormBase
    protected new Item GetCurrentItem(DataContext context, Message message, Language language)
    {
      Assert.ArgumentNotNull(message, "message");
      string text = message["id"];
      Item folder = context.GetFolder();
      if (folder != null)
      {
        language = folder.Language;
      }
      if (!string.IsNullOrEmpty(text))
      {
        return Client.ContentDatabase.GetItem(text, language);
      }
      if (folder != null && context.Folder != folder.ID.ToString())
      {
        Item[] virtualChildren = folder.GetVirtualChildren();
        if (virtualChildren.Any())
        {
          #region Modified code
          Item selectedItem = folder.Database.GetItem(context.Folder);
          #endregion
          if (selectedItem != null && virtualChildren.Any((Item v) => v.Axes.IsAncestorOf(selectedItem)))
          {
            return selectedItem;
          }
        }
      }
      return folder;
    }
    #endregion

    public override void HandleMessage(Message message)
    {
      Assert.ArgumentNotNull(message, "message");
      if (message.Name == "item:load")
      {
        #region Modified code
        base.HandleMessage(message);
        #endregion
      }
      else
      {
        Dispatcher.Dispatch(message, GetCurrentItem(message));
        base.HandleMessage(message);
      }
    }
  }
}