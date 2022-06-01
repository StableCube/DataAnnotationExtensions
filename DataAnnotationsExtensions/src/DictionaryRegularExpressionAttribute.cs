using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Tests if dictionary with string key/values match a regular expression
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, 
        AllowMultiple = false)]
    public class DictionaryRegularExpressionAttribute : ValidationAttribute
    {
        public string KeyPattern { get; set; }
        public string ValuePattern { get; set; }

        public DictionaryRegularExpressionAttribute(string keyPattern, string valuePattern)
        {
            if(string.IsNullOrEmpty(keyPattern))
                throw new ArgumentNullException("Must provide a regex pattern for the key");
            
            if(string.IsNullOrEmpty(valuePattern))
                throw new ArgumentNullException("Must provide a regex pattern for the value");

            KeyPattern = keyPattern;
            ValuePattern = valuePattern;
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

            Regex rKey = new Regex(KeyPattern, default(RegexOptions), TimeSpan.FromMilliseconds(50));
            Regex rVal = new Regex(ValuePattern, default(RegexOptions), TimeSpan.FromMilliseconds(50));
            foreach (var item in dictionary)
            {
                string keyString = Convert.ToString(item.Key, CultureInfo.CurrentCulture);
                Match keyMatch = rKey.Match(keyString);
                string valueString = Convert.ToString(item.Value, CultureInfo.CurrentCulture);
                Match valueMatch = rVal.Match(valueString);

                if(!(keyMatch.Success && keyMatch.Index == 0 && keyMatch.Length == keyString.Length))
                {
                    ErrorMessage = $"The key '{keyString}' in {{0}} does not match the pattern";
                    return false;
                }

                if(!(valueMatch.Success && valueMatch.Index == 0 && valueMatch.Length == valueString.Length))
                {
                    ErrorMessage = $"The value '{valueString}' in {{0}} does not match the pattern";
                    return false;
                }
            }

            return true;
        }
    }
}