using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;

using RegExLib.Framework;


namespace RegExLib.Data {

    public class CategoryManager {

        private CategoryManager() { }

        #region Category ( CRUD methods for working with Category objects )

        
        public static Category CreateCategory(Category category) {
            return CreateCategory(category.Description);
        }


        public static Category CreateCategory(string description) {
            string sql = "rxl_CategoryInsert";
            SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Description", description);

                cmd.Connection.Open();
                int newId = (int)cmd.ExecuteScalar();

                return GetCategory(newId, true);
            }
            finally
            {
                if (cmd.Connection != null) cmd.Connection.Close();
            }
        }


        public static Category GetCategory(int categoryId) {
            return GetCategory(categoryId, false);
        }


        public static Category GetCategory(int categoryId, bool forceRefresh) {
            string sql = "rxl_CategoryGet";
            string cacheKey = "CategoryId_" + categoryId.ToString();
            SqlParameter p1 = new SqlParameter("@CategoryId", categoryId);
            return DataObjectHelpers<Category>.Fetch(sql, cacheKey, forceRefresh, new SqlParameter[] { p1 });
        }


        public static List<Category> ListCategories() {
            return ListCategories(string.Empty, -1, -1);
        }

        public static List<Category> ListCategories(string sortExpression) {
            return ListCategories(sortExpression, -1, -1);
        }

        public static List<Category> ListCategories(int maximumRows, int startRowIndex) {
            return ListCategories(string.Empty, maximumRows, startRowIndex);
        }

        /// <param name="sortExpression">fieldName[ DESC]</param>
        /// <returns></returns>
        public static List<Category> ListCategories(string sortExpression, int maximumRows, int startRowIndex) {
            string sql = "rxl_CategoryListByAll";
            string cacheKey = "CategoryListByAll";

            CategorySorter comparer = GetComparer(sortExpression);
            return DataObjectHelpers<Category>.FetchList(sql, cacheKey, maximumRows, startRowIndex, comparer);
        }

        #endregion

        #region Inner sorter class

        private static CategorySorter GetComparer(string sortExpression) {
            CategorySorter comparer = null;
            if (sortExpression.Length > 0) {
                string[] sortInfo = sortExpression.Split(' ');
                string sortField = sortInfo[0];
                bool ascending = (sortInfo.Length == 1);
                comparer = new CategorySorter(sortField, ascending);
            }
            return comparer;
        }


        private class CategorySorter : IComparer<Category> {
            string sortExpression;
            bool sortAscending;

            public CategorySorter(string sortExpression, bool sortAscending) {
                this.sortExpression = sortExpression.ToLower();
                this.sortAscending = sortAscending;
            }

            #region IComparer Members
            public int Compare(Category x, Category y) {
                Category e1, e2;
                if (sortAscending) {
                    e1 = x; e2 = y;
                } else {
                    e1 = y; e2 = x;
                }

                switch (sortExpression) {
                    case "created":
                        return e1.DateCreated.CompareTo(e2.DateCreated);
                    case "modified":
                        return e1.DateModified.CompareTo(e2.DateModified);
                    case "id":
                        return e1.Id.CompareTo(e2.Id);
                    default:
                        return e1.Description.CompareTo(e2.Description);
                }
            }
            #endregion
        }

        #endregion

    }
}
