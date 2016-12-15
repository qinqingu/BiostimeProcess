using BiostimeProcess.Service.DataService;
using BiostimeProcess.Service.Domain;

namespace BiostimeProcess.Service.AppService
{
    public class FaArchiveService
    {
        private readonly FaArchiveRepository _faArchiveRepository;

        public FaArchiveService()
        {
            _faArchiveRepository = new FaArchiveRepository();
        }

        public FaArchive GetFaArchiveByRemark(string remark)
        {
            return _faArchiveRepository.FindFaArchiveByRemark(remark);
        }
    }
}