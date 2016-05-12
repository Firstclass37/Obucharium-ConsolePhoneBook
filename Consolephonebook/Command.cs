﻿using System;
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
        Sort,
        Edit,

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
                case "sort":return CommandType.Sort;
                case "edit":return CommandType.Edit;

                default: return CommandType.Unknown;
            }
        }

        public static int ShowDialog()
        {
            int result = 10;
            Console.Write("Show first <count>:  ");
            int.TryParse(Console.ReadLine(), out result);
            return result; 
        }

        public static Person SearchDialog()
        {
            Console.Write("Input Name: "); string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input Surname: "); string surname = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input PhoneNumber: "); string phoneNumber = Console.ReadLine();
            Console.WriteLine();
            return new Person(name,surname,phoneNumber);
        }

        public static Person AddDialog()
        {
            Console.Write("Input Name: "); string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input Surname: "); string surname = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input PhoneNumber: "); string phoneNumber = Console.ReadLine();
            Console.WriteLine();
            return new Person(name, surname, phoneNumber);
        }

        public static int SortDialog()
        {
            int result = 0;
            Console.WriteLine("Sort By..");
            Console.Write("Chose 1-by name , 2 - by surname , 3 - by phonenumber :");       
            int.TryParse(Console.ReadKey().KeyChar.ToString(),out result);
            Console.WriteLine();

            return result;
        }

        public static int RemoveIndexDialog()
        {
            int result = -1;
            Console.Write("Chose index: ");
            int.TryParse(Console.ReadKey().KeyChar.ToString(),out result);
            Console.WriteLine();
            return result;
        }

    }
}
