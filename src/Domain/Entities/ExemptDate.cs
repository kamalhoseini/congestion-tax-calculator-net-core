using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ExemptDate
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public List<DateTime> PublicHolidays { get; set; }
        public List<int> FreeMonths { get; set; }
        public List<DayOfWeek> FreeDaysOfWeek { get; set; }

        public ExemptDate(int cityId, List<DateTime> publicHolidays, List<int> freeMonths, List<DayOfWeek> freeDaysOfWeek)
        {
            CityId = cityId;
            PublicHolidays = publicHolidays;
            FreeMonths = freeMonths;
            FreeDaysOfWeek = freeDaysOfWeek;
        }

        public bool IsInFreeMonth(DateTime date)
        {
            return FreeMonths.Contains(date.Month);
        }

        public bool IsFreeDaysOfWeek(DateTime date)
        {
            return FreeDaysOfWeek.Contains(date.DayOfWeek);
        }

        public bool IsPublicHoliday(DateTime date)
        {
            return PublicHolidays.Contains(date);
        }

        public bool IsDayBeforePublicHoliday(DateTime date)
        {
            return IsPublicHoliday(date.AddDays(1));
        }

    }
}
