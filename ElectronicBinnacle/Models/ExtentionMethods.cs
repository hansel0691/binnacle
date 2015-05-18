using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models
{
    public static class ExtentionMethods
    {
        public static long GetUnixEpoch(this DateTime dateTime)
        {
            //pueden haber errores de Time Zone, ver el de dateTime en caso de problemas.
            var unixTime = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)unixTime.TotalMilliseconds + (long)TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime()).TotalMilliseconds;
        }
    }
}