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
    public partial class WebForm2 : System.Web.UI.Page
    {
        WindchimeEntities wce = new WindchimeEntities();

        public Boolean publishAsset(Asset a)
        {
            if (a.Published)
            {
                return false;
            }
            a.Published = true;
            wce.SaveChanges();
            return true;
        }

        public void publishAssetToggle(Asset a)
        {
            if (a.Published)
            {
                a.Published = false;
            }
            else
            {
                a.Published = true;
            }
            wce.SaveChanges();
        }

        public List<Asset> getAssetsInCollection(Collection c)
        {
            List<Asset> asset_lst = (from p in c.Children.OfType<Asset>()
                                     select p).ToList<Asset>();
            return asset_lst;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
