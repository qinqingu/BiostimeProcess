using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;

namespace BiostimeProcess.Service.AppService
{
    public class FaProcessService
    {
        private readonly FaProcessRepository _faProcessRepository;
        private readonly FaArchiveTranferRepository _faArchiveTranferRepository;
        private readonly JieyueRepository _jieyueRepository;

        public FaProcessService()
        {
            _faProcessRepository = new FaProcessRepository();
            _faArchiveTranferRepository = new FaArchiveTranferRepository();
            _jieyueRepository =new JieyueRepository();
        }

        public FaArchiveTranfer GetFaArchiveTranferByFormId(long processFormId)
        {
            return _faArchiveTranferRepository.FindFaArchiveTranferByFormId(processFormId);
        }

        public void SaveFaArchiveTranfer(FaArchiveTranfer faArchiveTranfer)
        {
            _faArchiveTranferRepository.SaveFaArchiveTranfer(faArchiveTranfer);
        }

        public List<FaProcess> GetFaProcesslstByTranferId(long tranferId)
        {
            return _faProcessRepository.FindFaProcesslstByTranferId(tranferId);
        }

        public void UpdateLiuchengzhuangtai(FaArchiveTranfer faArchiveTranfer, LiuchengZhuangtaiEnum zhuangtai)
        {
            _faArchiveTranferRepository.ModifyLiuchengzhuangtaiByFileId(faArchiveTranfer, zhuangtai);
        }

        public IList<Jieyue> GetJieyuelstByTranferId(long tranferId)
        {
            return _jieyueRepository.FindJieyuelstByTranferId(tranferId);
        }

        public void SaveJieyueInfo(IList<Jieyue> entities)
        {
            _jieyueRepository.SaveJieyueInfo(entities);
        }

        public List<long> GetAllJieyueArchiveIds()
        {
            List<long> jieyueArchiveIds = new List<long>();
            jieyueArchiveIds.AddRange(_faArchiveTranferRepository.FindShenpiArchiveIds());
            jieyueArchiveIds.AddRange(_jieyueRepository.FindJieyueArchiveIds());
            return jieyueArchiveIds;
        }
    }
}
