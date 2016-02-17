﻿using System.Security.Principal;
using Microsoft.Dynamics.Framework.UI.Client;
using Microsoft.Dynamics.Nav.UserSession;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Dynamics.Nav.TestUtilities
{
    /// <summary>
    /// WindowsUserContextManager manages user contexts for a given tenant/company/user
    /// All virtual users use the current Windows Identity 
    /// </summary>
    public class WindowsUserContextManager : UserContextManager
    {
        /// <summary>
        /// Creates the WindowsUserContextManager for a given tenant/company/user
        /// </summary>
        /// <param name="navServerUrl">URL for NAV ClientService</param>
        /// <param name="defaultTenantId">Tenant</param>
        /// <param name="companyName">Company</param>
        /// <param name="roleCenterId">Role Center to use for the users</param>
        /// <param name="uiCultureId">The language culture Id. For example "da-DK"</param>
        public WindowsUserContextManager(
            string navServerUrl,
            string defaultTenantId,
            string companyName,
            int? roleCenterId,
            string uiCultureId = null)
            : base(navServerUrl, defaultTenantId, companyName, roleCenterId, uiCultureId)
        { }


        protected override UserContext CreateUserContext(TestContext testContext)
        {
            var userName = GetUserName(testContext);
            var userContext = new UserContext(DefaultTenantId, Company, AuthenticationScheme.Windows, userName);
            return userContext;
        }

        protected override string GetUserName(TestContext testContext)
        {
            return WindowsIdentity.GetCurrent().Name;
        }
    }
}
