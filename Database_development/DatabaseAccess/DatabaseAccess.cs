using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace DatabaseAccess
{
    public class DatabaseAccess<T> : IDatabaseAccess<T>
        where T : class
    {
        private static DatabaseAccess<T> databaseAccess;

        public void CreateEntity(T entity)
        {
            using (var sqlConnection = new SQLiteConnection("Data Source=../../../../TrainDB.db; Version=3;"))
            {
                sqlConnection.Open();
                var sb = new StringBuilder();

                //Add new Entitys to database
                sb.Clear();
                sb.Append("INSERT INTO Trains (TrainNumber, TrainName, Category, ArrivalTime_Hour, ArrivalTime_Minutes, DepartureTime_Hour, DepartureTime_Minutes) ");
                sb.Append("VALUES (@TrainNumber, @TrainName, @Category, @ArrivalTime_Hour, @ArrivalTime_Minutes, @DepartureTime_Hour, @DepartureTime_Minutes);");
                using (var command = new SQLiteCommand(sb.ToString(), sqlConnection))
                {
                    foreach (var train in collection)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@TrainNumber", train.TrainNumber.ToString());
                        command.Parameters.AddWithValue("@TrainName", train.TrainName);
                        command.Parameters.AddWithValue("@Category", train.Category);
                        command.Parameters.AddWithValue("@ArrivalTime_Hour", train.ArrivalTime.Hour.ToString());
                        command.Parameters.AddWithValue("@ArrivalTime_Minutes", train.ArrivalTime.Minutes.ToString());
                        command.Parameters.AddWithValue("@DepartureTime_Hour", train.DepartureTime.Hour.ToString());
                        command.Parameters.AddWithValue("@DepartureTime_Minutes", train.DepartureTime.Minutes.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T entityToReplace, T substituteEntity)
        {
            throw new NotImplementedException();
        }

        public static DatabaseAccess<T> Factory()
        {
            if (databaseAccess == null)
            {
                databaseAccess = new DatabaseAccess<T>();
            }

            return databaseAccess;
        }
    }
}
