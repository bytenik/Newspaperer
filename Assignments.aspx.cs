using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class Assignments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                foreach (Issue i in wce.PermissionableEntities.OfType<Issue>())
                    Issue_list.Items.Add(new ListItem(String.Format("{0} ({1})",
                            i.Name,
                            i.Date.ToShortDateString()),
                        i.EntityID.ToString()));

                Issue_list.Items.Insert(0, new ListItem("Any Issue", ""));

                var assignments = from Assignment a in wce.PermissionableEntities.OfType<Assignment>()
                                  select a;
                
                if(!string.IsNullOrEmpty(Issue_list.SelectedValue))
                {
                    int issueid = int.Parse(Issue_list.SelectedValue);

                    assignments = from Assignment a in assignments
                                  from Issue i in a.Parents.OfType<Issue>()
                                  where i.EntityID == issueid
                                  select a;
                }

                if (!string.IsNullOrEmpty(Keywords_box.Text))
                {
                    assignments = from Assignment a in assignments
                                  where a.Name.Contains(Keywords_box.Text)
                                    || a.Summary.Contains(Keywords_box.Text)
                                    || a.Location.Contains(Keywords_box.Text)
                                  select a;
                }

                if(!string.IsNullOrEmpty(Author_box.Text))
                {
                    assignments = from Assignment a in assignments
                                  from Creator c in a.Creators
                                  where (c.FirstName + " " + c.LastName).StartsWith(Author_box.Text)
                                  select a;
                }
                
                Assignments_grid.DataSource = assignments;
                Assignments_grid.DataBind();
            }
        }

        protected void Assignments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Assignments_grid.EditIndex = int.Parse(e.CommandArgument.ToString());
                Assignments_grid.DataBind();
            }
        }
    }
}
