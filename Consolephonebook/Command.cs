using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolephonebook
{
    public enum CommandType
    {
        Show,
        Help,
        Add,
        Search,
        Remove,
        RemovebyIndex,
        Contains,
        SearchByName,
        SearchBySurname,
        SearchByPhone,

        Unknown
    }

    public static class Command
    {
        public static CommandType GetCommandType(string inputString)
        {
            switch (inputString.ToLower())
            {
                case "show":return CommandType.Show;
                case "help":return CommandType.Help;
                case "add":return CommandType.Add;
                case "search":return CommandType.Search;
                case "remove":return CommandType.Remove;
                case "removebyindex": return CommandType.RemovebyIndex;
                case "contains":return CommandType.Contains;
                case "searchbyname":return CommandType.SearchByName;
                case "searchbysurname":return CommandType.SearchBySurname;
                case "searchbyphone":return CommandType.SearchByPhone;
                default: return CommandType.Unknown;
            }
        }

        public static int Show()
        {
            int result = 10;
            Console.Write("Show first <count>:  ");
            int.TryParse(Console.ReadLine(), out result);
            return result; 
        }
        public static Person Search()
        {
            Console.Write("Input Name: "); string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input Surname: "); string surname = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input PhoneNumber: "); string phoneNumber = Console.ReadLine();
            Console.WriteLine();
            return new Person(name,surname,phoneNumber);
        }

    }
}
