using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ToDoListApp
{
    public class Validation
    {

        public DateOnly ValidateDate()
        {
            Log.Information($"Validating date.");

            DateOnly validDate = DateOnly.MinValue;
            bool validChoice = false;
            while (!validChoice)
            {
                if (DateOnly.TryParse(Console.ReadLine(), out validDate))
                    if (DateOnly.FromDateTime(DateTime.Now) > validDate)
                    {
                        Console.WriteLine("The due date should be a later date than today.");
                    }
                    else
                    {
                        Log.Information($"Date is valid.");
                        validChoice = true;
                    }
                else
                    Console.WriteLine("The due date must be formatted properly, i.e. (YYYY-MM-DD).");
            }

            return validDate;
        }

        public int OptionValidation(int max)
        {
            bool validChoice = false;
            int menuChoice = 0;
            while (!validChoice)
            {
                if (int.TryParse(Console.ReadLine(), out menuChoice))
                    if (menuChoice > 0 && menuChoice <= max)
                        validChoice = true;
                    else
                        Console.WriteLine("Please choose a number corresponding to one of the items above.");
                else
                    Console.WriteLine("Please choose a number corresponding to one of the items above.");
            }

            return menuChoice;
        }
    }
}
