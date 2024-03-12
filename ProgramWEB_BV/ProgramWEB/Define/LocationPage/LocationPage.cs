using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Define
{
    public class LocationPage
    {
        public Page page { set; get; }
        public LocationPage child { set; get; }
        public LocationPage() {
            page = new Page();
            child = null;
        }
        public LocationPage(Page page)
        {
            this.page = page;
            child = null;
        }
        public LocationPage(Page page, LocationPage child)
        {
            this.page = page;
            this.child = child;
        }
        public static int count(LocationPage page)
        {
            if (page == null)
                return 0;
            return 1 + count(page.child);
        }
        public static LocationPage timNodeLa(LocationPage page)
        {
            if (page.child == null)
                return page;
            else
                return timNodeLa(page.child);
        }
    }
}