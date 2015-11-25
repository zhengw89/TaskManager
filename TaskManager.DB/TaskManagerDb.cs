using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TaskManager.DB
{
    internal sealed class TaskManagerDb : ITaskManagerDb
    {
        #region Management

        private TaskManagerDbType _dbType = TaskManagerDbType.Unknow;
        public TaskManagerDbType DbType
        {
            get
            {
                return this._dbType;
            }
        }

        private bool _isConnected = false;
        public string ConnectionString;
        public string ConnectionProvider;

        public bool Connected { get { return this._isConnected; } }

        private Database _db;

        /// <summary>
        /// 设置自动生成列--7.8段加
        /// </summary>
        public bool EnableAutoSelect
        {
            get
            {
                return _db.EnableAutoSelect;
            }
            set
            {
                _db.EnableAutoSelect = value;
            }
        }

        //释放连接
        public void Dispose()
        {
            if (_db != null)
            {
                _db.CloseSharedConnection();
                _db = null;
            }
        }

        #endregion

        #region Database Opreate

        /// <summary>
        /// 连接MySql数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        /// <param name="dbType"></param>
        public void Connection(string connectionString, string providerName, TaskManagerDbType dbType)
        {
            if (_isConnected)
            {
                throw new Exception("已经连接完成，不可再次连接");
            }
            _db = new Database(connectionString, providerName);
            _db.OpenSharedConnection();
            this._dbType = dbType;
            _isConnected = true;
            ConnectionString = connectionString;
            ConnectionProvider = providerName;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            Dispose();
            _isConnected = false;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            _db.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        /// <summary>
        /// 终止事务
        /// </summary>
        public void AbortTransaction()
        {
            _db.AbortTransaction();
        }

        /// <summary>
        /// 结束事务
        /// </summary>
        public void CompleteTransaction()
        {
            _db.CompleteTransaction();
        }

        /// <summary>
        /// 判断是否存在相应的主键ID
        /// </summary>
        /// <typeparam name="T">DB实体类</typeparam>
        /// <param name="primaryKey">主键ID</param>
        /// <returns>是否存在相应主键ID</returns>
        public bool Exists<T>(object primaryKey) { return _db.Exists<T>(primaryKey); }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>对应的实体对象</returns>
        public object Add(object entity) { return _db.Insert(entity); }

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="entitys">实体对象集合</param>
        /// <returns>操作结果</returns>
        public bool Adds<T>(IEnumerable<T> entitys) { return _db.InsertMutilRecord(entitys); }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(object entity) { return _db.Update(entity); }
        public int Update<T>(Sql sql) { return _db.Update<T>(sql); }
        public int Update<T>(string sql, params object[] args) { return _db.Update<T>(sql, args); }

        public int Delete<T>(object entity) { return _db.Delete<T>(entity); }
        public int Delete<T>(Sql sql) { return _db.Delete<T>(sql); }
        public int Delete<T>(string sql, params object[] args) { return _db.Delete<T>(sql, args); }

        public IEnumerable<T> Query<T>(string sql, params object[] args) { return _db.Query<T>(sql, args); }
        public IEnumerable<T> Query<T>(Sql sql) { return _db.Query<T>(sql); }

        public T SingleOrDefault<T>(object primaryKey) { return _db.SingleOrDefault<T>(primaryKey); }
        public T SingleOrDefault<T>(string sql, params object[] args) { return _db.SingleOrDefault<T>(sql, args); }
        public T SingleOrDefault<T>(Sql sql) { return _db.SingleOrDefault<T>(sql); }

        public T ExecuteScalar<T>(Sql sql) { return _db.ExecuteScalar<T>(sql); }

        public int Execute(string sql, params object[] args) { return _db.Execute(sql, args); }
        public int Execute(Sql sql) { return _db.Execute(sql); }

        public OrmPagedList<T> QueryPage<T>(int pageIndex, int pageSize, Sql sql)
        {
            var query = _db.Page<T>(pageIndex, pageSize, sql);
            return new OrmPagedList<T>(query.Items, pageIndex, pageSize, (int)query.TotalItems);
        }

        public OrmPagedList<T> QueryPage<T>(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var query = _db.Page<T>(pageIndex, pageSize, sql, args);
            return new OrmPagedList<T>(query.Items, pageIndex, pageSize, (int)query.TotalItems);
        }

        public IEnumerable<T> SkipTake<T>(long skip, long take, string sql, params object[] args)
        {
            return _db.SkipTake<T>(skip, take, sql, args);
        }

        public IEnumerable<T> SkipTake<T>(long skip, long take, Sql sql)
        {
            return _db.SkipTake<T>(skip, take, sql);
        }

        public IEnumerable<T> ExecProcedure<T>(string procName, params object[] args)
        {
            _db.EnableAutoSelect = false;

            StringBuilder sb = new StringBuilder(args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                sb.AppendFormat(" @{0},", i);
            }

            var result = _db.Query<T>(String.Format("EXEC {0} {1}"
                , procName, sb.ToString().TrimEnd(new char[] { ',' })), args).ToList();
            _db.EnableAutoSelect = true;
            return result;
        }

        #endregion
    }
}
