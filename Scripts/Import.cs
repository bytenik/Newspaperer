using System;
using System.IO;
using System.Collections;
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
    public class Import
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Windchime Importer");
            if (args.Length != 1)
            {
                usage();
            }
            if (! Directory.Exists(args[0]))
            {
                Console.WriteLine("Error: Invalid directory.");
                usage();
            }
            DirectoryInfo di = new DirectoryInfo(args[0]);
            FileInfo[] files = di.GetFiles(args[0] + "\\*");
            WindchimeEntities wce = new WindchimeEntities();
            foreach (FileInfo f in files)
            {
                BinaryVersion b = new BinaryVersion();
                b.Path = f.FullName;
                Asset a = new Asset();
                a.Versions.Add(b);
                wce.AddToPermissionableEntities(a);
                Console.WriteLine("Added binary asset from file " + f.FullName);
            }
            wce.SaveChanges();
        }

        static void usage()
        {
            Console.WriteLine("Usage: Import <Import Directory>");
        }
    }
}
