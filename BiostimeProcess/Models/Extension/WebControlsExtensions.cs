using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BiostimeProcess.Models.Extension
{
    internal static class WebControlsExtensions
    {
        public static void AddCssClass(this WebControl control, string cssClass)
        {
            List<string> classes = control.CssClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (!classes.Contains(cssClass))
            {
                classes.Add(cssClass);
            }
            control.CssClass = string.Join(" ", classes);
        }

        public static void RemoveCssClass(this WebControl control, string cssClass)
        {
            List<string> classes = control.CssClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (classes.Contains(cssClass))
            {
                classes.Remove(cssClass);
            }
            control.CssClass = string.Join(" ", classes);
        }
    }
}