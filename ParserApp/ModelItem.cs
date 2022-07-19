using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserApp
{
    class ModelItem
    {
        public string Title {get; set;}
        public string Link { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PubDate { get; set; }

        public ModelItem(string title, string link, string description, string category, DateTime pubDate)
        {
            Title = title;
            Link = link;
            Description = description;
            Category = category;
            PubDate = pubDate;
        }
    }
}
