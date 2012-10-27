using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class Assignment1 : System.Web.UI.Page
    {
        private int? id
        {
            get
            {
                return (int?)this.ViewState["ID"];
            }
            set
            {
                this.ViewState["ID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                Author_lst.DataTextField = "Name";
                Author_lst.DataValueField = "ID";
                Author_lst.DataSource = from User u in wce.CreatorSet.OfType<User>()
                                        select new
                                        {
                                            Name = u.FirstName + " " + u.LastName,
                                            ID = u.CreatorID
                                        };
                Author_lst.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    id = int.Parse(Request.QueryString["ID"]);

                    Assignment assgn = (from Assignment a in wce.PermissionableEntities.OfType<Assignment>()
                                        where a.EntityID == id
                                        select a).FirstOrDefault();
                    if (assgn == null)
                        throw new ArgumentException("Invalid ID specified as parameter.");

                    Location_box.Text = assgn.Location;
                    Completed_chk.Checked = (assgn.CompletedDate != null);
                    DueDate_box.Text = assgn.DueDate.ToString();
                    Name_box.Text = assgn.Name;
                    Summary_box.Text = assgn.Summary;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Save_btn.Click += new EventHandler(Save_Click);
            SaveQuit_btn.Click += new EventHandler(SaveQuit_Click);
        }

        void SaveQuit_Click(object sender, EventArgs e)
        {
            Save_Click(sender, e);
            Response.Redirect("~/Assignments.aspx", false);
        }

        void Save_Click(object sender, EventArgs e)
        {
            Assignment assgn = null;

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                if (id != null)
                {
                    assgn = (from Assignment a in wce.PermissionableEntities.OfType<Assignment>()
                             where a.EntityID == id
                             select a).FirstOrDefault();
                }

                if (assgn == null)
                {
                    assgn = new Assignment();
                    wce.AddToPermissionableEntities(assgn);
                }

                assgn.Summary = Summary_box.Text;
                assgn.Name = Name_box.Text;
                assgn.DueDate = DateTime.Parse(DueDate_box.Text);
                assgn.Location = Location_box.Text;

                if (Completed_chk.Checked && assgn.CompletedDate == null)
                    assgn.CompletedDate = DateTime.Now;
                else if (!Completed_chk.Checked)
                    assgn.CompletedDate = null;

                wce.SaveChanges();
                wce.Refresh(System.Data.Objects.RefreshMode.StoreWins, assgn);
                id = assgn.EntityID;
            }
        }
    }
}
