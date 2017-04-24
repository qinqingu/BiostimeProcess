using System;
using BiostimeProcess.Service.Domain.Enum;

namespace BiostimeProcess.Pages
{
    public partial class Complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.Step = FaProcessStepEnum.Complete;
            }
        }
    }
}