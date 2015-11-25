using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DB;
using TaskManager.DBEntity;
using TaskManager.LogicEntity.Entities;

namespace TaskManager.Repository
{
    internal class BaseQuery<T> where T : DbBaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db">上下文对象</param>
        /// <param name="tableName"></param>
        public BaseQuery(ITaskManagerDb db, string tableName)
        {
            _db = db;
            _sql = new Sql();
            _petaparams = new List<PetaParams>();
            this._tableName = tableName;
        }

        #region 私有成员
        private readonly ITaskManagerDb _db;
        private Sql _sql;
        //条件参数集合
        private readonly List<PetaParams> _petaparams;
        private object[] _orderBy;
        private object[] _groupBy;

        private long? _skip, _take;
        private readonly string _tableName;
        protected string TableName { get { return this._tableName; } }

        //条件枚举
        private enum OperatorType
        {
            Equal = 0,
            GreaterThan,
            GreaterThanOrEqualTo,
            LessThan,
            LessThanOrEqualTo,
            ContainsString,
            IsNotEqualTo,
            IsNull,
            IsNotNull,
            In,
            NotIn
        }

        //条件参数类
        private class PetaParams
        {
            public string PColumn { get; set; }
            public object PValue { get; set; }
            public OperatorType PType { get; set; }
        }

        #endregion

        #region 私有方法

        private string OperatorTransformation(OperatorType ot)
        {
            switch (ot)
            {
                case OperatorType.Equal:
                    return "=";
                case OperatorType.GreaterThan:
                    return ">";
                case OperatorType.GreaterThanOrEqualTo:
                    return ">=";
                case OperatorType.LessThan:
                    return "<";
                case OperatorType.LessThanOrEqualTo:
                    return "<=";
                case OperatorType.ContainsString:
                    return "LIKE";
                case OperatorType.IsNotEqualTo:
                    return "!=";
                case OperatorType.IsNull:
                    return "IS NULL";
                case OperatorType.IsNotNull:
                    return "IS NOT NULL";
                case OperatorType.In:
                    return "IN";
                default:
                    throw new ArgumentException("数据库未知操作");
            }
        }

        private bool CheckIn()
        {
            foreach (var p in _petaparams)
            {
                if (p.PType == OperatorType.In)
                {
                    var list = (IList)p.PValue;
                    if (list.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private void BuildWhere()
        {
            foreach (var p in _petaparams)
            {
                switch (p.PType)
                {
                    case OperatorType.ContainsString:
                        _sql.Where(String.Format(" {0} LIKE @0 ", p.PColumn), "%" + p.PValue.ToString().Replace("'", "’").Replace("%", "％") + "%");
                        break;
                    case OperatorType.IsNull:
                    case OperatorType.IsNotNull:
                        _sql.Where(String.Format(" {0} {1} ", p.PColumn, OperatorTransformation(p.PType)));
                        break;
                    case OperatorType.In:
                        _sql.Where(String.Format("{0} IN (@0)", p.PColumn), p.PValue);
                        break;
                    case OperatorType.NotIn:
                        _sql.Where(String.Format("{0} NOT IN (@0)", p.PColumn), p.PValue);
                        break;
                    case OperatorType.Equal:
                    case OperatorType.GreaterThan:
                    case OperatorType.GreaterThanOrEqualTo:
                    case OperatorType.LessThan:
                    case OperatorType.LessThanOrEqualTo:
                    case OperatorType.IsNotEqualTo:
                        _sql.Where(String.Format(" {0} {1} @0 ", p.PColumn, OperatorTransformation(p.PType)), p.PValue);
                        break;
                    default:
                        throw new ArgumentException("未知查询方法");
                        break;
                }
            }

            if (_orderBy != null)
            {
                _sql.OrderBy(_orderBy);
            }
            if (_groupBy != null)
            {
                _sql.GroupBy(_groupBy);
            }
        }

        #region 添加参数
        private void AddPetaParams(string column, OperatorType type, object value = null)
        {
            _petaparams.Add(new PetaParams()
            {
                PColumn = column,
                PType = type,
                PValue = value
            });
        }
        #endregion

        #endregion

        public void Reset()
        {
            _sql = new Sql();
            _skip = null;
            _take = null;
            _orderBy = null;
            _groupBy = null;
            _petaparams.Clear();
        }

        /// <summary>
        /// 添加 = 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="value">值</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> Equal(string column, object value)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    value = 1;
                }
                else
                {
                    value = 0;
                }
            }
            AddPetaParams(column, OperatorType.Equal, value);
            return this;
        }

        /// <summary>
        /// 添加 > 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="value">值</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> GreaterThan(string column, object value)
        {
            AddPetaParams(column, OperatorType.GreaterThan, value);
            return this;
        }

        /// <summary>
        /// 添加 >= 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="value">值</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> GreaterThanOrEqualTo(string column, object value)
        {
            AddPetaParams(column, OperatorType.GreaterThanOrEqualTo, value);
            return this;
        }

        /// <summary>
        /// 添加 < 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="value">值</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> LessThan(string column, object value)
        {
            AddPetaParams(column, OperatorType.LessThan, value);
            return this;
        }

        /// <summary>
        /// 添加 <= 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="value">值</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> LessThanOrEqualTo(string column, object value)
        {
            AddPetaParams(column, OperatorType.LessThanOrEqualTo, value);
            return this;
        }

        /// <summary>
        /// 添加模糊查询条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="value">值</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> ContainsString(string column, string value)
        {
            #region like 注意
            /*
             //注意有Like关键字用下面这种写法(写法1)
                //sql.Append("WHERE W.WaiterName Like '%" + strKeyword + "%' OR W.Sex Like '%" + strKeyword + "%' OR W.WaiterAccount Like '%" + strKeyword + "%' ", strKeyword);

                //来自博客园网友James-yu的指点（写法2）
                sql.Append("WHERE W.WaiterName Like @0 OR W.Sex Like @0 OR W.WaiterAccount Like @0 ", "%" + strKeyword + "%");

                //不要采用下面这种方式，实现不了模糊查询
                //sql.Append("WHERE W.WaiterName Like '%@0%' OR W.Sex Like '%@0%' OR W.WaiterAccount Like '%@0%' ", strKeyword);
             */

            #endregion
            // SetWhere(colume, "like", "%" + value + "%");
            AddPetaParams(column, OperatorType.ContainsString, value);
            return this;
        }

        /// <summary>
        /// 添加 != 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="value">值</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> IsNotEqualTo(string column, object value)
        {
            AddPetaParams(column, OperatorType.IsNotEqualTo, value);
            return this;
        }

        /// <summary>
        /// 添加 包含 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="values">string类型 值的集合</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> In(string column, List<string> values)
        {
            AddPetaParams(column, OperatorType.In, values);
            return this;
        }

        /// <summary>
        /// 添加 包含 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <param name="values">int类型 值的集合</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> In(string column, List<int> values)
        {
            AddPetaParams(column, OperatorType.In, values);
            return this;
        }

        public BaseQuery<T> NotIn(string column, List<string> values)
        {
            AddPetaParams(column, OperatorType.NotIn, values);
            return this;
        }

        public BaseQuery<T> NotIn(string column, List<int> values)
        {
            AddPetaParams(column, OperatorType.NotIn, values);
            return this;
        }

        /// <summary>
        /// 添加 排序 条件
        /// </summary>
        /// <param name="columns">指定列名集合</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> OrderBy(params object[] columns)
        {
            _orderBy = columns;
            return this;
        }

        /// <summary>
        /// 添加 分组 条件
        /// </summary>
        /// <param name="columns">指定列名集合</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> GroupBy(params object[] columns)
        {
            _groupBy = columns;
            return this;
        }

        /// <summary>
        /// 添加 Null 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> IsNull(string column)
        {
            AddPetaParams(column, OperatorType.IsNull);
            return this;
        }

        /// <summary>
        /// 添加 NotNull 条件
        /// </summary>
        /// <param name="column">指定列名</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> IsNotNull(string column)
        {
            AddPetaParams(column, OperatorType.IsNotNull);
            return this;
        }

        /// <summary>
        /// 提取数量
        /// </summary>
        /// <param name="skip">跳过的条数</param>
        /// <param name="take">提取的条数</param>
        /// <returns>各个表的QueryBuilder对象</returns>
        public BaseQuery<T> SkipTake(long skip, long take)
        {
            this._skip = skip;
            this._take = take;
            return this;
        }

        /// <summary>
        /// 多条件查询操作
        /// </summary>
        /// <param name="columns">指定列</param>
        /// <returns>返回指定列满足一定条件的结果集</returns>
        public List<T> Query(params object[] columns)
        {
            if (!CheckIn())
                return new List<T>();
            //first specify the columns, then build the query condition
            if (columns != null && columns.Length > 0)
            {
                _db.EnableAutoSelect = false;
                _sql.Select(columns).From(this._tableName);
            }
            BuildWhere();

            if (_skip != null && _take != null)
            {
                var result = _db.SkipTake<T>(_skip.Value, _take.Value, _sql).ToList();
                _db.EnableAutoSelect = true;
                return result;
            }
            else
            {
                var result = _db.Query<T>(_sql).ToList();
                _db.EnableAutoSelect = true;
                return result;
            }
        }

        public List<T> QueryDistinct(string column)
        {
            if (!CheckIn())
                return new List<T>();
            _db.EnableAutoSelect = false;
            _sql.Select("DISTINCT " + column).From(this._tableName);
            BuildWhere();

            if (_skip != null && _take != null)
            {
                var result = _db.SkipTake<T>(_skip.Value, _take.Value, _sql).ToList();
                _db.EnableAutoSelect = true;
                return result;
            }
            else
            {
                var result = _db.Query<T>(_sql).ToList();
                _db.EnableAutoSelect = true;
                return result;
            }
        }

        public PagedList<T> QueryDistinctByPage(string column, int pageIndex, int pageSize)
        {
            if (!CheckIn())
                return new PagedList<T>();
            _db.EnableAutoSelect = false;
            _sql.Select("DISTINCT " + column).From(this._tableName);
            BuildWhere();

            var result = ConvertToPageListFromOrmPageList(_db.QueryPage<T>(pageIndex, pageSize, _sql));
            _db.EnableAutoSelect = true;
            return result;
        }

        public PagedList<T> QueryByPage(int pageIndex, int pageSize, params object[] columns)
        {
            if (!CheckIn())
                return new PagedList<T>();
            //first specify the columns, then build the query condition
            if (columns != null && columns.Length > 0)
            {
                _db.EnableAutoSelect = false;
                _sql.Select(columns).From(this._tableName);
            }
            BuildWhere();

            var result = ConvertToPageListFromOrmPageList(_db.QueryPage<T>(pageIndex, pageSize, _sql));
            _db.EnableAutoSelect = true;
            return result;
        }

        public long QueryCount()
        {
            if (!CheckIn())
                return 0;
            _sql.Select("COUNT(*)").From(_tableName);
            BuildWhere();

            return _db.ExecuteScalar<long>(_sql);
        }

        public long QueryDistinctCount(string column)
        {
            if (!CheckIn())
                return 0;
            _sql.Select("COUNT(DISTINCT " + column + ")").From(_tableName);
            BuildWhere();

            return _db.ExecuteScalar<long>(_sql);
        }

        public T SingleOrDefault(params object[] columns)
        {
            var queryResult = Query(columns);
            if (queryResult.Count > 1)
            {
                throw new ArgumentOutOfRangeException("Query result more than one element !!!!!!!!");
            }
            return queryResult.SingleOrDefault();
        }

        public T FirstOrDefault(params object[] columns)
        {
            return Query(columns).FirstOrDefault();
        }

        /// <summary>
        /// 分页查询结果集类型转换
        /// </summary>
        /// <param name="ormPagedList">OrmPagedList集合对象</param>
        /// <returns>DbPagedList集合对象</returns>
        private PagedList<T> ConvertToPageListFromOrmPageList(OrmPagedList<T> ormPagedList)
        {
            if (ormPagedList == null) return new PagedList<T>();
            var result = new PagedList<T>()
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
    }
}
