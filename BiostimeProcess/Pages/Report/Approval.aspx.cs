﻿using System;
using System.Collections.Generic;
using BiostimeProcess.Models.UI;
using BiostimeProcess.Service.AppService;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.Pages.Baogao
{
    public partial class Approval : PageBase
    {
        private readonly FaProcessService _faProcessService = new FaProcessService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReturnPrevStepButton.Visible = false;
                Master.Step = FaProcessStepEnum.FaLeader;
            }
        }
        protected void SubmitProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                string desc = descText.Value;
                Submit(desc);
            }
            catch (Exception ex)
            {
                Edoc2LogHelper.WriteProcessSubmitExceptionLog(ex);
                Dialog("出现异常,请联系管理员。");
            }
        }

        protected void ReturnPrevStepButton_Click(object sender, EventArgs e)
        {
            try
            {
                string desc = descText.Value;
                ReturnPrevStep(desc);
            }
            catch (Exception ex)
            {
                Edoc2LogHelper.WriteProcessSubmitExceptionLog(ex);
                Dialog("出现异常,请联系管理员。");
            }
        }

        protected void ReturnStartorButton_Click(object sender, EventArgs e)
        {
            try
            {
                string desc = descText.Value;
                ReturnStartor(desc);
            }
            catch (Exception ex)
            {
                Edoc2LogHelper.WriteProcessSubmitExceptionLog(ex);
                Dialog("出现异常,请联系管理员。");
            }
        }
    }
}