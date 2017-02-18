using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02CollectionLinq
{
    class NoListAsBaseClass
    {
        public static void Method1()
        {
            /*
            Persons personList = new Persons()
            {
                new Person() { Name = "Rose", Age = 19},
                new Person() { Name = "Steve", Age = 45}
            };
            */

            IList<Person> persons = new Persons()
            {
                new Person() { Name = "Rose", Age = 19},
                new Person() { Name = "Steve", Age = 45}
            };

            //会调用基类的Add方法
            persons.Add(new Person() { Name = "Jessica", Age = 20 });

            foreach (Person p in persons)
            {
                Console.WriteLine(p.Name);
            }
        }        

        public static void Method2()
        {
            IList<Person> persons = new Persons2()
            {
                new Person() { Name = "Rose", Age = 19},
                new Person() { Name = "Steve", Age = 45}
            };
            
            persons.Add(new Person() { Name = "Jessica", Age = 20 });

            foreach (Person p in persons)
            {
                Console.WriteLine(p.Name);
            }
        }
    }

    class Persons: List<Person>
    {
        public new void Add(Person p)
        {
            p.Name += " Changed!";
            base.Add(p);
        }
    }

    class Persons2 : IEnumerable<Person>, IList<Person>
    {
        List<Person> items = new List<Person>();

        public Person this[int index]
        {
            get
            {
                return ((IList<Person>)items)[index];
            }

            set
            {
                ((IList<Person>)items)[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return ((IList<Person>)items).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<Person>)items).IsReadOnly;
            }
        }

        public void Add(Person item)
        {
            item.Name += " Changed!";
            ((IList<Person>)items).Add(item);
        }

        public void Clear()
        {
            ((IList<Person>)items).Clear();
        }

        public bool Contains(Person item)
        {
            return ((IList<Person>)items).Contains(item);
        }

        public void CopyTo(Person[] array, int arrayIndex)
        {
            ((IList<Person>)items).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return ((IEnumerable<Person>)items).GetEnumerator();
        }

        public int IndexOf(Person item)
        {
            return ((IList<Person>)items).IndexOf(item);
        }

        public void Insert(int index, Person item)
        {
            ((IList<Person>)items).Insert(index, item);
        }

        public bool Remove(Person item)
        {
            return ((IList<Person>)items).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Person>)items).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Person>)items).GetEnumerator();
        }
    }
}
