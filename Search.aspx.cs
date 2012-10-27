using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class Search : System.Web.UI.Page
    {

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string args = searchbox.Text.Trim().ToLower();
            if (args.CompareTo("") == 0)
            {
                resultlabel.Text = "Enter Search Params";
                return;
            }
            else
            {
                using (WindchimeEntities wce = new WindchimeEntities())
                {

                    var results = (from TextVersion t in wce.VersionSet.OfType<TextVersion>()
                                   where t.Text.Contains(args) || t.Assets.Headline.Contains(args) && t.Assets.Approved
                                   select t);
                    if (results.Count() != 0)
                    {
                        resultlabel.Text = "";
                        foreach (var r in results)
                        {
                            resultlabel.Text += r.VersionID;
                        }
                    }
                    else
                    {
                        resultlabel.Text = "No Results Returned";
                    }
                }

            }

        }

    }
}
