using System.Collections.Generic;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Tests if dictionary with string key/values are under a max length
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, 
        AllowMultiple = false)]
    public class DictionaryStringMaxLengthAttribute : ValidationAttribute
    {
        public int Length { get; set; }

        public DictionaryStringMaxLengthAttribute(int length)
        {
            Length = length;
        }

        public override bool IsValid(object value)
        {
            if(value == null)
                return true;
            
            var dictionary = value as IDictionary<string, string>;
            if(dictionary == null)
            {
                ErrorMessage = "{0} is not a dictionary or does not have string keys/values";
                return false;
            }

            foreach (var item in dictionary)
            {
                string keyString = Convert.ToString(item.Key, CultureInfo.CurrentCulture);
                string valueString = Convert.ToString(item.Value, CultureInfo.CurrentCulture);

                if(keyString.Length > Length)
                {
                    ErrorMessage = $"The key '{keyString}' in {{0}} exceeds allowed length of {Length}";
                    return false;
                }

                if(valueString.Length > Length)
                {
                    ErrorMessage = $"The value '{valueString}' in {{0}} exceeds allowed length of {Length}";
                    return false;
                }
            }

            return true;
        }
    }
}