using System;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, 
        AllowMultiple = false)]
    public class GuidNotEmptyAttribute : ValidationAttribute
    {
        public const string DefaultErrorMessage = "The {0} field must not be empty";
        public GuidNotEmptyAttribute() : base(DefaultErrorMessage) { }

        public override bool IsValid(object value)
        {
            //NotEmpty doesn't necessarily mean required
            if (value is null)
            {
                return true;
            }

            switch (value)
            {
                case Guid guid:
                    return guid != Guid.Empty;
                default:
                    return true;
            }
        }
    }

}