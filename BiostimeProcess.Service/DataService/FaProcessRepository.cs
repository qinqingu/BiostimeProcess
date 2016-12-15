using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService._RepositoryCore;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;

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
