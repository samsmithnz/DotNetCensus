namespace DotNetCensus.Core
{
    public static class TimingHelper
    {
        public static string GetTime(TimeSpan timespan)
        {
            if (timespan.TotalMinutes > 1)
            {
                return $"{timespan.TotalMinutes.ToString()}:{timespan.TotalSeconds.ToString("00")}.{timespan.TotalMilliseconds.ToString("0")} mins";
            }
            else if (timespan.TotalSeconds > 1) 
            {
                return $"{timespan.TotalSeconds.ToString("0")}.{timespan.TotalMilliseconds.ToString("0")} seconds";
            }
            else
            {
                return $"{timespan.TotalMilliseconds.ToString("0")} ms";
            }
        }
    }
}
