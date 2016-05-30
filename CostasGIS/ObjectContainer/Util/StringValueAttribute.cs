using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectContainer.Util
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
