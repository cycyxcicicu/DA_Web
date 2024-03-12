using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Define
{
    public class DefineSession
    {
        public static string userSession { get; }= "SESSION_USER";
        public static string beforeUrlSession { get; } = "SESSION_URL_BEFORE";
    }
}