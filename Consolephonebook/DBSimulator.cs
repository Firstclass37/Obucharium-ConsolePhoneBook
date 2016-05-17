using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Consolephonebook
{
    public class DBSimulator
    {
        List<Person> persons = new List<Person>();

        public DBSimulator()
        {
            GeneratePersons();
        }

        public List<Person> GetAll()
        {
            return persons.ToList();
        }
        public void Add(Person person)
        {
            if (!persons.Contains(person)) persons.Add(person);
        }
        public void Remove(Person person)
        {
            persons.Remove(person);
        }
        public void RemoveByIndex(int index)
        {
            if (index > 0 && index < persons.Count) persons.Remove(persons[index]);
        }
        public bool Contains(Person person)
        {
            return persons.Contains(person);
        }
        public IEnumerable<Person> SearchByName(string name)
        {
            return persons.Where(n => n.Name == name);
        }
        public IEnumerable<Person> SearchBySurname(string surname)
        {
            return persons.Where(n => n.Surname == surname);
        }
        public IEnumerable<Person> SearchByPhone(string phone)
        {
            return persons.Where(n => n.PhoneNumber == phone);
        }
        public void Edit(Person targetPerson,string newName,string newSurname,string newPhone)
        {
            if (persons.Contains(targetPerson))
            {
                Person personForChange = persons.Where(n => n == targetPerson).First();
                if (newName != string.Empty) personForChange.Name = newName;
                if (newSurname != string.Empty) personForChange.Surname = newSurname;
                if (newPhone != string.Empty) personForChange.PhoneNumber = newPhone;
            }
        }


        private void GeneratePersons()
        {
            for (int i = 0; i < 10; i++)
            {
                persons.Add(new Person(GenerateName(), GenerateSurname(), GeneratePhoneNumber()));                
            } 
        }
        private string GenerateName()
        {
            Thread.Sleep(100);
            string[] names = new string[] { "Ivan", "Alex", "Nikolay", "Vladimir", "Evgeny", "Kirill", "Leonty" };
            int index = new Random().Next(0, names.Length);
            return names[index];
        }
        private string GenerateSurname()
        {
            Thread.Sleep(100);
            string[] surnames = new string[] { "Adamov","Lavrov","Medvedev","Karasev","Kochergin","Ivanov","Volkov"};
            return surnames[new Random().Next(0, surnames.Length)];
        }
        private string GeneratePhoneNumber()
        {
            Thread.Sleep(100);
            Random rand = new Random();
            return string.Format("{0}-({1}{2}{3})-{4}{5}{6}{7}{8}{9}{10}","+7","9",
                                                                      rand.Next(0,4),
                                                                      rand.Next(0,10),
                                                                      rand.Next(0, 10),
                                                                      rand.Next(0, 10),
                                                                      rand.Next(0, 10),
                                                                      rand.Next(0, 10),
                                                                      rand.Next(0, 10),
                                                                      rand.Next(0, 10),
                                                                      rand.Next(0, 10));
        }
    }
}
