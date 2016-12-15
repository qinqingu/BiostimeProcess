using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.Service.DataService._RepositoryCore
{
    public static class DataContextFactory
    {
        private const string DataContextKey = "DataContext";

        public static BiostimeDataCaptureDataContext CreateDataContext()
        {
            string connectionString = WebConfig.BiostimeDataCaptureConnection;

            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                if (httpContext.Items[DataContextKey] == null)
                {
                    httpContext.Items[DataContextKey] = new BiostimeDataCaptureDataContext(connectionString);
                }

                return httpContext.Items[DataContextKey] as BiostimeDataCaptureDataContext;
            }

            LocalDataStoreSlot localDataStoreSlot = Thread.GetNamedDataSlot(DataContextKey);

            object dataContext = Thread.GetData(localDataStoreSlot);

            if (dataContext == null)
            {
                dataContext = new BiostimeDataCaptureDataContext(connectionString);

                Thread.SetData(localDataStoreSlot, dataContext);
            }

            return dataContext as BiostimeDataCaptureDataContext;
        }
    }
}
