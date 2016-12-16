using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService._RepositoryCore;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;

namespace BiostimeProcess.Service.DataService
{
    public class JieyueRepository : AbstractRepository
    {
        public IList<long> FindJieyueArchiveIds()
        {
            return DataContext.Jieyues.Select(t => t.ArchiveId).ToList();
        }

        public IList<Jieyue> FindJieyuelstByTranferId(long tranferId)
        {
            return DataContext.Jieyues.Where(t => t.TranferId == tranferId).ToList();
        }

        public Jieyue FindJieyueById(long id)
        {
            return DataContext.Jieyues.FirstOrDefault(t => t.Id == id);
        }

        public void SaveJieyueInfo(IList<Jieyue> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    if (entity.Id == 0)
                    {
                        DataContext.Jieyues.InsertOnSubmit(entity);
                    }
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
