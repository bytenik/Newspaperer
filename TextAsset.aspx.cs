using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class TextAsset : System.Web.UI.Page
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
                //                this.ViewState
                return;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                id = int.Parse(Request.QueryString["ID"]);

                using (WindchimeEntities wce = new WindchimeEntities())
                {
                    Asset asst = (from Asset a in wce.PermissionableEntities.OfType<Asset>()
                                        where a.EntityID == id
                                        select a).FirstOrDefault();
                    if (asst == null)
                        throw new ArgumentException("Invalid ID specified as parameter.");

                    Name_box.Text = asst.Headline;

                    TextVersion version = (from TextVersion tv in asst.Versions
                                           orderby tv.CreatedDate descending
                                           select tv).FirstOrDefault();
                    if (version != null)
                    {
                        Content_box.Text = version.Text;
                    }
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
            Response.Redirect("~/Default.aspx", false);
        }

        void Save_Click(object sender, EventArgs e)
        {
            Asset asst = null;

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                if (id != null)
                {
                    asst = (from Asset a in wce.PermissionableEntities.OfType<Asset>()
                             where a.EntityID == id
                             select a).FirstOrDefault();
                }

                if (asst == null)
                {
                    asst = new Asset();
                    wce.AddToPermissionableEntities(asst);
                }

                asst.Headline = Name_box.Text;

                TextVersion tv = new TextVersion();
                tv.Text = Content_box.Text;
                tv.CreatedDate = DateTime.Now;

                asst.Versions.Add(tv);
                wce.AddToVersionSet(tv);

                wce.SaveChanges();
                wce.Refresh(System.Data.Objects.RefreshMode.StoreWins, asst);
                id = asst.EntityID;
            }
        }
    }
}
