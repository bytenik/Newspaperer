using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Windchime
{
    public partial class UserPref : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //WindchimeSession.Current.User = new User(); //***** TESTING ONLY
            //WindchimeSession.Current.User.Username = "jyoungs"; //***** TESTING ONLY
            if (WindchimeSession.Current.User == null)
            {
                Response.Redirect("~/Login.aspx?ReturnUrl=/UserPref.aspx", false);
                return;
            }
            if (!IsPostBack)
                reset_data();
            WCMembershipProvider wcm = new WCMembershipProvider();
            Regex regexPassword = new Regex(wcm.PasswordStrengthRegularExpression);
            RegularExpressionValidator3.ValidationExpression = regexPassword.ToString();
        }

        protected void reset_data()
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                var user1 = (from User u in wce.CreatorSet.OfType<User>()
                             where u.Username.Equals( WindchimeSession.Current.User.Username )
                             select u);
                if (user1.Count() != 1)
                {
                    Response.Redirect("~/Login.aspx?ReturnUrl=/UserPref.aspx", false);
                    return;
                }
                User usr = user1.First<User>();
                boxUsername.Text = usr.Username;
                boxEmail.Text = usr.Email;
                boxFirstName.Text = usr.FirstName;
                boxLastName.Text = usr.LastName;
                boxAddr1.Text = usr.Address1;
                boxAddr2.Text = usr.Address2;
                boxCity.Text = usr.City;
                listState.SelectedValue = usr.State;
                boxZip.Text = usr.PostalCode;
                boxPassword1.Text = "";
                boxPassword2.Text = "";
                WindchimeSession.Current.User = usr;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                reset_data();
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                var user1 = (from User u in wce.CreatorSet.OfType<User>()
                             where u.Username.Equals(WindchimeSession.Current.User.Username)
                             select u);
                if (user1.Count() != 1)
                {
                    Response.Redirect("~/Login.aspx?ReturnUrl=/UserPref.aspx", false);
                    return;
                }
                User usr = user1.First<User>();
                Regex RegexObj = new Regex("^\\w[\\w.]*@\\w+\\.\\w[\\.\\w]*$");
                if ((!boxEmail.Text.Equals("")) && RegexObj.IsMatch(boxEmail.Text))
                {
                    usr.Email = boxEmail.Text;
                }
                if( !boxFirstName.Text.Equals( "" ) )
                    usr.FirstName = boxFirstName.Text;
                if( !boxLastName.Text.Equals( "" ) )
                    usr.LastName = boxLastName.Text;
                if (!boxAddr1.Text.Equals(""))
                    usr.Address1 = boxAddr1.Text;
                if (!boxAddr2.Text.Equals(""))
                    usr.Address2 = boxAddr2.Text;
                if (!boxCity.Text.Equals(""))
                    usr.City = boxCity.Text;
                usr.State = listState.SelectedValue;
                if (!boxZip.Text.Equals(""))
                    usr.PostalCode = boxZip.Text;

                WCMembershipProvider wcm = new WCMembershipProvider();
                Regex regexPassword = new Regex(wcm.PasswordStrengthRegularExpression);
                if (boxPassword1.Text.Equals(boxPassword2.Text) && (!boxPassword1.Text.Equals( "" )))
                    if (regexPassword.IsMatch(boxPassword1.Text))
                    {
                        usr.Password = SecurityManager.HashPasswordForStoringInDatabase(boxPassword1.Text);
                    }
                wce.SaveChanges();
            }
            reset_data();
        }
    }
}