using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiostimeProcess.Models.UI;
using BiostimeProcess.Service.AppService;
using BiostimeProcess.Service.Domain;
using Newtonsoft.Json;

namespace BiostimeProcess.Pages
{
    public partial class Controller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }
            Response.ContentType = "application/json";
            if (action.Equals("GetPathAndCabinetNo", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetPathAndCabinetNo();
            }
            Response.End();
        }

        protected void GetPathAndCabinetNo()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string remark = Request["remark"];
                var faArchiveService = new FaArchiveService();
                FaArchive archive = faArchiveService.GetFaArchiveByRemark(remark);
                if (archive != null)
                {
                    resultModel.data = new { Id = archive.Id, CabinetNo = archive.CabinetNo, Path = archive.Path };
                }
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));

        }
    }
}