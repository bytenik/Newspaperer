using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;


namespace Windchime
{

    //WARNING!!
    //Once in production, DO NOT remove any of these or change the order.
    //More may be added to the END, though
    public enum Permission : int
    {
        /// <summary>
        /// Permission to edit the asset or collection.
        /// </summary>
        Edit,

        /// <summary>
        /// Permission to view the asset or collection.
        /// </summary>
        View,

        /// <summary>
        /// Permission to delete the asset or collection.
        /// </summary>
        Delete,

        /// <summary>
        /// Permission to create a new comment.
        /// </summary>
        CreateComment,

        /// <summary>
        /// Permission to delete an owned comment.
        /// </summary>
        DeleteCommment,

        /// <summary>
        /// Permission to edit permissions.
        /// </summary>
        EditPermissions
    };

    //WARNING!!
    //Once in production, DO NOT remove any of these or change the order.
    //More may be added to the END, though
    public enum Policy : int
    {
        /// <summary>
        /// Ability to create a new user.
        /// </summary>
        CreateUser,

        /// <summary>
        /// Ability to edit an existing user.
        /// </summary>
        EditUser,

        /// <summary>
        /// Ability to delete an existing user.
        /// </summary>
        DeleteUser,

        /// <summary>
        /// Ability to verify an existing user.
        /// </summary>
        VerifyUser,

        /// <summary>
        /// Ability to create a new group.
        /// </summary>
        CreateGroup,

        /// <summary>
        /// Ability to edit an existing group.
        /// </summary>
        EditGroup,

        /// <summary>
        /// Ability to delete an existing group.
        /// </summary>
        DeleteGroup,

        /// <summary>
        /// Ability to create a new asset or collection.
        /// </summary>
        Create,

        /// <summary>
        /// Ability to delete any comment.
        /// </summary>
        DeleteAnyComment,

        /// <summary>
        /// Ability to edit the website.
        /// </summary>
        EditWebsite,

        /// <summary>
        /// Ability to edit the website template.
        /// </summary>
        EditWebsiteTemplate,

        /// <summary>
        /// Ability to create a policy entry.
        /// </summary>
        CreatePolicy,

        /// <summary>
        /// Ability to delete a group's policies.
        /// </summary>
        DeletePolicy,

        /// <summary>
        /// Ability to delete old versions of assets.
        /// </summary>
        DeleteOldVersions
    };

    /// <summary>
    /// Thrown if the currently logged-in user does not have the ability to perform a certain action.
    /// </summary>
    public class NoPolicyException : Exception
    {
        private Policy rp;
        /// <summary>
        /// The policy required to perform the action.
        /// </summary>
        public Policy RequiredPolicy { get { return rp; } }

        public NoPolicyException(Policy requiredPolicy) : base("Current user (" + (WindchimeSession.Current.User != null ? WindchimeSession.Current.User.Username : "null") + ") does not have required policy (" + requiredPolicy + ")") { rp = requiredPolicy; }
    }

    /// <summary>
    /// Thrown if the currently logged-in user does not have permission to perform a certain action.
    /// </summary>
    public class NoPermissionException : Exception
    {
        private Permission rp;
        /// <summary>
        /// The permission required to perform the action.
        /// </summary>
        public Permission RequiredPermission { get { return rp; } }

        public NoPermissionException(Permission requiredPermission) : base("Current user (" + (WindchimeSession.Current.User != null ? WindchimeSession.Current.User.Username : "null") + ") does not have required permission (" + requiredPermission + ")") { rp = requiredPermission; }
    }

    public static class SecurityManager
    {
        private static string SqlConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\Windchime.mdf;Integrated Security=True;User Instance=True;MultipleActiveResultSets=True";

        public static bool DoesGroupHavePerm(Group g, PermissionableEntity ent, Permission perm)
        {
            return true;
        }

        public static bool DoesUserHavePerm(User u, PermissionableEntity ent, Permission perm)
        {
            return true;
        }

        /// <summary>
        /// Checks to see if a group has a policy.
        /// </summary>
        /// <param name="g">The group to check for the policy.</param>
        /// <param name="pol">The policy to check for.</param>
        /// <returns>TRUE if the group has the policy, FALSE otherwise.</returns>
        public static bool DoesGroupHavePolicy(Group g, Policy pol)
        {
            int result;

            if (g == null)
                return false;

            SqlConnection conn = new SqlConnection(SqlConnectionString);
            SqlCommand sqlcom = new SqlCommand("DoesGroupHavePolicy", conn);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@GID", SqlDbType.Int);
            sqlcom.Parameters["@GID"].Value = g.GroupID;
            sqlcom.Parameters.Add("@PID", SqlDbType.Int);
            sqlcom.Parameters["@PID"].Value = (int)pol;
            sqlcom.Parameters.Add("@RESULT", SqlDbType.Int);
            sqlcom.Parameters["@RESULT"].Value = 1;
            sqlcom.Parameters["@RESULT"].Direction = ParameterDirection.Output;
            conn.Open();
            sqlcom.ExecuteNonQuery();
            result = (int)sqlcom.Parameters["@RESULT"].Value;
            conn.Close();

            return (result == 1);
        }

        /// <summary>
        /// Checks to see if a user has a policy.
        /// </summary>
        /// <param name="u">The user to check for the policy.</param>
        /// <param name="pol">The policy to check for.</param>
        /// <returns>TRUE if the user has the policy, FALSE otherwise.</returns>
        public static bool DoesUserHavePolicy(User u, Policy pol)
        {
            int result;

            if (u == null)
                return false;

            SqlConnection conn = new SqlConnection(SqlConnectionString);
            SqlCommand sqlcom = new SqlCommand("DoesUserHavePolicy", conn);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@UID", SqlDbType.Int);
            sqlcom.Parameters["@UID"].Value = u.CreatorID;
            sqlcom.Parameters.Add("@PID", SqlDbType.Int);
            sqlcom.Parameters["@PID"].Value = (int)pol;
            sqlcom.Parameters.Add("@RESULT", SqlDbType.Int);
            sqlcom.Parameters["@RESULT"].Value = 1;
            sqlcom.Parameters["@RESULT"].Direction = ParameterDirection.Output;
            conn.Open();
            sqlcom.ExecuteNonQuery();
            result = (int)sqlcom.Parameters["@RESULT"].Value;
            conn.Close();

            return (result == 1);
        }

        /// <summary>
        /// Creates a new user and adds it to the database.
        /// </summary>
        /// <param name="c">The Creator the user will derive from.</param>
        /// <param name="username">The username of the new user.</param>
        /// <param name="password">The non-hashed password of the new user.</param>
        /// <returns>The new user or null.</returns>
        /// <exception cref="NoPolicyException">If the current user does not have the ability to create a new user.</exception>
        public static User CreateUser(Creator c, string username, string password, bool isStaff)
        {
            return CreateUser(c, username, password, isStaff, true);
        }

        public static User CreateUser(Creator c, string username, string password, bool isStaff, bool overRide)
        {
            if (!overRide && !DoesUserHavePolicy(WindchimeSession.Current.User, Policy.CreateUser))
                throw new NoPolicyException(Policy.CreateUser);

            WCMembershipProvider wcm = new WCMembershipProvider();
            Regex re = new Regex(wcm.PasswordStrengthRegularExpression);
            User u;
            Group g = new Group();

            if (c == null || username.Length < 1 || !re.IsMatch(password))
            {
                return null;
            }

            u = User.CreateUser(c.CreatorID, c.FirstName, c.LastName, username, SecurityManager.HashPasswordForStoringInDatabase(password), isStaff);
            g.Name = username;
            g.IsSpecial = true;
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                wce.AddToCreatorSet(u);
                wce.AddToGroups(g);
                g.Users.Add(u);
                wce.SaveChanges();
                wce.Detach(g);
                wce.Detach(u);
            }

            return u;
        }

        /// <summary>
        /// Creates a new group and adds it to the database.
        /// </summary>
        /// <param name="name">The name of the new group.</param>
        /// <returns>The new group or null.</returns>
        /// <exception cref="NoPolicyException">If the current user does not have the ability to create a new group.</exception>
        public static Group CreateGroup(string name, bool isSpecial)
        {
            return CreateGroup(name, isSpecial, false);
        }

        public static Group CreateGroup(string name, bool isSpecial, bool overRide)
        {
            if (!overRide && !DoesUserHavePolicy(WindchimeSession.Current.User, Policy.CreateGroup))
                throw new NoPolicyException(Policy.CreateGroup);

            if (name.Length < 1)
                return null;

            Group g = new Group();
            g.Name = name;
            g.IsSpecial = isSpecial;
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                wce.AddToGroups(g);
                wce.SaveChanges();
                wce.Detach(g);
            }

            return g;
        }

        /// <summary>
        /// Creates a new permission and adds it to the database.
        /// </summary>
        /// <param name="g">The group to give permission to.</param>
        /// <param name="ent">The entity on which the permissio act.</param>
        /// <param name="perm">The permission to give to the group.</param>
        /// <param name="deny">Whether or not to explicitly deny this permission.</param>
        /// <param name="applytarget">Whether or not the permission applies to this entity (versus its contents).</param>
        /// <param name="applycolls">Whether or not the permission applies to collections within this entity.</param>
        /// <param name="applyassets">Whether or not the permission applies to assets within this entity.</param>
        /// <returns>The new permission entry or null.</returns>
        /// <exception cref="NoPolicyException">If the current user does not have the ability to create a new permission.</exception>
        public static PermissionEntry CreatePermissionEntry(Group g, PermissionableEntity ent, Permission perm, bool deny, bool applytarget, bool applycolls, bool applyassets)
        {
            if (!DoesUserHavePerm(WindchimeSession.Current.User, ent, Permission.EditPermissions))
                throw new NoPermissionException(Permission.EditPermissions);

            if (g == null || ent == null)
                return null;

            PermissionEntry pent = new PermissionEntry();
            pent.GroupID = g.GroupID;
            pent.EntityID = ent.EntityID;
            pent.Permission = (int)perm;
            pent.IsDeny = deny;
            pent.DoesApplyToTarget = applytarget;
            pent.DoesApplyToCollections = applycolls;
            pent.DoesApplyToAssets = applyassets;

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                wce.AddToPermissionEntrySet(pent);
                wce.SaveChanges();
                wce.Detach(pent);
            }

            return pent;
        }

        public static void DeletePermissionEntry(Group g, PermissionableEntity ent, Permission perm)
        {
            if (!DoesUserHavePerm(WindchimeSession.Current.User, ent, Permission.EditPermissions))
                throw new NoPermissionException(Permission.EditPermissions);

            if (g == null || ent == null)
                return;

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                var deletePerm = (from PermissionEntry pe in wce.PermissionEntrySet
                                  where pe.GroupID == g.GroupID && pe.EntityID == ent.EntityID && pe.Permission == (int)perm
                                  select pe);

                foreach (var pe in deletePerm)
                    wce.DeleteObject(pe);
            }
        }

        public static PolicyEntry CreatePolicy(Group g, Policy pol)
        {
            return CreatePolicy(g, pol, false);
        }

        public static PolicyEntry CreatePolicy(Group g, Policy pol, bool overRide)
        {
            if (!overRide && !DoesUserHavePolicy(WindchimeSession.Current.User, Policy.CreatePolicy))
                throw new NoPolicyException(Policy.CreatePolicy);

            if (g == null)
                return null;

            PolicyEntry pent = new PolicyEntry();
            pent.GroupID = g.GroupID;
            pent.Policy = pol;

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                wce.AddToPolicyEntries(pent);
                wce.SaveChanges();
                wce.Detach(pent);
            }

            return pent;            
        }

        public static void DeletePolicy(Group g, Policy pol)
        {
            if (!DoesUserHavePolicy(WindchimeSession.Current.User, Policy.DeletePolicy))
                throw new NoPolicyException(Policy.DeletePolicy);

            using (WindchimeEntities wce = new WindchimeEntities())
            {
                var deletePol = (from PolicyEntry pe in wce.PolicyEntries
                                 where pe.GroupID == g.GroupID && pe.Policy == pol
                                 select pe);
                foreach (var p in deletePol)
                    wce.DeleteObject(p);
                wce.SaveChanges();
            }
        }

        public static void AddUserToGroup(Group g, User u)
        {
            if (!DoesUserHavePolicy(WindchimeSession.Current.User, Policy.EditGroup))
                throw new NoPolicyException(Policy.EditGroup);

            g.Users.Load();
            g.Users.Add(u);
        }

        public static void RemoveUserFromGroup(Group g, User u)
        {
            if (!DoesUserHavePolicy(WindchimeSession.Current.User, Policy.EditGroup))
                throw new NoPolicyException(Policy.EditGroup);

            g.Users.Load();
            g.Users.Remove(u);

        }

        /// <summary>
        /// Hash a password string using a predetermined hash algorithm.
        /// </summary>
        /// <param name="Password">The password to hash.</param>
        /// <returns>The hashed password.</returns>
        public static string HashPasswordForStoringInDatabase(string Password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "sha1");
        }


        public static void WriteToLog(Exception e)
        {
            try 
            {
                string path = "~/Debug/" + DateTime.Today.ToString() + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }
                
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString());
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                              ". Error Message:" + e.Message +
                              ". Stack Trace: " + e.StackTrace + 
                              ". Source: " + e.Source;

                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToLog(ex);
            }
 
        }

        public static void WriteToLog(string msg)
        {
            try
            {
                string path = "~/Debug/" + DateTime.Today.ToString() + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }

                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString());
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                              ". Error Message:" + msg;

                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToLog(ex);
            }

        }    
    }

    public partial class PolicyEntry
    {
        public Policy Policy
        {
            get { return (Policy)this.policy_int; }
            set { this.policy_int = (int)value; }
        }
    }    
}
