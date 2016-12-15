using System;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService._RepositoryCore;
using BiostimeProcess.Service.Domain;

namespace BiostimeProcess.Service.DataService
{
    public class FaArchiveTranferRepository : AbstractRepository
    {
        public FaArchiveTranfer FindFaArchiveTranferByFormId(long processFormId)
        {
            return DataContext.FaArchiveTranfers.FirstOrDefault(t => t.ProcessFormId == processFormId);
        }

        public void SaveFaArchiveTranfer(FaArchiveTranfer entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    DataContext.FaArchiveTranfers.InsertOnSubmit(entity);
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
