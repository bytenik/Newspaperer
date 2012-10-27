using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class UserDirectory : System.Web.UI.Page
    {
        WindchimeEntities wce = new WindchimeEntities();
        
        // gets all users
        public object getUsers()
        {
            return (from p in wce.CreatorSet.OfType<User>()
                        select p);
        }

        // gets all creators
        public object getCreators()
        {
            return (from p in wce.CreatorSet.OfType<Creator>()
                    select p);
        }

        // takes a creator, returns user if exists (first result)
        public User getUserByCreator(Creator c)
        {
            return (from p in wce.CreatorSet.OfType<User>()
                    where p.CreatorID == c.CreatorID
                    select p).FirstOrDefault<User>();
        }

        // if user does not have role, add and return true, otherwise false
        public Boolean addGroup(User u, Group g)
        {
            if (!u.Groups.IsLoaded)
            {
                u.Groups.Load();
            }
            if (!u.Groups.Contains(g))
            {
                u.Groups.Add(g);
                wce.SaveChanges();
                return true;
            }
            else 
            {
                return false;
            }
        }

        // if user has role, removes and returns true, otherwise false
        public Boolean removeGroup(User u, Group g)
        {
            if (!u.Groups.IsLoaded)
            {
                u.Groups.Load();
            }
            if (u.Groups.Contains(g))
            {
                u.Groups.Remove(g);
                wce.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        // toggles User.IsStaff on/off
        public void toggleStaff(User u)
        {
            if (!u.IsStaff)
            {
                u.IsStaff = true;
            }
            else
            {
                u.IsStaff = false;
            }
            wce.SaveChanges();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
