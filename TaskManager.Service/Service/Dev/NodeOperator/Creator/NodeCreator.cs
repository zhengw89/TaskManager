using System;
using System.Text.RegularExpressions;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Dev.NodeOperator.Creator
{
    internal class NodeCreatorDependent : TmBaseDependentProvider
    {
        public NodeCreatorDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeRepository>();
        }
    }

    internal class NodeCreator : TmOperateProcess
    {
        private readonly string _nodeName, _ip, _remark;
        private readonly int _port;

        private readonly INodeRepository _nodeRepository;

        public NodeCreator(ITmProcessConfig config, string nodeName, string ip, int port, string remark)
            : base(config)
        {
            this._nodeName = nodeName;
            this._ip = ip;
            this._port = port;
            this._remark = remark;

            this._nodeRepository = base.ResolveDependency<INodeRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._nodeName))
            {
                base.CacheProcessError("节点名不可为空");
                return false;
            }
            if (string.IsNullOrEmpty(this._ip))
            {
                base.CacheProcessError("节点IP不可为空");
                return false;
            }
            if (!Regex.IsMatch(this._ip, @"\b(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\b"))
            {
                base.CacheProcessError("节点IP格错误");
                return false;
            }
            if (this._port < 0 || this._port > 65536)
            {
                base.CacheProcessError("节点端口错误");
                return false;
            }
            if (this._nodeRepository.Exists(this._nodeName))
            {
                base.CacheProcessError("已存在相同节点名");
                return false;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            if (!this._nodeRepository.Create(new Node()
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                IP = this._ip,
                IsActive = true,
                Name = this._nodeName,
                Port = this._port,
                Remark = this._remark,
                UpdateTime = DateTime.Now
            }))
            {
                base.CacheProcessError("创建节点错误");
                return false;
            }

            return true;
        }
    }
}
