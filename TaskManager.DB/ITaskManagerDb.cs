using System;
using System.Collections.Generic;

namespace TaskManager.DB
{
    public interface ITaskManagerDb : IDisposable
    {
        #region Management

        TaskManagerDbType DbType { get; }

        bool Connected { get; }

        bool EnableAutoSelect { get; set; }

        #endregion

        #region Database Opreate

        void Connection(string connectionString, string providerName, TaskManagerDbType dbType);

        void CloseConnection();

        void BeginTransaction();

        void AbortTransaction();

        void CompleteTransaction();

        bool Exists<T>(object primaryKey);

        object Add(object entity);

        bool Adds<T>(IEnumerable<T> entitys);

        int Update(object entity);
        int Update<T>(Sql sql);
        int Update<T>(string sql, params object[] args);

        int Delete<T>(object entity);
        int Delete<T>(Sql sql);
        int Delete<T>(string sql, params object[] args);

        IEnumerable<T> Query<T>(string sql, params object[] args);
        IEnumerable<T> Query<T>(Sql sql);

        T SingleOrDefault<T>(object primaryKey);
        T SingleOrDefault<T>(string sql, params object[] args);
        T SingleOrDefault<T>(Sql sql);

        T ExecuteScalar<T>(Sql sql);

        int Execute(string sql, params object[] args);
        int Execute(Sql sql);

        OrmPagedList<T> QueryPage<T>(int pageIndex, int pageSize, Sql sql);
        OrmPagedList<T> QueryPage<T>(int pageIndex, int pageSize, string sql, params object[] args);

        IEnumerable<T> SkipTake<T>(long skip, long take, string sql, params object[] args);
        IEnumerable<T> SkipTake<T>(long skip, long take, Sql sql);

        IEnumerable<T> ExecProcedure<T>(string procName, params object[] args);

        #endregion
    }
}
