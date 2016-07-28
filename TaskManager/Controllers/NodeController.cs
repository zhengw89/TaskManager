using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManager.Controllers.Base;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.Models.Dev;
using TaskManager.Service.Interfaces.Dev;

namespace TaskManager.Controllers
{
    public class NodeController : WebBaseController
    {
        protected override string GetPageCategoryName()
        {
            return "NODE";
        }

        #region Views

        [HttpGet]
        public ActionResult Index(int pageIndex = 1)
        {
            var service = base.ResolveService<INodeService>();
            var queryResult = service.GetByCondition(pageIndex, DefaultPageSize);

            var vm = new TmProcessResult<PagedList<NodeItemViewModel>>(queryResult.Error);
            if (!vm.HasError && queryResult.Data != null)
            {
                var hbQueryResult = service.GetLatestHeartBeat(queryResult.Data.Select(a => a.Id).Distinct().ToList());
                if (!hbQueryResult.HasError)
                {
                    vm.Data = new PagedList<NodeItemViewModel>();
                    foreach (var node in queryResult.Data)
                    {
                        vm.Data.Add(new NodeItemViewModel(node,
                            hbQueryResult.Data.SingleOrDefault(a => a.NodeId.Equals(node.Id))));
                    }
                    vm.Data.CopyPagedInfo(queryResult.Data);
                }
                else
                {
                    vm.Error = hbQueryResult.Error;
                }
            }

            return base.ReturnView(vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return base.ReturnView();
        }

        #endregion

        #region Post

        [HttpPost]
        public ActionResult Create(CreateNodeModel model)
        {
            if (ModelState.IsValid)
            {
                var service = base.ResolveService<INodeService>();
                var result = service.Create(model.Name, model.IP, model.Port, model.Remark);
                if (result.HasError)
                {
                    ViewBag.Error = result.Error.Message;
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }



            return base.ReturnView("Create", model);
        }

        #endregion
    }
}