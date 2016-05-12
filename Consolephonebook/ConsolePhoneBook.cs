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
        private List<Person> LastShow;

        public void Start()
        {
            AllPersons = db.GetAll();
            LastShow = AllPersons;
            ShowPersons(AllPersons);
            string inputString = string.Empty;
            while ((inputString = Command.InputCommandDialog()) != "-exit")
            {
                CommandType command = Command.GetCommandType(inputString);
                if (command == CommandType.ShowAll) ShowPersons(AllPersons, Command.ShowDialog());
                else if (command == CommandType.Search) Search(Command.SearchDialog());
                else if (command == CommandType.Add) Add(Command.AddDialog());
                else if (command == CommandType.Sort) Sort(Command.SortDialog());
                else if (command == CommandType.Remove) RemoveByIndex();
                else if (command == CommandType.Edit) Edit();
                else ShowPersons(this.LastShow);
            }
        }

        private void Add(Person person)
        {
            if (person.Name == string.Empty || person.Surname == string.Empty || person.PhoneNumber == string.Empty)
            {
                ShowMessage("Operation failed", 1);
                return;
            }
            db.Add(person);
            ShowMessage("Complete!", 0);
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
            LastShow = inputPerson.ToList(); ;
            Console.Clear();
            Console.WriteLine(new String('-',71));
            Console.WriteLine("|   №  |        Name        |       Surname      |        Phone       |");
            Console.WriteLine(new String('=', 71));

            int i = 0;
            foreach (Person p in inputPerson)
                {     
                    if (i > count-1) break;
                    string number = "|" + new string(' ', (6 - i.ToString().Length) / 2) + i.ToString() + new string(' ', 6 - i.ToString().Length - (6 - i.ToString().Length) / 2);
                    string name = "|" + new string(' ', (20 - p.Name.Length) / 2) + p.Name + new string(' ', 20 - p.Name.Length - (20 - p.Name.Length) / 2);
                    string surname = "|" + new string(' ', (20 - p.Surname.Length) / 2) + p.Surname + new string(' ', 20 - p.Surname.Length - (20 - p.Surname.Length) / 2);
                    string phone = "|" + new string(' ', (20 - p.PhoneNumber.Length) / 2) + p.PhoneNumber + new string(' ', 20 - p.PhoneNumber.Length - (20 - p.PhoneNumber.Length) / 2);
                    string result = number + name + surname + phone + "|";
                    Console.WriteLine(result);
                    Console.WriteLine(new String('-', 71));                
                i++;
                }
            Console.WriteLine("Current total person count: {0}",this.LastShow.Count);
            Console.WriteLine(new String('-', 71));

        }

        private void Sort(int type)
        {
            if (type == 0) { ShowMessage("Не выбран параметр сортировки! Операция не выполнена", 1); return; }
            else if (type == 1)
            {
                ShowPersons(this.LastShow.OrderBy(n => n.Name));
            }
            else if (type == 2)
            {    
                ShowPersons(this.LastShow.OrderBy(n => n.Surname));
            }
            else if (type == 3)
            {
                ShowPersons(this.LastShow.OrderBy(n => n.PhoneNumber));
            }
        }

        private void RemoveByIndex()
        {
            int targetIndex = Command.IndexChoseDialog();
            if ( targetIndex== -1) return;
            if (targetIndex > this.LastShow.Count - 1) { ShowMessage("Index out from range!!",1); return; }
            Person targetPerson = LastShow[targetIndex];
            LastShow.Remove(targetPerson);
            db.Remove(targetPerson);
            ShowMessage("Complete!!",0);
            ShowPersons(this.LastShow);
        }

        private void Edit()
        {
            int targetIndex = Command.IndexChoseDialog();
            if (targetIndex > this.LastShow.Count - 1) { ShowMessage("Index out from range!",1);return; }
            Person targetPerson = this.LastShow[targetIndex];
            Person personWihtChange = Command.EditDialog(targetPerson);
            db.Edit(targetPerson,personWihtChange.Name,personWihtChange.Surname,personWihtChange.PhoneNumber);
            ShowMessage("Complete!!",0);
            ShowPersons(LastShow);
        }

        private void ShowMessage(string message,int status)
        {
            if (status == 0) Console.ForegroundColor = ConsoleColor.Blue;
            if (status == 1) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
            ShowPersons(this.LastShow);
        }
        
    }
}
