using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Define
{
    public class Page
    {
        public string name { set; get; }
        public string url { set; get; }
        public Page() {
            name = "";
            url = "";
        }
        public Page(string name, string url)
        {
            this.name = name;
            this.url = url;
        }
    }
}