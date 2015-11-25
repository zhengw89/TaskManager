namespace TaskManager.LogicEntity.Entities
{
    public interface IPagedList
    {
        int CurrentPageIndex { get; set; }
        int PageSize { get; set; }
        int TotalItemCount { get; set; }
        int TotalPageCount { get; set; }
        int StartRecordIndex { get; set; }
        int EndRecordIndex { get; set; }

        void CopyPagedInfo(IPagedList source);
    }
}
