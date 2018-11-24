using Client.Data;
using Client.Models;
using System;
using System.Linq;
using System.Reflection;

namespace Client
{
    class Program
    {
        private static Random _random = new Random();

        private static void Sync()
        {
            Console.WriteLine("Should start syncing here");
        }

        static void Main(string[] args)
        {
            Assembly.Load("Microsoft.EntityFrameworkCore.Design");
            var people = new[] { "Hans", "Peter", "Jens", "Sabine" };



            using (var _context = new ClientDbContext())
            {
                string decision = null;
                while (decision != "exit")
                {
                    Console.WriteLine(
                        "Press 's' to show all People, " + 
                        "'c' to randomly create a new Person or " + 
                        "'sync' to synchronize with the server " +
                        "'exit' to exit");
                    decision = Console.ReadLine();

                    if (decision == "s")
                    {
                        foreach (var person in _context.People)
                        {
                            Console.WriteLine(person.ToString());
                        }
                    }
                    else if (decision == "c")
                    {
                        var person = new Person
                        {
                            Age = _random.Next(100),
                            Name = people[_random.Next(people.Count())],
                        };

                        _context.People.Add(person);
                        _context.SaveChanges();
                    }
                    else if (decision == "sync")
                    {
                        Sync();
                    }

                }
            }
        }
    }
}
