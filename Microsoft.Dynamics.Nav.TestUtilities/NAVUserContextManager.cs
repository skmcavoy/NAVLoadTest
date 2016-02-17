﻿using System;
using Microsoft.Dynamics.Framework.UI.Client;
using Microsoft.Dynamics.Nav.UserSession;
using Microsoft.VisualStudio.TestTools.LoadTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Dynamics.Nav.TestUtilities
{
    /// <summary>
    /// UserContextManager user contexts for a given tenant/company/user
    /// This allows tests to reuse sessions for a given user
    /// to use this class you need to first create a default NAVUser and enough NAVUsers for each virtual test user eg. User, User1, User2...
    /// For simplicity all NAV Users share the same password
    /// </summary>
    public class NAVUserContextManager : UserContextManager
    {
        public string DefaultNAVUserName { get; private set; }
        public string DefaultNAVPassword { get; private set; }

        /// <summary>
        /// Create a UserContextManager using NAVUserPassword Authentication
        /// </summary>
        /// <param name="navServerUrl">URL for NAV ClientService</param>
        /// <param name="defaultTenantId">Default Tenant Id</param>
        /// <param name="companyName">Company</param>
        /// <param name="roleCenterId">Role Center to use for the users</param>
        /// <param name="defaultNAVUserName">Default User Name</param>
        /// <param name="defaultNAVPassword">Default Password</param>
        /// <param name="uiCultureId">The language culture Id. For example "da-DK"</param>
        public NAVUserContextManager(
            string navServerUrl,
            string defaultTenantId,
            string companyName,
            int? roleCenterId,
            string defaultNAVUserName,
            string defaultNAVPassword,
            string uiCultureId = null)
            : base(navServerUrl, defaultTenantId, companyName, roleCenterId, uiCultureId)
        {
            DefaultNAVUserName = defaultNAVUserName;
            DefaultNAVPassword = defaultNAVPassword;
        }

        protected override UserContext CreateUserContext(TestContext testContext)
        {
            var userName = GetUserName(testContext);
            var userContext = new UserContext(DefaultTenantId, Company, AuthenticationScheme.UserNamePassword, userName, DefaultNAVPassword);
            return userContext;
        }
        
        protected override string GetUserName(TestContext testContext)
        {
            // empty user name will use the default user name, this is the case when running as unit tests
            var loadTestUserContext = testContext.GetLoadTestUserContext();
            return loadTestUserContext != null ? 
                string.Format("{0}{1}", DefaultNAVUserName, loadTestUserContext.UserId) :
                DefaultNAVUserName;
        }
    }
}
