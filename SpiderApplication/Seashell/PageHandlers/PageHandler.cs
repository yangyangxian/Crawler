using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderApplication.Seashell.PageHandlers
{
    public class PageHandler { 

        protected Yang.Entities.SeashellContext context;

        public PageHandler() {
            context = new Yang.Entities.SeashellContext();
        }

    }
}
