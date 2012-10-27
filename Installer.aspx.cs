using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class Installer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ClearDBButton_Click(object sender, EventArgs e)
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                var deleteAssetTypes = (from x in wce.AssetTypeSet select x);
                var deleteComments = (from x in wce.CommentSet select x);
                var deleteCreators = (from x in wce.CreatorSet select x);
                var deleteGroups = (from x in wce.Groups select x);
                var deleteEnts = (from x in wce.PermissionableEntities select x);
                var deletePerms = (from x in wce.PermissionEntrySet select x);
                var deletePols = (from x in wce.PolicyEntries select x);
                var deleteVers = (from x in wce.VersionSet select x);
                var deleteAssets = (from x in wce.Assets select x);
                var deleteColls = (from x in wce.Collections select x);

                foreach (Group go in wce.Groups)
                {
                    go.Parents.Load();
                    var Parents = go.Parents.ToList();
                    foreach (Group gi in Parents)
                        go.Parents.Remove(gi);
                }
                wce.SaveChanges();

                foreach (Collection co in wce.Collections)
                {
                    co.Parents.Load();
                    var Parents = co.Parents.ToList();
                    foreach (Collection ci in Parents)
                        co.Parents.Remove(ci);
                }
                wce.SaveChanges();

                foreach (Asset ao in wce.Assets)
                {
                    ao.Collections.Load();
                    var Parents = ao.Collections.ToList();
                    foreach (Collection ci in Parents)
                        ao.Collections.Remove(ci);
                }
                wce.SaveChanges();

                foreach (Group go in wce.Groups)
                {
                    go.Users.Load();
                    var Parents = go.Users.ToList();
                    foreach (User ui in Parents)
                        go.Users.Remove(ui);
                }
                wce.SaveChanges();

                foreach (var a in deleteAssetTypes)
                    wce.DeleteObject(a);
                foreach (var c in deleteComments)
                    wce.DeleteObject(c);
                foreach (var c in deleteCreators)
                    wce.DeleteObject(c);
                foreach (var g in deleteGroups)
                    wce.DeleteObject(g);
                foreach (var d in deleteEnts)
                    wce.DeleteObject(d);
                foreach (var p in deletePerms)
                    wce.DeleteObject(p);
                foreach (var p in deletePols)
                    wce.DeleteObject(p);
                foreach (var v in deleteVers)
                    wce.DeleteObject(v);
                foreach (var a in deleteAssets)
                    wce.DeleteObject(a);
                foreach (var c in deleteColls)
                    wce.DeleteObject(c);

                wce.SaveChanges();
            }

            LoadTestDataButton.Enabled = true;
        }

        protected void LoadTestDataButton_Click(object sender, EventArgs e)
        {
            WindchimeEntities wce = new WindchimeEntities();

            Creator c = new Creator();
            c.FirstName = "EIC";
            c.LastName = "EIC";
            User u1 = SecurityManager.CreateUser(c, "EIC", "windchime*", true, true);

            c.FirstName = "Regular";
            c.LastName = "Staff";
            User u2 = SecurityManager.CreateUser(c, "Staff1", "windchime*", true, true);

            c.FirstName = "Regular";
            c.LastName = "User";
            User u3 = SecurityManager.CreateUser(c, "Regular1", "windchime*", false, true);

            Group g = new Group();
            g.Name = "Staff";
            g.IsSpecial = false;
            wce.Attach(u1);
            wce.Attach(u2);
            u1.Groups.Load();
            u2.Groups.Load();
            g.Children.Add(u1.Groups.First());
            g.Children.Add(u2.Groups.First());
            wce.AddToGroups(g);

            Asset a1 = new Asset(), a2 = new Asset(), a3 = new Asset(), a4 = new Asset();
            AssetType at1 = new AssetType();
            TextVersion tv1 = new TextVersion(), tv2 = new TextVersion(), tv3 = new TextVersion(), tv4 = new TextVersion(), tv5 = new TextVersion();
            at1.Name = "Text";
            a1.Approved = true;
            a2.Approved = false;
            a3.Approved = false;
            a4.Approved = true;
            a1.CompletedDate = DateTime.Now;
            a2.CompletedDate = DateTime.Now;
            a3.CompletedDate = DateTime.Now;
            a4.CompletedDate = DateTime.Now;
            a1.Creator = u1;
            a2.Creator = u1;
            a3.Creator = u2;
            a4.Creator = u2;
            a1.Headline = "Asset1!";
            a2.Headline = "Asset2!";
            a3.Headline = "Asset3!";
            a4.Headline = "Asset4!";
            a1.Summary = "An asset";
            a2.Summary = "An asset";
            a3.Summary = "An asset";
            a4.Summary = "An asset";
            a1.AssetType = at1;
            a2.AssetType = at1;
            a3.AssetType = at1;
            a4.AssetType = at1;

            tv1.Text = "Asset1 v1";
            tv1.CreatedDate = DateTime.Now;
            tv1.Log = "";
            tv2.Text = "Asset1 v2";
            tv2.CreatedDate = DateTime.Now;
            tv2.Log = "";
            tv3.Text = "Asset2 v1";
            tv3.CreatedDate = DateTime.Now;
            tv3.Log = "";
            tv4.Text = "Asset3 v1";
            tv4.CreatedDate = DateTime.Now;
            tv4.Log = "";
            tv5.Text = "Asset4 v1";
            tv5.CreatedDate = DateTime.Now;
            tv5.Log = "";

            a1.Versions.Add(tv1);
            a1.Versions.Add(tv2);
            a2.Versions.Add(tv3);
            a3.Versions.Add(tv4);
            a4.Versions.Add(tv5);

            wce.AddToPermissionableEntities(a1);
            wce.AddToPermissionableEntities(a2);
            wce.AddToPermissionableEntities(a3);
            wce.AddToPermissionableEntities(a4);
            wce.AddToVersionSet(tv1);
            wce.AddToVersionSet(tv2);
            wce.AddToVersionSet(tv3);
            wce.AddToVersionSet(tv4);
            wce.AddToVersionSet(tv5);

            Collection col1 = new Collection(), col2 = new Collection(), col3 = new Collection(), col4 = new Collection();
            col1.Name = "Text stuff1";
            col1.Assets.Add(a1);
            col2.Name = "Text stuff2";
            col2.Assets.Add(a2);
            col3.Name = "Text stuff3";
            col3.Assets.Add(a3);
            col4.Name = "Text stuff4";
            col4.Assets.Add(a4);

            col1.Children.Add(col2);
            col1.Children.Add(col3);
            col4.Children.Add(col3);

            wce.SaveChanges();


            g = (from Group gr in wce.Groups where gr.IsSpecial && gr.Name.CompareTo("EIC") == 0 select gr).First();
            g.PolicyEntries.Load();
            foreach (Policy p in Enum.GetValues(typeof(Policy)))
            {
                PolicyEntry pe = new PolicyEntry();
                pe.Policy = p;
                pe.GroupID = g.GroupID;
                g.PolicyEntries.Add(pe);
                wce.SaveChanges();
            }

            LoadTestDataButton.Enabled = false;
        }
    }
}
