using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class Backend : System.Web.UI.MasterPage
    {
        public string Title { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(this.Title))
                PageName_label.Text = "";
            else
                PageName_label.Text = this.Title;

            if (WindchimeSession.Current.User != null)
                Label1.Text = "Welcome, <b>" + WindchimeSession.Current.User.Username + "</b> | ";
            else
                Label1.Text = "";

        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            Session.Abandon();
        }
    }
}
