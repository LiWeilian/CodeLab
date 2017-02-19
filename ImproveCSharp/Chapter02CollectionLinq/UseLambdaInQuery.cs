using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02CollectionLinq
{
    class UseLambdaInQuery
    {
        class Person
        {
            public string Name { get; set; }
            public int CompanyID { get; set; }
        }
        static public void Method1()
        {
            List<Person> personList = new List<Person>()
            {
                new Person() { Name = "Rose", CompanyID = 1},
                new Person() { Name = "Steve", CompanyID = 0},
                new Person() { Name = "Jessica", CompanyID = 1}
            };

            var personWithCompanyList = from person in personList
                                        select new
                                        {
                                            PersonName = person.Name,
                                            CompanyName = person.CompanyID == 0 ? "Micro": "Sun"
                                        };

            foreach (var item in personWithCompanyList)
            {
                Console.WriteLine(string.Format("{0} : {1}", item.PersonName, item.CompanyName));
            }
            Console.WriteLine("");

            foreach (var item in personWithCompanyList.Where(p => p.CompanyName == "Sun"))
            {
                Console.WriteLine(string.Format("{0} : {1}", item.PersonName, item.CompanyName));
            }
            Console.WriteLine("");

            foreach (var item in personWithCompanyList.OrderByDescending(p => p.PersonName))
            {
                Console.WriteLine(string.Format("{0} : {1}", item.PersonName, item.CompanyName));
            }
            Console.WriteLine("");
        }

        static public void Method2()
        {
            List<Person> personList = new List<Person>()
            {
                new Person() { Name = "Rose", CompanyID = 1},
                new Person() { Name = "Steve", CompanyID = 0},
                new Person() { Name = "Jessica", CompanyID = 1}
            };

            foreach (var item in personList.Select(person => new { PersonName = person.Name, CompanyName = person.CompanyID == 0 ? "Micro" : "Sun" }))
            {
                Console.WriteLine(string.Format("{0} : {1}", item.PersonName, item.CompanyName));
            }
        }
    }
}
