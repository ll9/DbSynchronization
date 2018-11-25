using Client.Data;
using Client.Models;
using Client.Utils;
using System;
using System.Linq;
using System.Reflection;

namespace Client
{
    class Program
    {
        private static Random _random = new Random();
        private static ClientDbContext _context = new ClientDbContext();

        private static void Sync()
        {
            Console.WriteLine("Should start syncing here");
        }

        private static void SyncTo()
        {
        }

        static void Main(string[] args)
        {
            Assembly.Load("Microsoft.EntityFrameworkCore.Design");
            var people = new[] { "Hans", "Peter", "Jens", "Sabine" };



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
                    var remotePeople = Rest.GetPeople();

                    var localPeopleIds = _context.Status
                        .Select(s => s.Id);

                    var newPeople = remotePeople
                        .Where(remotePerson => !localPeopleIds.Contains(remotePerson.Id));

                    _context.People.AddRange(newPeople);
                    _context.Status.AddRange(localPeopleIds.Select(lpi => new Status { Id = lpi }));



                    var remotePeopleIds = remotePeople
                        .Select(s => s.Id);

                    var deletedStatuses = _context.Status
                        .Where(status => !remotePeopleIds.Contains(status.Id));

                    _context.People
                        .RemoveRange(deletedStatuses.Select(dp => new Person { Id = dp.Id }));
                    _context.Status.RemoveRange(deletedStatuses);

                    _context.SaveChanges();

                }
            }
        }
    }
}
