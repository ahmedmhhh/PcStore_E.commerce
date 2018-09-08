using PcStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PcStore.WebUI.HtmlHelper
{
    public static class PaginHelper
    {
        public static MvcHtmlString PageLinks(this System.Web.Mvc.HtmlHelper html,PagingInfo pageInfo,Func<int,string> PageUrl) {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", PageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");

                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create( result.ToString());
        }
    }
}