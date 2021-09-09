using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BotTest
{
    public class Pagination
    {
        private readonly AplicationContext aplicationContext;
        private readonly Func<ApplicationModel, bool> filter;
        private readonly int pageSize;

        public Pagination(AplicationContext aplicationContext, Func<ApplicationModel,bool> filter,int pageSize)
        {
            this.aplicationContext = aplicationContext;
            this.filter = filter;
            this.pageSize = pageSize;
        }

        public List<ApplicationModel> GetPage(int numberPage)
        {
            return aplicationContext.Applications.Include((t)=>t.User).Include((e)=>e.PhotoPathes).Where(filter).Skip(numberPage * pageSize).Take(pageSize).ToList();
        }
    }
}
