using CodingTracker.UserInteraction;
using System.Configuration;
using System.Data.SQLite;

namespace CodingTracker.Database
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        public static void InitializeDatabase()
        {
            if (!File.Exists(@"..\..\..\Database\CodingTracker.db"))
            {
                SQLiteConnection.CreateFile(@"..\..\..\Database\CodingTracker.db");

                using (SQLiteConnection connection = new(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new(connection))
                    {
                        command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS sessions(
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                start_time TEXT NOT NULL,
                                end_time TEXT NOT NULL,
                                duration TEXT NOT NULL
                            );";

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void AddCodingSessionToDB(CodingSession codingSession)
        {
            Console.WriteLine(codingSession.DurationString);
            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText =
                        @"INSERT INTO sessions (start_time, end_time, duration)
                        VALUES (@start_time, @end_time, @duration);";

                    command.Parameters.AddWithValue("@start_time", codingSession.StartTimeString);
                    command.Parameters.AddWithValue("@end_time", codingSession.EndTimeString);
                    command.Parameters.AddWithValue("@duration", codingSession.DurationString);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }


        public static CodingSession GetCodingSessionById(int id)
        {
            CodingSession codingSession = new(DateTime.Now, DateTime.Now);
            DateTime startTime = DateTime.Now, endTime = DateTime.Now;
            TimeSpan duration = TimeSpan.Zero;
            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText = @"SELECT * FROM sessions WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            startTime = Validation.GetDate(reader.GetString(reader.GetOrdinal("start_time")), Program.dateFormat);
                            endTime = Validation.GetDate(reader.GetString(reader.GetOrdinal("end_time")), Program.dateFormat);
                            duration = Validation.GetTimeSpan(reader.GetString(reader.GetOrdinal("duration")));

                            codingSession = new CodingSession(id, startTime, endTime, duration);
                        }
                    }

                }
            }
            Console.WriteLine(codingSession.DurationString);
            return codingSession;
        }

        public static List<TimeSpan> GetCodingSessionsTotalTime()
        {
            List<TimeSpan> totalTime = new();

            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText =
                        @"SELECT duration FROM sessions";

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalTime.Add(Validation.GetTimeSpan(reader.GetString(reader.GetOrdinal("duration"))));
                        }
                    }
                }
            }

            return totalTime;
        }

        public static int GetHighestId()
        {
            int highestId = 0;

            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();
                string getHighestId = @"
                        SELECT id 
                        FROM sessions
                        ORDER BY id DESC LIMIT 1
                        ";

                using (SQLiteCommand command = new(getHighestId, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            if (id > highestId)
                            {
                                highestId = id;
                            }
                        }
                    }
                }
            }

            return highestId;
        }

        public static void RemoveSpecificByIdFromDB(int id)
        {
            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText = @"DELETE FROM sessions WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }


    }
}
