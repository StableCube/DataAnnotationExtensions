using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class DictionaryRegularExpressionAttributeObject
    {
        [DictionaryRegularExpression("([a-zA-Z0-9_-]+)", @"([a-zA-Z0-9_\s-]+)")]
        public Dictionary<string, string> TestVal { get; set; }
    }

    public class DictionaryKeyRegularExpressionAttributeTests
    {
        [Fact]
        public void Should_Validate_Valid_Object()
        {
            var obj = new DictionaryRegularExpressionAttributeObject()
            {
                TestVal = new Dictionary<string, string>()
                {
                    { "this-is-fine", "this is fine" }
                }
            };

            var context = new ValidationContext(obj);
            var isValid = Validator.TryValidateObject(obj, context, null, true);

            Assert.True(isValid);
        }

        [Fact]
        public void Should_Not_Validate_Invalid_Object()
        {
            var obj = new DictionaryRegularExpressionAttributeObject()
            {
                TestVal = new Dictionary<string, string>()
                {
                    { "this is bad!", "this is bad!" }
                }
            };

            var context = new ValidationContext(obj);
            var isValid = Validator.TryValidateObject(obj, context, null, true);

            Assert.False(isValid);
        }
    }
}