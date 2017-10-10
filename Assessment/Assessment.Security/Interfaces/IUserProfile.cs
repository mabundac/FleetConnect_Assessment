using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

 namespace Assessment.Security.Interfaces
{
    public interface IUserProfile : ISerializable
    {
        object Id { get; }
        string FullName { get; }
        bool HasAuthorization { get; }
        bool CheckPermission(string permission);
        bool CheckRetailerAccess(int orgId);
    }
}