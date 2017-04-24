namespace BiostimeProcess.Service.AppService.Jsons
{
    public class JsonArchiveInfo
    {
        /// <summary>
        ///     序号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        ///     档案Id
        /// </summary>
        public long ArchiveId { get; set; }

        /// <summary>
        ///     公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        ///     年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        ///     月份
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        ///     凭证字
        /// </summary>
        public string VoucherWord { get; set; }

        /// <summary>
        ///     凭证号
        /// </summary>
        public int? VoucherNumber { get; set; }

        /// <summary>
        ///     凭证券号
        /// </summary>
        public string VoucherNo { get; set; }

        /// <summary>
        ///     报告名称
        /// </summary>
        public string BaogaoMingcheng { get; set; }

        /// <summary>
        ///     合同号
        /// </summary>
        public string HetongHao { get; set; }

        /// <summary>
        ///     存储位置
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     存储柜号
        /// </summary>
        public string CabinetNo { get; set; }

        /// <summary>
        ///     借阅天数
        /// </summary>
        public int Day { get; set; }
    }
}