using System;

namespace Plugins.Survicate
{
    internal static class SurvicateDateUtils
    {
        internal static string FormatDateToTimeZoneIso(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }
}
