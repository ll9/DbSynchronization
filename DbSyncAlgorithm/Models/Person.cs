using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DbSyncAlgorithm.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        private DateTime? dateCreated = null;
        public DateTime DateCreated
        {
            get
            {
                return dateCreated.HasValue
                   ? dateCreated.Value
                   : DateTime.Now;
            }

            set { dateCreated = value; }
        }

    }
}
