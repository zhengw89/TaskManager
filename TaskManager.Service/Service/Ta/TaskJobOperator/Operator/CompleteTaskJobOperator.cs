using System;
using TaskManager.DB;
using TaskManager.LogicEntity.Enums.Ta;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Ta.TaskJobOperator.Operator
{
    internal class CompleteTaskJobOperatorDependent : TmBaseDependentProvider
    {
        public CompleteTaskJobOperatorDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<ITaskJobRepository>();
        }
    }

    internal class CompleteTaskJobOperator : TmOperateProcess
    {
        private readonly string _jobId, _resutlMessage;
        private readonly bool _success;

        private readonly ITaskJobRepository _taskJobRepository;

        public CompleteTaskJobOperator(ITmProcessConfig config, string jobId, bool success, string result)
            : base(config)
        {
            this._jobId = jobId;
            this._resutlMessage = result;
            this._success = success;

            this._taskJobRepository = base.ResolveDependency<ITaskJobRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._jobId))
            {
                base.CacheProcessError("JobId不可为空");
                return false;
            }

            if (!this._taskJobRepository.Exists(this._jobId))
            {
                base.CacheProcessError("Job不存在");
                return false;
            }

            var status = this._taskJobRepository.GetJobStatusById(this._jobId);
            switch (status)
            {
                case TaskJobStatus.Fail:
                case TaskJobStatus.Success:
                case TaskJobStatus.Timeout:
                    base.DirectSuccessProcess();
                    return true;
                case TaskJobStatus.Executing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            var jobStatus = TaskJobStatus.Fail;
            if (this._success) jobStatus = TaskJobStatus.Success;

            if (!this._taskJobRepository.Update(this._jobId, jobStatus, DateTime.Now, this._resutlMessage, DateTime.Now))
            {
                base.CacheProcessError("记录完成失败");
                return false;
            }

            return true;
        }
    }
}
