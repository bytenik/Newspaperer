using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class Groups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WindchimeSession.Current.User == null)
            {
                Response.Redirect("~/Login.aspx?ReturnUrl=/Groups.aspx", false);
                return;
            }
        }

        protected override PageStatePersister PageStatePersister 
        {
            get
            {
                return new SessionPageStatePersister(this);
            }
        }

        WindchimeEntities wce = new WindchimeEntities();

        protected void btnAddNewGroup_Click(object sender, EventArgs e)
        {
            string newgroup = txtNewGroupName.Text.Trim();
            string newgrouplow = txtNewGroupName.Text.Trim().ToLower();
            if (newgrouplow.CompareTo("") == 0)
                return;
            var dupgroups = (from Group g in wce.Groups
                             where g.Name.ToLower().CompareTo(newgrouplow) == 0
                             select g);

            txtNewGroupName.Text = "";

            if (dupgroups.Count() < 1)
            {
                Group g;
                try
                {
                    g = SecurityManager.CreateGroup(newgroup, false, true);
                    AddUserToGroup1.wce.Attach(g);
                    lblNewGroupErr.Text = "";
                }
                catch (NoPolicyException ex)
                {
                    lblNewGroupErr.Text = "You do not have permission to create a new group.";
                    SecurityManager.WriteToLog(ex);
                    return;
                }
                AddUserToGroup1.Refresh(false);
            }
            else
            {
                lblNewGroupErr.Text = "Group name \"" + newgroup + "\" already exists.";
            }
        }
    }
}
