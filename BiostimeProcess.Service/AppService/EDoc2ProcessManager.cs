using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Utitity;
using EDoc2.Organization;
using EDoc2.Website;
using Macrowing.DMS.WorkFlow.Definition;
using Macrowing.DMS.WorkFlow.Definition.Impl;
using Macrowing.DMS.WorkFlow.Execution;
using Macrowing.DMS.WorkFlow.Execution.Impl;

namespace BiostimeProcess.Service.AppService
{
    public class EDoc2ProcessManager
    {
        public ITask GetTask(string taskId)
        {
            IExecutionHandler handler = new ExecutionHandler();
            return handler.GetTaskByTaskId(taskId);
        }

        public IProcess GetProcess(string processId)
        {
            IDefinitionHandler handler2 = new DefinitionHandler();
            return handler2.GetProcessById(processId);
        }

        public IStep GetStartStep(string processId)
        {
            IDefinitionHandler handler = new DefinitionHandler();
            return handler.GetStartStep(processId);
        }

        public IStep GetStep(string stepId)
        {
            IDefinitionHandler handler = new DefinitionHandler();
            return handler.GetStepById(stepId);
        }

        /// <summary>
        ///     获取事件编号
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <returns>事件编号</returns>
        public string GetTaskIncidentId(string taskId)
        {
            ITask task = GetTask(taskId);
            return task.IncidentId;
        }

        /// <summary>
        ///     获取步骤标识
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <returns>步骤标识</returns>
        public string GetStepIdentity(string taskId)
        {
            ITask task = GetTask(taskId);
            if (task.Status == (int) TaskStatusEnum.Complete)
            {
                return string.Empty;
            }
            IStep step = GetStep(task.StepId);
            return step.Identity;
        }


        /// <summary>
        ///     发起流程
        /// </summary>
        /// <param name="processId">流程编号</param>
        /// <param name="incidentId">事件编号</param>
        /// <param name="hashVariation">流程变量</param>
        public void Start(string processId, string incidentId, Hashtable hashVariation)
        {
            Start(processId, incidentId, "", hashVariation);
        }

        /// <summary>
        ///     发起流程
        /// </summary>
        /// <param name="processId">流程编号</param>
        /// <param name="incidentId">事件编号</param>
        /// <param name="remark">流程备注</param>
        /// <param name="hashVariation">流程变量</param>
        /// <returns></returns>
        public ITask Start(string processId, string incidentId, string remark, Hashtable hashVariation)
        {
            EDoc2UserInfo userInfo = WebsiteUtility.CurrentUser;
            string startUserId = userInfo.UserId.ToString(CultureInfo.InvariantCulture);
            IProcess process = GetProcess(processId);
            if (process == null)
            {
                throw new Exception(string.Format("无法获取processId:{0}的流程", processId));
            }
            IStep startStep = GetStartStep(processId);
            if (startStep == null)
            {
                throw new Exception(string.Format("无法获取processId:{0}的开始步骤", processId));
            }

            var task = new Task
                {
                    Remark = remark,
                    IncidentId = incidentId,
                    ProcessId = processId,
                    ProcessName = process.Name,
                    TaskStarter = startUserId,
                    StepId = startStep.Id
                };
            IExecutionHandler handler = new ExecutionHandler();
            if (hashVariation == null)
            {
                hashVariation = new Hashtable();
            }
            int result = handler.StartProcessInstance(task, hashVariation);
            if (result != 0)
            {
                throw new Exception(string.Format("SubmitTask出错:{0}", result));
            }

            var mailHandler = new ExecutionHandler();
            mailHandler.SendMail(task, task.NextStepType);
            return task;
        }

        /// <summary>
        ///     提交流程
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="hashVariation">流程变量</param>
        /// <param name="desc">审核意见</param>
        public void Submit(string taskId, Hashtable hashVariation, string desc)
        {
            ITask task = GetTask(taskId);
            if (task == null)
            {
                throw new Exception(string.Format("无法获取taskId:{0}的任务", taskId));
            }
            task.Desc = desc;
            IExecutionHandler executionHandler = new ExecutionHandler();
            if (hashVariation == null)
            {
                hashVariation = new Hashtable();
            }
            int result = executionHandler.SubmitTask(task, hashVariation);
            if (result != 0)
            {
                throw new Exception(string.Format("SubmitTask出错:{0}", result));
            }

            var mailHandler = new ExecutionHandler();
            mailHandler.SendMail(task, task.NextStepType);
        }

        /// <summary>
        ///     退回发起人
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="hashVariation">流程变量</param>
        /// <param name="desc">审核意见</param>
        public void ReturnStartor(string taskId, Hashtable hashVariation, string desc)
        {
            ITask task = GetTask(taskId);
            if (task == null)
            {
                throw new Exception(string.Format("无法获取taskId:{0}的任务", taskId));
            }
            task.Desc = desc;
            IExecutionHandler executionHandler = new ExecutionHandler();
            if (hashVariation == null)
            {
                hashVariation = new Hashtable();
            }
            int result = executionHandler.ReturnTaskToStarter(task, hashVariation);
            if (result != 0)
            {
                throw new Exception(string.Format("SubmitTask出错:{0}", result));
            }

            var mailHandler = new ExecutionHandler();
            mailHandler.SendMail(task, task.NextStepType);
        }

        /// <summary>
        ///     退回上一步
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="hashVariation">流程变量</param>
        /// <param name="desc">审核意见</param>
        public void ReturnPrevStep(string taskId, Hashtable hashVariation, string desc)
        {
            ITask task = GetTask(taskId);
            if (task == null)
            {
                throw new Exception(string.Format("无法获取taskId:{0}的任务", taskId));
            }
            task.Desc = desc;
            IExecutionHandler executionHandler = new ExecutionHandler();
            if (hashVariation == null)
            {
                hashVariation = new Hashtable();
            }
            int result = executionHandler.ReturnTaskToPrevious(task, hashVariation);
            if (result != 0)
            {
                throw new Exception(string.Format("SubmitTask出错:{0}", result));
            }

            var mailHandler = new ExecutionHandler();
            mailHandler.SendMail(task, task.NextStepType);
        }

        /// <summary>
        ///     返回特定节点
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="stepId">要返回的节点编号</param>
        /// <param name="hashVariation">流程变量</param>
        /// <param name="desc">审核意见</param>
        public void ReturnTaskToStep(string taskId, string stepId, Hashtable hashVariation, string desc)
        {
            ITask task = GetTask(taskId);
            if (task == null)
            {
                throw new Exception(string.Format("无法获取taskId:{0}的任务", taskId));
            }
            task.Desc = desc;
            IExecutionHandler executionHandler = new ExecutionHandler();
            if (hashVariation == null)
            {
                hashVariation = new Hashtable();
            }
            ITask returnTask = GetRetrunTask(task.IncidentId, stepId);
            if (returnTask == null)
            {
                throw new ArgumentNullException("returnTask");
            }
            var returnTaskIds = new List<string> {returnTask.Id};
            int result = executionHandler.ReturnTaskToStepSelected(task, returnTaskIds, hashVariation);
            if (result != 0)
            {
                throw new Exception(string.Format("SubmitTask出错:{0}", result));
            }

            var mailHandler = new ExecutionHandler();
            mailHandler.SendMail(task, task.NextStepType);
        }

        public void AbortIncident(string taskId)
        {
            IExecutionHandler executionHandler = new ExecutionHandler();
            ITask task = GetTask(taskId);
            bool result = executionHandler.AbortIncident(task);
            if (!result)
            {
                throw new Exception("撤销出错");
            }
        }

        private ITask GetRetrunTask(string incedentId, string stepId)
        {
            List<ITask> tasks = GetTasks(incedentId);
            return tasks.Where(x => x.StepId == stepId)
                        .OrderByDescending(x => x.EndTime)
                        .FirstOrDefault();
        }

        /// <summary>
        ///     获取任务列表
        /// </summary>
        /// <param name="incedentId">事件编号</param>
        /// <returns>任务列表</returns>
        public List<ITask> GetTasks(string incedentId)
        {
            IExecutionHandler executionHandler = new ExecutionHandler();
            IList taskIList = executionHandler.GetTaskListFlowLog(incedentId);
            return taskIList.Cast<ITask>().ToList();
        }

        /// <summary>
        ///     获取任务列表(可用于显示审核信息)
        /// </summary>
        /// <param name="incedentId">事件编号</param>
        /// <returns>任务列表</returns>
        public List<TaskEntity> GetTaskData(string incedentId)
        {
            IExecutionHandler executionHandler = new ExecutionHandler();
            IList taskIList = executionHandler.GetTaskListFlowLog(incedentId);

            return (from ITask task in taskIList
                    select new TaskEntity
                        {
                            Status = GetTaskStatus(task.Status),
                            StepName = task.StepName,
                            Assigner = EDoc2Helper.GetUserNameById(task.Assigner),
                            Owner = EDoc2Helper.GetUserNameById(task.Owner),
                            EndTime = FormatHelper.GetNonNullIsoDateString(task.EndTime),
                            Desc = task.Desc
                        }).ToList();
        }

        private string GetTaskStatus(int status)
        {
            switch (status)
            {
                case (int) TaskStatusEnum.Active:
                    return "激活";
                case (int) TaskStatusEnum.Complete:
                    return "已完成";
                case (int) TaskStatusEnum.Abort:
                    return "终止";
                case (int) TaskStatusEnum.Archive:
                    return "已归档";
                case (int) TaskStatusEnum.Monitor:
                    return "监控";
                case (int) TaskStatusEnum.Begin:
                    return "开始";
            }
            return FormatHelper.GetNonNullIntString(status);
        }
    }
}