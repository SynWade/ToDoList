using ToDoListApp;

namespace ToDoListUnitTests
{
    public class ItemManagementUnitTests
    {
        ItemManagement itemManagement;

        public ItemManagementUnitTests()
        {
            itemManagement = new ItemManagement();
        }

        /// <summary>
        /// Tests a fail scenario for adding an item
        /// </summary>
        [Fact]
        public void AddItem_Fail()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");

            //Asserts
            Assert.False(itemManagement.AddItem(null, "Dust, vacuum, and re-organize", date));
        }

        /// <summary>
        /// Test for successfully adding an item
        /// </summary>
        [Fact]
        public void AddItem_Succeed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));
        }

        /// <summary>
        /// Tests an empty list
        /// </summary>
        [Fact]
        public void GetItems_EmptySucceed()
        {
            Assert.Empty(itemManagement.GetItems());
        }

        /// <summary>
        /// Tests retrieving a list of items
        /// </summary>
        [Fact]
        public void GetItems_OneSucceed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.Single(itemManagement.GetItems());
            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
            Assert.False(itemManagement.GetItems()[0].completed);
        }

        /// <summary>
        /// Tests retrieving a list with multiple items
        /// </summary>
        [Fact]
        public void GetItems_MultipleSucceed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));
            Assert.True(itemManagement.AddItem("Clean bathrooms", "Dust, mop, and clean shower", date));

            //Asserts
            Assert.Equal(2, itemManagement.GetItems().Count);

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
            Assert.False(itemManagement.GetItems()[0].completed);

            Assert.Equal("Clean bathrooms", itemManagement.GetItems()[1].title);
            Assert.Equal("Dust, mop, and clean shower", itemManagement.GetItems()[1].description);
            Assert.Equal(date, itemManagement.GetItems()[1].dueDate);
            Assert.False(itemManagement.GetItems()[0].completed);
        }

        /// <summary>
        /// Tests updating the title of an item
        /// </summary>
        [Fact]
        public void UpdateItem_UpdateTitle()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);

            //Update title field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "Clean house", ""));
            
            Assert.Equal("Clean house", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
            Assert.False(itemManagement.GetItems()[0].completed);
        }

        /// <summary>
        /// Tests updating the description of an item
        /// </summary>
        [Fact]
        public void UpdateItem_UpdateDescription()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);

            //Update description field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "", "Make the bed"));

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Make the bed", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
            Assert.False(itemManagement.GetItems()[0].completed);
        }

        /// <summary>
        /// Tests updating a due date
        /// </summary>
        [Fact]
        public void UpdateItem_UpdateDueDate()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));
            DateOnly newDate = DateOnly.Parse("2030-10-23");

            //Asserts
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);

            //Update due date field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, newDate, "", ""));

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(newDate, itemManagement.GetItems()[0].dueDate);
            Assert.False(itemManagement.GetItems()[0].completed);
        }

        /// <summary>
        /// Tests updating the completion status of an item
        /// </summary>
        [Fact]
        public void UpdateItem_UpdateCompleted()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.False(itemManagement.GetItems()[0].completed);

            //Update completed field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "", "", true));

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
            Assert.True(itemManagement.GetItems()[0].completed);
        }

        /// <summary>
        /// Tests changing a status back to incomplete
        /// </summary>
        [Fact]
        public void UpdateItem_UpdateCompletedToFalse()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.False(itemManagement.GetItems()[0].completed);

            //Update completed field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "", "", true));

            //Update completed field back to false
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "", "", false));

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
            Assert.False(itemManagement.GetItems()[0].completed);
        }

        /// <summary>
        /// Tests not being able to find the item to update
        /// </summary>
        [Fact]
        public void UpdateItem_NoItemFound()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Finding new guid not in list
            Assert.False(itemManagement.UpdateItem(new Guid(), DateOnly.MinValue, "", ""));
        }

        /// <summary>
        /// Tests a scenario when nothing is provided to update
        /// </summary>
        [Fact]
        public void UpdateItem_NothingToUpdate()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Finding new guid not in list
            Assert.False(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "", ""));
        }

        /// <summary>
        /// Tests delete item with no item found
        /// </summary>
        [Fact]
        public void DeleteItem_NoItemFound()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Finding new guid not in list
            Assert.False(itemManagement.DeleteItem(new Guid()));
        }

        /// <summary>
        /// Tests successful deletion of an item
        /// </summary>
        [Fact]
        public void DeleteItem_ItemDeleted()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.Equal(1, itemManagement.GetItems().Count);
            Assert.True(itemManagement.DeleteItem(itemManagement.GetItems()[0].id));
            Assert.Equal(0, itemManagement.GetItems().Count);
        }

        /// <summary>
        /// Tests failing to sort by title
        /// </summary>
        [Fact]
        public void SortItem_TitleFail()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.False(itemManagement.SortByTitle());
        }

        /// <summary>
        /// Tests successfully sorting by title
        /// </summary>
        [Fact]
        public void SortItem_TitleSucceed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));
            Assert.True(itemManagement.AddItem("Air bedrooms", "Open windows", date));

            //Asserts
            Assert.True(itemManagement.SortByTitle());
            Assert.Equal("Air bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[1].title);
        }

        /// <summary>
        /// Tests failing to sort by description
        /// </summary>
        [Fact]
        public void SortItem_DescriptionFail()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));

            //Asserts
            Assert.False(itemManagement.SortByDescription());
        }

        /// <summary>
        /// Tests succeeding to sort by description
        /// </summary>
        [Fact]
        public void SortItem_DescriptionSucceed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Vacuum and re-organize", date));
            Assert.True(itemManagement.AddItem("Air bedrooms", "Open windows", date));

            //Asserts
            Assert.True(itemManagement.SortByDescription());
            Assert.Equal("Open windows", itemManagement.GetItems()[0].description);
            Assert.Equal("Vacuum and re-organize", itemManagement.GetItems()[1].description);
        }

        /// <summary>
        /// Tests retrieving only complete items
        /// </summary>
        [Fact]
        public void ShowComplete_Succeed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Vacuum and re-organize", date));
            Assert.True(itemManagement.AddItem("Air bedrooms", "Open windows", date));

            //Asserts
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[1].id, DateOnly.MinValue, "", "", true));
            Assert.Equal("Open windows", itemManagement.ShowComplete()[0].description);
        }

        /// <summary>
        /// Tests retrieving incomplete items
        /// </summary>
        [Fact]
        public void ShowIncomplete_Succeed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Vacuum and re-organize", date));
            Assert.True(itemManagement.AddItem("Air bedrooms", "Open windows", date));

            //Asserts
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "", "", true));
            Assert.Equal("Open windows", itemManagement.ShowIncomplete()[0].description);
        }
    }
}