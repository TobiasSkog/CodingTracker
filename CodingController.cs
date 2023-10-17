using CodingTracker.Database;
using CodingTracker.UserInteraction;

namespace CodingTracker
{
    internal static class CodingController
    {
        public static void CodeSession()
        {
            DatabaseHelper.InitializeDatabase();
            CodingSession codingSession;
            List<TimeSpan> totalTime = new();
            bool runApplication = true;
            //


            //0). Exit the application.
            //1). Start a new Code-Session.
            //2). Add a previous Code-Session.
            //3). View a previous Code-Session.
            //4). Get total duration of all Code-Sessions.


            while (runApplication)
            {

                Console.WriteLine(ConsoleOutput.Menu);
                int id, maxRange, choice = Validation.GetIntegerRange("\nWhat would you like to do: ", 0, 5);

                switch (choice)
                {
                    case 0:
                        //Exit the application

                        runApplication = false;
                        break;
                    case 1:
                        //Start a new Code-Session
                        Console.Clear();
                        codingSession = CodingSession.StartCodingSession();
                        DatabaseHelper.AddCodingSessionToDB(codingSession);
                        break;
                    case 2:
                        //Add a previous Code-Session
                        Console.Clear();
                        codingSession = CodingSession.CreatePreviousSession();
                        DatabaseHelper.AddCodingSessionToDB(codingSession);
                        break;
                    case 3:
                        //View a previous Code-Session
                        Console.Clear();
                        maxRange = DatabaseHelper.GetHighestId();
                        if (maxRange == 0)
                        {
                            return;
                        }
                        id = Validation.GetIntegerRange($"\nSpecify the ID of the Code-Session you would like to view (1 - {maxRange}): ", 1, maxRange, "Så får du inte göra");
                        codingSession = DatabaseHelper.GetCodingSessionById(id);
                        Console.WriteLine(codingSession.ToString());
                        break;
                    case 4:
                        //Get total duration of all Code-Sessions
                        Console.Clear();
                        totalTime = DatabaseHelper.GetCodingSessionsTotalTime();
                        ConsoleOutput.OutputTable(totalTime);
                        //Database.DatabaseHelper.RemoveSpecificHabitByIdFromDB(id);
                        break;
                    case 5:
                        Console.Clear();
                        maxRange = DatabaseHelper.GetHighestId();
                        if (maxRange == 0)
                        {
                            break;
                        }
                        id = Validation.GetIntegerRange($"\nSpecify the ID of the Code-Session you would like to remove (1- {maxRange})", 1, maxRange);
                        DatabaseHelper.RemoveSpecificByIdFromDB(id);
                        break;

                }
            }

        }
    }
}
