using System.Collections.Generic;
using CommonProcess;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Dev.NodeOperator.Queryer
{
    internal class AllNodeQueryerDependent : TmBaseDependentProvider
    {
        public AllNodeQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeRepository>();
        }
    }

    internal class AllNodeQueryer : TmQueryProcess<List<Node>>
    {
        private readonly INodeRepository _nodeRepository;

        public AllNodeQueryer(ITmProcessConfig config)
            : base(config)
        {
            this._nodeRepository = base.ResolveDependency<INodeRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            return true;
        }

        protected override List<Node> Query()
        {
            return this._nodeRepository.GetAll(true);
        }
    }
}
