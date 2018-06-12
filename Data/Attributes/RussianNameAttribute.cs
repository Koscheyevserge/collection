using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RussianNameAttribute : Attribute
    {
        public string Name { get; set; }

        public RussianNameAttribute(string name)
        {
            Name = name;
        }
    }
}
