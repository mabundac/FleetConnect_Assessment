using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 namespace Assessment.Security.Interfaces
{
    public interface IContextStoreProvider
    {
        bool HasPrincipal { get; }
        SecurityPrincipal GetPrincipal();
        void SetPrincipal(SecurityPrincipal principal);
        void ClearPrincipal();
        void SetData(string key, object data);
        object GetData(string key);
        void ClearData(string key);
        bool ContainsData(string key);
    }
}
