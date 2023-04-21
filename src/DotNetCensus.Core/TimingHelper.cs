namespace DotNetCensus.Core
{
    public static class TimingHelper
    {
        public static string GetTime(TimeSpan timespan)
        {
            int mins = (int)timespan.TotalMinutes;
            int secs = (int)timespan.TotalSeconds - (mins * 60);
            int ms = (int)timespan.TotalMilliseconds - (secs * 1000) - (mins * 60 * 1000);
            if (mins > 0)
            {
                return $"{mins.ToString()}:{secs.ToString("00")}.{ms.ToString("0")} mins";
            }
            else if (secs>0) 
            {
                return $"{secs.ToString("0")}.{ms.ToString("0")} seconds";
            }
            else
            {
                return $"{ms.ToString("0")} ms";
            }
        }
    }
}
