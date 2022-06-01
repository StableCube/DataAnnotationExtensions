
namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Ensure an enum is set. This requires an enum not have a value for 0.
    /// </summary>
    public class RequiredEnumAttribute: RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                 return false;

            var type = value.GetType();

            if(Enum.IsDefined(type, 0))
            {
                ErrorMessage = "{0} must not define a 0 index in order to use RequiredEnumAttribute";
                return false;
            }

            return type.IsEnum && Enum.IsDefined(type, value);
        }
   }
}