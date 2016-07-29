using System;
using TaskManager.LogicEntity.Entities;

namespace TaskManager.Models.Shared
{
    public class PaginationViewModel
    {
        public Func<int, string> GenerateUrlFunc { get; set; }

        public IPagedList PagedList { get; set; }
    }
}