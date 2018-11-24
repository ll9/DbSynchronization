using System.Collections;
using System.Collections.Generic;

namespace Client.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Person> People { get; set; }
    }
}