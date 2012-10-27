using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windchime
{
    public class WindchimeSession
    {
        public User User { get; set; }

        public static WindchimeSession Current
        {
            get
            {
                if(HttpContext.Current.Session["WindchimeSession"] == null)
                    HttpContext.Current.Session["WindchimeSession"] = new WindchimeSession();
                
                return (WindchimeSession)HttpContext.Current.Session["WindchimeSession"];
            }
        }
    }
}
