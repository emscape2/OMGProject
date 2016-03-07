using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BenchmarkingFramework
{
    class DatabaseConnection
    {
        SQLiteConnection m_dbConnection;

        public DatabaseConnection()
        {
            m_dbConnection =
new SQLiteConnection("Data Source=" +Form1.location +"/Database.db;Version=3;");
            m_dbConnection.Open();


        }

        public void AddResults(TestData data)
        {
            int key = getKey("results");
            QueryDatabase("INSERT INTO results (id, arraytype, datatype, arraysize, buildtime, inserttime, lookuptime, deletetime, memsize) VALUES (" + key.ToString() + ", '" + data.arrayType + "', '" + data.dataType + "', " + data.arraySize.ToString() + ", '" + data.buildTime.ToString() + "', '" + data.insertTime.ToString() + "', '" + data.lookupTime.ToString() + "', '" + data.deleteTime.ToString() + "', " + data.memorySize.ToString() + ");", true);
        }

        public int getKey(string table, string id)
        {
            object[] temp = QueryDatabase("SELECT MAX(" + id + ") FROM " + table + ";", true);
            if (temp == null || temp.Length == 0 || temp[0].ToString() == "")
            {
                return 0;
            }
            return Convert.ToInt32(temp[0]) + 1;
        }
        public int getKey(string table)
        {
            return getKey(table, "id");
        }

        public object[] QueryDatabase(string query, bool isReadQuery)
        {
            var dbcmd = m_dbConnection.CreateCommand();
            dbcmd.CommandText = query;
            var reader = dbcmd.ExecuteReader();
            object[] output = null;
            if (isReadQuery)
            {
                var values = new List<object>();

                int current = 0;
                while (reader.Read())
                {
                    
                    
                        if (reader.HasRows)
                        {
                            current = 0;
                        }
                        values.Add(reader.GetValue(current++));
                    
                }
                output = values.ToArray();
            }
            return output;
        }
    }
}
