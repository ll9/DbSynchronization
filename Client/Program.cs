﻿using Client.Data;
using Client.Models;
using Client.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
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
            var req = RemotePeopleRepository.Get();
            if (req.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpRequestException("No Internet Exception");
            }

            var source = req.Data;
            var souceIds = source
                .Select(s => s.Id);

            var deletedStatuses = _context.Status
                .Where(status => !souceIds.Contains(status.Id));
            var deletedPeople = _context.People
                .Where(person => deletedStatuses.Select(s => s.Id).Contains(person.Id));

            _context.People
                .RemoveRange(deletedPeople);
            _context.Status.RemoveRange(deletedStatuses);
        }

        private static void DeleteDeletedPeopleToRemote()
        {
            var localPeopleIds = _context.People
                .Select(s => s.Id);

            var deletedStatuses = _context.Status
                .Where(status => !localPeopleIds.Contains(status.Id));


            var req = RemotePeopleRepository.Delete(deletedStatuses.Select(s => s.Id).ToList());
            if (req.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpRequestException("No Internet Exception");
            }
            _context.Status.RemoveRange(deletedStatuses);

        }

        private static void CopyNewPeopleToLocal()
        {
            var req = RemotePeopleRepository.Get();
            if (req.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpRequestException("No Internet Exception");
            }

            var source = req.Data;

            var statusIds = _context.Status
                .Select(s => s.Id);

            var newPeople = source
                .Where(s => !statusIds.Contains(s.Id));

            _context.People.AddRange(newPeople);
            _context.Status.AddRange(newPeople.Select(np => new Status { Id = np.Id }));
        }

        private static void CopyNewPeopleToRemote()
        {

            var statusIds = _context.Status
                .Select(s => s.Id);

            var newPeople = _context.People
                .Where(localPerson => !statusIds.Contains(localPerson.Id));

            var req = RemotePeopleRepository.Add(newPeople.ToList());
            if (req.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new HttpRequestException("No Internet Exception");
            }
            _context.Status.AddRange(newPeople.Select(p => new Status { Id = p.Id }));
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
                    "'d' to delete first Person, " + 
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
                else if (decision == "d")
                {
                    _context.People.Remove(_context.People.First());
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
