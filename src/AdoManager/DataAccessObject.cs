using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoManager
{
    public static class DataAccessObject
    {
        public static ConnectionManager Conn = ConnectionManager.GetDefaultConnection();

        public static DataTable GetFrom(string table, params Condition[] conditions)
        {
            return GetFrom(table, new string[] { }, conditions);
        }

        public static DataTable GetFrom(string table, string[] columns, params Condition[] conditions)
        {
            var cols = columns.Any() ? string.Join(", ", columns) : "*";

            string query = $"SELECT {cols} FROM Xomorod.dbo.{table} ";

            if (conditions.Any())
            {
                query += $@"WHERE {conditions.ConvertToQuery()}";
            }

            var result = Conn.ExecuteDataSet(query);

            return result;
        }
    }
}
