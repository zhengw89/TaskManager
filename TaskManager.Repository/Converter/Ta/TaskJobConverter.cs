using TaskManager.DBEntity.TA;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Repository.Converter.Ta
{
    internal static class TaskJobConverter
    {
        public static T_TASK_JOB ToT(this TaskJob l)
        {
            return ModelConvertorHelper.ConvertModel<T_TASK_JOB, TaskJob>(l);
        }

        public static TaskJob FromT(this T_TASK_JOB t)
        {
            return ModelConvertorHelper.ConvertModel<TaskJob, T_TASK_JOB>(t);
        }
    }
}
