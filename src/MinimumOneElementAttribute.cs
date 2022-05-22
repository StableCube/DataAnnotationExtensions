using System.Collections;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Ensure a collection has at least one element
    /// </summary>
    public class MinimumOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as IEnumerable;
            if (collection != null && collection.GetEnumerator().MoveNext())
                return true;

            ErrorMessage = "{0} must have at least one element";
            return false;
        }
    }
}