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
                return $"{mins}:{secs:00}.{ms:0} mins";
            }
            else if (secs > 0)
            {
                return $"{secs:0}.{ms:0} seconds";
            }
            else
            {
                return $"{ms:0} ms";
            }
        }
    }
}
