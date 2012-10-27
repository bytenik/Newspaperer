using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Objects;

namespace Windchime
{
    public partial class WindchimeEntities
    {
        public ObjectQuery<Collection> Collections { get { return this.PermissionableEntities.OfType<Collection>(); } }
        public ObjectQuery<Asset> Assets { get { return this.PermissionableEntities.OfType<Asset>(); } }
    }
}
