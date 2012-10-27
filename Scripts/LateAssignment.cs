using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace Windchime
{
    public class LateAssignment
    {

        WindchimeEntities wce = new WindchimeEntities();

        // returns assignments that are not completed past due date
        public Assignment[] GetLateAssignments()
        {
            return (from p in wce.PermissionableEntities.OfType<Assignment>()
                    where p.DueDate > DateTime.Now
                    where p.CompletedDate == null
                    select p).ToArray<Assignment>();
        }

        public void sendLateMail()
        {
            String fromMail = "stuteadmin@stevens.edu";
            String body = "";
            String toMail = "";
            String subject = "Stute Assignment Overdue Notification";
            Assignment[] allAssignments = GetLateAssignments();
            SmtpClient mailer = new SmtpClient();
            for (int i = 0; i < allAssignments.Length; i++)
            {
                User[] creators = allAssignments[i].Creators.ToArray<User>();
                for (int j = 0; j < creators.Length; j++)
                {
                    toMail = creators[j].Email;
                    body = creators[j].FirstName + " " + creators[j].LastName + ",\r\n";
                    body += "This email is to inform you that an assignment is overdue.\r\n";
                    body += "Assignment: " + allAssignments[i].Name + "\r\n";
                    body += "Due Date: " + ((DateTime)allAssignments[i].DueDate).ToLongDateString() + "\r\n";
                    body += "Current Date: " + DateTime.Now.ToLongDateString() + "\r\n";
                    mailer.Send(fromMail, toMail, subject, body);
                }
            }
        }
        public void Main()
        {
            sendLateMail();
        }
    }
}
