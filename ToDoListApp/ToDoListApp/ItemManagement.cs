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
                        completed = false
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
        /// <param name="completed">Used to update item completion status, or defaulted to null if updating another field</param>
        /// <returns>Boolean true if successful, or false if it failed</returns>
        public bool UpdateItem(Guid id, DateOnly dueDate, string title, string description, bool? completed = null)
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
                    else if (completed != null)
                        item.completed = (bool)completed;
                    else
                    {
                        Log.Information($"Item: {id} nothing to update.");
                        return false;
                    }

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

        /// <summary>
        /// Sorts the list by the title
        /// </summary>
        public bool SortByTitle()
        {
            return ValidateSort("title");
        }

        /// <summary>
        /// Sorts the list by the description
        /// </summary>
        public bool SortByDescription()
        {
            return ValidateSort("description");

        }

        /// <summary>
        /// Shows only the completed items
        /// </summary>
        /// <returns>The to-do list with only items that have been completed</returns>
        public List<Item> ShowComplete()
        {
            //Returns a list with only completed items
            return items.Where(x => x.completed == true).ToList();
        }

        /// <summary>
        /// Shows only the incomplete items
        /// </summary>
        /// <returns>The to-do list with only items that are still incomplete</returns>
        public List<Item> ShowIncomplete()
        {
            //Returns a list with only incomplete items
            return items.Where(x => x.completed == false).ToList();
        }

        /// <summary>
        /// Sorts the list based on the chosen field
        /// </summary>
        /// <param name="field">The field by which to sort, either title or description</param>
        /// <returns>True or false based on whether or not it sorted</returns>
        private bool ValidateSort(string field)
        {
            Log.Information($"Attempting to sort list items by {field}");
            //Checks if there are enough items to sort
            if (items.Count > 1)
            {
                //Orders the list by the chosen field
                if(field == "title")
                    items = items.OrderBy(x => x.title).ToList();
                else
                    items = items.OrderBy(x => x.description).ToList();

                Log.Information($"The items have been sorted by {field}");
                return true;
            }
            else
            {
                Log.Information("Not enough items to sort");
                Console.WriteLine("There needs to be at least two items to sort");
                return false;
            }
        }
    }
}
