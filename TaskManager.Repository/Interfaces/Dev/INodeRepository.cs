using System.Collections.Generic;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Dev;

namespace TaskManager.Repository.Interfaces.Dev
{
    public interface INodeRepository
    {
        bool ExistById(string nodeId);

        bool ExistByName(string nodeName);

        bool Create(Node node);

        List<Node> GetAll(bool onlyAvailable);

        Node GetById(string id);

        PagedList<Node> GetByCondition(int pageIndex, int pageSize);
    }
}
