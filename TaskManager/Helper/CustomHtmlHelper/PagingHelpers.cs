using System;
using System.Text;
using System.Web.Mvc;
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
                ? pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex - 1), "<", false, true)
                : pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex - 1), "<");
            result.Append(prevLink);

            var start = (pagingInfo.CurrentPageIndex <= NavPageSize + 1) ? 1 : (pagingInfo.CurrentPageIndex - NavPageSize);

            var end = (pagingInfo.CurrentPageIndex > (pagingInfo.TotalPageCount - NavPageSize)) ?
                pagingInfo.TotalPageCount :
                pagingInfo.CurrentPageIndex + NavPageSize;

            for (int i = start; i < pagingInfo.CurrentPageIndex; i++)
            {
                result.Append(pagingBuilder.BuildHtmlItem(pageUrl(i), i.ToString()));
            }

            result.Append(pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex), pagingInfo.CurrentPageIndex.ToString(), true));

            for (int i = pagingInfo.CurrentPageIndex + 1; i <= end; i++)
            {
                result.Append(pagingBuilder.BuildHtmlItem(pageUrl(i), i.ToString()));
            }

            //for (int i = start; i <= end; i++)
            //{
            //    string pageHtml = (i == pagingInfo.CurrentPageIndex)
            //        ? pagingBuilder.BuildHtmlItem(pageUrl(i), i.ToString(), true)
            //        : pagingBuilder.BuildHtmlItem(pageUrl(i), i.ToString());
            //    result.Append(pageHtml);
            //}

            // next link
            string nextLink = (pagingInfo.CurrentPageIndex >= pagingInfo.TotalPageCount)
                ? pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex + 1), ">", false, true)
                : pagingBuilder.BuildHtmlItem(pageUrl(pagingInfo.CurrentPageIndex + 1), ">");
            result.Append(nextLink);

            return MvcHtmlString.Create(result.ToString());
        }
    }
}