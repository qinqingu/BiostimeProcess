using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BiostimeProcess.Models.Extension;
using BiostimeProcess.Models.UI;
using BiostimeProcess.Service.AppService;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.Pages
{
    public partial class _FaProcessLayout : MasterPageBase
    {
        private readonly FaArchiveInfoJsonService _faArchiveInfoJsonService = new FaArchiveInfoJsonService();

        public FaProcessStepEnum Step
        {
            set
            {
                try
                {
                    StepValue.Value = FormatHelper.GetIntString((int)value);
                    FaArchiveDetailsControl.Step = value;
                    InitControls(value);
                }
                catch (Exception ex)
                {
                    MainPlaceHolder.Visible = false;
                    string erroLog = string.Format(
                        "Set Step Exception,ex.Message={0},ex.StackTrace={1}", ex.Message, ex.StackTrace);
                    Edoc2LogHelper.WriteLog(erroLog);
                    DialogAndCloseBrowser("出现错误,请联系管理员。");
                }
            }
            get { return (FaProcessStepEnum)int.Parse(StepValue.Value); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            Master.TitleText = "合生元财务档案借阅";
            Master.SubTitleText = "财务档案借阅信息";
            Master.ProcessNameText = "借阅审批流程";
            try
            {
                if (!WebConfig.IsDebug)
                {
                    InitStepOn();
                    BindProcessForm();
                }
            }
            catch (Exception ex)
            {
                MainPlaceHolder.Visible = false;
                string erroLog = string.Format(
                    "FaProcessLayout.Page_Load Exception,ex.Message={0},ex.StackTrace={1}", ex.Message, ex.StackTrace);
                Edoc2LogHelper.WriteLog(erroLog);
                DialogAndCloseBrowser("出现错误,请联系管理员。");
            }
        }

        /// <summary>
        ///     根据流程实际进度做界面显示(去除Step值判断)
        /// </summary>
        private void InitStepOn()
        {
            List<TaskEntity> list = EDoc2ProcessManager.GetTaskData(IncidentId);
            TaskEntity endItem = list.FirstOrDefault(i => i.Status == "已完成" && i.StepName == "结束");
            if (endItem != null)
            {
                Page.Title = FaProcessSteppService.GetStepText(FaProcessStepEnum.Complete);
                Step2.AddCssClass("on");
                return;
            }
            TaskEntity cItem = list.FirstOrDefault(i => i.Status == "激活");
            if (cItem != null)
            {
                FaProcessStepEnum stepEnum = FaProcessSteppService.GetStepVal(cItem.StepName);
                switch (stepEnum)
                {
                    case FaProcessStepEnum.Start:
                        Step0.AddCssClass("on");
                        break;
                    case FaProcessStepEnum.FaLeader:
                        Step1.AddCssClass("on");
                        break;
                    case FaProcessStepEnum.Complete:
                        Step2.AddCssClass("on");
                        break;
                }
                Page.Title = FaProcessSteppService.GetStepText(stepEnum);
            }
            else
            {
                Page.Title = FaProcessSteppService.GetStepText(FaProcessStepEnum.Start);
                Step0.AddCssClass("on");
            }
        }

        private void BindProcessForm()
        {
            var formService = new ProcessFormService();
            var faProcessService = new FaProcessService();
            var faArchiveInfoJsonService = new FaArchiveInfoJsonService();
            if (!IsBeginTask)
            {
                ProcessForm processForm = formService.GetByInstanceId(IncidentId);
                FaArchiveTranfer entity = faProcessService.GetFaArchiveTranferByFormId(processForm.Id);
                ShenQingrenName.Text = entity.ShenQingRenName;
                ShenQingrenDeptName.Text = entity.ShenQingRenBumenName;
                ShenQingrenId.Value = entity.ShenQingRenId.ToString();
                ShenQingrenDeptId.Value = entity.ShenQingRenBumenId.ToString();
                ShenQingRiqi.Text = FormatHelper.GetIsoDateString(entity.ShenQingRiqi);
                JieyueYuanyin.InnerText = entity.JieyueYuanyin;
                List<FaProcess> faProcesses = faProcessService.GetFaProcesslstByTranferId(entity.Id);
                faArchiveInfoData.Value = faArchiveInfoJsonService.GetFileInfosJson(faProcesses);
            }
            else
            {
                ShenQingrenName.Text = EDoc2Helper.GetCurrentUserRealName();
                ShenQingrenId.Value = EDoc2Helper.GetCurrentUserId().ToString();
                ShenQingRiqi.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ShenQingrenDeptName.Text = EDoc2Helper.GetCurrentUserDeptName();
                ShenQingrenDeptId.Value = EDoc2Helper.GetCurrentUserDeptId().ToString();
            }
            List<long> archiveIds = faProcessService.GetAllJieyueArchiveIds();
            jieyueArchiveIds.Value = faArchiveInfoJsonService.GetGetAllJieyueArchiveIdsJson(archiveIds);
            FaArchiveDetailsControl.JieyueArchiveIds = jieyueArchiveIds.Value;
        }

        private void InitControls(FaProcessStepEnum value)
        {
            if (value != FaProcessStepEnum.Start)
            {
                JieyueYuanyin.Disabled = true;
                foreach (object control in FaArchiveDetailsControl.Controls)
                {
                    var textBox = control as TextBox;
                    if (textBox != null)
                    {
                        textBox.Enabled = false;
                    }
                }
            }
        }

        public FaArchiveTranfer GetFaArchiveTranfer()
        {
            var entity = new FaArchiveTranfer
            {
                ShenQingRenName = ShenQingrenName.Text,
                ShenQingRenBumenName = ShenQingrenDeptName.Text,
                ShenQingRiqi = DateTime.Parse(ShenQingRiqi.Text),
                ShenQingRenId = Convert.ToInt32(ShenQingrenId.Value),
                ShenQingRenBumenId = Convert.ToInt32(ShenQingrenDeptId.Value),
                JieyueYuanyin = JieyueYuanyin.InnerText,
                LiuchengZhuangtai = (int)LiuchengZhuangtaiEnum.Shenpizhong
            };
            return entity;
        }

        public IList<FaProcess> GetFaProcess()
        {
            string json = faArchiveInfoData.Value;
            return _faArchiveInfoJsonService.GetFaArchiveInfos(json);
        }

        public ProcessForm SaveEntity()
        {
            var faProcessService = new FaProcessService();
            ProcessForm processForm = ProcessFormService.GetByInstanceId(IncidentId);
            FaArchiveTranfer faArchiveTranferentitynewEntity = GetFaArchiveTranfer();
            IList<FaProcess> newFaprocess = GetFaProcess();
            FaArchiveTranfer faArchiveTranferentity = faProcessService.GetFaArchiveTranferByFormId(processForm.Id);
            faArchiveTranferentity.JieyueYuanyin = faArchiveTranferentitynewEntity.JieyueYuanyin;
            faArchiveTranferentity.FaProcesses.Clear();
            faArchiveTranferentity.FaProcesses.AddRange(newFaprocess);
            faProcessService.SaveFaArchiveTranfer(faArchiveTranferentity);
            return processForm;
        }

        public IList<Jieyue> GetJieyues(IList<FaProcess> faProcesses)
        {
            IList<Jieyue> jieyues = new List<Jieyue>();
            foreach (var faProcess in faProcesses)
            {
                jieyues.Add(Mapping(faProcess));
            }
            return jieyues;
        }

        private Jieyue Mapping(FaProcess faProcess)
        {
            Jieyue jieyue = new Jieyue();
            jieyue.TranferId = faProcess.TransferId;
            jieyue.ArchiveId = faProcess.ArchiveId;
            jieyue.JieyueTianshu = faProcess.JieyueTianshu;
            jieyue.JieyueShijian = DateTime.Now;
            jieyue.Jieyuezhuangtai = (int)JieyueZhuangtaiEnum.WeiJieyue;
            jieyue.Guihuanzhuangtai = (int)GuihuanZhuangtaiEnum.WeiGuihuan;
            return jieyue;
        }
    }
}