using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Autofac;
using CryptSharp;
using WebMatrix.WebData;
using Zetbox.API;
using Zetbox.App.Base;
using Zetbox.Client.ASPNET;

namespace Zetbox.Client.ASPNET
{
    public class ZetboxMembershipProvider : ExtendedMembershipProvider
    {
        public override string ApplicationName { get; set; }

        #region Helper
        private IZetboxContext GetContext()
        {
            var scope = DependencyResolver.Current.GetService<ZetboxContextHttpScope>();
            return scope.Context;
        }

        private Identity GetUser(IZetboxContext ctx, string username)
        {
            return ctx.GetQuery<Identity>().FirstOrDefault(i => i.UserName.ToLower() == username.ToLower());
        }

        private Identity GetUser(IZetboxContext ctx, int userid)
        {
            return ctx.GetQuery<Identity>().First(i => i.ID == userid);
        }

        private Identity GetUser(IZetboxContext ctx, string provider, string providerUserId)
        {
            return ctx.GetQuery<Identity>().FirstOrDefault(i => i.OpenID.Provider == provider && i.OpenID.UserID == providerUserId);
        }

        private System.Web.Security.MembershipUser ToMembershipUser(Identity usr)
        {
            return new System.Web.Security.MembershipUser(this.Name, usr.UserName, usr.ID, string.Empty, string.Empty, string.Empty, true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }
        #endregion

        #region provider settings
        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { return 10; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 0; }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return null; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }
        #endregion

        #region local password
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username, oldPassword))
            {
                var ctx = GetContext();
                var usr = GetUser(ctx, username);
                usr.SetPassword(newPassword);
                ctx.SubmitChanges();
                return true;
            }
            return false;
        }

        public override string ResetPassword(string username, string answer)
        {
            var password = Membership.GeneratePassword(9, 1);
            var ctx = GetContext();
            var usr = GetUser(ctx, username);
            usr.SetPassword(password);
            ctx.SubmitChanges();
            return password;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region CreateUser + Account
        /// <summary>
        /// A user has a user profile and a local account
        /// </summary>
        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            var ctx = GetContext();
            if (GetUser(ctx, username) != null)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            var usr = ctx.Create<Identity>();
            usr.UserName = username;
            usr.SetPassword(password);
            usr.DisplayName = username;
            usr.Groups.Add(NamedObjects.Base.Groups.Everyone.Find(ctx));
            ctx.SubmitChanges();

            status = MembershipCreateStatus.Success;
            return ToMembershipUser(usr);
        }

        /// <summary>
        /// A user has a user profile and a local account
        /// </summary>
        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            var ctx = GetContext();
            if (GetUser(ctx, userName) != null)
            {
                return null;
            }

            var usr = ctx.Create<Identity>();
            usr.UserName = userName;
            usr.SetPassword(password);
            usr.DisplayName = userName;
            usr.Groups.Add(NamedObjects.Base.Groups.Everyone.Find(ctx));
            ctx.SubmitChanges();

            return userName;
        }

        /// <summary>
        /// creates a user with a profile and a OpenID account, no local account (a account with a password)
        /// </summary>
        public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, userName);
            if (usr == null)
            {
                // create the user, if it does not exists yet
                usr = ctx.Create<Identity>();
                usr.DisplayName = userName;
                usr.UserName = userName;
                usr.Password = null; // no local password yet
                usr.Groups.Add(NamedObjects.Base.Groups.Everyone.Find(ctx));
            }

            usr.OpenID = ctx.CreateCompoundObject<OpenID>();
            usr.OpenID.Provider = provider;
            usr.OpenID.UserID = providerUserId;
            ctx.SubmitChanges();
        }

        /// <summary>
        /// creates a local account, that is a password associated with a user
        /// </summary>
        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, userName);
            if (usr == null)
            {
                throw new InvalidOperationException("user does not exist");
            }
            usr.SetPassword(password);
            ctx.SubmitChanges();

            return userName;
        }
        #endregion

        #region Update + Delete User
        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            // Nothing to update
            throw new NotSupportedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteAccount(string userName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Administration
        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }
        #endregion

        #region Get user & validate
        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, username);
            return usr != null ? ToMembershipUser(usr) : null;
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey is int)
            {
                var ctx = GetContext();
                var usr = GetUser(ctx, (int)providerUserKey);
                return usr != null ? ToMembershipUser(usr) : null;
            }
            return null;
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            if (string.IsNullOrEmpty(password)) return false;

            try
            {
                var ctx = GetContext();
                var usr = GetUser(ctx, username);
                return usr != null && !string.IsNullOrWhiteSpace(usr.Password) && Crypter.CheckPassword(password, usr.Password);
            }
            catch
            {
                return false;
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameFromId(int userId)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, userId);
            return usr != null ? usr.UserName : null;
        }
        #endregion

        #region OpenID support
        public override bool HasLocalAccount(int userId)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, userId);
            return usr != null && !string.IsNullOrWhiteSpace(usr.Password);
        }

        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, provider, providerUserId);
            return usr != null ? usr.ID : -1;
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, userName);
            if (usr.OpenID != null && !string.IsNullOrWhiteSpace(usr.OpenID.UserID))
            {
                return new[] { new OAuthAccountData(usr.OpenID.Provider, usr.OpenID.UserID) };
            }
            else
            {
                return new OAuthAccountData[] { };
            }
        }

        public override void DeleteOAuthAccount(string provider, string providerUserId)
        {
            var ctx = GetContext();
            var usr = GetUser(ctx, provider, providerUserId);
            if (usr != null && usr.OpenID != null)
            {
                usr.OpenID.Provider = null;
                usr.OpenID.UserID = null;
            }
            else
            {
                throw new InvalidOperationException("account was not found");
            }
        }

        public override string GetOAuthTokenSecret(string token)
        {
            return base.GetOAuthTokenSecret(token);
        }
        public override void StoreOAuthRequestToken(string requestToken, string requestTokenSecret)
        {
            base.StoreOAuthRequestToken(requestToken, requestTokenSecret);
        }

        public override void DeleteOAuthToken(string token)
        {
            base.DeleteOAuthToken(token);
        }

        public override void ReplaceOAuthRequestTokenWithAccessToken(string requestToken, string accessToken, string accessTokenSecret)
        {
            base.ReplaceOAuthRequestTokenWithAccessToken(requestToken, accessToken, accessTokenSecret);
        }
        #endregion

        #region NotSupportedException Stuff
        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            throw new NotSupportedException();
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            throw new NotSupportedException();
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new NotImplementedException();
        }
        public override DateTime GetCreateDate(string userName)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            throw new NotSupportedException();
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            throw new NotSupportedException();
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            throw new NotSupportedException();
        }

        public override bool IsConfirmed(string userName)
        {
            throw new NotSupportedException();
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
