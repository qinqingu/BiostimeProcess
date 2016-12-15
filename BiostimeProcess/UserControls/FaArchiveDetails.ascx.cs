using System;
using System.Web.UI.WebControls;
using BiostimeProcess.Models.UI;
using BiostimeProcess.Service.Domain.Enum;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.UserControls
{
    public partial class FaArchiveDetails : MasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public FaProcessStepEnum Step
        {
            set
            {
                stepValue.Value = FormatHelper.GetIntString((int)value); ;
                if (value != FaProcessStepEnum.Start)
                {
                    foreach (object control in pnlFaArchiveManagementContainer.Controls)
                    {
                        var textBox = control as TextBox;
                        if (textBox != null)
                        {
                            textBox.Enabled = false;
                        }
                        var dropDownList = control as DropDownList;
                        if (dropDownList != null)
                        {
                            dropDownList.Enabled = false;
                        }
                    }
                }
            }
        }
    }
}