using System;
using System.Collections;
using System.Collections.Generic;
using BiostimeProcess.Models.UI;
using BiostimeProcess.Service.AppService;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.Pages.Baogao
{
    public partial class Start : PageBase
    {
        private readonly FaProcessService _faProcessService = new FaProcessService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.Step = FaProcessStepEnum.Start;
                InitializeButton();
            }
        }

        private void InitializeButton()
        {
            if (IsBeginTask)
            {
                StartProcessButton.Visible = true;
            }
            else
            {
                SubmitProcessButton.Visible = true;
                AbortProcessButton.Visible = true;
            }
        }

        protected void StartProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                string incidentId = IncidentId;
                FaArchiveTranfer faArchiveTranfer = Master.GetFaArchiveTranfer();
                IList<FaProcess> faProcesses = Master.GetFaProcess();
                ProcessForm processForm = ProcessFormService.GetNewProcessForm(ProcessId, incidentId);
                processForm.FaArchiveTranfers.Add(faArchiveTranfer);
                faArchiveTranfer.FaProcesses.AddRange(faProcesses);
                ProcessFormService.Save(processForm);
                Hashtable taskVariation = new Hashtable();
                if (EDoc2Helper.IsUserInUserGroup(EDoc2Helper.GetCurrentUserId(), WebConfig.ManagerGroup))
                {
                    taskVariation.Add("fenzhizouxiang", "是");
                }
                else
                {
                    taskVariation.Add("fenzhizouxiang", "否");
                }
                StartFa(ProcessId, incidentId, taskVariation);
            }
            catch (Exception ex)
            {
                Edoc2LogHelper.WriteProcessSubmitExceptionLog(ex);
                Dialog("出现异常,请联系管理员。");
            }
        }

        protected void SubmitProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                Master.SaveEntity();
                StartAdSubmit();
            }
            catch (Exception ex)
            {
                Edoc2LogHelper.WriteProcessSubmitExceptionLog(ex);
                Dialog("出现异常,请联系管理员。");
            }
        }

        protected void AbortProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                //ProcessForm processForm = ProcessFormService.GetByInstanceId(IncidentId);
                //FaArchiveTranfer faArchiveTranfer = FaProcessService.GetFaArchiveTranferByFormId(processForm.Id);
                //_faProcessService.UpdateLiuchengzhuangtai(faArchiveTranfer, LiuchengZhuangtaiEnum.YiChexiao);
                //AbortIncident();
            }
            catch (Exception ex)
            {
                Edoc2LogHelper.WriteProcessSubmitExceptionLog(ex);
                Dialog("出现异常,请联系管理员。");
            }
        }
    }
}