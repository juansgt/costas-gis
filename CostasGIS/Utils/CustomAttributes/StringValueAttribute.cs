using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.CustomAttributes
{
    public class StringValueAttribute: Attribute
    {
        private string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
