using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService._RepositoryCore;
using BiostimeProcess.Service.Domain;

namespace BiostimeProcess.Service.DataService
{
    public class FaArchiveRepository : AbstractRepository
    {
        public FaArchive FindFaArchiveByRemark(string remark)
        {
            return DataContext.FaArchives.FirstOrDefault(t => t.Remark == remark);
        }
    }
}
