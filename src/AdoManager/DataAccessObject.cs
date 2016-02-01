using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AdoManager
{
    public static class DataAccessObject
    {
        public static ConnectionManager Conn = ConnectionManager.GetDefaultConnection();

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
    }
}
