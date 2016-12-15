using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiostimeProcess.Service.DataService._RepositoryCore;
using BiostimeProcess.Service.Domain;
using BiostimeProcess.Service.Domain.Enum;

namespace BiostimeProcess.Service.DataService
{
    public class JieyueRepository : AbstractRepository
    {
        public IList<Jieyue> FindJieyuelstByTranferId(long tranferId)
        {
            return DataContext.Jieyues.Where(t => t.TranferId == tranferId).ToList();
        }

        public Jieyue FindJieyueById(long id)
        {
            return DataContext.Jieyues.FirstOrDefault(t => t.Id == id);
        }

        public void ModifyLiuchengzhuangtaiByFileId(IList<Jieyue> jieyues, LiuchengZhuangtaiEnum zhuangtai)
        {
            foreach (var entity in jieyues)
            {
                Jieyue jieyue = FindJieyueById(entity.Id);
                jieyue.Liuchengzhuangtai = (int)zhuangtai;
                if (zhuangtai == LiuchengZhuangtaiEnum.YiShenpi)
                {
                    jieyue.JieyueShijian = DateTime.Now;
                }
                DataContext.SubmitChanges();
            }
           
        }

        public IList<long> FindAllJieyueArchiveIds()
        {
            IList<Jieyue> jieyues = DataContext.Jieyues.Where(
                item =>
                item.Liuchengzhuangtai != (int) LiuchengZhuangtaiEnum.YiChexiao ||
                item.Guihuanzhuangtai == (int) GuihuanZhuangtaiEnum.YiGuihuan).ToList();
            return jieyues.Select(item => item.ArchiveId).ToList(); ;
        }

        public void SaveLendInfo(Jieyue entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    DataContext.Jieyues.InsertOnSubmit(entity);
                }
                DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                DataContext.Dispose();
                throw new Exception(ex.Message);
            }
        }
    }
}
