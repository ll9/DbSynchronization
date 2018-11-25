using Client.Data;
using Client.Models;
using Client.Utils;
using Microsoft.EntityFrameworkCore;
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
            DownloadChanges();
            UploadChanges();

            _context.SaveChanges();

        }

        private static void UploadChanges()
        {
            CopyNewPeopleToRemote();
            DeleteDeletedPeopleToRemote();
        }

        private static void DownloadChanges()
        {
            CopyNewPeopleToLocal();
            DeleteDeletedPeopleToLocal();
        }

        private static void DeleteDeletedPeopleToLocal()
        {
            var remotePeople = RemotePeopleRepository.Get();
            var remotePeopleIds = remotePeople
                .Select(s => s.Id);

            var deletedStatuses = _context.Status
                .Where(status => !remotePeopleIds.Contains(status.Id));

            _context.People
                .RemoveRange(deletedStatuses.Select(dp => new Person { Id = dp.Id }));
            _context.Status.RemoveRange(deletedStatuses);
        }

        private static void DeleteDeletedPeopleToRemote()
        {
            var localPeopleIds = _context.People
                .Select(s => s.Id);

            var deletedStatuses = _context.Status
                .Where(status => !localPeopleIds.Contains(status.Id));


            // TODO: REMOVE deleted Statuses in remote
            _context.Status.RemoveRange(deletedStatuses);

        }

        private static void CopyNewPeopleToLocal()
        {
            var remotePeople = RemotePeopleRepository.Get();

            var statusIds = _context.Status
                .Select(s => s.Id);

            var newPeople = remotePeople
                .Where(remotePerson => !statusIds.Contains(remotePerson.Id));

            _context.People.AddRange(newPeople);
            _context.Status.AddRange(statusIds.Select(lpi => new Status { Id = lpi }));
        }

        private static void CopyNewPeopleToRemote()
        {

            var statusIds = _context.Status
                .Select(s => s.Id);

            var newPeople = _context.People
                .Where(localPerson => !statusIds.Contains(localPerson.Id));

            _context.Status.AddRange(statusIds.Select(lpi => new Status { Id = lpi }));
            // TODO: POST PEOPLE TO REMOTE
            // _context.People.AddRange(newPeople);
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


                }
            }
        }
    }
}
