using System.IO;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Service.Interfaces.Ta;
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

        public TmProcessResult<bool> CreateTask(string name, string nodeId, string cron, string className, string methodName, string remark, 
            string taskFileName, Stream taskFileStream)
        {
            return base.ExeProcess(db =>
            {
                var creator = new TaskCreator(
                    base.ResloveProcessConfig<TaskCreator>(db),
                    name, nodeId, cron, className, methodName, remark, taskFileStream);

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
    }
}
