using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Windchime
{
    /// <summary>
    /// Returns a string list of staff members for auto-complete
    /// </summary>
    [WebService(Namespace = "http://www.stevens.edu/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StaffAutoComplete : System.Web.Services.WebService
    {
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetCompletionList(string prefixText, int count)
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                return (from User u in wce.CreatorSet.OfType<User>()
                        where u.IsStaff
                            && (u.FirstName + " " + u.LastName).StartsWith(prefixText)
                        select u.FirstName + " " + u.LastName).ToArray();
            }
        }
    }
}
