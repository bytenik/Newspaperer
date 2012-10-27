using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Windchime
{
    public class WCMembershipProvider : System.Web.Security.MembershipProvider
    {

        public static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public override string ApplicationName { get { return "Windchime"; } set { ; } }
        //
        // Summary:
        //     Indicates whether the membership provider is configured to allow users to
        //     reset their passwords.
        //
        // Returns:
        //     true if the membership provider supports password reset; otherwise, false.
        //     The default is true.
        public override bool EnablePasswordReset { get { return false; } }
        //
        // Summary:
        //     Indicates whether the membership provider is configured to allow users to
        //     retrieve their passwords.
        //
        // Returns:
        //     true if the membership provider is configured to support password retrieval;
        //     otherwise, false. The default is false.
        public override bool EnablePasswordRetrieval { get { return false; } }
        //
        // Summary:
        //     Gets the number of invalid password or password-answer attempts allowed before
        //     the membership user is locked out.
        //
        // Returns:
        //     The number of invalid password or password-answer attempts allowed before
        //     the membership user is locked out.
        public override int MaxInvalidPasswordAttempts { get { return 9999; } }
        //
        // Summary:
        //     Gets the minimum number of special characters that must be present in a valid
        //     password.
        //
        // Returns:
        //     The minimum number of special characters that must be present in a valid
        //     password.
        public override int MinRequiredNonAlphanumericCharacters { get { return 1; } }
        //
        // Summary:
        //     Gets the minimum length required for a password.
        //
        // Returns:
        //     The minimum length required for a password.
        public override int MinRequiredPasswordLength { get { return 8; } }
        //
        // Summary:
        //     Gets the number of minutes in which a maximum number of invalid password
        //     or password-answer attempts are allowed before the membership user is locked
        //     out.
        //
        // Returns:
        //     The number of minutes in which a maximum number of invalid password or password-answer
        //     attempts are allowed before the membership user is locked out.
        public override int PasswordAttemptWindow { get { return 9999; } }
        //
        // Summary:
        //     Gets a value indicating the format for storing passwords in the membership
        //     data store.
        //
        // Returns:
        //     One of the System.Web.Security.MembershipPasswordFormat values indicating
        //     the format for storing passwords in the data store.
        public override MembershipPasswordFormat PasswordFormat { get { return MembershipPasswordFormat.Hashed; } }
        //
        // Summary:
        //     Gets the regular expression used to evaluate a password.
        //
        // Returns:
        //     A regular expression used to evaluate a password.
        public override string PasswordStrengthRegularExpression
        { 
            get 
            { 
                //at least 1 alphanum, at least 1 special, 8-20 characters all together
                return "^(?=.*[a-zA-Z]+.*)(?=.*[!@#$%^&*]+.*)[0-9a-zA-Z!@#$%^&*]{" + MinRequiredPasswordLength.ToString() + ",}$"; 
            } 
        }
        //
        // Summary:
        //     Gets a value indicating whether the membership provider is configured to
        //     require the user to answer a password question for password reset and retrieval.
        //
        // Returns:
        //     true if a password answer is required for password reset and retrieval; otherwise,
        //     false. The default is true.
        public override bool RequiresQuestionAndAnswer { get { return false; } }
        //
        // Summary:
        //     Gets a value indicating whether the membership provider is configured to
        //     require a unique e-mail address for each user name.
        //
        // Returns:
        //     true if the membership provider requires a unique e-mail address; otherwise,
        //     false. The default is true.
        public override bool RequiresUniqueEmail { get { return true; } }

        // Summary:
        //     Occurs when a user is created, a password is changed, or a password is reset.
        //public event MembershipValidatePasswordEventHandler ValidatingPassword;

        // Summary:
        //     Processes a request to update the password for a membership user.
        //
        // Parameters:
        //   username:
        //     The user to update the password for.
        //
        //   oldPassword:
        //     The current password for the specified user.
        //
        //   newPassword:
        //     The new password for the specified user.
        //
        // Returns:
        //     true if the password was updated successfully; otherwise, false.
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return false;
        }
        //
        // Summary:
        //     Processes a request to update the password question and answer for a membership
        //     user.
        //
        // Parameters:
        //   username:
        //     The user to change the password question and answer for.
        //
        //   password:
        //     The password for the specified user.
        //
        //   newPasswordQuestion:
        //     The new password question for the specified user.
        //
        //   newPasswordAnswer:
        //     The new password answer for the specified user.
        //
        // Returns:
        //     true if the password question and answer are updated successfully; otherwise,
        //     false.
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }
        //
        // Summary:
        //     Adds a new membership user to the data source.
        //
        // Parameters:
        //   username:
        //     The user name for the new user.
        //
        //   password:
        //     The password for the new user.
        //
        //   email:
        //     The e-mail address for the new user.
        //
        //   passwordQuestion:
        //     The password question for the new user.
        //
        //   passwordAnswer:
        //     The password answer for the new user
        //
        //   isApproved:
        //     Whether or not the new user is approved to be validated.
        //
        //   providerUserKey:
        //     The unique identifier from the membership data source for the user.
        //
        //   status:
        //     A System.Web.Security.MembershipCreateStatus enumeration value indicating
        //     whether the user was created successfully.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object populated with the information
        //     for the newly created user.
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                Regex re = new Regex(this.PasswordStrengthRegularExpression);
                User u = new User();
                Group g = new Group();
                u.FirstName = "";
                u.LastName = "";
                u.Username = username;
                u.Password = SecurityManager.HashPasswordForStoringInDatabase(password);
                u.IsStaff = false;
                u.Email = email;
                g.Name = username;
                g.IsSpecial = false;

                if (username.Length < 6)
                {
                    status = MembershipCreateStatus.UserRejected;
                }
                else if ((from User k in wce.CreatorSet.OfType<User>()
                     where k.Username == username
                     select k).Count<User>() > 0)
                {
                   status = MembershipCreateStatus.DuplicateUserName;
                }
                else if (!re.IsMatch(password))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                }
                else if (!isEmail(email))
                {
                    status = MembershipCreateStatus.InvalidEmail;
                }
                else if ((from User k in wce.CreatorSet.OfType<User>()
                          where k.Email == email
                          select k).Count<User>() > 0)
                {
                    status = MembershipCreateStatus.DuplicateEmail;
                }
                else
                {
                    status = MembershipCreateStatus.Success;
                    wce.AddToCreatorSet(u);
                    wce.AddToGroups(g);
                    g.Users.Add(u);
                    wce.SaveChanges();
                    // log in the user
                    WindchimeSession.Current.User = u;
                }
            }

            return null;
        }
        //
        // Summary:
        //     Decrypts an encrypted password.
        //
        // Parameters:
        //   encodedPassword:
        //     A byte array that contains the encrypted password to decrypt.
        //
        // Returns:
        //     A byte array that contains the decrypted password.
        //
        // Exceptions:
        //   System.Configuration.Provider.ProviderException:
        //     The System.Web.Configuration.MachineKeySection.ValidationKey property or
        //     System.Web.Configuration.MachineKeySection.DecryptionKey property is set
        //     to AutoGenerate.
        /*protected virtual byte[] DecryptPassword(byte[] encodedPassword)
        {
            return null;
        }*/
        //
        // Summary:
        //     Removes a user from the membership data source.
        //
        // Parameters:
        //   username:
        //     The name of the user to delete.
        //
        //   deleteAllRelatedData:
        //     true to delete data related to the user from the database; false to leave
        //     data related to the user in the database.
        //
        // Returns:
        //     true if the user was successfully deleted; otherwise, false.
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return false;
        }
        //
        // Summary:
        //     Encrypts a password.
        //
        // Parameters:
        //   password:
        //     A byte array that contains the password to encrypt.
        //
        // Returns:
        //     A byte array that contains the encrypted password.
        //
        // Exceptions:
        //   System.Configuration.Provider.ProviderException:
        //     The System.Web.Configuration.MachineKeySection.ValidationKey property or
        //     System.Web.Configuration.MachineKeySection.DecryptionKey property is set
        //     to AutoGenerate.
        /*protected virtual byte[] EncryptPassword(byte[] password)
        {
            return null;
        }*/
        //
        // Summary:
        //     Gets a collection of membership users where the e-mail address contains the
        //     specified e-mail address to match.
        //
        // Parameters:
        //   emailToMatch:
        //     The e-mail address to search for.
        //
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection collection that contains a
        //     page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the
        //     page specified by pageIndex.
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            return null;
        }
        //
        // Summary:
        //     Gets a collection of membership users where the user name contains the specified
        //     user name to match.
        //
        // Parameters:
        //   usernameToMatch:
        //     The user name to search for.
        //
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection collection that contains a
        //     page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the
        //     page specified by pageIndex.
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            return null;
        }
        //
        // Summary:
        //     Gets a collection of all the users in the data source in pages of data.
        //
        // Parameters:
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection collection that contains a
        //     page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the
        //     page specified by pageIndex.
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            return null;
        }
        //
        // Summary:
        //     Gets the number of users currently accessing the application.
        //
        // Returns:
        //     The number of users currently accessing the application.
        public override int GetNumberOfUsersOnline()
        {
            return 0;
        }
        //
        // Summary:
        //     Gets the password for the specified user name from the data source.
        //
        // Parameters:
        //   username:
        //     The user to retrieve the password for.
        //
        //   answer:
        //     The password answer for the user.
        //
        // Returns:
        //     The password for the specified user name.
        public override string GetPassword(string username, string answer)
        {
            return "";
        }
        //
        // Summary:
        //     Gets user information from the data source based on the unique identifier
        //     for the membership user. Provides an option to update the last-activity date/time
        //     stamp for the user.
        //
        // Parameters:
        //   providerUserKey:
        //     The unique identifier for the membership user to get information for.
        //
        //   userIsOnline:
        //     true to update the last-activity date/time stamp for the user; false to return
        //     user information without updating the last-activity date/time stamp for the
        //     user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object populated with the specified
        //     user's information from the data source.
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return null;
        }
        //
        // Summary:
        //     Gets information from the data source for a user. Provides an option to update
        //     the last-activity date/time stamp for the user.
        //
        // Parameters:
        //   username:
        //     The name of the user to get information for.
        //
        //   userIsOnline:
        //     true to update the last-activity date/time stamp for the user; false to return
        //     user information without updating the last-activity date/time stamp for the
        //     user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object populated with the specified
        //     user's information from the data source.
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return null;
        }
        //
        // Summary:
        //     Gets the user name associated with the specified e-mail address.
        //
        // Parameters:
        //   email:
        //     The e-mail address to search for.
        //
        // Returns:
        //     The user name associated with the specified e-mail address. If no match is
        //     found, return null.
        public override string GetUserNameByEmail(string email)
        {
            return null;
        }
        //
        // Summary:
        //     Raises the System.Web.Security.MembershipProvider.ValidatingPassword event
        //     if an event handler has been defined.
        //
        // Parameters:
        //   e:
        //     The System.Web.Security.ValidatePasswordEventArgs to pass to the System.Web.Security.MembershipProvider.ValidatingPassword
        //     event handler.
        /*protected override virtual void OnValidatingPassword(ValidatePasswordEventArgs e)
        {

        }*/
        //
        // Summary:
        //     Resets a user's password to a new, automatically generated password.
        //
        // Parameters:
        //   username:
        //     The user to reset the password for.
        //
        //   answer:
        //     The password answer for the specified user.
        //
        // Returns:
        //     The new password for the specified user.
        public override string ResetPassword(string username, string answer)
        {
            return "";
        }
        //
        // Summary:
        //     Clears a lock so that the membership user can be validated.
        //
        // Parameters:
        //   userName:
        //     The membership user whose lock status you want to clear.
        //
        // Returns:
        //     true if the membership user was successfully unlocked; otherwise, false.
        public override bool UnlockUser(string userName)
        {
            return false;
        }
        //
        // Summary:
        //     Updates information about a user in the data source.
        //
        // Parameters:
        //   user:
        //     A System.Web.Security.MembershipUser object that represents the user to update
        //     and the updated information for the user.
        public override void UpdateUser(MembershipUser user)
        {

        }

        public override bool ValidateUser(string strName, string strPassword)
        {
            using (WindchimeEntities wce = new WindchimeEntities())
            {
                string pw = SecurityManager.HashPasswordForStoringInDatabase(strPassword);
                var users = (from User u in wce.CreatorSet.OfType<User>()
                             where u.Username == strName && u.Password == pw
                             select u);

                int num = users.Count<User>();

                if (num > 1)
                {
                    //throw new MultipleUsersException(); //doesn't exist right now
                    throw new Exception("Multiple users in system with same credentials!");
                }
                else if (num == 0)
                {
                    return false;
                }
                else
                {
                    WindchimeSession.Current.User = users.First<User>();
                    return true;
                }
            }
        }
    }
}
