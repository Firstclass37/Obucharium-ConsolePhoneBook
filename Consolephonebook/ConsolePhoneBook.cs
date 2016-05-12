using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolephonebook
{
    class ConsolePhoneBook
    {
        DBSimulator db = new DBSimulator();

        public List<Person> AllPersons { get; private set; }

        public void Start()
        {
            AllPersons = db.GetAll();
            string inputString = string.Empty;
            while ((inputString = Console.ReadLine()) != "-exit")
            {
                CommandType command = Command.GetCommandType(inputString);
                if (command == CommandType.Show) ShowPersons(AllPersons,Command.Show());
                if (command == CommandType.Search) Search(Command.Search());

            }
        }


        private void Search(Person person)
        {
            if (person.Name != String.Empty && person.Surname != String.Empty && person.PhoneNumber != String.Empty)
            {
                if (db.Contains(person))
                {
                    List<Person> result = new List<Person>();
                    result.Add(person);
                    ShowPersons(result);
                }
            }
            else if (person.Name != String.Empty && person.Surname != String.Empty && person.PhoneNumber == String.Empty)
            {
                IEnumerable<Person> nameResult = db.SearchByName(person.Name);
                IEnumerable<Person> surnameResult = db.SearchBySurname(person.Surname);
                IEnumerable<Person> result = nameResult.Where(n => surnameResult.Where(s => s.Name == n.Name).Contains(n));
                ShowPersons(result);
            }
            else if (person.Name != String.Empty && person.Surname == String.Empty && person.PhoneNumber != String.Empty)
            {
                IEnumerable<Person> nameResult = db.SearchByName(person.Name);
                IEnumerable<Person> phoneResult = db.SearchByPhone(person.PhoneNumber);
                IEnumerable<Person> result = nameResult.Where(n => phoneResult.Where(s => s.Name == n.Name).Contains(n));
                ShowPersons(result);
            }
            else if (person.Name == String.Empty && person.Surname != String.Empty && person.PhoneNumber != String.Empty)
            {
                IEnumerable<Person> surnameResult = db.SearchBySurname(person.Surname);
                IEnumerable<Person> phoneResult = db.SearchByPhone(person.PhoneNumber);
                IEnumerable<Person> result = surnameResult.Where(n => phoneResult.Where(s => s.Surname == n.Surname).Contains(n));
                ShowPersons(result);
            }
            else if (person.Name != String.Empty && person.Surname == String.Empty && person.PhoneNumber == String.Empty)
            {
                ShowPersons(db.SearchByName(person.Name));
            }
            else if (person.Name == String.Empty && person.Surname != String.Empty && person.PhoneNumber == String.Empty)
            {
                ShowPersons(db.SearchBySurname(person.Surname));
            }
            else if (person.Name == String.Empty && person.Surname == String.Empty && person.PhoneNumber != String.Empty)
            {
                ShowPersons(db.SearchByPhone(person.PhoneNumber));
            }
        }

        private void ShowPersons(IEnumerable<Person> inputPerson,int count=10)
        {
            //Ширина колонки в таблице 20
            Console.WriteLine(new String('-',71));
            Console.WriteLine("|   №  |        Name        |       Surname      |        Phone       |");
            Console.WriteLine(new String('=', 71));
            
                foreach (Person p in inputPerson)
                {
                    if (--count < 0) break;
                    string number = "|" + new string(' ', (6 - AllPersons.IndexOf(p).ToString().Length) / 2) + AllPersons.IndexOf(p).ToString() + new string(' ', 6 - AllPersons.IndexOf(p).ToString().Length - (6 - AllPersons.IndexOf(p).ToString().Length) / 2);
                    string name = "|" + new string(' ', (20 - p.Name.Length) / 2) + p.Name + new string(' ', 20 - p.Name.Length - (20 - p.Name.Length) / 2);
                    string surname = "|" + new string(' ', (20 - p.Surname.Length) / 2) + p.Surname + new string(' ', 20 - p.Surname.Length - (20 - p.Surname.Length) / 2);
                    string phone = "|" + new string(' ', (20 - p.PhoneNumber.Length) / 2) + p.PhoneNumber + new string(' ', 20 - p.PhoneNumber.Length - (20 - p.PhoneNumber.Length) / 2);
                    string result = number + name + surname + phone + "|";
                    Console.WriteLine(result);
                    Console.WriteLine(new String('-', 71));
                }
            
        }

    }
}
