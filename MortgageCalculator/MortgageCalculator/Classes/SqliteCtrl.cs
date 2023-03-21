//※NuGetにて、Microsoft.Data.Sqliteをインストールして使用する
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator
{
    public class SqliteCtrl
    {
        //*************************************************************************
        public static bool CreateDbFile(string dbFileName, string saveDir)
        {
            string bufDir = saveDir;
            string dataSource = "";

#if WINDOWS
        if (saveDir.LastIndexOf("\\") != dbFileName.Length - 1) bufDir += "\\";
        dataSource = $"Data Source={bufDir}{dbFileName};";
#elif ANDROID
        if (saveDir.LastIndexOf("/") != saveDir.Length - 1) bufDir += "/";
        dataSource = $"Data Source={bufDir}{dbFileName};";
#endif
            try
            {
                using (var connection = new SqliteConnection(dataSource))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //*************************************************************************

        public static bool CreateTable(string tableName, string[] columns, string dbFilePath)
        {
            try
            {
                string dataSource = $"Data Source={dbFilePath};";
                using (var connection = new SqliteConnection(dataSource))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    // usersテーブルの作成
                    command.CommandText = $"create table {tableName}(";

                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (i != 0) command.CommandText += ",";
                        command.CommandText += columns[i];
                    }
                    command.CommandText += ");";
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //*************************************************************************
        public static bool InsertRecord(string tableName, string dbFilePath, string[] vals, string[] cols = null)
        {

            try
            {
                string dataSource = $"Data Source={dbFilePath};";
                using (var connection = new SqliteConnection(dataSource))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    //usersテーブルの作成
                    command.CommandText = $"insert into {tableName}";

                    if (cols != null)
                    {
                        command.CommandText = "(";
                        for (int i = 0; i < cols.Length; i++)
                        {
                            if (i != 0) command.CommandText += ",";
                            command.CommandText += cols[i];
                        }
                        command.CommandText = ") ";
                    }

                    command.CommandText = "values(";

                    for (int i = 0; i < vals.Length; i++)
                    {
                        if (i != 0) command.CommandText += ",";
                        command.CommandText += vals[i];
                    }
                    command.CommandText += ");";
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //*************************************************************************
        public static List<string> ReadQuery(string db_file, string query)
        {
            var result = new List<string>();

            if (!System.IO.File.Exists(db_file))
            {
                Debug.WriteLine("none file");
                return null;
            }

            string dataSource = $"Data Source={db_file};";

            // 接続やSQL実行に必要なインスタンスの生成
            using (var connection = new SqliteConnection(dataSource))
            using (var command = connection.CreateCommand())
            {
                // 接続開始
                connection.Open();

                // SELECT文の実行
                //command.CommandText = @"select * from tbl_essential_items;";
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    int cnt = 0;
                    Debug.WriteLine(reader.FieldCount);
                    Debug.WriteLine(reader);
                    while (reader.Read())
                    {
                        string buf = "{\n";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            buf += $"\t\"{reader.GetName(i)}\":\"{reader[reader.GetName(i)]}\"\n";
                        }
                        buf += "}";
                        Debug.WriteLine(buf);
                        result.Add(buf);
                        cnt++;
                    }
                }
            }
            return result;
        }
    }
}
