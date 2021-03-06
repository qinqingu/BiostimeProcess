﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.AppService.Jsons;
using BiostimeProcess.Service.Domain;

namespace BiostimeProcess.Service.AppService
{
    public class FaReportInfoJsonService : AbstractJsonService
    {
        public IList<FaProcess> GetFaArchiveInfos(string json)
        {
            var jsonArchiveInfos = Deserialize<IList<JsonArchiveInfo>>(json);
            IList<FaProcess> archiveInfos = jsonArchiveInfos.Select(jsonArchiveInfo => new FaProcess
            {
                ArchiveId = jsonArchiveInfo.ArchiveId,
                Xuhao = jsonArchiveInfo.No,
                JieyueTianshu = jsonArchiveInfo.Day,
                CreateTime = DateTime.Now,
                ModifiedTime = DateTime.Now
            }).ToList();
            return archiveInfos;
        }

        public string GetArchiveInfosJson(List<FaProcess> archiveInfos)
        {
            List<JsonArchiveInfo> jsonArchiveInfos = GetJsonArchiveInfos(archiveInfos);
            return Serialize(jsonArchiveInfos);
        }

        public List<JsonArchiveInfo> GetJsonArchiveInfos(List<FaProcess> archiveInfos)
        {
            var jsonArchiveInfos = new List<JsonArchiveInfo>();
            for (int archiveInfoIndex = 0; archiveInfoIndex < archiveInfos.Count; archiveInfoIndex++)
            {
                FaProcess archiveInfo = archiveInfos[archiveInfoIndex];
                jsonArchiveInfos.Add(new JsonArchiveInfo
                {
                    No = archiveInfo.Xuhao,
                    ArchiveId = archiveInfo.ArchiveId,
                    Company = archiveInfo.FaArchive.Company,
                    Year = archiveInfo.FaArchive.Year,
                    BaogaoMingcheng = archiveInfo.FaArchive.BaogaoMingcheng,
                    Path = archiveInfo.FaArchive.Path,
                    CabinetNo = archiveInfo.FaArchive.CabinetNo,
                    Day = archiveInfo.JieyueTianshu
                });
            }
            return jsonArchiveInfos;
        }

        public string GetGetAllJieyueArchiveIdsJson(List<long> jieyueArchiveIds)
        {
            return Serialize(jieyueArchiveIds);
        }
    }
}
