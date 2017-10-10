using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

 namespace Assessment.Security.Interfaces
{
    interface IAuthenticationProvider
    {
        SecurityPrincipal Authenticate(NetworkCredential credential);
    }
}
