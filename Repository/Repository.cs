using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository
    {
        private Efdbcontext context = new Efdbcontext();

        public IEnumerable<Person> People
        {
            get
            {
                return context.People.ToList();
            }
        }

        public void SavePerson(Person person)
        {
            if (person.Id == 0)
            {
                context.People.Add(person);
            }
            else
            {
                Person targetPerson = context.People.Find(person.Id);
                if (targetPerson != null)
                {
                    targetPerson.Name = person.Name;
                    targetPerson.Surname = person.Surname;
                    targetPerson.PhoneNumber = person.PhoneNumber;
                }
            }
            context.SaveChanges();
        }

        public void RemovePerson(Person person)
        {
            context.People.Remove(person);
            context.SaveChanges();
        }
    }
}
