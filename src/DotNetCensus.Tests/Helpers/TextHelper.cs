namespace DotNetCensus.Tests.Helpers
{
    public static class TextHelper
    {
        public static string CleanTimingFromResult(string str)
        {
            //All results have a timing value at the end of the line. 
            //Remove this, for example: "Time to process: 23 ms"
            //Also correctly processes the end of line characters
            str = str.Remove(str.LastIndexOf(Environment.NewLine));
            str = str.Remove(str.LastIndexOf(Environment.NewLine));
            str += Environment.NewLine;
            return str;
        }
    }
}
