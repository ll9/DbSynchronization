using Client.Data;
using Client.Models;
using System;
using System.Linq;
using System.Reflection;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly.Load("Microsoft.EntityFrameworkCore.Design");



            using (var _context = new ClientDbContext())
            {
                string decision = null;
                while (decision != "exit")
                {
                    Console.WriteLine("Press 's' to show all People, 'c' to randomly create a new Person or 'exit' to exit");
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
                        var project = _context.Projects.First();
                        var person = new Person { Age = 22, Name = "Hans", Project = project };
                        _context.People.Add(person);
                        _context.SaveChanges();
                    }

                }
            }
        }
    }
}
