using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 namespace Assessment.Security
{
    public static class Permissions
    {
        public class Assessment
        {
            /// <summary>
            /// Defining permissions required to use the application
            /// </summary>
            public const string CAN_APPLY_FOR_A_JOB = "FF931DDF-D590-4B84-A361-8611D47FFE17";
            public const string CAN_SAVE_A_JOB = "ABDD1E3C-4002-4198-BD46-802DBDA56391";
            public const string CAN_VIEW_A_JOB = "2C22877A-D350-4FCE-9548-0329054D479F";
            public const string CAN_VIEW_A_JOBS_AS_EMPLOYER = "50FD74A8-4B7C-4167-9548-E277812B992C";
        }                                                                                 
    }
}