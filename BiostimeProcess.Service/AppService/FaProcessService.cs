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

        public List<FaProcess> GetFaProcesslstByTranferId(long tranferId)
        {
            return _faProcessRepository.FindFaProcesslstByTranferId(tranferId);
        }

        public IList<Jieyue> GetJieyuelstByTranferId(long tranferId)
        {
            return _jieyueRepository.FindJieyuelstByTranferId(tranferId);
        }
 
        public void UpdateLiuchengzhuangtai(IList<Jieyue> jieyues,LiuchengZhuangtaiEnum zhuangtai)
        {
            _jieyueRepository.ModifyLiuchengzhuangtaiByFileId(jieyues, zhuangtai);
        }

        public void SaveFaArchiveTranfer(FaArchiveTranfer entity)
        {
            _faArchiveTranferRepository.SaveFaArchiveTranfer(entity);
        }

        public IList<long> GetAllJieyueArchiveIds()
        {
            return _jieyueRepository.FindAllJieyueArchiveIds();
        }

        //public void SaveFaProcess(List<FaProcess> faprocesses)
        //{
        //    _faProcessRepository.SaveFaProcess(faprocesses);
        //}

        public void SaveLendInfo(List<FaProcess> faProcesses )
        {
            foreach (var faProcess in faProcesses)
            {
                string remark = faProcess.Remark ?? string.Empty;
                var lendInfo = new Jieyue
                {
                    ArchiveId = faProcess.ArchiveId,
                    TranferId = faProcess.Id,
                    JieyueTianshu = faProcess.JieyueTianshu,
                    Liuchengzhuangtai = (int)LiuchengZhuangtaiEnum.Shenpizhong,
                    Jieyuezhuangtai = null,
                    Guihuanzhuangtai = null,
                    Remark = remark
                };
                _jieyueRepository.SaveLendInfo(lendInfo);
            }
        }
    }
}
