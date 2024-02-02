using Application.Interfaces;

namespace Application.Services
{
    public class CurrentTime : ICurrentTime
    {
        //public DateTime GetCurrentTime() => DateTime.UtcNow;
        public DateTime GetCurrentTime()
        {
            // Lấy múi giờ UTC
            DateTime utcNow = DateTime.UtcNow;

            // Đặt múi giờ mong muốn là "SE Asia Standard Time" (đối với Việt Nam)
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Chuyển đổi từ múi giờ UTC sang múi giờ Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, vietnamTimeZone);

            return vietnamTime;
        }
    }
}
