using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiostimeProcess.Service.Domain
{
    public class TaskEntity
    {
        /// <summary>
        ///     任务状态
        /// </summary>
        public string Status { set; get; }

        /// <summary>
        ///     步骤名
        /// </summary>
        public string StepName { set; get; }

        /// <summary>
        ///     拥有者
        /// </summary>
        public string Owner { set; get; }

        /// <summary>
        ///     执行者
        /// </summary>
        public string Assigner { set; get; }

        /// <summary>
        ///     完成时间
        /// </summary>
        public string EndTime { set; get; }

        /// <summary>
        ///     审批意见
        /// </summary>
        public string Desc { set; get; }
    }
}
