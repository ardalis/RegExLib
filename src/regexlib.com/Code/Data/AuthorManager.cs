using System;
using System.Web;
using System.Web.Security;

using RegExLib.Framework;


namespace RegExLib.Data {

    public class AuthorManager {

        private AuthorManager() { }


        public static Author GetCurrentAuthor() {
            return AuthorManager.GetCurrentAuthor(false);
        }

        public static Author GetCurrentAuthor(bool forceRefresh) {
            
            MembershipUser currentUser = Membership.GetUser();

            if (currentUser == null) {
                return null;
			}

			Author author = new Author();
			author.Id = new Guid( currentUser.ProviderUserKey.ToString() );
			author.FullName = Convert.ToString( HttpContext.Current.Profile.GetPropertyValue( "FullName" ) );
			return author;

			//string cacheKey = "AuthorUserId_" + currentUser.ProviderUserKey.ToString();
			//string sql = "rxl_AuthorGetByUserId";
			//SqlParameter p1 = new SqlParameter("@UserId", currentUser.ProviderUserKey);
			//return DataObjectHelpers<Author>.Fetch(sql, cacheKey, forceRefresh, new SqlParameter[] { p1 });
        }
	
	}
}
