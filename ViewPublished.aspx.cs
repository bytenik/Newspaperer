using System;
using System.Collections;
using System.Collections.Generic;
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

namespace Windchime
{
    public partial class Publish : System.Web.UI.Page
    {
        WindchimeEntities wce = new WindchimeEntities();
        List<Assignment> publishableAssignments = new List<Assignment>();
        String category = "";
        String issueID_str = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // get category and issue
            category = Request.QueryString["cat"];
            issueID_str = Request.QueryString["issue"];
            // list categories
            listCategories();
            Issue currentIssue;
            if (issueID_str == null)
            {
                List<Issue> issues = getPublishedIssues();
                currentIssue = issues.FirstOrDefault<Issue>();
            }
            else
            {
                currentIssue = (from Issue p in wce.PermissionableEntities.OfType<Issue>()
                                where p.Published
                                where p.EntityID == Convert.ToInt32(issueID_str)
                                select p).FirstOrDefault<Issue>();
            }
            if (currentIssue == null) return;
            getCompletedAssignments(currentIssue);
            // sort items by rank. If same rank, sort by alphabetical order.
            publishableAssignments.Sort(delegate(Assignment x, Assignment y)
            {
                if (x.PublishPriority == y.PublishPriority) return x.Name.CompareTo(y.Name);
                if (x.PublishPriority < y.PublishPriority) return -1;
                else return 1;
            });
            // list assignments in placeholders
            listAssets();

        }

        public List<Issue> getPublishedIssues()
        {
            return (from Issue p in wce.PermissionableEntities.OfType<Issue>()
                    where p.Published
                    orderby p.Date descending
                    select p).ToList<Issue>();
        }

        public void appendAssignments(List<Collection> loc)
        {
            Category cat = null;
            if (category.Length > 0)
            {
                cat = (from Category x in wce.PermissionableEntities.OfType<Category>()
                                where x.Name == category
                                select x).FirstOrDefault<Category>();
            }
            foreach (Collection c in loc)
            {
                if (cat == null)
                {
                    publishableAssignments.AddRange((from Assignment a in c.Children.OfType<Assignment>()
                                                     where a.CompletedDate != null
                                                     select a).ToList<Assignment>());
                }
                else
                {
                    publishableAssignments.AddRange((from Assignment a in c.Children.OfType<Assignment>()
                                                     where a.CompletedDate != null
                                                     where a.Parents.Contains(cat)
                                                     select a).ToList<Assignment>());
                }
                List<Collection> colList = (from Collection col in c.Children.OfType<Collection>()
                                            select col).ToList<Collection>();
                // recursive break point: no child collections
                if (colList.Count == 0) return;
                appendAssignments(colList);
            }
        }

        public List<Category> getCategories()
        {
            return (from c in wce.Collections.OfType<Category>()
                    select c).ToList<Category>();
        }

        public void listCategories()
        {
            List<Category> cats = getCategories();
            HyperLink hl;
            foreach (Category c in cats)
            {
                hl = new HyperLink();
                hl.Text = c.Name;
                hl.Target = "./ViewPublished.aspx?cat=" + c.Name;
                if (issueID_str.Length > 0)
                {
                    hl.Target += issueID_str;
                }
                AssetPrinter_placeholder_cats.Controls.Add(hl);
            }
        }

        // recursively go through sub-collections in issue and add assignments to publishableAssignments list
        public void getCompletedAssignments(Issue i)
        {
            // nodeChildren is the list of child collections in current node
            List<Collection> nodeChildren = i.Children.ToList<Collection>();
            if (nodeChildren.Count > 0){
                appendAssignments(nodeChildren);
            }
        }

        public List<Asset> getPublishedAssignmentAssets(Assignment a)
        {
            List<Asset> assets = (from Asset ast in a.Children.OfType<Asset>()
                                  where ast.Published == true
                                  select ast).ToList<Asset>();
            return assets;
        }

        public TextVersion getTextVersion(Asset a)
        {
            TextVersion t = (from TextVersion txt in a.Versions
                             where txt.Assets.EntityID == a.EntityID
                             select txt).FirstOrDefault<TextVersion>();
            return t;
        }

        public BinaryVersion getBinaryVersion(Asset a)
        {
            BinaryVersion b = (from BinaryVersion bin in a.Versions
                               where bin.Assets.EntityID == a.EntityID
                               select bin).FirstOrDefault<BinaryVersion>();
            return b;
        }

        public void addMissingLabel(String type, int count)
        {
            Label Missing_lbl = new Label();
            Missing_lbl.Text = "Error: " + type + " not found.<br />";
            getPlaceHolder(count).Controls.Add(Missing_lbl);
        }

        public PlaceHolder getPlaceHolder(int count)
        {
            switch (count)
            {
                case 0:
                    return AssetPrinter_placeholder1;
                case 1:
                    return AssetPrinter_placeholder2;
                case 2:
                    return AssetPrinter_placeholder3;
                case 3:
                    return AssetPrinter_placeholder4;
                case 4:
                    return AssetPrinter_placeholder5;
                default:
                    return AssetPrinter_placeholder_else;
            }
        }

        public void listAssets()
        {
            int count = 0;
            foreach (Assignment asgn in publishableAssignments)
            {
                asgn.Assets.Load();
                List<Asset> a = asgn.Assets.ToList<Asset>();
                foreach (Asset asset in a)
                {
                    Label Headline_lbl = new Label();
                    Headline_lbl.Text = asset.Headline + "<br />";
                    getPlaceHolder(count).Controls.Add(Headline_lbl);
                    Label Summary_lbl = new Label();
                    Summary_lbl.Text = asset.Summary + "<br />";
                    // if asset type is 1 - photo, create an image control
                    if (Convert.ToInt32(asset.AssetTypeReference.EntityKey.EntityKeyValues[0].Value) == 1)
                    {
                        BinaryVersion thisPhoto = getBinaryVersion(asset);
                        if (thisPhoto != null)
                        {
                            Image Photo_img = new Image();
                            Photo_img.ImageUrl = thisPhoto.Path;
                            getPlaceHolder(count).Controls.Add(Photo_img);
                            Label Space_lbl = new Label();
                            Space_lbl.Text = "<br />";
                            getPlaceHolder(count).Controls.Add(Space_lbl);
                        }
                        else
                        {
                            addMissingLabel("Photo", count);
                        }
                    }
                    // if asset type is 2 - article, create a label control
                    else if (Convert.ToInt32(asset.AssetTypeReference.EntityKey.EntityKeyValues[0].Value) == 2)
                    {
                        TextVersion thisArticle = getTextVersion(asset);
                        if (thisArticle != null)
                        {
                            Label Article_lbl = new Label();
                            Article_lbl.Text = thisArticle.Text + "<br />";
                            getPlaceHolder(count).Controls.Add(Article_lbl);
                        }
                        else
                        {
                            addMissingLabel("Article", count);
                        }
                    }
                    else
                    {
                        BinaryVersion thisAd = getBinaryVersion(asset);
                        if (thisAd != null)
                        {
                            Image Photo_img = new Image();
                            Photo_img.ImageUrl = thisAd.Path;
                            AssetPrinter_placeholder_ads.Controls.Add(Photo_img);
                            Label Space_lbl = new Label();
                            Space_lbl.Text = "<br />";
                            AssetPrinter_placeholder_ads.Controls.Add(Space_lbl);
                        }
                        else
                        {
                            addMissingLabel("Unable to display ads. Advertisement", count);
                        }
                    }
                }
                count++;
            }
        }
    }
}
