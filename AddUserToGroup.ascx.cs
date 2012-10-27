using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Windchime
{
    public partial class AddUserToGroup : System.Web.UI.UserControl
    {
        private WindchimeEntities AUTG_wce = new WindchimeEntities();
        public WindchimeEntities wce { get { return AUTG_wce; } }

        public delegate void GroupChangedHandler(object sender, EventArgs e);
        public event GroupChangedHandler GroupChanged;
        protected virtual void OnGroupChanged(EventArgs e)
        {
            if (GroupChanged != null)
                GroupChanged(this, e);
        }

        public Group CurrentGroup
        {
            get
            {
                int gid = Int32.Parse(AUTG_lstGroupNames.SelectedValue);
                return (from Group gr in AUTG_wce.Groups
                        where gr.GroupID == gid
                        select gr).FirstOrDefault();
            }
        }

        private Comparison<ListItem> compare;

        public AddUserToGroup()
        {
            compare = new Comparison<ListItem>(CompareListItems);
        }

        private int CompareListItems(ListItem li1, ListItem li2)
        {
            return String.Compare(li1.Text, li2.Text);
        }

        public void Refresh(bool clearLists)
        {
            List<Group> groups = new List<Group>();

            groups = (from Group g
                       in AUTG_wce.Groups
                      select g).ToList<Group>();

            List<ListItem> listitems = new List<ListItem>();
            foreach (Group g in groups)
            {
                ListItem l = new ListItem();
                l.Text = g.Name;
                if (g.IsSpecial)
                    l.Text = "*" + l.Text;
                l.Value = g.GroupID.ToString();
                listitems.Add(l);
            }
            listitems.Sort(compare);

            String selected = AUTG_lstGroupNames.SelectedValue;
            AUTG_lstGroupNames.Items.Clear();
            AUTG_lstGroupNames.Items.AddRange(listitems.ToArray<ListItem>());
            for (int i=0; i<AUTG_lstGroupNames.Items.Count; i++)
            {
                if (AUTG_lstGroupNames.Items[i].Value.CompareTo(selected) == 0)
                {
                    AUTG_lstGroupNames.SelectedIndex = i;
                    break;
                }
            }
            if (clearLists)
            {
                AUTG_lstGroupMembers.Items.Clear();
                AUTG_lstNotMembers.Items.Clear();
                AUTG_chkStaffOnly.Checked = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Refresh(true);
            }
        }

        protected void AUTG_lstGroupNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<User> users = new List<User>();
            List<User> notusers = new List<User>();

            Group g = CurrentGroup;

            if (g.IsSpecial)
                AUTG_chkStaffOnly.Enabled = AUTG_btnAddUser.Enabled = AUTG_btnRemoveUser.Enabled = false;
            else
                AUTG_chkStaffOnly.Enabled = AUTG_btnAddUser.Enabled = AUTG_btnRemoveUser.Enabled = true;

            g.Users.Load();
            users = g.Users.ToList<User>();

            List<ListItem> listitems = new List<ListItem>();
            foreach (User u in users)
            {
                if ((AUTG_chkStaffOnly.Checked && AUTG_chkStaffOnly.Enabled && u.IsStaff) || !AUTG_chkStaffOnly.Checked || !AUTG_chkStaffOnly.Enabled)
                {
                    ListItem l = new ListItem();
                    l.Text = u.Username;
                    l.Value = u.CreatorID.ToString();
                    listitems.Add(l);
                }
            }
            listitems.Sort(compare);

            if (!AUTG_chkStaffOnly.Checked || !AUTG_chkStaffOnly.Enabled)
                notusers = (from User u in AUTG_wce.CreatorSet.OfType<User>()
                            select u).ToList<User>();
            else
                notusers = (from User u in AUTG_wce.CreatorSet.OfType<User>()
                            where u.IsStaff
                            select u).ToList<User>();

            List<ListItem> listitems2 = new List<ListItem>();
            foreach (User u in notusers)
            {
                if (!users.Contains(u))
                {
                    ListItem l = new ListItem();
                    l.Text = u.Username;
                    l.Value = u.CreatorID.ToString();
                    listitems2.Add(l);
                }
            }
            listitems2.Sort(compare);

            AUTG_lstGroupMembers.Items.Clear();
            AUTG_lstNotMembers.Items.Clear();
            AUTG_lstGroupMembers.Items.AddRange(listitems.ToArray<ListItem>());
            AUTG_lstNotMembers.Items.AddRange(listitems2.ToArray<ListItem>());

            AUTG_lstGroupNames.Focus();

            OnGroupChanged(e);
        }

        protected void AUTG_btnAddUser_Click(object sender, EventArgs e)
        {
            Group g = CurrentGroup;

            AUTG_lblAddRemError.Text = "";

            for (int i = 0; i < AUTG_lstNotMembers.Items.Count; i++)
            {
                if (AUTG_lstNotMembers.Items[i].Selected)
                {
                    int uid = Int32.Parse(AUTG_lstNotMembers.Items[i].Value);
                    User u = (from User user in AUTG_wce.CreatorSet.OfType<User>()
                              where user.CreatorID == uid
                              select user).FirstOrDefault();
                    try
                    {
                        SecurityManager.AddUserToGroup(g, u);
                        AUTG_wce.SaveChanges();
                    }
                    catch (NoPolicyException ex)
                    {
                        AUTG_lblAddRemError.Text = "You do not have permission to modify groups.";
                        //SecurityManager.WriteToLog(ex);
                        break;
                    }
                }
            }

            AUTG_lstGroupNames_SelectedIndexChanged(null, null);
        }

        protected void AUTG_btnRemoveUser_Click(object sender, EventArgs e)
        {
            Group g = CurrentGroup;

            for (int i = 0; i < AUTG_lstGroupMembers.Items.Count; i++)
            {
                if (AUTG_lstGroupMembers.Items[i].Selected)
                {
                    int uid = Int32.Parse(AUTG_lstGroupMembers.Items[i].Value);
                    User u = (from User user in AUTG_wce.CreatorSet.OfType<User>()
                              where user.CreatorID == uid
                              select user).FirstOrDefault();

                    try
                    {
                        SecurityManager.RemoveUserFromGroup(g, u);
                        AUTG_wce.SaveChanges();
                    }
                    catch (NoPolicyException ex)
                    {
                        AUTG_lblAddRemError.Text = "You do not have permission to modify groups.";
                        //SecurityManager.WriteToLog(ex);
                        break;
                    }
                }
            }

            AUTG_lstGroupNames_SelectedIndexChanged(null, null);
        }

        protected void AUTG_chkStaffOnly_CheckedChanged(object sender, EventArgs e)
        {
            AUTG_lstGroupNames_SelectedIndexChanged(null, null);
        }
    }
}