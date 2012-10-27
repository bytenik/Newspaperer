using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;

namespace Windchime
{
    public partial class UnassignedAssignment : System.Web.UI.Page
    {
        WindchimeEntities wce = new WindchimeEntities();

        // returns assignments that are not completed past due date
        public Assignment[] GetUnAssignments()
        {
            return (from p in wce.PermissionableEntities.OfType<Assignment>()
                    where p.Creators.ToArray<User>().Length == 0
                    where p.CompletedDate == null
                    select p).ToArray<Assignment>();
        }

        public User[] GetAllStaff()
        {
            return (from p in wce.CreatorSet.OfType<User>()
                    where p.IsStaff
                    select p).ToArray<User>();
        }

        public void sendUnassignedMail()
        {
            String fromMail = "stuteadmin@stevens.edu";
            String subject = "Stute Unassigned Assignment Notification";
            SmtpClient mailer = new SmtpClient();
            User[] users = GetAllStaff();
            foreach (User u in users)
            {
                mailer.Send(fromMail, u.Email, subject, email_txt.Text);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            String txt = "The following assignments have not been assigned: \r\n";
            Assignment[] assignments = GetUnAssignments();
            if (assignments.Length == 0)
            {
                txt = "There are no unassigned assignments.";
            }
            else
            {
                foreach (Assignment a in assignments)
                {
                    txt += "Assignment: " + a.Name + ", Due: " + ((DateTime)a.DueDate).ToLongDateString() + "\r\n";
                }
            }
            email_txt.Text = txt;
        }
    }
}
