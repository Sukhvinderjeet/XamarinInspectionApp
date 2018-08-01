using CompanyX.Data.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyX.Data.Repository
{
    public class InspectionRepository
    {
        static object locker = new object();

        SQLiteConnection database;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public InspectionRepository(SQLiteConnection conn)
        {
            database = conn;
            // create the tables
            database.CreateTable<InspectionEntity>();
        }

        public IEnumerable<InspectionEntity> GetItems(string userId)
        {
            lock (locker)
            {
                return (from i in database.Table<InspectionEntity>() select i).Where(x => x.UserID == userId && !x.IsCompleted).ToList().Where(x => Convert.ToDateTime(x.DueDate) >= DateTime.Now).ToList();
            }
        }


        public int GetItemsCount(string userId)
        {
            lock (locker)
            {
                var data = (from i in database.Table<InspectionEntity>() select i).Where(x => x.UserID == userId && !x.IsCompleted).ToList();

                return data.Where(x => Convert.ToDateTime(x.DueDate) >= DateTime.Now).Count();
            }
        }

        public InspectionEntity GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<InspectionEntity>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem(InspectionEntity item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public void SaveItems(List<InspectionEntity> items)
        {
            var itemsToInsert = new List<InspectionEntity>(items);
            var ids = items.Select(x => x.ServerId).ToList();
            var exist = database.Table<InspectionEntity>().Where(w => ids.Contains(w.ServerId)).Select(x => x).ToList().Select(x => x.ServerId);
            if (exist != null && exist.Any())
            {
                itemsToInsert = items.Where(x => !exist.Contains(x.ServerId)).ToList();
            }
            database.InsertAll(itemsToInsert);
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<InspectionEntity>(id);
            }
        }
    }
}
