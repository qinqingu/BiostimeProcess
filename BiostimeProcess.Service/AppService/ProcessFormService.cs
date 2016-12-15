using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService;
using BiostimeProcess.Service.Domain;
using EDoc2.Organization;
using EDoc2.Website;

namespace BiostimeProcess.Service.AppService
{
    public class ProcessFormService
    {
        private readonly ProcessFormRepository _processFormRepository;

        public ProcessFormService()
        {
            _processFormRepository = new ProcessFormRepository();
        }

        public List<ProcessForm> GetAll()
        {
            return _processFormRepository.FindAll();
        }

        public ProcessForm GetByInstanceId(string instanceId)
        {
            return _processFormRepository.FindByInstanceId(instanceId);
        }

        public List<ProcessForm> GetByProcessId(string processId)
        {
            return _processFormRepository.FindByProcessId(processId);
        }

        public void Save(ProcessForm entity)
        {
            _processFormRepository.Save(entity);
        }

        public ProcessForm GetNewProcessForm()
        {
            return GetNewProcessForm(string.Empty, string.Empty);
        }

        public ProcessForm GetNewProcessForm(string processId, string incidentId)
        {
            EDoc2UserInfo userInfo;
            ApiManager.Api.OrgnizationManagement.GetCurrentUser(ApiManager.CurrentUserToken, out userInfo);
            string departmentName = "";
            if (!string.IsNullOrEmpty(userInfo.DepartmentName))
            {
                departmentName = userInfo.DepartmentName;
            }
            return new ProcessForm
            {
                InitiatorName = userInfo.UserRealName,
                InitiatorDept = departmentName,
                InstanceId = incidentId,
                StartDate = DateTime.Now,
                ProcessId = processId,
                InitiatorId = userInfo.UserId
            };
        }
    }
}
