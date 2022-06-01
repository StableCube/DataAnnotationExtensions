using System;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeUntilCurrentYearAttribute : RangeAttribute
    {
        /// <summary>
        /// Range between entered minimum year until the current year as reported by DateTime.UtcNow.Year
        /// </summary>
        /// <param name="minimum">Minimum year</param>
        public RangeUntilCurrentYearAttribute(int minimum) : base(minimum, DateTime.UtcNow.Year)
        {
        }

        public RangeUntilCurrentYearAttribute(DateTime minimum) : base(minimum.Year, DateTime.UtcNow.Year)
        {
        }
    }
}