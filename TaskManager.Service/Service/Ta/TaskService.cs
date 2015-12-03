using System.Collections.Generic;
using System.IO;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Service.Interfaces.Ta;
using TaskManager.Service.Service.Ta.TaskJobOperator.Operator;
using TaskManager.Service.Service.Ta.TaskOperator.Creator;
using TaskManager.Service.Service.Ta.TaskOperator.Queryer;

namespace TaskManager.Service.Service.Ta
{
    internal class TaskService : BaseService, ITaskService
    {
        public TaskService(ServiceConfig config)
            : base(config)
        {
        }

        public TmProcessResult<bool> CreateTask(string name, string nodeId, string cron, string dllName, string className, string remark,
            string taskFileName, Stream taskFileStream)
        {
            return base.ExeProcess(db =>
            {
                var creator = new TaskCreator(
                    base.ResloveProcessConfig<TaskCreator>(db),
                    name, nodeId, cron, dllName, className, remark, taskFileStream);

                return base.ExeOperateProcess(creator);
            });
        }

        public TmProcessResult<PagedList<Task>> GetByCondition(int pageIndex, int pageSize)
        {
            return base.ExeProcess(db =>
            {
                var queryer = new TaskByConditionQueryer(
                    base.ResloveProcessConfig<TaskByConditionQueryer>(db),
                    pageIndex, pageSize);

                return base.ExeQueryProcess(queryer);
            });
        }

        public TmProcessResult<List<Task>> GetByNode(string nodeId)
        {
            return base.ExeProcess(db =>
            {
                var queryer = new TaskByNodeQueryer(
                    base.ResloveProcessConfig<TaskByNodeQueryer>(db),
                    nodeId);

                return base.ExeQueryProcess(queryer);
            });
        }

        public TmProcessResult<string> StartTaskJob(string nodeId, string taskId)
        {
            return base.ExeProcess(db =>
            {
                var @operator = new StartTaskJobOperator(
                    base.ResloveProcessConfig<StartTaskJobOperator>(db),
                    nodeId, taskId);

                return base.ExeOperateProcess(@operator);
            });
        }

        public TmProcessResult<bool> CompleteTaskJob(string jobId, bool success, string result)
        {
            return base.ExeProcess(db =>
            {
                var @operator = new CompleteTaskJobOperator(
                    base.ResloveProcessConfig<CompleteTaskJobOperator>(db),
                    jobId, success, result);

                return base.ExeOperateProcess(@operator);
            });
        }

        public TmProcessResult<Stream> GetTaskFile(string taskId)
        {
            return base.ExeProcess(db =>
            {
                var queryer = new TaskFileQueryer(
                    base.ResloveProcessConfig<TaskFileQueryer>(db),
                    taskId);

                return base.ExeQueryProcess(queryer);
            });
        }
    }
}
