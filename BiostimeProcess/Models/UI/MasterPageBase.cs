using System;
using System.Web.UI;
using BiostimeProcess.Service.AppService;

namespace BiostimeProcess.Models.UI
{
    public class MasterPageBase : MasterPage
    {
        protected readonly EDoc2ProcessManager EDoc2ProcessManager;
        protected readonly ProcessFormService ProcessFormService;

        public MasterPageBase()
        {
            EDoc2ProcessManager = new EDoc2ProcessManager();
            ProcessFormService = new ProcessFormService();
        }
        protected bool IsBeginTask
        {
            get { return Request["taskType"] != null && TaskType == "BeginTask"; }
        }
        
        protected string TaskType
        {
            get { return Request["taskType"] ?? string.Empty; }
        }

        protected string TaskId
        {
            get { return Request["taskId"] ?? string.Empty; }
        }
        protected string IncidentId
        {
            get { return IsBeginTask ? Guid.NewGuid().ToString() : EDoc2ProcessManager.GetTaskIncidentId(TaskId); }
        }
        protected void DialogAndCloseBrowser(string msg)
        {
            string closeBrowser = string.Format("function() {{ window.opener='';window.close();}}");
            string js = string.Format("$(document).ready(function(){{webui.alert('{0}',{1});}});", msg,
                                      closeBrowser);
            Page.ClientScript.RegisterStartupScript(GetType(), "DialogAndCloseBrowser", js, true);
        }
    }
}