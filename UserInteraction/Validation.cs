using System.Globalization;

namespace CodingTracker.UserInteraction
{
    internal static class Validation
    {
        public static int GetIntegerRange(string prompt, int minRange, int maxRange, string errorMessage = $"Invalid input. Number must be an integer within the range ")
        {
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int validInt))
                {
                    if (validInt >= minRange && validInt <= maxRange)
                    {
                        return validInt;
                    }
                }

                Console.WriteLine($"{errorMessage} ({minRange} - {maxRange}).");
            }
        }

        public static bool GetYesOrNo(string prompt, Dictionary<string, bool>? allowedAnswers = null, string error = "Please answer (Y)es or (N)o.")
        {
            allowedAnswers ??= new()
                {
                    { "Y", true },
                    { "YE", true },
                    { "YES", true },
                    { "J", true },
                    { "JA", true },
                    { "K", true },
                    { "OK", true },
                    { "N", false },
                    { "NO", false },
                    { "NE", false },
                    { "NEJ", false },
                    { "NOP", false },
                    { "NOPE", false },
                    { "NOPER", false },
                    { "NOPERS", false },
                };
            while (true)
            {
                Console.Write(prompt);

                string? userInput = Console.ReadLine().ToUpper();

                if (!string.IsNullOrWhiteSpace(userInput) && allowedAnswers.ContainsKey(userInput))
                {
                    return allowedAnswers[userInput];
                }

                Console.WriteLine(error);
            }
        }
        public static string GetString(string prompt)
        {

            while (true)
            {
                Console.Write(prompt);

                string? userInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    return userInput;
                }

                Console.WriteLine("Invalid input. You cannot use an empty text or only white spaces.");
            }
        }

        public static DateTime GetDate(string date, string format)
        {


            if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDateTime))
            {
                return validDateTime;
            }
            else
            {
                return DateTime.Now;
            }
        }
        public static TimeSpan GetTimeSpan(string timeSpan)
        {
            if (TimeSpan.TryParse(timeSpan, out TimeSpan validTimeSpan))
            {
                return validTimeSpan;
            }
            else
            {
                return DateTime.Now - DateTime.Now.AddMinutes(-1);
            }
        }
        public static DateTime GetExactDateTime(string prompt, string format)
        {
            while (true)
            {
                Console.Write($"{prompt}, in this format ({format}): ");

                if (DateTime.TryParseExact(Console.ReadLine(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDateTime))
                {
                    return validDateTime;
                }

                Console.WriteLine($"Invalid date format. Enter a valid date and time in the format {format}.");
            }
        }

        public static DateTime GetFutureDateTime(string prompt, string format)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                DateTime userInput = GetExactDateTime(prompt, format);

                if (userInput > now)
                {
                    return userInput;
                }

                Console.WriteLine("Invalid date. The date and time must be in the future.");
            }
        }



    }
}
