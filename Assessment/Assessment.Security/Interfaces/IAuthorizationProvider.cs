using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 namespace Assessment.Security.Interfaces
{
    public interface IAuthorizationProvider
    {
        IUserProfile Authorize(SecurityPrincipal principal);
    }
}
