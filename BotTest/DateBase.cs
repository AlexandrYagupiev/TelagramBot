using System;
using System.Collections.Generic;
using System.Text;

namespace BotTest
{
    public class DateBase
    {
        private readonly string path;

        public DateBase (string path)
        {
            this.path = path;
        }

        public void WriteApplication(Application application)
        {
            var readApplication = GetApplicationByGuid(application.Guid);
            if(readApplication=null)
            {
                
            }
            else
            {

            }
        }

        public Application GetApplicationByGuid(Guid guid)
        {

        }
    }
}
