using System.Collections.Generic;

namespace TaskManager.LogicEntity.Entities
{
    public class PagedList<T> : List<T>, IPagedList
    {
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public int StartRecordIndex { get; set; }
        public int EndRecordIndex { get; set; }

        public void CopyPagedInfo(IPagedList source)
        {
            this.CurrentPageIndex = source.CurrentPageIndex;
            this.PageSize = source.PageSize;
            this.TotalItemCount = source.TotalItemCount;
            this.TotalPageCount = source.TotalPageCount;
            this.StartRecordIndex = source.StartRecordIndex;
            this.EndRecordIndex = source.EndRecordIndex;
        }
    }
}
