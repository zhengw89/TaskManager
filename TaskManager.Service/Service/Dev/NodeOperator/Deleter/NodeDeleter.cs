using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DB;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Dev.NodeOperator.Deleter
{
    internal class NodeDeleterDependent : TmBaseDependentProvider
    {
        public NodeDeleterDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeRepository>();
        }
    }

    internal class NodeDeleter : TmOperateProcess
    {
        private readonly string _nodeId;

        private readonly INodeRepository _nodeRepository;

        public NodeDeleter(ITmProcessConfig config, string nodeId)
            : base(config)
        {
            this._nodeId = nodeId;

            this._nodeRepository = base.ResolveDependency<INodeRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._nodeId))
            {
                base.CacheProcessError("节点ID不可为空");
                return false;
            }

            if (!this._nodeRepository.ExistById(this._nodeId))
            {
                base.DirectSuccessProcess();
                return true;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            if (!this._nodeRepository.Delete(this._nodeId))
            {
                base.CacheProcessError("删除节点");
                return false;
            }

            return true;
        }
    }
}
