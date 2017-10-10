using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 namespace Assessment.Helpers
{
    public static class HtmlHelpers
    {
        public static string ApplicationVersion()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var version = asm.GetName().Version;
            var product = asm.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), true).FirstOrDefault() as System.Reflection.AssemblyProductAttribute;

            if (version != null && product != null)
            {
                return string.Format("<span>v{0}.{1}.{2} ({3})</span>", version.Major, version.Minor, version.Build, version.Revision);
            }
            else
            {
                return "";
            }
        }
    }
}