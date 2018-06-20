using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RussianNameAttribute : Attribute
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string ShortName { get; set; }
        public string Abbreviation { get; set; }

        public RussianNameAttribute(string name)
        {
            Name = name;
        }

        public RussianNameAttribute()
        {

        }
    }
}
