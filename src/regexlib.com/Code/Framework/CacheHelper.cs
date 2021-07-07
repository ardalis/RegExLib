using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace RegExLib.Framework {
    /// <summary>
    /// Summary description for CacheHelper
    /// </summary>
    public class CacheHelper {
        private CacheHelper() { }

        private static DateTime expiryDuration {
            get {
                return DateTime.Now.AddSeconds(Globals.CacheTimeoutSeconds);
            }
        }

        public static void InvalidateCache(string keyToInvalidate) {
            HttpRuntime.Cache.Remove(keyToInvalidate);
        }

        public static void Insert(string cacheKey, object item) {
            CacheHelper.Insert(cacheKey, item, null);
        }

        public static void Insert(string cacheKey, object item, CacheDependency dependencies) {
            HttpRuntime.Cache.Insert(cacheKey, item, dependencies, expiryDuration, TimeSpan.Zero);
        }
    }
}