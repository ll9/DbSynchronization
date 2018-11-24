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

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
