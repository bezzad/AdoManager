using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace AdoManager
{
    public static class DataAccessObject
    {
        public static ConnectionManager Conn = ConnectionManager.GetDefaultConnection();
        
        #region Sync Methods


        public static IEnumerable<dynamic> GetFrom(string table, params Condition[] conditions)
        {
            return GetFrom(table, new string[] { }, conditions);
        }
        
        public static IEnumerable<dynamic> GetFrom(string table, string[] columns, params Condition[] conditions)
        {
            var cols = columns.Any() ? string.Join(", ", columns) : "*";

            string query = $"SELECT {cols} FROM Xomorod.dbo.{table} ";

            if (conditions.Any())
            {
                query += $@"WHERE {conditions.ConvertToQuery()}";
            }

            var result = Conn.SqlConn.Query<dynamic>(query);

            return result;
        }

        public static IEnumerable<dynamic> GetFromQuery(string query, ExpandoObject param)
        {
            var result = Conn.SqlConn.Query<dynamic>(query, param);

            return result;
        }


        #endregion
        
        #region Async Methods

        public static async Task<IEnumerable<dynamic>> GetFromAsync(string table, params Condition[] conditions)
        {
            return await GetFromAsync(table, new string[] { }, conditions);
        }

        public static async Task<IEnumerable<dynamic>> GetFromAsync(string table, string[] columns, params Condition[] conditions)
        {
            var cols = columns.Any() ? string.Join(", ", columns) : "*";

            string query = $"SELECT {cols} FROM Xomorod.dbo.{table} ";

            if (conditions.Any())
            {
                query += $@"WHERE {conditions.ConvertToQuery()}";
            }

            var result = await Conn.SqlConn.QueryAsync<dynamic>(query);

            return result;
        }

        public static async Task<IEnumerable<dynamic>> GetFromQueryAsync(string query)
        {
            var result = await Conn.SqlConn.QueryAsync<dynamic>(query);

            return result;
        }

        public static async Task<IEnumerable<dynamic>> GetFromQueryAsync(string query, ExpandoObject param)
        {
            var result = await Conn.SqlConn.QueryAsync<dynamic>(query, param);

            return result;
        }

        #endregion
    }
}
