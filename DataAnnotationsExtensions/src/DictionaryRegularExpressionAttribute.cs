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
        public string Pattern { get; set; }

        public DictionaryRegularExpressionAttribute(string pattern)
        {
            if(string.IsNullOrEmpty(pattern))
                throw new ArgumentNullException("Must provide a regex pattern");
            
            Pattern = pattern;
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

            Regex r = new Regex(Pattern, default(RegexOptions), TimeSpan.FromMilliseconds(50));
            foreach (var item in dictionary)
            {
                string keyString = Convert.ToString(item.Key, CultureInfo.CurrentCulture);
                Match keyMatch = r.Match(keyString);
                string valueString = Convert.ToString(item.Value, CultureInfo.CurrentCulture);
                Match valueMatch = r.Match(valueString);

                if(!(keyMatch.Success && keyMatch.Index == 0 && keyMatch.Length == keyString.Length))
                {
                    ErrorMessage = $"The key '{item.Key}' in {{0}} does not match the pattern";
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