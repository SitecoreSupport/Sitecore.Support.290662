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
      return GetCurrentItem(DataContext, message, DataContext.Language);
    }

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