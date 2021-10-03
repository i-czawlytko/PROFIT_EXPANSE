using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using DataAccess.Interfaces;

namespace DataAccess.Handlers
{
    public class WindowsAuthLogin : ILogin
    {
        public WindowsAuthLogin()
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        }
        public string GetLogin()
        {           
            WindowsPrincipal principial = (WindowsPrincipal)Thread.CurrentPrincipal;
            WindowsIdentity identity = (WindowsIdentity)principial.Identity;

            if (identity.IsAnonymous) return null;

            return identity.Name;
        }

    }
}
