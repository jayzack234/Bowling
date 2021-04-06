using Bowling.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling.Infrastructure
{
    //
    [HtmlTargetElement("div", Attributes = "page-info")]
    //Inherit from tag helper class
    public class PaginationTagHelper : TagHelper
    {

        private IUrlHelperFactory urlInfo;

        //Constructor, get info about URL
        public PaginationTagHelper (IUrlHelperFactory uhf)
        {
            //Set urlInfo to the uhf being passed in 
            urlInfo = uhf;
        }

        public PageNumberingInfo PageInfo { get; set; }
        public string TeamType { get; set; }

        //dictionary, set a key and value, and when needed we retrieve from here
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        //public instance of a view context
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        //Process method, done when tag is referred to
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);
            TagBuilder finishedTag = new TagBuilder("div");

            for (int i = 1; i <= PageInfo.NumPages; i++)
            {

                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;
                //Go into individual tags and set the attributes
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                individualTag.InnerHtml.Append(i.ToString());
                finishedTag.InnerHtml.AppendHtml(individualTag);

            }
            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
