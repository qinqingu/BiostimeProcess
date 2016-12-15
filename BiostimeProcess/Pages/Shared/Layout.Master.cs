using System;
using BiostimeProcess.Models.UI;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.Pages.shared
{
    public partial class Layout : MasterPageBase
    {
        public string TitleText
        {
            set { ProcessTitle.Text = value; }
        }

        public string SubTitleText
        {
            set { ProcessSubTitle.Text = value; }
        }

        public string ProcessNameText
        {
            set { ProcessName.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentUserRealName.Text = WebConfig.IsDebug ? "Test User" : EDoc2Helper.GetCurrentUserRealName();
            }
        }
    }
}