namespace TaskManager.DB
{
    public static class TaskManagerDbFactory
    {
        public static ITaskManagerDb CreateDb()
        {
            return new TaskManagerDb();
        }
    }
}
