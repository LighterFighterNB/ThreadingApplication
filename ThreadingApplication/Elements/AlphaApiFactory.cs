using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication.Elements
{
    class AlphaApiFactory
    {
        public AlphaApiFactory()
        {

        }

        public AlphaManager getApiRequest(String requestPeriod, String from, String to)
        {
            if(requestPeriod != null)
            {
                if(requestPeriod.Equals("daily", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new ApiRequestDaily(from, to);
                }else if(requestPeriod.Equals("weekly", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new ApiRequestWeekly(from, to);
                }else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
