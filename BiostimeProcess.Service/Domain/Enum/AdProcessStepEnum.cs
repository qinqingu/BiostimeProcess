namespace BiostimeProcess.Service.Domain.Enum
{
    public enum FaProcessStepEnum
    {
        /// <summary>
        ///     流程发起
        /// </summary>
        Start = 0,

        /// <summary>
        ///     财务档案管理员
        /// </summary>
        FaLeader = 1,

        /// <summary>
        ///     已完成
        /// </summary>
        Complete = 2
    }

    public class FaProcessSteppService
    {
        public static string GetStepText(FaProcessStepEnum value)
        {
            string stepText = "";
            switch (value)
            {
                case FaProcessStepEnum.Start:
                    stepText = "流程发起";
                    break;
                case FaProcessStepEnum.FaLeader:
                    stepText = "财务档案管理员";
                    break;
                case FaProcessStepEnum.Complete:
                    stepText = "已完成";
                    break;
            }
            return stepText;
        }

        public static FaProcessStepEnum GetStepVal(string value)
        {
            switch (value)
            {
                case "开始": //流程发起
                    return FaProcessStepEnum.Start;
                case "档案管理员审批":
                    return FaProcessStepEnum.FaLeader;
                case "结束": //已完成
                    return FaProcessStepEnum.Complete;
                default:
                    return FaProcessStepEnum.Start;
            }
        }
    }
}