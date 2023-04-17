namespace DotNetCensus.Core
{
    public static class TimingHelper
    {
        public static string GetTime(TimeSpan timespan)
        {
            if (timespan.TotalMinutes > 1)
            {
                return $"{timespan.TotalMinutes.ToString()}:{timespan.TotalSeconds.ToString("00")} mins";
            }
            else
            {
                return $"{timespan.TotalSeconds.ToString("0")} seconds";
            }
        }
    }
}
