using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    public class PosterTagHelper : TagHelper
    {
        private const string Url = "https://image.tmdb.org/t/p/w300_and_h450_bestv2/";
        private const string DefaultUrl = "/images/noimage.png";
        public string Path { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            string resUrl = String.IsNullOrEmpty(Path) ? DefaultUrl : Url + Path;
            output.Attributes.Add("src", resUrl);
        }
    }
}   
