using System;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Tests if a DateTime or DateTimeOffset's year is within specified year until current year
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateYearRangeUntilCurrentAttribute : ValidationAttribute
    {
        public int From { get; set; }

        public DateYearRangeUntilCurrentAttribute(int year)
        {
            From = year;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;

            int currentYear = DateTime.UtcNow.Year;
            if(value is DateTime)
            {
                DateTime date = (DateTime)value;
                if(date.Year >= From && date.Year <= currentYear)
                    return ValidationResult.Success;
            }
            
            if(value is DateTimeOffset)
            {
                DateTimeOffset date = (DateTimeOffset)value;
                if(date.Year >= From && date.Year <= currentYear)
                    return ValidationResult.Success;
            }

            #if NET6_0
            if (value is DateOnly)
            {
                DateOnly date = (DateOnly)value;
                if(date.Year >= From && date.Year <= currentYear)
                    return ValidationResult.Success;
            }
            #endif

            var msg = $"Year must be between {From} and {currentYear}.";
            return new ValidationResult(msg);
        }
    }
}