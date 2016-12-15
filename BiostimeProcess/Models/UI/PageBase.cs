using System;
using System.Collections;
using System.Web.UI;
using BiostimeProcess.Service.AppService;

namespace BiostimeProcess.Models.UI
{
    public class PageBase : Page
    {
        protected readonly EDoc2ProcessManager EDoc2ProcessManager;
        protected readonly FaProcessService FaProcessService;
        protected readonly ProcessFormService ProcessFormService;

        public PageBase()
        {
            EDoc2ProcessManager = new EDoc2ProcessManager();
            ProcessFormService = new ProcessFormService();
            FaProcessService = new FaProcessService();
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

        protected string ProcessId
        {
            get { return Request["ProcessId"] ?? string.Empty; }
        }

        protected string IncidentId
        {
            get { return IsBeginTask ? Guid.NewGuid().ToString() : EDoc2ProcessManager.GetTaskIncidentId(TaskId); }
        }

        /// <summary>
        ///     外部流程发起
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="incidentId"></param>
        /// <param name="hashVariation"></param>
        protected void Start(string processId, string incidentId, Hashtable hashVariation)
        {
            EDoc2ProcessManager.Start(ProcessId, incidentId, hashVariation);
            DialogAndCloseBrowser();
        }

        /// <summary>
        ///     外部流程发起
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="incidentId"></param>
        /// <param name="hashVariation"></param>
        protected void StartFa(string processId, string incidentId, Hashtable hashVariation)
        {
            EDoc2ProcessManager.Start(ProcessId, incidentId, hashVariation);
            const string msg = "提交成功";
            string closeBrowser = string.Format("function() {{ window.opener='';window.close();}}");
            string js = string.Format("$(document).ready(function(){{webui.alert('{0}',{1});}});", msg,
                                      closeBrowser);
            Page.ClientScript.RegisterStartupScript(GetType(), "DialogAndCloseBrowser", js, true);
        }

        protected void StartAdSubmit()
        {
            EDoc2ProcessManager.Submit(TaskId, null, null);
            const string msg = "提交成功";
            string closeBrowser = string.Format("function() {{ window.opener='';window.close();}}");
            string js = string.Format("$(document).ready(function(){{webui.alert('{0}',{1});}});", msg,
                                      closeBrowser);
            Page.ClientScript.RegisterStartupScript(GetType(), "DialogAndCloseBrowser", js, true);
        }

        /// <summary>
        ///     内置流程发起
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="incidentId"></param>
        /// <param name="hashVariation"></param>
        protected void InternalStart(string processId, string incidentId, Hashtable hashVariation)
        {
            EDoc2ProcessManager.Start(ProcessId, incidentId, hashVariation);
            DialogAndCloseDlgBeginTask();
        }

        protected void Submit()
        {
            Submit(null, null);
        }

        protected void Submit(string desc)
        {
            Submit(null, desc);
        }

        protected void Submit(Hashtable hashVariation)
        {
            Submit(hashVariation, null);
        }

        protected void Submit(Hashtable hashVariation, string desc)
        {
            EDoc2ProcessManager.Submit(TaskId, hashVariation, desc);
            DialogAndCloseBrowser();
        }

        protected void ReturnPrevStep(string desc)
        {
            ReturnPrevStep(null, desc);
        }

        protected void ReturnPrevStep(Hashtable hashVariation, string desc)
        {
            EDoc2ProcessManager.ReturnPrevStep(TaskId, hashVariation, desc);
            DialogAndCloseBrowser();
        }

        protected void ReturnStartor(string desc)
        {
            ReturnStartor(null, desc);
        }

        protected void ReturnStartor(Hashtable hashVariation, string desc)
        {
            EDoc2ProcessManager.ReturnStartor(TaskId, hashVariation, desc);
            DialogAndCloseBrowser();
        }

        protected void AbortIncident()
        {
            EDoc2ProcessManager.AbortIncident(TaskId);
            DialogAndCloseBrowser();
        }

        private void DialogAndCloseDlgBeginTask()
        {
            //内置流程
            //parent.dlgBeginTask.close();top.dlgBeginTask.close()其中一个
            string function = string.Format("function() {{parent.dlgBeginTask.close();}}");
            const string msg = "提交成功";
            string js = string.Format("$(document).ready(function(){{webui.alert('{0}',{1});}});", msg,
                                      function);
            Page.ClientScript.RegisterStartupScript(GetType(), "DialogAndCloseBrowser", js, true);
        }

        private void DialogAndCloseBrowser()
        {
            const string msg = "提交成功";
            string closeBrowser = string.Format("function() {{ window.opener='';window.close();}}");
            string js = string.Format("$(document).ready(function(){{webui.alert('{0}',{1});}});", msg,
                                      closeBrowser);
            Page.ClientScript.RegisterStartupScript(GetType(), "DialogAndCloseBrowser", js, true);
        }

        protected void Dialog(string msg)
        {
            string js = string.Format("$(document).ready(function(){{webui.alert('{0}');}});", msg);
            Page.ClientScript.RegisterStartupScript(GetType(), "Dialog", js, true);
        }
    }
}