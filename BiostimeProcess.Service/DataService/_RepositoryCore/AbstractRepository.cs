using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.Domain;

namespace BiostimeProcess.Service.DataService._RepositoryCore
{
    public abstract class AbstractRepository
    {
        protected AbstractRepository()
        {
            DataContext = DataContextFactory.CreateDataContext();
        }

        public BiostimeDataCaptureDataContext DataContext { get; set; }
    }
}
