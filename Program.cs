using System.Configuration;

namespace CodingTracker
{
    internal class Program
    {
        private static readonly string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
        public static readonly string dateFormat = ConfigurationManager.AppSettings.Get("dateFormatDateAndTime");
        public static readonly string timeFormat = ConfigurationManager.AppSettings.Get("dateFormatTime");

        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Initialization error! The ConfigurationManager got a null value from the connectionString!");
                Console.WriteLine("This will lead to failure in creating the database and using it if it already exists!");
            }

            CodingController.CodeSession();
        }
    }
}