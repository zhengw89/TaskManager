using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DB;
using TaskManager.DBEntity.DEV;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Converter.Dev;
using TaskManager.Repository.Interfaces.Dev;

namespace TaskManager.Repository.Repositories.Base.Dev
{
    internal abstract class BaseNodeRepository : BaseRepository<Node, T_DEV_NODE>, INodeRepository
    {
        protected BaseNodeRepository(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override Node FromT(T_DEV_NODE t)
        {
            return t.FromT();
        }

        protected override T_DEV_NODE ToT(Node l)
        {
            return l.ToT();
        }

        public bool Exists(string nodeName)
        {
            return base.BaseQuery.Equal("NODE_Name", nodeName).Equal(IsActive, true).QueryCount() > 0;
        }

        public bool Create(Node node)
        {
            return base.Add(node.ToT());
        }

        public List<Node> GetAll(bool onlyAvailable)
        {
            var bq = base.BaseQuery;
            if (onlyAvailable)
            {
                bq.Equal(IsActive, true);
            }

            return base.ConvertToList(bq.Query());
        }

        public Node GetById(string id)
        {
            return base.BaseQuery.Equal("NODE_Id", id).Equal(IsActive, true).SingleOrDefault().FromT();
        }

        public PagedList<Node> GetByCondition(int pageIndex, int pageSize)
        {
            return
                base.ConvertToPagedList(base.BaseQuery.Equal(IsActive, true)
                    .OrderBy("NODE_Name")
                    .QueryByPage(pageIndex, pageSize));
        }
    }
}
