using System;
using NLog;
using Quartz;
using TaskManager.ApiSdk.Sdk;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Node.SystemRuntime
{
    internal class TaskJob : IJob
    {
        private readonly Logger _logger;
        private ITmSdk _sdk;

        private string _jobId;

        public TaskJob()
        {
            this._logger = LogManager.GetLogger("TaskLogger");
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var taskId = context.JobDetail.Key.Name;
                var taskInfo = TaskPoolManager.Instance.GetByTaskId(taskId);

                if (taskInfo == null || taskInfo.ExeTask == null || taskInfo.TmSdk == null)
                {
                    this._logger.Error("任务引用为空 TaskId：{0}", taskId);
                    return;
                }

                taskInfo.Lock.Invoke(() =>
                {
                    this._sdk = taskInfo.TmSdk;

                    try
                    {
                        var startTaskResult = this._sdk.StartTask(taskInfo.TaskInfo.NodeId, taskInfo.TaskInfo.Id);
                        if (startTaskResult.HasError)
                        {
                            this._logger.Error("sdk start job error:{0}", startTaskResult.ErrorMessage);
                            return;
                        }

                        this._jobId = startTaskResult.Data;

                        this._logger.Info("start start, taskId:{0};jobId:{1}", taskId, this._jobId);

                        try
                        {
                            string resultMessage = null;
                            var taskResult = taskInfo.ExeTask.Run(out resultMessage);

                            this._logger.Info("task completed, taskId:{0};jobId:{1}", taskId, this._jobId);

                            var endTaskResult = this._sdk.CompleteTask(this._jobId, taskResult, resultMessage);
                            if (endTaskResult.HasError)
                            {
                                this._logger.Error("sdk complete job error:{0}", startTaskResult.ErrorMessage);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            this._sdk.CompleteTask(this._jobId, false, string.Format("执行任务异常:{0}", ex.Message));
                            this._logger.Error("task job error:{0}", ex.Message);
                        }
                        finally
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        this._logger.Error("task job error:{0}", ex.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                this._logger.Error("执行任务严重错误：{0}", ex.Message);
            }
        }
    }
}
