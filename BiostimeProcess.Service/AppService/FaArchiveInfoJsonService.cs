using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.AppService.Jsons;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Utitity;

namespace BiostimeProcess.Service.AppService
{
    public class FaArchiveInfoJsonService : AbstractJsonService
    {
        public IList<FaProcess> GetFaArchiveInfos(string json)
        {
            var jsonArchiveInfos = Deserialize<IList<JsonArchiveInfo>>(json);
            IList<FaProcess> archiveInfos = jsonArchiveInfos.Select(jsonArchiveInfo => new FaProcess
                {
                    ArchiveId = jsonArchiveInfo.ArchiveId,
                    Xuhao = jsonArchiveInfo.No,
                    Company = jsonArchiveInfo.Company,
                    Year = jsonArchiveInfo.Year,
                    Month = jsonArchiveInfo.Month,
                    VoucherWord = jsonArchiveInfo.VoucherWord,
                    VoucherNumber = jsonArchiveInfo.VoucherNumber,
                    VoucherNo = jsonArchiveInfo.VoucherNo,
                    Path = jsonArchiveInfo.Path,
                    CabinetNo = jsonArchiveInfo.CabinetNo,
                    JieyueTianshu = jsonArchiveInfo.Day,
                    CreateTime = DateTime.Now,
                    ModifiedTime = DateTime.Now
                }).ToList();
            return archiveInfos;
        }

        public string GetFileInfosJson(List<FaProcess> archiveInfos)
        {
            List<JsonArchiveInfo> jsonArchiveInfos = GetJsonFileInfos(archiveInfos);
            return Serialize(jsonArchiveInfos);
        }

        public List<JsonArchiveInfo> GetJsonFileInfos(List<FaProcess> archiveInfos)
        {
            var jsonArchiveInfos = new List<JsonArchiveInfo>();
            for (int archiveInfoIndex = 0; archiveInfoIndex < archiveInfos.Count; archiveInfoIndex++)
            {
                FaProcess archiveInfo = archiveInfos[archiveInfoIndex];
                jsonArchiveInfos.Add(new JsonArchiveInfo
                {
                    No = archiveInfo.Xuhao,
                    ArchiveId = archiveInfo.ArchiveId,
                    Year = archiveInfo.Year,
                    Month = archiveInfo.Month,
                    Company = archiveInfo.Company,
                    VoucherWord = archiveInfo.VoucherWord,
                    VoucherNumber = archiveInfo.VoucherNumber,
                    VoucherNo = archiveInfo.VoucherNo,
                    Path = archiveInfo.Path,
                    CabinetNo = archiveInfo.CabinetNo,
                    Day = archiveInfo.JieyueTianshu
                });
            }
            return jsonArchiveInfos;
        }

        public string GetGetAllJieyueArchiveIdsJson(IList<long> jieyueArchiveIds)
        {
            return Serialize(jieyueArchiveIds);
        }
    }
}
