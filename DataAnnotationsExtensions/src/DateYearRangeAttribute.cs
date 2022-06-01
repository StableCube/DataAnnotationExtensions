using System;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Tests if a DateTime or DateTimeOffset's year is within specified range
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateYearRangeAttribute : ValidationAttribute
    {
        public int From { get; set; }
        public int To { get; set; }

        public DateYearRangeAttribute(int fromYear, int toYear)
        {
            From = fromYear;
            To = toYear;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;

            if(value is DateTimeOffset)
            {
                DateTimeOffset date = (DateTimeOffset)value;
                if(date.Year >= From && date.Year <= To)
                    return ValidationResult.Success;
            }
            
            if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                if(date.Year >= From && date.Year <= To)
                    return ValidationResult.Success;
            }

            #if NET6_0
            if (value is DateOnly)
            {
                DateOnly date = (DateOnly)value;
                if(date.Year >= From && date.Year <= To)
                    return ValidationResult.Success;
            }
            #endif
            
            var msg = $"Year must be between {From} and {To}.";
            return new ValidationResult(msg);
        }
    }
}