using System;

namespace API.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this DateTime dob)
        {
            var now = DateTime.Today;
            var age = now.Year - dob.Year;
            if (dob.Date > now.AddYears(-age)) age--;

            return age;
        }
    }
}