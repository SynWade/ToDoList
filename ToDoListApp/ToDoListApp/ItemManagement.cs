using Serilog;

namespace ToDoListApp
{
    public class ItemManagement
    {
        private List<Item> items;
        public Validation validation;

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemManagement() 
        {
            items = new List<Item>();
            validation = new Validation();
        }

        /// <summary>
        /// Adds an item to the to-do list
        /// </summary>
        /// <param name="title">The title of the to-do list item</param>
        /// <param name="description">The description of the to-do list item</param>
        /// <param name="dueDate">The due date of the to-do list item</param>
        /// <returns>True if successful and false otherwise</returns>
        public bool AddItem(string title, string description, DateOnly dueDate)
        {
            try
            {
                Log.Information("Adding an item to the to-do list.");

                //Ensure title and description aren't null
                if (title != null && description != null)
                {
                    //Generate a unique id
                    Guid id = Guid.NewGuid();

                    //Create a new item object
                    Item newItem = new Item()
                    {
                        id = id,
                        title = title,
                        description = description,
                        dueDate = dueDate,
                    };

                    //Add new item to list
                    items.Add(newItem);

                    Log.Information("Item was successfully added.");
                    return true;
                }
                else
                    Console.WriteLine("All fields are mandatory.");
            }
            //Catches any unforseen errors
            catch (Exception e)
            {
                Log.Error(e, $"Item with title: {title}, description: {description}, and due date: {dueDate} failed to add.");
                Console.WriteLine("I couldn't create the item.");
            }

            return false;
        }

        /// <summary>
        /// Returns the to-do list full of items
        /// </summary>
        /// <returns>The to-do list</returns>
        public List<Item> GetItems()
        {
            Log.Information("Retrieving to-do list.");
            return items;
        }

        /// <summary>
        /// Updates the field of a provided item from the list
        /// </summary>
        /// <param name="id">The id of the item to be updated</param>
        /// <param name="dueDate">The new due date, or defaulted to minimum value if updating another field</param>
        /// <param name="title">The new title, or defaulted to an empty string if updating another field</param>
        /// <param name="description">The new description, or defaulted to an empty string if updating another field</param>
        /// <returns>Boolean true if successful, or false if it failed</returns>
        public bool UpdateItem(Guid id, DateOnly dueDate, string title, string description)
        {
            try
            {
                Log.Information($"Attempting to update an item {id}.");

                //Find the desired item
                Item item = items.FirstOrDefault(x => x.id == id);

                //Check if item exists
                if (item != null)
                {
                    //Check which field requires updating
                    if (title != "")
                        item.title = title;
                    else if (description != "")
                        item.description = description;
                    else if (dueDate != DateOnly.MinValue)
                        item.dueDate = dueDate;

                    Log.Information($"Item: {id} updated successfully.");
                    return true;
                }
                Log.Information($"Item: {id} not found.");
                Console.WriteLine("I could not find the item.");
            }
            //Catches any unforseen errors
            catch (Exception e)
            {
                Log.Error(e, $"Item: {id} failed to update.");
                Console.WriteLine("I couldn't update the item.");
            }

            return false;
        }

        /// <summary>
        /// Deletes an item from the to-do list
        /// </summary>
        /// <param name="id">The id of the item to be deleted</param>
        /// <returns></returns>
        public bool DeleteItem(Guid id)
        {
            try
            {
                Log.Information($"Attempting to remove item {id}.");

                //Find the item to delete
                Item item = items.FirstOrDefault(x => x.id == id);

                //Check if item exists
                if (item != null)
                {
                    //Remove the item
                    items.Remove(item);
                    Log.Information($"Item: {id} removed successfully.");
                    return true;
                }
                Log.Information($"Item: {id} could not be found.");
                Console.WriteLine("I could not find the item.");
            }
            //Catches any unforseen errors
            catch (Exception e)
            {
                Log.Error(e, $"Item: {id} failed to be removed.");
                Console.WriteLine("I couldn't delete the item.");
            }

            return false;
        }
    }
}
