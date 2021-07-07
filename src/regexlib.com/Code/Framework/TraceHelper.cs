#define TRACE

using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace RegExLib.Framework
{
    /// <summary>
    /// TODO: Remove All References to this before calculating final lines of code
    /// </summary>
    public class TraceHelper
    {
        private TraceHelper() { }
        //[Conditional("TRACE")]
        public static void Write(string message)
        {
            System.Diagnostics.StackTrace trace = new StackTrace();
            System.Reflection.MemberInfo method = trace.GetFrame(1).GetMethod();

            Write(method.DeclaringType.Namespace.ToString() + "." + method.DeclaringType.Name + "." + method.Name, message);
        }

        //[Conditional("TRACE")]
        public static void Write(string category, string message)
        {
            Write(category, message, null);
        }

        //[Conditional("TRACE")]
        public static void Write(string category, string message, Exception myException)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
                context.Trace.Write(category, message, myException);
        }

        //[Conditional("TRACE")]
        public static void Warn(string message)
        {
            Warn(new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, message);
        }

        //[Conditional("TRACE")]
        public static void Warn(string category, string message)
        {
            Warn(category, message, null);
        }

        //[Conditional("TRACE")]
        public static void Warn(string category, string message, Exception myException)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
                context.Trace.Warn(category, message, myException);
        }
    }
}