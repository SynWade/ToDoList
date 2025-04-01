using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp
{
    /// <summary>
    /// Holds the information for individual to-do list items
    /// </summary>
    public class Item
    {
        //The unique identifier
        public Guid id { get; set; }
        //The title of the item
        public string title { get; set; }
        //The description of the item
        public string description { get; set; }
        //The due date of the item
        public DateOnly dueDate { get; set; }
        //Check if item is completed
        public bool completed { get; set; }
    }
}
