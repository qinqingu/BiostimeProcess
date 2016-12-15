using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace BiostimeProcess.Models.UI
{
    public class UserControlBase : UserControl
    {
        protected void RegisterClientScript(string key, string url)
        {
            Type type = GetType();
            ClientScriptManager clientScriptManager = Page.ClientScript;
            if (!clientScriptManager.IsClientScriptIncludeRegistered(type, key))
            {
                clientScriptManager.RegisterClientScriptInclude(type, key, ResolveClientUrl(url));
            }
        }
    }
}