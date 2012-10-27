using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class SecurityManagerTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                Label2.Text = "";
                User u = (from User user in wce.CreatorSet.OfType<User>() 
                          where user.Username.CompareTo("EIC") == 0
                          select user).First();

                foreach (Policy p in Enum.GetValues(typeof(Policy)))
                {
                    Label2.Text += p.ToString() + "  " + SecurityManager.DoesUserHavePolicy(u, p) + "<br />";
                }
                Label2.Text += "<br />";
            }
        }
    }
}
