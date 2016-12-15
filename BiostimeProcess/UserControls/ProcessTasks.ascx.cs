using System;
using System.Collections.Generic;
using BiostimeProcess.Models.UI;
using BiostimeProcess.Service.AppService;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.UserControls
{
    public partial class ProcessTasks : UserControlBase
    {
        private readonly EDoc2ProcessManager _eDoc2ProcessManager = new EDoc2ProcessManager();

        protected string TaskId
        {
            get { return Request["taskId"] ?? string.Empty; }
        }

        private string TaskType
        {
            get { return Request["taskType"] ?? string.Empty; }
        }

        private bool IsBeginTask
        {
            get { return Request["taskType"] != null && TaskType == "BeginTask"; }
        }

        private string IncidentId
        {
            get { return IsBeginTask ? Guid.NewGuid().ToString() : _eDoc2ProcessManager.GetTaskIncidentId(TaskId); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            if (!WebConfig.IsDebug)
            {
                BindTasks();
            }
        }

        private void BindTasks()
        {
            if (IsBeginTask)
            {
                return;
            }
            List<TaskEntity> list = _eDoc2ProcessManager.GetTaskData(IncidentId);
            //合生元流程处理
            var hsyList = new List<TaskEntity>();
            foreach (TaskEntity item in list)
            {
                if (item.Status == "激活")
                {
                    continue;
                }
                if (item.Status == "已完成" && item.StepName == "结束")
                {
                    item.Status = "借阅完成";
                    hsyList.Add(item);
                    continue;
                }
                item.Status = "已提交";
                hsyList.Add(item);
            }
            Tasks.DataSource = hsyList;
            Tasks.DataBind();
        }
    }
}