using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiostimeProcess.Models.UI
{
    public enum ActionResult
    {
        Succeed,
        Error
    }

    public class ActionResultModel
    {
        public ActionResult result;

        public string message;

        public object data;

        public int total;
    }
}