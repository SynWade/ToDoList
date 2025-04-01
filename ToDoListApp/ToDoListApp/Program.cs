using Serilog;
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
    Console.WriteLine("4. delete item.");
    Console.WriteLine("9. end program.");

    //Validate proper menu item
    bool validChoice = false;
    while (!validChoice)
    {
        if(int.TryParse(Console.ReadLine(), out menuChoice))
            if((menuChoice >= 1 && menuChoice <= 4) || menuChoice == 9)
                validChoice = true;
            else
                Console.WriteLine("Please choose a number corresponding to one of the items above.");
        else
            Console.WriteLine("Please choose a number corresponding to one of the items above.");
    }

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
            break;
        //Displays the list of items
        case 2:
            //Gets the list items
            items = itemManagement.GetItems();
            //Displays the list items
            common.DisplayOptions(items, "list");
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

            //Validates proper choice of field to update
            int updateFieldChoice = validation.OptionValidation(3);

            Console.WriteLine("Please input the new value.");

            DateOnly newDate = DateOnly.MinValue;
            string newValue = "";

            if (updateFieldChoice == 3)
                //Validate date if that's what is being updated
                newDate = validation.ValidateDate();
            else
                newValue = Console.ReadLine();

            switch (updateFieldChoice)
                {
                    case 1:
                        //Updating title
                        itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, newValue, "");
                        break;
                    case 2:
                        //Updating description
                        itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, "", newValue);
                        break;
                    case 3:
                        //Updating due date
                        itemManagement.UpdateItem(items[itemChoice - 1].id, newDate, "", "");
                        break;
                }
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
            break;
    }
}