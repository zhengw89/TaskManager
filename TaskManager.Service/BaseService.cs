using System;
using CommonProcess.DependentProvider;
using TaskManager.DB;
using TaskManager.LogicEntity;
using TaskManager.Service.Core;
using TaskManager.Service.OperateDependentFactory;

namespace TaskManager.Service
{
    internal abstract class BaseService
    {
        private readonly ServiceConfig _config;
        protected ServiceConfig Config
        {
            get
            {
                return this._config;
            }
        }

        protected BaseService(ServiceConfig config)
        {
            if (config == null) throw new ArgumentNullException();
            this._config = config;
        }

        #region Protected Method

        protected T ExeProcess<T>(Func<ITaskManagerDb, T> func)
        {
            using (var db = this.CreateDb())
            {
                return func.Invoke(db);
            }
        }

        protected ITmProcessConfig ResloveProcessConfig<T>(ITaskManagerDb db)
        {
            return new TmProcessConfig(db)
            {
                DependentProvider = this.ResloveOperateDependent<T>(db),
            };
        }

        protected TmProcessResult<T> ExeQueryProcess<T>(TmQueryProcess<T> queryer)
        {
            return new TmProcessResult<T>()
            {
                Data = queryer.ExecuteQueryProcess(),
                Error = queryer.GetError().ToTmProcessError()
            };
        }

        protected TmProcessResult<bool> ExeOperateProcess(TmOperateProcess operate)
        {
            return new TmProcessResult<bool>()
            {
                Data = operate.ExecuteProcess(),
                Error = operate.GetError().ToTmProcessError()
            };
        }

        protected TmProcessResult<T> ExeOperateProcess<T>(TmOperateProcessWithResult<T> operate)
        {
            return new TmProcessResult<T>()
            {
                Data = operate.ExecuteProcess(),
                Error = operate.GetError().ToTmProcessError()
            };
        }

        #endregion

        #region Private Method

        private IDependentProvider ResloveOperateDependent<T>(ITaskManagerDb db)
        {
            return OperateDependentLocator.Container.Resolve<T>(db);
        }

        private ITaskManagerDb CreateDb()
        {
            var db = TaskManagerDbFactory.CreateDb();
            db.Connection(this._config.ConnectionString, this._config.ConnetionProvider, this._config.DbType);

            return db;
        }

        #endregion

    }
}
