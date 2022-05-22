using System;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Tests if a string is parsable to an enum
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ParsesToEnumAttribute : ValidationAttribute
    {
        public Type EnumType { get; set; }
        public bool IgnoreCase { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;
            
            if(EnumType == null || !EnumType.IsEnum)
                return new ValidationResult("Provided type must be an enum");

            if(!(value is string))
                return new ValidationResult("Provided value is not a string");

            try
            {
                Enum.Parse(EnumType, value as string, IgnoreCase);
            }
            catch (System.Exception)
            {
                string values = "";
                foreach (string name in Enum.GetNames(EnumType))
                    values += name + ", ";

                var msg = $"Please enter one of the allowable values: {values}";
                return new ValidationResult(msg);
            }
            
            return ValidationResult.Success;
        }
    }
}