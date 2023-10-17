using CodingTracker.UserInteraction;

namespace CodingTracker.Database
{
    internal class CodingSession
    {
        private int _Id;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private TimeSpan _Duration;

        public CodingSession(DateTime startTime, DateTime endTime)
        {
            _Id = -1;
            _StartTime = startTime;
            _EndTime = endTime;
            _Duration = CalculateDuration.GetDuration(startTime, endTime);
            Console.WriteLine(StartTime.ToString());
            Console.WriteLine(_EndTime.ToString());
            Console.WriteLine(_Duration.ToString());
        }
        public CodingSession(int id, DateTime startTime, DateTime endTime, TimeSpan duration)
        {
            _Id = id;
            _StartTime = startTime;
            _EndTime = endTime;
            _Duration = duration;
        }
        public static CodingSession StartCodingSession()
        {
            DateTime startTime = DateTime.Now;
            Dictionary<string, bool> allowedAnswers = new()
            {
                    { "S", true },
                    { "ST", true },
                    { "STO", true },
                    { "STOP", true },
            };

            while (true)
            {
                Console.WriteLine();
                if (Validation.GetYesOrNo("Enter 'Stop' to end the session: ", allowedAnswers, "Please type 'Stop' to stop the coding-session"))
                {
                    break;
                }
            }

            DateTime endTime = DateTime.Now;
            return new CodingSession(startTime, endTime);
        }

        public static CodingSession CreatePreviousSession()
        {
            DateTime startTime = Validation.GetExactDateTime("What date/time did you start your code-session: ", Program.dateFormat);
            DateTime endTime = Validation.GetExactDateTime("What date/time did you end your code-session: ", Program.dateFormat);

            return new CodingSession(startTime, endTime);
        }
        public int Id { get { return _Id; } }
        public DateTime StartTime { get { return _StartTime; } }
        public DateTime EndTime { get { return _EndTime; } }
        public TimeSpan Duration { get { return _Duration; } }

        public string StartTimeString { get { return _StartTime.ToString(Program.dateFormat); } }
        public string EndTimeString { get { return _EndTime.ToString(Program.dateFormat); } }
        public string DurationString { get { return _Duration.ToString(); } }

    }
}
