using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

using RegExLib.Framework;

namespace RegExLib.Data {
    
    public class DataObjectHelpers<T> where T : IDataObject, new() {

        private DataObjectHelpers() { }

        internal static T Fill(IDataReader reader) {
            T dataItem = default(T);
            if (reader.Read()) {
                dataItem = new T();
                dataItem.Fill(reader);
            }

            return dataItem;
        }


        internal static List<T> FillList(IDataReader reader) {
            List<T> dataItems = null;
            if (reader.Read()) {
                dataItems = new List<T>();
                do {
                    T dataItem = new T();
                    dataItem.Fill(reader);
                    dataItems.Add(dataItem);
                } while (reader.Read());
            }
            return dataItems;
        }


        internal static T Fetch(string sql, string cacheKey, bool forceRefresh, SqlParameter[] parameters) {

            T item = default(T);
            HttpContext ctx = HttpContext.Current;

            if (ctx.Cache[cacheKey] == null || forceRefresh) {
                SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        item = DataObjectHelpers<T>.Fill(reader);
                    }

                    if (item != null)
                    {
                        System.Web.Caching.SqlCacheDependency tableDependency;
                        tableDependency = new System.Web.Caching.SqlCacheDependency("Production", item.DependsOnTable);
                        TraceHelper.Write("List" + typeof(T).Name + " Fetch", "Created Cache Dependency on " + item.DependsOnTable);
                        CacheHelper.Insert(cacheKey, item, tableDependency);
                        //ctx.Cache.Insert(cacheKey, item, null, DateTime.Now.AddSeconds(Globals.CacheTimeout), TimeSpan.Zero);
                    }
                }
                finally
                {
                    if (cmd.Connection != null) cmd.Connection.Close();
                }
            }
            return (T) ctx.Cache[cacheKey];
        }



        // overload which doesn't take a parameter array 
        internal static List<T> FetchList(string sql, string cacheKey, int maximumRows, int startRowIndex, IComparer<T> comparer) {
            return DataObjectHelpers<T>.FetchList(sql, cacheKey, maximumRows, startRowIndex, comparer, null);    
        }


        internal static List<T> FetchList(string sql, string cacheKey, int maximumRows, int startRowIndex, IComparer<T> comparer, SqlParameter[] parameters) {
            
            List<T> items = null;

            HttpContext ctx = HttpContext.Current;
            TraceHelper.Write("List" + typeof(T).Name + " FetchList", "Entering");

            try
            {
                if (ctx.Cache[cacheKey] == null)
                {
                    TraceHelper.Write("List" + typeof(T).Name + " FetchList", "Reading from data source.");
                    SqlCommand cmd = new SqlCommand(sql, Globals.ApplicationConnection);
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            items = DataObjectHelpers<T>.FillList(reader);
                        }

                        if (items != null)
                        {
                            System.Web.Caching.SqlCacheDependency tableDependency;
                            tableDependency = new System.Web.Caching.SqlCacheDependency("Production", items[0].DependsOnTable);
                            TraceHelper.Write("List" + typeof(T).Name + " FetchList", "Created Cache Dependency on " + items[0].DependsOnTable);
                            CacheHelper.Insert(cacheKey, items, tableDependency);
                            //ctx.Cache.Insert(cacheKey, items, null, DateTime.Now.AddSeconds(Globals.CacheTimeout), TimeSpan.Zero);
                        }
                    }
                    finally
                    {
                        if (cmd.Connection != null) cmd.Connection.Close();
                    }
                }
                else
                {
                    TraceHelper.Write("List" + typeof(T).Name + " FetchList", "Reading from cache.");
                }

                items = ctx.Cache[cacheKey] as List<T>;

                if (items != null)
                {
                    if (comparer != null)
                        items.Sort(comparer);
                    DataObjectHelpers<T>.Page(ref items, maximumRows, startRowIndex);
                }

                return items;
            }
            finally
            {
                TraceHelper.Write("List" + typeof(T).Name + " FetchList", "Exiting");
            }
        }



        internal static void Page(ref List<T> data, int maximumRows, int startRowIndex) {

            if (data.Count > 0) {
                if (maximumRows > 0 && startRowIndex >= 0) {
                    List<T> tmpColl = null;

                    int remainingRowCount = data.Count - startRowIndex;
                    int count = (remainingRowCount >= maximumRows) ? maximumRows : remainingRowCount;

                    if (count > 0) {
                        tmpColl = new List<T>();
                        tmpColl.AddRange(data.GetRange(startRowIndex, count));
                    }
                    data = tmpColl;
                }
            }
        }

        
    }
}