using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RegExLib {

    public class Globals {
        private Globals() { }

        private const long defaultCacheTimeoutSeconds = 3600; /* 1 hour */
        private static long cacheTimeoutSeconds = 30;
        public static long CacheTimeoutSeconds {
            get {
                if (cacheTimeoutSeconds == -1) {
                    long.TryParse(ConfigurationManager.AppSettings["DefaultCacheTimeoutSeconds"], out cacheTimeoutSeconds);
                    if (cacheTimeoutSeconds <= 0) {
                        cacheTimeoutSeconds = defaultCacheTimeoutSeconds;
                    }
                }
                return cacheTimeoutSeconds;
            }
        }


        private const string defaultApplicationConnectionStringName = "LocalSqlServer";
        private static string applicationConnectionStringName;
        private static string ApplicationConnectionStringName {
            get {
                if (applicationConnectionStringName == null) {
                    applicationConnectionStringName = ConfigurationManager.AppSettings["ApplicationConnectionStringName"] as string;

                    if (applicationConnectionStringName == null) {
                        applicationConnectionStringName = defaultApplicationConnectionStringName;
                    }
                }
                return applicationConnectionStringName;
            }
        }

        private const string defaultApplicationConnectionString = "server=lyris.orcsweb.com;database=regexlib;uid=INVALID;pwd=enoharng";
        private static string applicationConnectionString;
        public static string ApplicationConnectionString {
            get {
                if (applicationConnectionString == null) {
                    ConnectionStringSettings cnnStrng = ConfigurationManager.ConnectionStrings[ApplicationConnectionStringName] as ConnectionStringSettings;

                    if (cnnStrng == null)
                    {
                        applicationConnectionString = defaultApplicationConnectionString;
                    } else {
                        applicationConnectionString = cnnStrng.ConnectionString;
                    }
                }
                return applicationConnectionString;
            }
        }

        public static SqlConnection ApplicationConnection {
            get { return new SqlConnection(ApplicationConnectionString); }
        }


        /// <summary>
        /// Retrieves the base part of the url path from the Web.Config file.
        /// </summary>
        /// // this is the application name when hosted on localhost
        /// // would be an empty string when hosted on the live site
        public static string UrlStartPath {
            get {
				String urlStartPath = HttpRuntime.AppDomainAppVirtualPath;
				if ( !urlStartPath.EndsWith( "/" ) )
				{
					urlStartPath += "/";
				}
				return urlStartPath;
            }
        }


        public static string BaseUrl {
            get {
                string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
                if (((port == null) || (port == "80")) || (port == "443")) {
                    port = string.Empty;
                } else {
                    port = ":" + port;
                }
                string secure = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
                if ((secure == null) || (secure == "0")) {
                    secure = "http://";
                } else {
                    secure = "https://";
                }
				String appPath = UrlStartPath;
                return (secure + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port ) + appPath;
            }
        }

        private const string defaultMailServer = "localhost";
        private static string mailServer;
        public static string MailServer
        {
            get
            {
                if (mailServer == null)
                {
                    mailServer = ConfigurationManager.AppSettings["MailServer"] as string;

                    if (mailServer == null)
                    {
                        mailServer = defaultMailServer;
                    }
                }
                return mailServer;
            }
        }

        private const string defaultAdminEmail = "yourname@yourserver.com";
        private static string adminEmail;
        public static string AdminEmail
        {
            get
            {
                if (adminEmail == null)
                {
                    adminEmail = ConfigurationManager.AppSettings["AdminEmail"] as string;

                    if (adminEmail == null)
                    {
                        adminEmail = defaultAdminEmail;
                    }
                }
                return adminEmail;
            }
        }


        private const int defaultFeedItemCount = 20;
        private static int feedItemCount = -1;
        public static int FeedItemCount {
            get {
                if (feedItemCount == -1) {
                    int.TryParse(ConfigurationManager.AppSettings["FeedItemCount"], out feedItemCount);
                    if (feedItemCount <= 0) {
                        feedItemCount = defaultFeedItemCount;
                    }
                }
                return feedItemCount;
            }
        }

        public static void NotifyAdministrator(string body, string email)
        {
             try
            {
                System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress("donotreply@regexlib.com");
                System.Net.Mail.MailAddress toAddress = new System.Net.Mail.MailAddress(Globals.AdminEmail);
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddress, toAddress);
                message.Subject = "RegExLib Notification";
                message.Body = body + "\n" + email;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Globals.MailServer);

                client.Send(message);
            }
            catch {}

        }

    }
}
