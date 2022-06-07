using System;

namespace API.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this DateTime dob)//NIJE ISKORISCENA, NE RAD I EDITU!!!
        {
            var now = DateTime.Today;
            var age = now.Year - dob.Year;
            if (dob.Date > now.AddYears(-age)) age--;

            return age;
        }
    }
}