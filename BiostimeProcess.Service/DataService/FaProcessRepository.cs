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

        public IList<FaCompany> FindEnableCompany()
        {
            return DataContext.FaCompanies.ToList();
        }
        
        public IList<string> FindEnableCompanyName()
        {
            return DataContext.FaCompanies.Select(t =>t.Name).ToList();
        }

        public IList<string> FindReportName()
        {
            return DataContext.FaReportNames.Select(item=>item.Name).ToList();
        }
    }
}