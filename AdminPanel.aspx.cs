using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

namespace Windchime
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        FileStream xmlSettings = new FileStream("./settings.config", FileMode.Open );
       

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlReader reader = XmlReader.Create(xmlSettings);

            reader.MoveToAttribute("test");

            string str = reader.ReadContentAsString();

            TextBox1.Text = str;


            
        }
    }
}
