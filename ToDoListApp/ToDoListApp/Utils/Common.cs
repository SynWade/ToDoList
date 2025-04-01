using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public class Common
    {
        /// <summary>
        /// Displays the current to-do list items
        /// </summary>
        /// <param name="items">The current to-do list</param>
        /// <param name="action">The action being performed (list, update, delete)</param>
        public void DisplayOptions(List<Item> items, string action)
        {
            //Display the desire for input if performing update or delete
            if (action != "list")
                Console.WriteLine($"Please input the number corresponding to the item you wish to {action}.");
            
            //Displays each item in the to-do list
            int itemNumber = 1;
            foreach (Item item in items)
            {
                //Used to count items if performing update or delete for selection
                if (action != "list")
                    Console.WriteLine($"Item {itemNumber.ToString()}:");

                //Displays item fields
                Console.WriteLine($"Title: {item.title}");
                Console.WriteLine($"Description: {item.description}");
                Console.WriteLine($"Due date: {item.dueDate.ToString()}");
                Console.WriteLine();
                itemNumber++;
            }
        }
    }
}
