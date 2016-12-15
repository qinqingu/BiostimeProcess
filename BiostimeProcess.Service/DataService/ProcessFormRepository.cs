using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService._RepositoryCore;
using BiostimeProcess.Service.Domain;

namespace BiostimeProcess.Service.DataService
{
    public class ProcessFormRepository : AbstractRepository
    {
        public List<ProcessForm> FindAll()
        {
            return DataContext.ProcessForms.ToList();
        }

        public ProcessForm FindByInstanceId(string instanceId)
        {
            return DataContext.ProcessForms.FirstOrDefault(t => t.InstanceId == instanceId);
        }

        public List<ProcessForm> FindByProcessId(string processId)
        {
            return DataContext.ProcessForms.Where(t => t.ProcessId == processId).ToList();
        }

        public void Save(ProcessForm entity)
        {
            try
            {
                entity.LastUpdated = DateTime.Now;
                if (entity.Id == 0)
                {
                    entity.CreateTime = DateTime.Now;
                    DataContext.ProcessForms.InsertOnSubmit(entity);
                }
                DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                DataContext.Dispose();
                throw new Exception(ex.Message);
            }
        }
    }
}
