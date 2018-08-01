namespace CompanyX.Data
{
    using SQLite;
    using System;
    using System.IO;
    public class DatabaseInit
    {
        public static SQLiteConnection Connection { get; private set; }

        private static string databaseFilePath;

        public static void Initialize()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            databaseFilePath = Path.Combine(docs, "Inspection_8.db");
            Connection = new SQLiteConnection(databaseFilePath);

        }
    }
}
