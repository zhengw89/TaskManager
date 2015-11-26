using TaskManager.DBEntity.TA;
using Task = TaskManager.LogicEntity.Entities.Ta.Task;

namespace TaskManager.Repository.Converter.Ta
{
    internal static class TaskConverter
    {
        public static T_TASK ToT(this Task l)
        {
            return ModelConvertorHelper.ConvertModel<T_TASK, Task>(l);
        }

        public static Task FromT(this T_TASK t)
        {
            return ModelConvertorHelper.ConvertModel<Task, T_TASK>(t);
        }
    }
}
