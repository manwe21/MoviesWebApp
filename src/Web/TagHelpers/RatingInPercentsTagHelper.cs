using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers        
{
    public class RatingInPercentsTagHelper : TagHelper  
    {
        public double Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.Content.SetContent($"{(int)(Value * 10)}%");
        }
    }
}
