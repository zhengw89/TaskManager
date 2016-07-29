using System.Collections.Generic;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Dev;

namespace TaskManager.Service.Interfaces.Dev
{
    public interface INodeService
    {
        TmProcessResult<bool> Create(string nodeName, string ip, int port, string remark);

        TmProcessResult<bool> DeleteNode(string nodeId);
        
        TmProcessResult<PagedList<Node>> GetByCondition(int pageIndex, int pageSize);

        TmProcessResult<List<Node>> GetAllNode();

        TmProcessResult<Node> GetNodeById(string nodeId);

        
        
        
        TmProcessResult<bool> CreateNodeHeartBeat(string nodeId);

        TmProcessResult<List<NodeHeartBeat>> GetLatestHeartBeat(List<string> nodeIds);
    }
}
