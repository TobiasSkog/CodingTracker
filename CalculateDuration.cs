namespace CodingTracker
{
    internal static class CalculateDuration
    {
        public static TimeSpan GetDuration(DateTime startTime, DateTime endTime)
        {
            TimeSpan duration = endTime - startTime;
            return duration;
        }

        public static TimeSpan TotalTime(List<TimeSpan> timeSpans)
        {
            TimeSpan totalTimeSpan = new(0);
            foreach (var ts in timeSpans)
            {
                totalTimeSpan += ts;
            }
            return totalTimeSpan;
        }
    }
}
