using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTemplate.Application.Interfaces;
using TestTemplate.Application.Wrappers;

namespace TestTemplate.WebUi.Infrastracture.Helper
{
    public static class PagedListPagerHelpers
    {
        public static IHtmlContent PagedListPager<T>(this IHtmlHelper htmlHelper, PagedResponse<T> Model, Func<int, string> generatePageUrl, ITranslator localizer)
        {
            var Main = new TagBuilder("nav");
            Main.AddCssClass("Page navigation example");

            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");
            if (Model.HasPreviousPage)
            {
                SetLink(ulTag, localizer["Back"], generatePageUrl(Model.PageNumber - 1), false);
                if (Model.PageNumber > 5)
                {
                    SetLink(ulTag, localizer["First"], generatePageUrl(1), false);
                }
            }
            var nPages = Getpages(Model);
            for (int i = nPages.Item1; i < nPages.Item2; i++)
            {
                SetLink(ulTag, i.ToString(), generatePageUrl(i), Model.PageNumber == i);
            }

            if (Model.HasNextPage)
            {
                if (Model.PageNumber < Model.TotalPages - 4)
                {
                    SetLink(ulTag, localizer["End"], generatePageUrl(Model.TotalPages), false);
                }
                SetLink(ulTag, localizer["Next"], generatePageUrl(Model.PageNumber + 1), false);
            }
            Main.InnerHtml.AppendHtml(ulTag.RenderStartTag());
            Main.InnerHtml.AppendHtml(ulTag.RenderBody());
            Main.InnerHtml.AppendHtml(ulTag.RenderEndTag());

            return Main.RenderBody();
        }

        public static void SetLink(TagBuilder tagBuilder, string text, string href, bool active = false)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");

            var aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");
            if (active)
            {
                liTag.AddCssClass("active");
            }
            aTag.MergeAttribute("href", href);
            aTag.InnerHtml.AppendHtml(text);
            liTag.InnerHtml.AppendHtml(aTag.RenderStartTag());
            liTag.InnerHtml.AppendHtml(aTag.RenderBody());
            liTag.InnerHtml.AppendHtml(aTag.RenderEndTag());
            tagBuilder.InnerHtml.AppendHtml(liTag.RenderStartTag());
            tagBuilder.InnerHtml.AppendHtml(liTag.RenderBody());
            tagBuilder.InnerHtml.AppendHtml(liTag.RenderEndTag());

        }

        public static Tuple<int, int> Getpages<T>(PagedResponse<T> Model)
        {
            if (Model.TotalPages <= 10)
            {
                return new Tuple<int, int>(1, Model.TotalPages + 1);
            }
            else if (Model.PageNumber <= 5)
            {
                return new Tuple<int, int>(1, 10);
            }
            else if (Model.PageNumber > Model.TotalPages - 5)
            {
                return new Tuple<int, int>(Model.TotalPages - 8, Model.TotalPages + 1);
            }
            return new Tuple<int, int>(Model.PageNumber - 4, Model.PageNumber + 5);
        }
    }
    public class PagentaionResult<Tquery, Tresult>
    {
        public PagentaionResult(Tquery query, Tresult result)
        {
            Query = query;
            Result = result;
        }
        public Tquery Query { get; set; }
        public Tresult Result { get; set; }
    }

}
