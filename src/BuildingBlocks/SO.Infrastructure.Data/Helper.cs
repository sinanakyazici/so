using Microsoft.AspNetCore.Http;

namespace SO.Infrastructure.Data
{
    public static class Helper
    {
        public static string GetCurrentUserId(this HttpContext httpContext)
        {
            var value = httpContext?.User?.FindFirst("preferred_username")?.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                value = httpContext?.User?.FindFirst("client_id")?.Value;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = "unknown";
            }

            return value;
        }

        public static DateTime DateTimeNow()
        {
            var date =  DateTime.Now.SetKindUtc();
            return date;
        }

        public static DateTime SetKindUtc(this DateTime dateTime)
        {
            return dateTime.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
    }
}
