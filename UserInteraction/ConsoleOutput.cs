namespace CodingTracker.UserInteraction
{
    internal static class ConsoleOutput
    {
        public static readonly string Menu = @"
0). Exit the application.
1). Start a new Code-Session.
2). Add a previous Code-Session.
3). View a previous Code-Session.
4). Get total duration of all Code-Sessions.
5). Delete a Code-Session by Id.
";

        public static void OutputTable(List<TimeSpan> totalTime)
        {
            TimeSpan time = new(0);
            foreach (var item in totalTime)
            {
                time += item;
            }

            Console.WriteLine($"Total Time in coding-sessions: {time}");
        }

    }
}
