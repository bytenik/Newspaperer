using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;

namespace Windchime
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (WindchimeSession.Current.User != null)
            {
                // send logged in users to the home page
                Response.Redirect("~/Main.aspx");
                return;
            }*/
        }
		
        /*
        protected void Submit_Click(object sender, EventArgs e)
        {
            this.Validate();
            if (this.IsValid)
            {
                using (WindchimeEntities wce = new WindchimeEntities())
                {
                    User u = new User();
                    u.Username = Username.Text;     
                    u.Password = SecurityManager.HashPasswordForStoringInDatabase(Password.Text);
                    u.FirstName = FirstName.Text;   
                    u.LastName = LastName.Text;     
                    wce.AddToCreatorSet(u);
                    wce.SaveChanges();
                    
                    // log in the user
                    WindchimeSession.Current.User = u;
                }
            }
        }
        
        protected void ExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                var users = (from User u in wce.CreatorSet.OfType<User>()
                             where u.Username == CreateUserWizard1.UserName.ToString()  
                             select u);

                if (users.Count<User>() == 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }*/
    }
}
