using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }


        public override string ToString()
        {
            var builder = new StringBuilder();
            var props = typeof(Person).GetProperties();

            foreach (var prop in props)
            {
                builder.Append(prop.GetValue(this));
                builder.Append("\n");
            }

            return builder.ToString();
        }
    }
}
