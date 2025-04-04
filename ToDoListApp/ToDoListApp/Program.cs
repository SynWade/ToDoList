﻿using Serilog;
using ToDoListApp;

//Setting up a log file
Log.Logger = new LoggerConfiguration().WriteTo.File("log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

//Adding classes
ItemManagement itemManagement = new ItemManagement();
Validation validation = new Validation();
Common common = new Common();

Console.WriteLine("Welcome to your to-do list!");
int menuChoice = 0;

//Continues running until the user chooses 9 to exit from the program.
while(menuChoice != 9)
{
    //Display main menu
    Console.WriteLine("Please input the number corresponding to the action you wish to take.");
    Console.WriteLine("1. Add item.");
    Console.WriteLine("2. Display list.");
    Console.WriteLine("3. Update item.");
    Console.WriteLine("4. Delete item.");
    Console.WriteLine("5. Sort by title.");
    Console.WriteLine("6. Sort by description.");
    Console.WriteLine("7. Show completed items.");
    Console.WriteLine("8. Show incomplete items.");
    Console.WriteLine("9. end program.");

    menuChoice = validation.OptionValidation(9);

    //Declare variables
    List<Item> items;
    int itemChoice;

    switch (menuChoice)
    {
        //Adding a new item
        case 1:
            Console.WriteLine("What is the title of your to-do list item:");
            string title = Console.ReadLine();
            Console.WriteLine("What is the description of your to-do list item:");
            string description = Console.ReadLine();
            Console.WriteLine("What is the due date of your to-do list item (YYYY-MM-DD):");
            
            //Validates due date
            DateOnly dueDate = validation.ValidateDate();

            //Add the item
            itemManagement.AddItem(title, description, dueDate);
            Console.WriteLine();
            break;
        //Displays the list of items
        case 2:
            //Gets the list items
            items = itemManagement.GetItems();
            //Displays the list items
            common.DisplayOptions(items, "list");
            Console.WriteLine();
            break;
        case 3:
            //Gets the items from the list
            items = itemManagement.GetItems();

            //Displays the list items
            common.DisplayOptions(items, "update");

            //Validates proper choice of item to update
            itemChoice = validation.OptionValidation(items.Count);

            Console.WriteLine("Please input the number corresponding to the field you wish to update.");
            Console.WriteLine("1: title.");
            Console.WriteLine("2: description.");
            Console.WriteLine("3: due date.");
            Console.WriteLine("4: completed.");

            //Validates proper choice of field to update
            int updateFieldChoice = validation.OptionValidation(4);

            Console.WriteLine("Please input the new value.");

            DateOnly newDate = DateOnly.MinValue;
            string newValue = "";

            switch (updateFieldChoice)
            {
                case 1:
                    //Updating title
                    newValue = Console.ReadLine();
                    itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, newValue, "");
                    break;
                case 2:
                    //Updating description
                    newValue = Console.ReadLine();
                    itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, "", newValue);
                    break;
                case 3:
                    //Updating due date
                    newDate = validation.ValidateDate();
                    itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, "", "");
                    break;
                case 4:
                    //Updating completed status
                    Console.WriteLine("Please input the number corresponding to whether or not the item is completed.");
                    Console.WriteLine("1: true.");
                    Console.WriteLine("2: false.");

                    //Validates proper choice of status to update
                    if(validation.OptionValidation(2) == 1)
                        itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, "", "", true);
                    else
                        itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, "", "", false);
                break;
            }
            Console.WriteLine();
            break;
        case 4:
            //Get list items
            items = itemManagement.GetItems();

            //Display list items
            common.DisplayOptions(items, "delete");

            //Validate a proper item was chosen
            itemChoice = validation.OptionValidation(items.Count);

            //Delete the item
            itemManagement.DeleteItem(items[itemChoice - 1].id);
            Console.WriteLine();
            break;
        case 5:
            //Sorts the list by title
            itemManagement.SortByTitle();
            Console.WriteLine();
            break;
        case 6:
            //Sorts the list by description
            itemManagement.SortByDescription();
            Console.WriteLine();
            break;
        case 7:
            //Gets the list items that are completed
            items = itemManagement.ShowComplete();
            if(items.Count == 0)
                Console.WriteLine("There are no completed tasks.");
            else 
                //Displays the list items
                common.DisplayOptions(items, "list");
            Console.WriteLine();
            break;
        case 8:
            //Gets the list items that are incomplete
            items = itemManagement.ShowIncomplete();
            if (items.Count == 0)
                Console.WriteLine("There are no incomplete tasks.");
            else
                //Displays the list items
                common.DisplayOptions(items, "list");
            Console.WriteLine();
            break;
    }
}