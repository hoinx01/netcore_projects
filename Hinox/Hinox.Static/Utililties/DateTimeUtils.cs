using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Hinox.Static.Utililties
{
    public static class DateTimeUtils
    {
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
        public static string GetTimeAgoDisplay(DateTime date)
        {
            DateTime now = DateTime.Now;
            if (DateTime.Compare(now, date) >= 0)
            {
                TimeSpan s = DateTime.Now.Subtract(date);
                TimeSpan w = DateTime.Now.StartOfWeek(DayOfWeek.Monday).Subtract(date);

                int numberDays = (now - date).Days;

                int dayDiff = (int)s.TotalDays;
                int weekDiff = DateTime.Now.DayOfWeek == DayOfWeek.Monday ? (int)w.TotalDays + 7 : (int)w.TotalDays;

                int secDiff = (int)s.TotalSeconds;

                if (dayDiff == 0)
                {
                    if (secDiff < 60)
                    {
                        return "vừa xong";
                    }
                    if (secDiff < 120)
                    {
                        return "1 phút trước";
                    }
                    if (secDiff < 3600)
                    {
                        return string.Format("{0} phút trước", Math.Floor((double)secDiff / 60));
                    }
                    if (secDiff < 7200)
                    {
                        return "1 giờ trước";
                    }
                    if (secDiff < 86400)
                    {
                        return string.Format("{0} giờ trước", Math.Floor((double)secDiff / 3600));
                    }
                }
                if (numberDays == 1)
                {
                    return "Hôm qua lúc " + date.ToString("HH:mm");
                }
                if (dayDiff > 1 && weekDiff < 7)
                {
                    string dateOfWeek = string.Empty;
                    switch (date.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            dateOfWeek = "Thứ hai";
                            break;
                        case DayOfWeek.Tuesday:
                            dateOfWeek = "Thứ ba";
                            break;
                        case DayOfWeek.Wednesday:
                            dateOfWeek = "Thứ tư";
                            break;
                        case DayOfWeek.Thursday:
                            dateOfWeek = "Thứ năm";
                            break;
                        case DayOfWeek.Friday:
                            dateOfWeek = "Thứ sáu";
                            break;
                        case DayOfWeek.Saturday:
                            dateOfWeek = "Thứ bảy";
                            break;
                        case DayOfWeek.Sunday:
                            dateOfWeek = "Chủ nhật";
                            break;
                    }
                    return string.Format("{0} lúc {1}", dateOfWeek, date.ToString("HH:mm"));
                }
                //if (dayDiff < 7)
                //{
                //    return string.Format("{0} ngày trước", dayDiff);
                //}
                if (dayDiff < 31)
                {
                    //return string.Format("{0} tuần trước", Math.Ceiling((double)dayDiff / 7));
                    return string.Format("{0} ngày trước", dayDiff);
                }
                if (dayDiff < 365)
                {
                    return string.Format("{0} tháng trước", Math.Ceiling((double)dayDiff / 30));
                }
                if (dayDiff >= 365)
                {
                    return string.Format("{0} {1}:{2}", date.ToString("dd/MM/yyyy"), date.Hour < 10 ? "0" + date.Hour : date.Hour + "", date.Minute < 10 ? "0" + date.Minute : date.Minute + "");
                }
                return string.Empty;
            }
            else
                return string.Empty;
        }

        public static string GetTimeAgoDisplay(long ticks)
        {
            if (ticks > 0)
            {
                var date = ConvertTicksToDateTime(ticks);
                if (date.HasValue)
                {
                    DateTime now = DateTime.Now;
                    if (DateTime.Compare(now, date.Value) >= 0)
                    {
                        TimeSpan s = DateTime.Now.Subtract(date.Value);
                        TimeSpan w = DateTime.Now.StartOfWeek(DayOfWeek.Monday).Subtract(date.Value);

                        int numberDays = (now - date).Value.Days;

                        int weekDiff = DateTime.Now.DayOfWeek == DayOfWeek.Monday ? (int)w.TotalDays + 7 : (int)w.TotalDays;

                        int dayDiff = (int)s.TotalDays;

                        int secDiff = (int)s.TotalSeconds;

                        if (dayDiff == 0)
                        {
                            if (secDiff < 60)
                            {
                                return "vừa xong";
                            }
                            if (secDiff < 120)
                            {
                                return "1 phút trước";
                            }
                            if (secDiff < 3600)
                            {
                                return string.Format("{0} phút trước", Math.Floor((double)secDiff / 60));
                            }
                            if (secDiff < 7200)
                            {
                                return "1 giờ trước";
                            }
                            if (secDiff < 86400)
                            {
                                return string.Format("{0} giờ trước", Math.Floor((double)secDiff / 3600));
                            }
                        }
                        if (numberDays == 1)
                        {
                            return "Hôm qua lúc " + date.Value.ToString("HH:mm");
                        }
                        if (dayDiff > 1 && weekDiff < 7)
                        {
                            string dateOfWeek = string.Empty;
                            switch (date.Value.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    dateOfWeek = "Thứ Hai";
                                    break;
                                case DayOfWeek.Tuesday:
                                    dateOfWeek = "Thứ Ba";
                                    break;
                                case DayOfWeek.Wednesday:
                                    dateOfWeek = "Thứ Tư";
                                    break;
                                case DayOfWeek.Thursday:
                                    dateOfWeek = "Thứ Năm";
                                    break;
                                case DayOfWeek.Friday:
                                    dateOfWeek = "Thứ Sáu";
                                    break;
                                case DayOfWeek.Saturday:
                                    dateOfWeek = "Thứ Bảy";
                                    break;
                                case DayOfWeek.Sunday:
                                    dateOfWeek = "Chủ Nhật";
                                    break;
                            }
                            return string.Format("{0} lúc {1}", dateOfWeek, date.Value.ToString("HH:mm"));
                        }
                        //if (dayDiff < 7)
                        //{
                        //    return string.Format("{0} ngày trước", dayDiff);
                        //}
                        if (dayDiff < 31)
                        {
                            //return string.Format("{0} tuần trước", Math.Ceiling((double)dayDiff / 7));
                            return string.Format("{0} ngày trước", dayDiff);
                        }
                        if (dayDiff < 365)
                        {
                            return string.Format("{0} tháng trước", Math.Ceiling((double)dayDiff / 30));
                        }
                        if (dayDiff >= 365)
                        {
                            return string.Format("{0} {1}:{2}", date.Value.ToString("dd/MM/yyyy"), date.Value.Hour < 10 ? "0" + date.Value.Hour : date.Value.Hour + "", date.Value.Minute < 10 ? "0" + date.Value.Minute : date.Value.Minute + "");
                        }
                        return string.Empty;
                    }
                    else
                        return string.Empty;
                }
            }
            return string.Empty;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = startOfWeek - dt.DayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(diff).Date;
        }

        public static string GetTimeDisplayByDay(long ticks)
        {
            if (ticks > 0)
            {
                var date = ConvertTicksToDateTime(ticks);
                if (date.HasValue)
                {
                    DateTime now = DateTime.Now;
                    if (DateTime.Compare(now, date.Value) >= 0)
                    {
                        TimeSpan s = DateTime.Now.Subtract(date.Value);

                        int dayDiff = (int)s.TotalDays;

                        int secDiff = (int)s.TotalSeconds;

                        if (dayDiff == 0)
                        {
                            if (secDiff < 60)
                            {
                                return "vừa xong";
                            }
                            if (secDiff < 120)
                            {
                                return "1 phút trước";
                            }
                            if (secDiff < 3600)
                            {
                                return string.Format("{0} phút trước", Math.Floor((double)secDiff / 60));
                            }
                            if (secDiff < 7200)
                            {
                                return "1 giờ trước";
                            }
                            if (secDiff < 86400)
                            {
                                return string.Format("{0} giờ trước", Math.Floor((double)secDiff / 3600));
                            }
                        }
                        else if (dayDiff == 1)
                        {
                            return date.Value.ToString("HH:mm") + " - Hôm qua";
                        }
                        else
                        {
                            return string.Format("{0}:{1} - {2}", date.Value.Hour < 10 ? "0" + date.Value.Hour : date.Value.Hour + "", date.Value.Minute < 10 ? "0" + date.Value.Minute : date.Value.Minute + "", date.Value.ToString("dd/MM/yyyy"));
                        }
                        return string.Empty;
                    }
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        public static DateTime? ConvertTicksToDateTime(long lticks)
        {
            if (lticks == 0) return null;
            return new DateTime(lticks);
        }

        public static string ConvertTicksToStringFormat(long ticks)
        {
            if (ticks == 0) return string.Empty;
            var convertDate = ConvertTicksToDateTime(ticks);
            var strDateFormat = "{0} {1}";
            if (!convertDate.HasValue) return string.Empty;

            return string.Format(strDateFormat, convertDate.Value.ToString("dd/MM/yyyy"), convertDate.Value.ToString("HH:mm"));

        }

        public static string ConvertTicksToStringFormatWithDash(long ticks)
        {
            if (ticks == 0) return string.Empty;
            var convertDate = ConvertTicksToDateTime(ticks);
            var strDateFormat = "{0} - {1}";
            if (!convertDate.HasValue) return string.Empty;

            return string.Format(strDateFormat, convertDate.Value.ToString("dd/MM/yyyy"), convertDate.Value.ToString("HH:mm"));

        }

        public static string ConvertTicksToStringFormat(long ticks, string formatString)
        {
            if (ticks == 0) return string.Empty;
            var convertDate = ConvertTicksToDateTime(ticks);
            var strDateFormat = "{0}";
            if (!convertDate.HasValue) return string.Empty;
            return string.Format(strDateFormat, convertDate.Value.ToString(formatString));
        }

        public static string ConvertTicksToStringTimeFormat(long ticks)
        {
            if (ticks == 0) return string.Empty;
            var prefix = "";
            var convertDate = ConvertTicksToDateTime(ticks);
            if (!convertDate.HasValue) return string.Empty;

            int hour = convertDate.Value.Hour;
            if (hour >= 0 && hour <= 12) prefix = "sáng";
            if (hour >= 13 && hour <= 17) prefix = "chiều";
            if (hour >= 18 && hour <= 23) prefix = "tối";
            var strDateFormat = "lúc {0} " + prefix;
            return string.Format(strDateFormat, convertDate.Value.ToString("HH:mm"));
        }

        public static DateTime ConvertStringToDateTime(string value, string format)
        {
            return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
        }

        public static DateTime ConvertToDateTime(object value)
        {
            DateTime returnValue;

            if (null == value || !DateTime.TryParse(value.ToString(), out returnValue))
            {
                returnValue = DateTime.MinValue;
            }

            return returnValue;
        }

        /// <summary>
        /// Return double, > 0 then dateTo > dateFrom
        /// </summary>
        /// <pre>instant: d = Date, h = Hour, m = Minute, s = Second</pre>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="instant"></param>
        /// <returns></returns>
        public static double DateDiff(DateTime dateFrom, DateTime dateTo, string instant)
        {
            TimeSpan span = (TimeSpan)(dateTo - dateFrom);
            double num = 0.0;
            string str = instant.ToLower();
            if (str == null)
            {
                return num;
            }
            if (str != "d")
            {
                if (str != "h")
                {
                    if (str == "m")
                    {
                        return span.TotalMinutes;
                    }
                    if (str != "s")
                    {
                        return num;
                    }
                    return span.TotalSeconds;
                }
            }
            else
            {
                return span.TotalDays;
            }
            return span.TotalHours;
        }

        public static long DateTimeToUnixTime(DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (TimeSpan)(dateTime - epoch.ToLocalTime());
            return (long)(span.TotalSeconds * 1000.0 + DateTime.UtcNow.Millisecond);
        }

        public static long DateTimeToUnixTimeDaily(DateTime dateTime)
        {
            dateTime = DateTime.Parse(dateTime.ToString("MM/dd/yyyy 00:00:00"));
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (TimeSpan)(dateTime.Date - epoch.ToLocalTime());
            return (long)(span.TotalSeconds * 1000.0);
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp / 1000.0).ToLocalTime();
            return dtDateTime;
        }

        public static long DateTimeToSpanHourly(DateTime dateTime)
        {
            dateTime = DateTime.Parse(dateTime.ToString("MM/dd/yyyy HH:00:00"));
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (TimeSpan)(dateTime - time.ToLocalTime());
            return (long)(span.TotalSeconds * 1000.0);
        }
    }
}
