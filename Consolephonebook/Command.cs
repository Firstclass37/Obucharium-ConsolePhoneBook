﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Consolephonebook
{
    

    public static class Command
    {
        public static CommandType GetCommandType(string inputString)
        {
            switch (inputString.ToLower())
            {
                case "showall":return CommandType.ShowAll;
                case "help":return CommandType.Help;
                case "add":return CommandType.Add;
                case "search":return CommandType.Search;
                case "remove":return CommandType.Remove;
                case "sort":return CommandType.Sort;
                case "edit":return CommandType.Edit;

                default: return CommandType.Unknown;
            }
        }

        public static string InputCommandDialog()
        {
            string result = string.Empty;
            Console.Write(@"Input a command(enter 'help' for show all comamnds): ");
            result = Console.ReadLine().Trim();
            Console.WriteLine();
            return result;
        } 

        public static int ShowDialog()
        {
            int result = 15;
            Console.Write("     Enter count:  ");
            int.TryParse(Console.ReadLine().Trim(), out result);
            return result; 
        }

        public static Person SearchDialog()
        {
            Console.Write("     Input Name: "); string name = Console.ReadLine().Trim();
            Console.WriteLine();
            Console.Write("     Input Surname: "); string surname = Console.ReadLine().Trim();
            Console.WriteLine();
            Console.Write("     Input PhoneNumber: "); string phoneNumber = Console.ReadLine().Trim();
            Console.WriteLine();
            return new Person() {Name = name, Surname = surname,PhoneNumber = phoneNumber};
        }

        public static Person AddDialog()
        {
            Console.Write("     Input Name: "); string name = Console.ReadLine().Trim();
            Console.WriteLine();
            Console.Write("     Input Surname: "); string surname = Console.ReadLine().Trim();
            Console.WriteLine();
            Console.Write("     Input PhoneNumber: "); string phoneNumber = Console.ReadLine().Trim();
            Console.WriteLine();
            return new Person() { Name = name, Surname = surname, PhoneNumber = phoneNumber };
        }

        public static int SortDialog()
        {
            int result = 0;
            Console.WriteLine("     Sort By..");
            Console.Write("     Chose 1-by name , 2 - by surname , 3 - by phonenumber :");       
            int.TryParse(Console.ReadKey().KeyChar.ToString(), out result);
            Console.WriteLine();

            return result;
        }

        public static int IndexChoseDialog()
        {
            int result = -1;
            Console.Write("     Chose index: ");
            int.TryParse(Console.ReadLine().Trim(),out result);
            Console.WriteLine();
            return result;
        }

        public static void EditDialog(Person targetPerson)
        {
            Console.Write("     Current Name: {0}. New Name:",targetPerson.Name);
            string name = Console.ReadLine().Trim();
            if (name != string.Empty) targetPerson.Name = name;
            Console.WriteLine();
            Console.Write("     Current Surname: {0}. New Surname:", targetPerson.Surname);
            string surname = Console.ReadLine().Trim();
            if (surname != string.Empty) targetPerson.Surname = surname;
            Console.WriteLine();
            Console.Write("     Current Phone: {0}. New Phone:", targetPerson.PhoneNumber);
            string phone = Console.ReadLine().Trim();
            if (phone != string.Empty) targetPerson.PhoneNumber = phone;
            Console.WriteLine();
            
        }

        public static bool ConfirmDialog()
        {
            Console.Write("     Are you sure?(y/n)");
            char result = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (result == 'y') return true;
            return false;
        }

    }
}
