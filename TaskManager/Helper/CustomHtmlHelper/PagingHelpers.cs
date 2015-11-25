using System;
using System.Text;
using System.Web.Mvc;
using SMS.Helper.CustomHtmlHelper;
using TaskManager.LogicEntity.Entities;

namespace TaskManager.Helper.CustomHtmlHelper
{
    public static class PagingHelpers
    {
        //current index left&right buffer
        private const int NavPageSize = 3;

        public static MvcHtmlString PageLinks(
            this HtmlHelper html,
            IPagedList pagingInfo,
            Func<int, string> pageUrl
        )
        {
            var pagingBuilder = new PagingHtmlBuilder();
            var result = new StringBuilder();
            //previous link
            string prevLink = (pagingInfo.CurrentPageIndex == 1)
                ? pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex - 1), "Prev", false, true)
                : pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex - 1), "Prev");
            result.Append(prevLink);

            var start = (pagingInfo.CurrentPageIndex <= NavPageSize + 1) ? 1 : (pagingInfo.CurrentPageIndex - NavPageSize);

            var end = (pagingInfo.CurrentPageIndex > (pagingInfo.TotalPageCount - NavPageSize)) ? pagingInfo.TotalPageCount : pagingInfo.CurrentPageIndex + NavPageSize;

            for (int i = start; i <= end; i++)
            {
                string pageHtml = (i == pagingInfo.CurrentPageIndex)
                    ? pagingBuilder.BuildHtmlItem(pageUrl(i), i.ToString(), true)
                    : pagingBuilder.BuildHtmlItem(pageUrl(i), i.ToString());
                result.Append(pageHtml);
            }

            // next link
            string nextLink = (pagingInfo.CurrentPageIndex == pagingInfo.TotalPageCount)
                ? pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex + 1), "Next", false, true)
                : pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex + 1), "Next");
            result.Append(nextLink);

            return MvcHtmlString.Create(result.ToString());
        }
    }
}