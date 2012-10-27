using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NineRays.WebControls;

namespace Windchime
{
    public partial class CollectionBrowser : System.Web.UI.UserControl
    {
        protected void Collections_PopulateNodes(object sender, FlyTreeNodeEventArgs e)
        {
            int collectionID = int.Parse(e.Node.Value);

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                var children = (from Collection c in wce.Collections
                                where c.EntityID == collectionID
                                select c.Children).First();

                foreach (Collection c in children)
                {
                    var node = new FlyTreeNode(c.Name, c.EntityID.ToString());

                    //node.ImageUrl = "~/Images/folder-small.png";

                    // if there are children out there, populate them later. if not, no point in confusing the user
                    c.Children.Load();
                    if (c.Children.Count != 0)
                        node.PopulateNodesOnDemand = true;
                    else
                        node.PopulateNodesOnDemand = false;

                    e.Node.ChildNodes.Add(node);
                }
            }
        }

        protected void Collections_SelectedNodeChanged(object sender, SelectedNodeChangedEventArgs e)
        {
            int collectionID = int.Parse(e.Node.Value);

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                Collection loadme = (from Collection c in wce.Collections
                                     where c.EntityID == collectionID
                                     select c).First();
                OpenCollectionInInspector(loadme);
            }
        }

        private void OpenCollectionInInspector(Collection c)
        {
            List<object> Collections_inspectorList = new List<object>();

            c.Children.Load();
            foreach (Collection child in c.Children)
            {
                Collections_inspectorList.Add(new
                {
                    Name = child.Name,
                    Image = "folder.png",
                    ID = child.EntityID
                });
            }

            CollectionInspector_Collections_lvw.DataSource = Collections_inspectorList;
            CollectionInspector_Collections_lvw.DataBind();

            List<object> Assets_inspectorList = new List<object>();

            c.Assets.Load();
            foreach (Asset a in c.Assets)
            {
                Assets_inspectorList.Add(new
                {
                    Name = a.Headline,
                    Image = "ascii.png",
                    ID = a.EntityID
                });
            }

            CollectionInspector_Assets_lvw.DataSource = Assets_inspectorList;
            CollectionInspector_Assets_lvw.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            // fresh tree
            //Collections_tree.Nodes.Clear();

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                // only want to display top level collections at the root level
                var topLevelCollections = from Collection c in wce.Collections
                                          where c.Parents.Count == 0
                                          select c;
                foreach (Collection c in topLevelCollections)
                {
                    var node = new FlyTreeNode(c.Name, c.EntityID.ToString());

                    //node.ImageUrl = "~/Images/folder-small.png";

                    // if there are children out there, populate them later. if not, no point in confusing the user
                    c.Children.Load();
                    if (c.Children.Count != 0)
                        node.PopulateNodesOnDemand = true;
                    else
                        node.PopulateNodesOnDemand = false;

                    Collections_tree.Nodes.Add(node);
                }
            }
        }
    }
}