using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiostimeProcess.Service.AppService;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.UserControls
{
    public partial class FaPaymentDetails : System.Web.UI.UserControl
    {
        private readonly FaProcessService _faProcessService = new FaProcessService();
        public FaProcessStepEnum Step
        {
            set
            {
                stepValue.Value = FormatHelper.GetIntString((int)value); ;
                if (value != FaProcessStepEnum.Start)
                {
                    foreach (object control in pnlFaPaymentManagementContainer.Controls)
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

        public string JieyueIds
        {
            set { jieyueIds.Value = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            InitFaCompanyNames();
        }

        private void InitFaCompanyNames()
        {
            IList<string> list = _faProcessService.GetEnableCompanyNames();
            var jsSerializer = new JavaScriptSerializer();
            string json = jsSerializer.Serialize(list);
            CompanyNames.Value = json;
        }
    }
}