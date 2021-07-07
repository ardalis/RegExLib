using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;

using RegExLib.Framework;


namespace RegExLib.Data {

    public class CommentManager {

        private CommentManager() { }

        #region Expressions ( CRUD methods for working with CommentInfo objects )

        public static void DeleteComment(CommentInfo comment) {
            DeleteComment(comment.Id);
        }


        public static void DeleteComment(int commentId) {

            string sql = "rxl_CommentDelete";
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CommentId", commentId);

                cmd.Connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0) {
                    CacheHelper.InvalidateCache("CommentId_" + commentId.ToString());
                    CacheHelper.InvalidateCache("CommentListByExpressionId");
                }
            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }

            
        }


        public static CommentInfo CreateComment(CommentInfo comment) {
            return CreateComment(comment.Comment, comment.ExpressionId, comment.Name, comment.Title, comment.Url);
        }


        public static CommentInfo CreateComment(string comment, int expressionId, string name, string title, string url) {
            string sql = "rxl_CommentInsert";
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
            try
            {
                object userId = Guid.Empty;

                if (Membership.GetUser() != null) {
                    userId = Membership.GetUser().ProviderUserKey;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Comment", comment);
                cmd.Parameters.AddWithValue("@ExpressionId", expressionId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Url", url);

                cmd.Connection.Open();
                int newId = 0;
                object tmp = cmd.ExecuteScalar();
                int.TryParse(tmp.ToString(), out newId);

                return GetComment(newId, true);
            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }


        }

        public static CommentInfo GetComment(int commentId) {
            return GetComment(commentId, false);
        }

        public static CommentInfo GetComment(int commentId, bool forceRefresh) {
            string sql = "rxl_CommentGet";
            string cacheKey = "CommentId_" + commentId.ToString();
            SqlParameter p1 = new SqlParameter("@CommentId", commentId);
            return DataObjectHelpers<CommentInfo>.Fetch(sql, cacheKey, forceRefresh, new SqlParameter[] { p1 });
        }


        public static List<CommentInfo> ListCommentsByExpression(int expressionId) {
            return ListCommentsByExpression(expressionId, string.Empty);
        }


        public static List<CommentInfo> ListCommentsByExpression(int expressionId, string sortExpression) {
            return ListCommentsByExpression(expressionId, string.Empty, -1, -1);
        }

        public static List<CommentInfo> ListCommentsByExpression(int expressionId, int maximumRows, int startRowIndex) {
            return ListCommentsByExpression(expressionId, "created DESC", maximumRows, startRowIndex);
        }

        /// <param name="sortExpression">fieldName[ DESC]</param>
        /// <returns></returns>
        public static List<CommentInfo> ListCommentsByExpression(int expressionId, string sortExpression, int maximumRows, int startRowIndex) {
            string sql = "rxl_CommentListByExpressionId";
            string cacheKey = "CommentListByExpressionId_" + expressionId.ToString();

            SqlParameter p1 = new SqlParameter("@expressionId", expressionId);

            CommentSorter comparer = GetComparer( sortExpression);
            return DataObjectHelpers<CommentInfo>.FetchList(sql, cacheKey, maximumRows, startRowIndex, comparer, new SqlParameter[] { p1 });
        }

        #endregion

        #region ListCommentsByMostRecent

        public static List<CommentInfo> ListCommentsByMostRecent(int count) {
            string sql = "rxl_CommentListByLatest";
            string cacheKey = "CommentListByLatest";

            SqlParameter p1 = new SqlParameter("@count", count);

            // not sorted because they are returned as Date Desc
            return DataObjectHelpers<CommentInfo>.FetchList(sql, cacheKey, -1, -1, null, new SqlParameter[] { p1 });
        }

        #endregion


        #region Inner sorter class

        private static CommentSorter GetComparer(string sortExpression) {
            CommentSorter comparer = null;
            if (sortExpression.Length > 0) {
                string[] sortInfo = sortExpression.Split(' ');
                string sortField = sortInfo[0];
                bool ascending = (sortInfo.Length == 1);
                comparer = new CommentSorter(sortField, ascending);
            }
            return comparer;
        }

        private class CommentSorter : IComparer<CommentInfo> {
            string sortExpression;
            bool sortAscending;

            public CommentSorter(string sortExpression, bool sortAscending) {
                this.sortExpression = sortExpression.ToLower();
                this.sortAscending = sortAscending;
            }

            #region IComparer Members
            public int Compare(CommentInfo x, CommentInfo y) {
                CommentInfo e1, e2;
                if (sortAscending) {
                    e1 = x; e2 = y;
                } else {
                    e1 = y; e2 = x;
                }

                switch (sortExpression) {
                    case "comment":
                        return e1.Comment.CompareTo(e2.Comment);
                    case "created":
                        return e1.DateCreated.CompareTo(e2.DateCreated);
                    case "modified":
                        return e1.DateModified.CompareTo(e2.DateModified);
                    case "expressionid":
                        return e1.ExpressionId.CompareTo(e2.ExpressionId);
                    case "name":
                        return e1.Name.CompareTo(e2.Name);
                    case "title":
                        return e1.Title.CompareTo(e2.Title);
                    case "url":
                        return e1.Url.CompareTo(e2.Url);
                    default:
                        return e1.Id.CompareTo(e2.Id);
                }
            }
            #endregion
        }

        #endregion
       
    }
}