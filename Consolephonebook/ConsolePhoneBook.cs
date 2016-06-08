using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Consolephonebook
{
    public class ConsolePhoneBook
    {
        private Repository.Repository repository = new Repository.Repository();

        public List<Person> AllPersons;
        private List<Person> LastShow;

        public void Start()
        {
            AllPersons = repository.People.ToList();
            LastShow = AllPersons;
            ShowPersons(AllPersons);
            string commandString = string.Empty;
            while ((commandString = Command.InputCommandDialog()) != "-exit")
            {
                CommandType command = Command.GetCommandType(commandString);
                switch (command)
                {
                    case CommandType.ShowAll:
                        ShowPersons(AllPersons, Command.ShowDialog());
                        break;
                    case CommandType.Search:
                        Search(Command.SearchDialog());
                        break;
                    case CommandType.Add:
                        Add(Command.AddDialog());
                        break;
                    case CommandType.Sort:
                        Sort(Command.SortDialog());
                        break;
                    case CommandType.Remove:
                        RemoveByIndex();
                        break;
                    case CommandType.Edit:
                        Edit();
                        break;
                    case CommandType.Help:
                        ShowHelp();
                        break;
                    default:
                        ShowPersons(this.LastShow);
                        break;
                }
                
            }
        }

        private void Add(Person person)
        {
            if (person.Name == string.Empty || person.Surname == string.Empty || person.PhoneNumber == string.Empty)
            {
                ShowMessage("Operation failed", 1);
                return;
            }
            if ( !CheckNameCorrectness(person.Name)||
                 !CheckSurnameCorrectness(person.Surname)||
                 !CheckPhoneCorrectness(person.PhoneNumber))
            {
                ShowMessage("Operation failed! InCorrectness input", 1);
                return;
            }
            repository.SavePerson(person);
            AllPersons = repository.People.ToList();
            ShowPersons(AllPersons);
            ShowMessage("Complete!", 0);
        }

        private void Search(Person person)
        {
            var nameResult = person.Name == string.Empty ? AllPersons : AllPersons.Where(p=>p.Name.Equals(person.Name));
            var surnameResult = person.Surname == string.Empty ? AllPersons : AllPersons.Where(p=>p.Surname.Equals(person.Surname));
            var phoneresult = person.PhoneNumber == string.Empty ? AllPersons : AllPersons.Where(p=>p.PhoneNumber.Equals(person.PhoneNumber));
            var result = nameResult.Intersect(surnameResult.Intersect(phoneresult));
            ShowPersons(result);
        }

        private void ShowPersons(IEnumerable<Person> inputPerson,int count = 15)
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
            if (type == 0)
            {
                ShowMessage("Не выбран параметр сортировки! Операция не выполнена", 1);
                return;
            }
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
            if ( targetIndex == -1) return;
            if (targetIndex > this.LastShow.Count - 1)
            {
                ShowMessage("Index out from range!!",1);
                return;
            }
            if (!Command.ConfirmDialog())
            {
                ShowMessage("Operation aborted",1);
                return;
            }
            Person targetPerson = LastShow[targetIndex];
            LastShow.Remove(targetPerson);
            repository.RemovePerson(targetPerson);
            AllPersons = repository.People.ToList();
            ShowMessage("Complete!!",0);
            ShowPersons(this.LastShow);
        }

        private void Edit()
        {
            int targetIndex = Command.IndexChoseDialog();
            if (targetIndex > this.LastShow.Count - 1)
            {
                ShowMessage("Index out from range!",1);
                return;
            }
            Person targetPerson = this.LastShow[targetIndex];
            Command.EditDialog(targetPerson);

            if (!CheckNameCorrectness(targetPerson.Name)  ||
                !CheckSurnameCorrectness(targetPerson.Surname) ||
                !CheckPhoneCorrectness(targetPerson.PhoneNumber))
            {
                ShowMessage("Operation failed! InCorrectness input", 1);
                return;
            }
            if (!Command.ConfirmDialog())
            {
                ShowMessage("Operation aborted", 1);
                return;
            }

            repository.SavePerson(targetPerson);
            AllPersons = repository.People.ToList();
            ShowMessage("Complete!!",0);
            ShowPersons(LastShow);
        }

        private void ShowMessage(string message,int status)
        {
            if (status == 0) Console.ForegroundColor = ConsoleColor.Blue;
            if (status == 1) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("     " + message);
            Console.ResetColor();
            Console.ReadKey();
            ShowPersons(this.LastShow);
        }

        private void ShowHelp()
        {
            Console.WriteLine(@"Command List:
        
        showall
        edit
        search
        add
        remove (only by index)
        sort
    
        Warning!!! max name, surname or phonenumber lenght is 15!!
            ");

            ShowMessage("Will be more commands in the future!",0);
        }

        private bool CheckNameCorrectness(string name)
        {
            if (name.Length > 15) return false;

            foreach (var c in name)
            {
                if (!char.IsLetter(c)) return false;
                
            }

            return true;
        }

        private bool CheckSurnameCorrectness(string surname)
        {
            if (surname.Length > 15) return false;

            foreach (var c in surname)
            {
                if (c == '-') continue;
                if (!char.IsLetter(c)) return false;

            }

            return true;
        }

        private bool CheckPhoneCorrectness(string phone)
        {
            if (phone.Length > 15) return false;
            string temp = string.Copy(phone);

            int indexRightBracket = phone.IndexOf("(");
            int indexLeftBracket = phone.IndexOf(")");
            if (indexRightBracket == -1 && indexLeftBracket != -1) return false;
            if (indexRightBracket != -1 && indexLeftBracket == -1) return false;

            foreach (var c in phone)
            {
                if (c == '-') continue;
                if (c == '+') continue;
                if (char.IsLetter(c)) return false;
            }
            return true;
        }



    }
}
