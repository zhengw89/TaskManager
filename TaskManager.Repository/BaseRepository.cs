using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManager.DB;
using TaskManager.DBEntity;
using TaskManager.LogicEntity.Entities;

namespace TaskManager.Repository
{
    internal abstract class BaseRepository<TL, TD>
        where TL : BaseEntity
        where TD : DbBaseEntity
    {
        private readonly string _tableName;
        public string TableName
        {
            get
            {
                return this._tableName;
            }
        }
        //对常用字段封装
        protected const string IsActive = "IsActive", CreateTime = "CreateTime", UpdateTime = "UpdateTime";
        //数据库
        protected ITaskManagerDb Db;

        private BaseQuery<TD> _baseQuery;
        protected BaseQuery<TD> BaseQuery
        {
            get
            {
                if (_baseQuery == null)
                {
                    _baseQuery = new BaseQuery<TD>(Db, this._tableName);
                }
                _baseQuery.Reset();
                return _baseQuery;
            }
        }

        protected BaseQuery<TE> GetNewBaseQuery<TE>()
            where TE : DbBaseEntity
        {
            var tnAttr = typeof(TE).GetCustomAttributes(typeof(TableNameAttribute), true);
            if (tnAttr.Length == 0)
            {
                throw new CustomAttributeFormatException("Miss TableNameAttribute ,must specify table name!!!");
            }
            return new BaseQuery<TE>(this.Db, (tnAttr[0] as TableNameAttribute).Value);
        }

        protected Sql NewSql
        {
            get
            {
                return new Sql();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db">上下文对象</param>
        protected BaseRepository(ITaskManagerDb db)
        {
            Db = db;
            var tnAttr = typeof(TD).GetCustomAttributes(typeof(TableNameAttribute), true);
            if (tnAttr.Length == 0)
            {
                throw new CustomAttributeFormatException("Miss TableNameAttribute ,must specify table name!!!");
            }
            else
            {
                this._tableName = (tnAttr[0] as TableNameAttribute).Value;
            }
        }

        #region Absctract

        protected abstract TL FromT(TD t);

        protected abstract TD ToT(TL l);

        protected List<TL> ConvertToList(List<TD> source)
        {
            if (source == null) return null;
            return source.Select(FromT).ToList();
        }

        protected PagedList<TL> ConvertToPagedList(PagedList<TD> source)
        {
            if (source == null) return null;
            var result = new PagedList<TL>()
            {
                CurrentPageIndex = source.CurrentPageIndex,
                PageSize = source.PageSize,
                TotalItemCount = source.TotalItemCount,
                TotalPageCount = source.TotalPageCount,
                StartRecordIndex = source.StartRecordIndex,
                EndRecordIndex = source.EndRecordIndex
            };

            result.AddRange(source.Select(FromT));
            return result;
        }

        #endregion

        #region Database operate

        #region Exists

        /// <summary>
        /// 判断是否存在相应的主键ID
        /// </summary>
        /// <param name="primaryKey">主键ID</param>
        /// <returns>是否存在相应主键ID</returns>
        protected bool Exists(object primaryKey)
        {
            return Db.Exists<TD>(primaryKey);
        }

        /// <summary>
        /// 判断是否存在相应的主键ID
        /// </summary>
        /// <typeparam name="TE">表类型</typeparam>
        /// <param name="primaryKey">主键ID</param>
        /// <returns></returns>
        protected bool Exists<TE>(object primaryKey)
        {
            return Db.Exists<TE>(primaryKey);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// 获取所有数据，谨慎使用，确定表内数据全部都是有效的！！！！
        /// </summary>
        /// <returns></returns>
        protected List<TD> GetAll()
        {
            return this.GetAll<TD>();
            //return Query(new Sql());
        }

        /// <summary>
        /// 获取所有数据，谨慎使用，确定表内数据全部都是有效的！！！！
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <returns></returns>
        protected List<TE> GetAll<TE>()
        {
            return this.Query<TE>(new Sql());
        }

        #endregion

        #region Add

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="entity">对象</param>
        /// <returns>是否添加成功</returns>
        protected bool Add(TD entity)
        {
            return this.Add<TD>(entity);
            //return (bool)Db.Add(entity);
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="entity">对象</param>
        /// <returns>是否添加成功</returns>
        protected bool Add<TE>(TE entity)
        {
            return (bool)this.Db.Add(entity);
        }

        /// <summary>
        /// 一次添加多个操作
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="entitys">对象结婚</param>
        /// <returns>是否添加成功</returns>
        protected bool Adds(IEnumerable<TD> entitys)
        {
            return this.Adds<TD>(entitys);
            //return Db.Adds(entitys);
        }

        /// <summary>
        /// 一次添加多个操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="entitys">对象结婚</param>
        /// <returns>是否添加成功</returns>
        protected bool Adds<TE>(IEnumerable<TE> entitys)
        {
            return this.Db.Adds(entitys);
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        protected bool Update(TD entity)
        {
            return this.Update<TD>(entity);
            //return Db.Update(entity) == 1;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        protected bool Update<TE>(TE entity)
        {
            return this.Db.Update(entity) == 1;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="sql">Sql对象</param>
        /// <returns>受影响行数</returns>
        protected bool Update(Sql sql)
        {
            return this.Update<TD>(sql);
            //return Db.Update<T>(sql) > -1;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">Sql对象</param>
        /// <returns>受影响行数</returns>
        protected bool Update<TE>(Sql sql)
        {
            return this.Db.Update<TE>(sql) > -1;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响行数</returns>
        protected int Update(string sql, params object[] args)
        {
            return this.Update<TD>(sql, args);
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响行数</returns>
        protected int Update<TE>(string sql, params object[] args)
        {
            return this.Db.Update<TE>(sql, args);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        protected bool Delete(TD entity)
        {
            return this.Delete<TD>(entity);
            //return Db.Delete<T>(entity) == 1;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        protected bool Delete<TE>(TE entity)
        {
            return this.Db.Delete<TE>(entity) == 1;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sql">Sql对象</param>
        /// <returns>受影响行数</returns>
        protected int Delete(Sql sql)
        {
            return this.Delete<TD>(sql);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">Sql对象</param>
        /// <returns>受影响行数</returns>
        protected int Delete<TE>(Sql sql)
        {
            return this.Db.Delete<TE>(sql);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响行数</returns>
        protected int Delete(string sql, params object[] args)
        {
            return this.Delete<TD>(sql, args);
            //return Db.Delete<T>(sql, args);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响行数</returns>
        protected int Delete<TE>(string sql, params object[] args)
        {
            return this.Db.Delete<TE>(sql, args);
        }

        #endregion

        #region Query

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>结果集</returns>
        protected List<TD> Query(string sql, params object[] args)
        {
            return this.Query<TD>(sql, args);
            //return Db.Query<T>(sql, args).ToList();
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>结果集</returns>
        protected List<TE> Query<TE>(string sql, params object[] args)
        {
            return this.Db.Query<TE>(sql, args).ToList();
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sql">Sql对象</param>
        /// <returns>结果集</returns>
        protected List<TD> Query(Sql sql)
        {
            return this.Query<TD>(sql);
            //return Db.Query<T>(sql).ToList();
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">Sql对象</param>
        /// <returns>结果集</returns>
        protected List<TE> Query<TE>(Sql sql)
        {
            return this.Db.Query<TE>(sql).ToList();
        }
        #endregion

        #region SingleOrDefault

        /// <summary>
        /// 返回满足指定条件的唯一元素
        /// </summary>
        /// <param name="primaryKey">主键ID</param>
        /// <returns>单个实体对象</returns>
        protected TD SingleOrDefault(object primaryKey)
        {
            return this.SingleOrDefault<TD>(primaryKey);
            //return Db.SingleOrDefault<T>(primaryKey);
        }

        /// <summary>
        /// 返回满足指定条件的唯一元素
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="primaryKey">主键ID</param>
        /// <returns>单个实体对象</returns>
        protected TE SingleOrDefault<TE>(object primaryKey)
        {
            return this.Db.SingleOrDefault<TE>(primaryKey);
        }

        /// <summary>
        /// 返回满足指定条件的唯一元素
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>单个实体对象</returns>
        protected TD SingleOrDefault(string sql, params object[] args)
        {
            return this.SingleOrDefault<TD>(sql, args);
            //return Db.SingleOrDefault<T>(sql, args);
        }

        /// <summary>
        /// 返回满足指定条件的唯一元素
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>单个实体对象</returns>
        protected TE SingleOrDefault<TE>(string sql, params object[] args)
        {
            return this.Db.SingleOrDefault<TE>(sql, args);
        }

        /// <summary>
        /// 返回满足指定条件的唯一元素
        /// </summary>
        /// <param name="sql">Sql对象</param>
        /// <returns>单个实体对象</returns>
        protected TD SingleOrDefault(Sql sql)
        {
            return this.SingleOrDefault<TD>(sql);
            //return Db.SingleOrDefault<T>(sql);
        }

        /// <summary>
        /// 返回满足指定条件的唯一元素
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="sql">Sql对象</param>
        /// <returns>单个实体对象</returns>
        protected TE SingleOrDefault<TE>(Sql sql)
        {
            return this.Db.SingleOrDefault<TE>(sql);
        }

        #endregion

        #region Execute

        protected TE ExecuteScalar<TE>(Sql sql)
        {
            return Db.ExecuteScalar<TE>(sql);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响行数</returns>
        protected int Execute(string sql, params object[] args)
        {
            return Db.Execute(sql, args);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">Sql对象</param>
        /// <returns>受影响行数</returns>
        protected int Execute(Sql sql)
        {
            return Db.Execute(sql);
        }

        #endregion

        #region QueryPage

        /// <summary>
        /// 分页查询操作
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sql">Sql对象</param>
        /// <returns>当前索引页结果集</returns>
        protected PagedList<TD> QueryPage(int pageIndex, int pageSize, Sql sql)
        {
            return this.QueryPage<TD>(pageIndex, pageSize, sql);
            //return ConvertToDbPageListFromOrmPageList<T>(Db.QueryPage<T>(pageIndex, pageSize, sql));
        }

        /// <summary>
        /// 分页查询操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sql">Sql对象</param>
        /// <returns>当前索引页结果集</returns>
        protected PagedList<TE> QueryPage<TE>(int pageIndex, int pageSize, Sql sql)
        {
            return ConvertToPageListFromOrmPageList<TE>(Db.QueryPage<TE>(pageIndex, pageSize, sql));
        }

        /// <summary>
        /// 分页查询操作:查询当前索引页所有结果集
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>当前索引页结果集</returns>
        protected PagedList<TD> QueryPage(int pageIndex, int pageSize)
        {
            return this.QueryPage<TD>(pageIndex, pageSize);
            //return ConvertToDbPageListFromOrmPageList(Db.QueryPage<T>(pageIndex, pageSize, new Sql()));
        }

        /// <summary>
        /// 分页查询操作:查询当前索引页所有结果集
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>当前索引页结果集</returns>
        protected PagedList<TE> QueryPage<TE>(int pageIndex, int pageSize)
        {
            return this.ConvertToPageListFromOrmPageList<TE>(Db.QueryPage<TE>(pageIndex, pageSize, new Sql()));
        }

        /// <summary>
        /// 分页查询操作
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>当前索引页结果集</returns>
        protected PagedList<TD> QueryPage(int pageIndex, int pageSize, string sql, params object[] args)
        {
            return this.QueryPage<TD>(pageIndex, pageSize, sql, args);
            //return ConvertToDbPageListFromOrmPageList(Db.QueryPage<T>(pageIndex, pageSize, sql, args));
        }

        /// <summary>
        /// 分页查询操作
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>当前索引页结果集</returns>
        protected PagedList<TE> QueryPage<TE>(int pageIndex, int pageSize, string sql, params object[] args)
        {
            return this.ConvertToPageListFromOrmPageList<TE>(this.Db.QueryPage<TE>(pageIndex, pageSize, sql, args));
        }

        #endregion

        #region SkipTake

        /// <summary>
        /// 跳过并提取结果集
        /// </summary>
        /// <param name="skip">跳过的条数</param>
        /// <param name="take">提取的条数</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>满足条件的结果集</returns>
        protected List<TD> SkipTake(long skip, long take, string sql, params object[] args)
        {
            return this.SkipTake<TD>(skip, take, sql, args);
            //return Db.SkipTake<T>(skip, take, sql, args).ToList();
        }

        /// <summary>
        /// 跳过并提取结果集
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="skip">跳过的条数</param>
        /// <param name="take">提取的条数</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>满足条件的结果集</returns>
        protected List<TE> SkipTake<TE>(long skip, long take, string sql, params object[] args)
        {
            return this.Db.SkipTake<TE>(skip, take, sql, args).ToList();
        }

        /// <summary>
        /// 跳过并提取结果集
        /// </summary>
        /// <param name="skip">跳过的条数</param>
        /// <param name="take">提取的条数</param>
        /// <param name="sql">Sql对象</param>
        /// <returns>满足条件的结果集</returns>
        protected List<TD> SkipTake(long skip, long take, Sql sql)
        {
            return this.SkipTake<TD>(skip, take, sql);
            //return Db.SkipTake<T>(skip, take, sql).ToList();
        }

        /// <summary>
        /// 跳过并提取结果集
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="skip">跳过的条数</param>
        /// <param name="take">提取的条数</param>
        /// <param name="sql">Sql对象</param>
        /// <returns>满足条件的结果集</returns>
        protected List<TE> SkipTake<TE>(long skip, long take, Sql sql)
        {
            return this.Db.SkipTake<TE>(skip, take, sql).ToList();
        }

        #endregion

        #region QueryPage

        /// <summary>
        /// 自定义总条数分页查询，适用于复杂多表相连并排序的查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示大小</param>
        /// <param name="sql">sql对象</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        protected PagedList<TD> QueryPage(int pageIndex, int pageSize, Sql sql, long totalCount)
        {
            return this.QueryPage<TD>(pageIndex, pageSize, sql, totalCount);
            //var result =
            //    ConvertToDbPageListFromOrmPageList(
            //        new OrmPagedList<T>(SkipTake((pageIndex - 1) * pageSize, pageSize, sql), pageIndex,
            //            pageSize, (int)totalCount));

            //return result;
        }

        /// <summary>
        /// 自定义总条数分页查询，适用于复杂多表相连并排序的查询
        /// </summary>
        /// <typeparam name="TE">数据类型</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示大小</param>
        /// <param name="sql">sql对象</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        protected PagedList<TE> QueryPage<TE>(int pageIndex, int pageSize, Sql sql, long totalCount)
        {
            var result =
                ConvertToPageListFromOrmPageList(
                    new OrmPagedList<TE>(SkipTake<TE>((pageIndex - 1) * pageSize, pageSize, sql), pageIndex,
                        pageSize, (int)totalCount));

            return result;
        }

        #endregion

        /// <summary>
        /// 获取表总条数
        /// </summary>
        /// <returns>表总条数</returns>
        protected long GetTotalCount()
        {
            var sql = new Sql();
            sql.Append(String.Format("SELECT COUNT(*) FROM {0}", this._tableName));
            return Db.ExecuteScalar<long>(sql);
        }

        /// <summary>
        /// 获取满足一定条件的记录数
        /// </summary>
        /// <param name="sql">Sql对象</param>
        /// <returns>满足一定条件的记录数</returns>
        protected long GetCount(Sql sqlWhere)
        {
            var sql = new Sql();
            sql.Append(String.Format("SELECT COUNT(*) FROM {0}", this._tableName)).Append(sqlWhere);
            return Db.ExecuteScalar<long>(sql);
        }

        /// <summary>
        /// 执行查询的存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected List<TD> ExecProcedure(string procName, params object[] args)
        {
            return Db.ExecProcedure<TD>(procName, args).ToList();
        }

        #endregion

        #region Private

        /// <summary>
        /// 分页查询结果集类型转换
        /// </summary>
        /// <typeparam name="TE">对象类型</typeparam>
        /// <param name="ormPagedList">OrmPagedList集合对象</param>
        /// <returns></returns>
        private PagedList<TE> ConvertToPageListFromOrmPageList<TE>(OrmPagedList<TE> ormPagedList)
        {
            if (ormPagedList == null) return new PagedList<TE>();
            var result = new PagedList<TE>()
            {
                CurrentPageIndex = ormPagedList.CurrentPageIndex,
                PageSize = ormPagedList.PageSize,
                TotalItemCount = ormPagedList.TotalItemCount,
                TotalPageCount = ormPagedList.TotalPageCount,
                StartRecordIndex = ormPagedList.StartRecordIndex,
                EndRecordIndex = ormPagedList.EndRecordIndex
            };
            result.AddRange(ormPagedList);
            return result;
        }

        #endregion
    }
}
