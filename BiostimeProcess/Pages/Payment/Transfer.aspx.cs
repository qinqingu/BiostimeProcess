using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiostimeProcess.Models.UI;

namespace BiostimeProcess.Pages
{
    public partial class Transfer1 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            string queryString = Request.QueryString.ToString();
            switch (TaskType)
            {
                case "BeginTask":
                    Response.Redirect("Start.aspx?" + queryString);
                    break;
                case "InboxTask":
                    InboxTaskResponse(queryString);
                    break;
                case "CompleteTask":
                case "ArchiveTask":
                case "MyStartTask":
                    Response.Redirect("Complete.aspx?" + queryString);
                    break;
            }
        }

        private void InboxTaskResponse(string queryString)
        {
            string stepIdentity = EDoc2ProcessManager.GetStepIdentity(TaskId);
            if (string.IsNullOrEmpty(stepIdentity))
            {
                Response.Write("该任务已经执行了!");
                Response.End();
            }
            switch (stepIdentity)
            {
                case "开始":
                    Response.Redirect("Start.aspx?" + queryString);
                    break;
                case "审批":
                    Response.Redirect("Approval.aspx?" + queryString);
                    break;
                case "最终审批":
                    Response.Redirect("FinalApproval.aspx?" + queryString);
                    break;
            }
        }
    }
}