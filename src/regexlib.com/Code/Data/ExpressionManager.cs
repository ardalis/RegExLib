using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;

using RegExLib.Framework;
using System.Text;
using System.Web.Profile;

using Ardalis.Framework.Diagnostics;

namespace RegExLib.Data
{

    public class ExpressionManager
    {

        private ExpressionManager() { }

        public static void GetCounts(out int totalExpressions, out int totalAuthors)
        {
            String cacheKey = "ExpressionManager.GetCounts";

            Object[] values = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                values = context.Cache[cacheKey] as Object[];
            }

            if (values == null)
            {
                using (SqlCommand cmd = new SqlCommand("rxl_ExpressionsGetCounts", Globals.ApplicationConnection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter totalExpressionsParam = new SqlParameter("@totalExpressions", SqlDbType.Int);
                        totalExpressionsParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(totalExpressionsParam);
                        SqlParameter totalAuthorsParam = new SqlParameter("@totalAuthors", SqlDbType.Int);
                        totalAuthorsParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(totalAuthorsParam);

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        values = new Object[] {
						 cmd.Parameters[ "@totalExpressions" ].Value
						,cmd.Parameters[ "@totalAuthors" ].Value
					};

                        System.Web.Caching.SqlCacheDependency tableDependency;
                        tableDependency = new System.Web.Caching.SqlCacheDependency("Production", "rxl_Expression");
                        CacheHelper.Insert(cacheKey, values, tableDependency);
                    }
                    finally
                    {
                        if (cmd.Connection != null)
                            cmd.Connection.Close();
                    }
                }
            }

            totalExpressions = (Int32)values[0];
            totalAuthors = (Int32)values[1];
        }

        public static bool AddRating(int expressionId, int rating, object userId)
        {

            int rowsAffected = 0;
            string sql = "rxl_RatingInsert";
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExpressionId", expressionId);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@UserId", userId);

                cmd.Connection.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    CacheHelper.InvalidateCache("ExpressionId_" + expressionId.ToString());
                    CacheHelper.InvalidateCache("ExpressionListByCategoryId");
                    CacheHelper.InvalidateCache("ExpressionListByUserId");
                }
            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static int GetUserRating(int expressionId, object userId)
        {

            int rating = 0;
            string sql = "rxl_RatingUserRating";
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExpressionId", expressionId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                cmd.Connection.Open();
                rating = (int)cmd.ExecuteScalar();

            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }

            return rating;
        }


        public static void DeleteExpression(int expressionId)
        {

            int rowsAffected = 0;
            string sql = "rxl_ExpressionDelete";
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExpressionId", expressionId);

                cmd.Connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    CacheHelper.InvalidateCache("ExpressionId_" + expressionId.ToString());
                    CacheHelper.InvalidateCache("ExpressionListByCategoryId");
                    CacheHelper.InvalidateCache("ExpressionListByUserId");
                }
            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }


        }


        public static Expression CreateExpression(Expression e)
        {
            return CreateExpression(e.ProviderId, e.Title, e.Pattern, e.MatchingText, e.NonMatchingText, e.Enabled, e.Source, e.Description);
        }

        /// <summary>
        /// This is called from the web application
        /// </summary>
        public static Expression CreateExpression(string title, string pattern, string matchingText, string nonMatchingText, bool enabled, string source, string description)
        {
            return CreateExpression(0, title, pattern, matchingText, nonMatchingText, enabled, source, description);
        }


        /// <summary>
        /// This is called from the Web Service
        /// </summary>
        public static Expression CreateExpression(
                int providerId, string title, string pattern, string matchingText, string nonMatchingText,
                bool enabled, string source, string description
            )
        {
            string sql = "rxl_ExpressionInsert";
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProviderId", providerId);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Pattern", pattern);
                cmd.Parameters.AddWithValue("@MatchingText", matchingText);
                cmd.Parameters.AddWithValue("@NonMatchingText", nonMatchingText);
                cmd.Parameters.AddWithValue("@Enabled", enabled);
                cmd.Parameters.AddWithValue("@Source", string.IsNullOrEmpty(source) ? string.Empty : source);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@UserProviderKey", Membership.GetUser().ProviderUserKey);

                cmd.Connection.Open();
                int newId = 0;
                object tmp = cmd.ExecuteScalar();
                int.TryParse(tmp.ToString(), out newId);

                return GetExpression(newId, true);
            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }


        }


        public static Expression UpdateExpression(Expression expression)
        {
            string sql = "rxl_ExpressionUpdate";

            if (!expression.IsDirty || expression == null) return expression;

            // TODO: write save code here
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExpressionId", expression.Id);
                cmd.Parameters.AddWithValue("@ProviderId", expression.ProviderId);
                cmd.Parameters.AddWithValue("@Title", expression.Title);

                cmd.Parameters.AddWithValue("@Pattern", expression.Pattern);
                cmd.Parameters.AddWithValue("@MatchingText", expression.MatchingText);
                cmd.Parameters.AddWithValue("@NonMatchingText", expression.NonMatchingText);
                cmd.Parameters.AddWithValue("@Enabled", expression.Enabled);
                cmd.Parameters.AddWithValue("@Source", string.IsNullOrEmpty(expression.Source) ? string.Empty : expression.Source);
                cmd.Parameters.AddWithValue("@Description", expression.Description);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                // TODO: Should this also invalidate the ListAll cache entry?
                return ExpressionManager.GetExpression(expression.Id, true);
            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }
        }


        public static Expression GetExpression(int expressionId)
        {
            return ExpressionManager.GetExpression(expressionId, false);
        }

        public static Expression GetExpression(int expressionId, bool forceRefresh)
        {
            string sql = "rxl_ExpressionGet";
            string cacheKey = "ExpressionId_" + expressionId.ToString();
            SqlParameter p1 = new SqlParameter("@ExpressionId", expressionId);
            return DataObjectHelpers<Expression>.Fetch(sql, cacheKey, forceRefresh, new SqlParameter[] { p1 });
        }

        #region ListExpressionsByAuthor
        // Lists expressions for the current user
        public static List<Expression> ListExpressionsByAuthor()
        {
            return ListExpressionsByAuthor(Guid.Empty, string.Empty);
        }

        public static List<Expression> ListExpressionsByAuthor(string sortExpression)
        {
            return ListExpressionsByAuthor(Guid.Empty, string.Empty, -1, -1);
        }

        public static List<Expression> ListExpressionsByAuthor(int maximumRows, int startRowIndex)
        {
            return ListExpressionsByAuthor(Guid.Empty, string.Empty, maximumRows, startRowIndex);
        }

        public static List<Expression> ListExpressionsByAuthor(string sortExpression, int maximumRows, int startRowIndex)
        {
            return ListExpressionsByAuthor(Guid.Empty, sortExpression, maximumRows, startRowIndex);
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId)
        {
            return ListExpressionsByAuthor(authorId, string.Empty);
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId, string sortExpression)
        {
            return ListExpressionsByAuthor(authorId, string.Empty, -1, -1);
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId, int maximumRows, int startRowIndex)
        {
            return ListExpressionsByAuthor(authorId, string.Empty, maximumRows, startRowIndex);
        }

        public static List<Expression> ListExpressionsByAuthor(bool enabledOnly)
        {
            return ListExpressionsByAuthor(Guid.Empty, enabledOnly);
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId, bool enabledOnly)
        {
            return ListExpressionsByAuthor(authorId, string.Empty, enabledOnly);
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId, string sortExpression, bool enabledOnly)
        {
            return ListExpressionsByAuthor(authorId, string.Empty, -1, -1, enabledOnly);
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId, int maximumRows, int startRowIndex, bool enabledOnly)
        {
            return ListExpressionsByAuthor(authorId, string.Empty, maximumRows, startRowIndex, enabledOnly);
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId, string sortExpression, int maximumRows, int startRowIndex)
        {
            return ListExpressionsByAuthor(authorId, sortExpression, maximumRows, startRowIndex, null);
        }

                public static List<Expression> ListExpressionsByAuthorPaged(Guid authorId,
            int pageSize,
            int page)
                {
                    return ListExpressionsByAuthorPaged(authorId, pageSize, page, null);
                }

        public static List<Expression> ListExpressionsByAuthorPaged(Guid authorId,
            int pageSize,
            int page,
            bool? enabledOnly)
        {
            Debug.Print("ListExpressionsByAuthorPaged(pagesize: " + pageSize + ",page:" + page + ")");
            int startRowIndex = pageSize * (page - 1);
            string sql = "rxl_ExpressionListByAuthorId";

            if (authorId == Guid.Empty)
            {
                Author author = AuthorManager.GetCurrentAuthor();
                if (author == null) return new List<Expression>();
                authorId = author.Id;
            }

            string cacheKey = "ExpressionListPagedByAuthorId_" + authorId.ToString();
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@authorId", authorId);

            parameters.Add(p1);
            if (enabledOnly.HasValue)
            {
                parameters.Add(new SqlParameter("@enabledOnly", enabledOnly));
            }

            ExpressionSorter comparer = GetComparer("");
            return DataObjectHelpers<Expression>.FetchList(sql, cacheKey, pageSize, startRowIndex, comparer, parameters.ToArray());
        }

        public static List<Expression> ListExpressionsByAuthor(Guid authorId, string sortExpression, int maximumRows, int startRowIndex, bool? enabledOnly)
        {
            string sql = "rxl_ExpressionListByAuthorId";

            if (authorId == Guid.Empty)
            {
                Author author = AuthorManager.GetCurrentAuthor();
                authorId = author.Id;
            }

            string cacheKey = "ExpressionListByAuthorId_" + authorId.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p1 = new SqlParameter("@authorId", authorId);

            parameters.Add(p1);

            if (enabledOnly.HasValue)
            {
                parameters.Add(new SqlParameter("@enabledOnly", enabledOnly));
            }

            ExpressionSorter comparer = GetComparer(sortExpression);
            return DataObjectHelpers<Expression>.FetchList(sql, cacheKey, maximumRows, startRowIndex, comparer, parameters.ToArray());
        }

        #endregion

        #region ListExpressionsByCategory

        public static List<Expression> ListExpressionsByCategory(int categoryId)
        {
            return ListExpressionsByCategory(categoryId, string.Empty);
        }

        public static List<Expression> ListExpressionsByCategory(int categoryId, string sortExpression)
        {
            return ListExpressionsByCategory(categoryId, string.Empty, -1, -1);
        }

        public static List<Expression> ListExpressionsByCategory(int categoryId, int maximumRows, int startRowIndex)
        {
            return ListExpressionsByCategory(categoryId, string.Empty, maximumRows, startRowIndex);
        }

        public static List<Expression> ListExpressionsByCategory(int categoryId, string sortExpression, int maximumRows, int startRowIndex)
        {
            string sql = "rxl_ExpressionListByCategoryId";
            string cacheKey = "ExpressionListByCategoryId" + categoryId.ToString();

            SqlParameter p1 = new SqlParameter("@categoryId", categoryId);

            ExpressionSorter comparer = GetComparer(sortExpression);
            return DataObjectHelpers<Expression>.FetchList(sql, cacheKey, maximumRows, startRowIndex, comparer, new SqlParameter[] { p1 });
        }

        public static List<Expression> ListExpressionsbyCategoryPaged(int categoryId, string sortExpression, int pageSize, int pageId)
        {
            Debug.Print("ListExpressionsbyCategoryPaged(pagesize: " + pageSize + ",page:" + pageId + ")");
            int startRowIndex = pageSize * (pageId - 1);
            string sql = "rxl_ExpressionListByCategoryId";

            string cacheKey = "ListExpressionsbyCategoryPaged_" + categoryId.ToString();

            SqlParameter p1 = new SqlParameter("@categoryId", categoryId);

            ExpressionSorter comparer = GetComparer(sortExpression);
            return DataObjectHelpers<Expression>.FetchList(sql, cacheKey, pageSize, startRowIndex, comparer, new SqlParameter[] { p1 });
        }
        #endregion

        public static DataTable ListTopContributors()
        {
            string sql = "rxl_Top10Contributors";
            string cacheKey = "TopContributorList";

            // return DataObjectHelpers<Author>.FetchList(sql, cacheKey, 10, 0, null);

            HttpContext ctx = HttpContext.Current;

            DataTable tbl = ctx.Cache[cacheKey] as DataTable;
            if (tbl == null)
            {

                SqlCommand cmd = new SqlCommand();
                tbl = new DataTable("TopContributors");
                cmd.Connection = new SqlConnection(Globals.ApplicationConnectionString);
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tbl);
                CacheHelper.Insert(cacheKey, tbl);
            }

            return tbl;
        }

        #region ListExpressionsBySearch

        public static List<Expression> ListExpressionsBySearch(string keywords)
        {
            return ListExpressionsBySearch(keywords, -1, -1, string.Empty, -1, -1);
        }

        public static List<Expression> ListExpressionsBySearch(string keywords, int maximumRows, int startRowIndex)
        {
            return ListExpressionsBySearch(keywords, -1, -1, string.Empty, maximumRows, startRowIndex);
        }

        public static List<Expression> ListExpressionsBySearch(string keywords, int categoryId, int minimumRating,
                int maximumRows, int startRowIndex)
        {
            return ListExpressionsBySearch(keywords, categoryId, minimumRating, string.Empty, maximumRows, startRowIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<Expression> ListExpressionsBySearch(string keywords, int categoryId, int minimumRating,
                string sortExpression, int maximumRows, int page)
        {

            int startRowIndex = maximumRows * (page - 1);

            string sql = "rxl_ExpressionListBySearch";

            string cacheKey = "ExpressionListBySearch";
            if (!string.IsNullOrEmpty(keywords)) cacheKey += "_" + keywords;
            cacheKey += "_" + categoryId.ToString();
            cacheKey += "_" + minimumRating.ToString();

            SqlParameter p1 = new SqlParameter("@WhereClause", BuildWhereClause(keywords, categoryId, minimumRating));

            ExpressionSorter comparer = GetComparer(sortExpression);
            return DataObjectHelpers<Expression>.FetchList(sql, cacheKey, maximumRows, startRowIndex, comparer, new SqlParameter[] { p1 });
        }

        public static DataTable ListExpressionsBySearchDS(string keywords, string patternText, int minimumRating,
                string sortExpression, int maximumRows)
        {
            string sql = "rxl_ExpressionListBySearch";
            string cacheKey = "rxl_ExpressionListBySearch" + "_" + keywords + "_" + minimumRating + "_" + sortExpression;

            HttpContext ctx = HttpContext.Current;

            if (ctx.Cache[cacheKey] == null)
            {

                SqlCommand cmd = new SqlCommand();
                DataTable tbl = new DataTable("Expressions");
                cmd.Connection = new SqlConnection(Globals.ApplicationConnectionString);
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter myParameter = new SqlParameter("@WhereClause", BuildWhereClause(keywords, patternText, 0, minimumRating));
                System.Diagnostics.Debug.Print(myParameter.Value.ToString());
                cmd.Parameters.Add(myParameter);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(tbl);
                CacheHelper.Insert(cacheKey, tbl);
            }

            return ctx.Cache[cacheKey] as DataTable;
        }

        #endregion

        #region ListExpressionsByMostRecent

        public static List<Expression> ListExpressionsByMostRecent(int count, Boolean enabledOnly)
        {
            string sql = "rxl_ExpressionListByLatest";
            string cacheKey = "ExpressionListByLatest";

            SqlParameter p1 = new SqlParameter("@count", count);
            SqlParameter p2 = new SqlParameter("@enabledOnly", SqlDbType.Bit);
            p2.Value = enabledOnly;

            // not sorted because they are returned as Date Desc
            return DataObjectHelpers<Expression>.FetchList(sql, cacheKey, -1, -1, null, new SqlParameter[] { p1, p2 });
        }

        public static List<Expression> ListExpressionsByMostRecent(int count)
        {
            return ListExpressionsByMostRecent(count, false);
        }

        #endregion


        #region Helpers

        private static string BuildWhereClause(string keywordText, string patternText, int categoryId, int minimumRating)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("WHERE [Enabled] = 1 ");

            if (!string.IsNullOrEmpty(keywordText))
            {
                string[] keywords = keywordText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < keywords.Length; i++)
                {
                    sb.AppendFormat("{1} (r.[Title] LIKE '%{0}%' OR r.[Description] LIKE '%{0}%') ", 
                        keywords[i].Trim().Replace("'", "''"), (i == 0) ? "AND" : "OR");
                }
            }

            if (!string.IsNullOrEmpty(patternText))
            {
                sb.AppendFormat("AND r.[Pattern] LIKE '%{0}%' ", patternText);
            }

            if (categoryId > 0)
            {
                sb.AppendFormat("AND rc.[CategoryId] = {0} ", categoryId);
            }

            if (minimumRating > 0)
            {
                sb.AppendFormat("AND r.[Rating] >= {0}  ", minimumRating);
            }

            return sb.ToString();
        }

        private static string BuildWhereClause(string keywordText, int categoryId, int minimumRating)
        {
            return BuildWhereClause(keywordText, "", categoryId, minimumRating);
        }


        #endregion

        #region Inner sorter class

        private static ExpressionSorter GetComparer(string sortExpression)
        {
            ExpressionSorter comparer = null;
            if (!String.IsNullOrEmpty(sortExpression))
            {
                string[] sortInfo = sortExpression.Split(' ');
                string sortField = sortInfo[0];
                bool ascending = (sortInfo.Length == 1);
                comparer = new ExpressionSorter(sortField, ascending);
            }
            return comparer;
        }

        private class ExpressionSorter : IComparer<Expression>
        {
            string sortExpression;
            bool sortAscending;

            public ExpressionSorter(string sortExpression, bool sortAscending)
            {
                this.sortExpression = sortExpression.ToLower();
                this.sortAscending = sortAscending;
            }

            #region IComparer Members
            public int Compare(Expression x, Expression y)
            {
                Expression e1, e2;
                if (sortAscending)
                {
                    e1 = x; e2 = y;
                }
                else
                {
                    e1 = y; e2 = x;
                }

                switch (sortExpression)
                {
                    case "created":
                        return e1.DateCreated.CompareTo(e2.DateCreated);
                    case "modified":
                        return e1.DateModified.CompareTo(e2.DateModified);
                    case "description":
                        return e1.Description.CompareTo(e2.Description);
                    case "enabled":
                        return e1.Enabled.CompareTo(e2.Enabled);
                    case "isdirty":
                        return e1.IsDirty.CompareTo(e2.IsDirty);
                    case "matchingtext":
                        return e1.MatchingText.CompareTo(e2.MatchingText);
                    case "nonmatchingtext":
                        return e1.NonMatchingText.CompareTo(e2.NonMatchingText);
                    case "pattern":
                        return e1.Pattern.CompareTo(e2.Pattern);
                    case "providerid":
                        return e1.ProviderId.CompareTo(e2.ProviderId);
                    case "rating":
                        return e1.Rating.CompareTo(e2.Rating);
                    case "source":
                        return e1.Source.CompareTo(e2.Source);
                    case "title":
                        return e1.Title.CompareTo(e2.Title);
                    case "userid":
                        return e1.AuthorId.CompareTo(e2.AuthorId);
                    default:
                        return e1.Id.CompareTo(e2.Id);
                }
            }
            #endregion
        }

        #endregion

    }
}
