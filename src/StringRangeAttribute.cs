using System;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Tests if a string is contained in an array of strings
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }
        public bool IgnoreCase { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;
            
            if(IgnoreCase)
            {
                if (AllowableValues?.Contains(value?.ToString(), StringComparer.OrdinalIgnoreCase) == true)
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                if (AllowableValues?.Contains(value?.ToString()) == true)
                {
                    return ValidationResult.Success;
                }
            }

            string allowableString = string.Join(", ", AllowableValues);
            var msg = $"Please enter one of the allowable values: {allowableString}.";
            return new ValidationResult(msg);
        }
    }
}