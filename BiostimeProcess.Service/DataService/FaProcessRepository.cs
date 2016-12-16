using System.Collections.Generic;
using System.Linq;
using BiostimeProcess.Service.DataService._RepositoryCore;
using BiostimeProcess.Service.Domain;

namespace BiostimeProcess.Service.DataService
{
    public class FaProcessRepository : AbstractRepository
    {
        public List<FaProcess> FindFaProcesslstByTranferId(long tranferId)
        {
            return DataContext.FaProcesses.Where(t => t.TransferId == tranferId).ToList();
        }
    }
}