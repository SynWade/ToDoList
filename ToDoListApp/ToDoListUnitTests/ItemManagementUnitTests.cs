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

        [Fact]
        public void AddItem_Fail()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");

            //Asserts
            Assert.False(itemManagement.AddItem(null, "Dust, vacuum, and re-organize", date));
        }

        [Fact]
        public void AddItem_Succeed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            Assert.True(itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date));
        }

        [Fact]
        public void GetItems_EmptySucceed()
        {
            Assert.Empty(itemManagement.GetItems());
        }

        [Fact]
        public void GetItems_OneSucceed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);

            //Asserts
            Assert.Single(itemManagement.GetItems());
            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
        }

        [Fact]
        public void GetItems_MultipleSucceed()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);
            itemManagement.AddItem("Clean bathrooms", "Dust, mop, and clean shower", date);

            //Asserts
            Assert.Equal(2, itemManagement.GetItems().Count);

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);

            Assert.Equal("Clean bathrooms", itemManagement.GetItems()[1].title);
            Assert.Equal("Dust, mop, and clean shower", itemManagement.GetItems()[1].description);
            Assert.Equal(date, itemManagement.GetItems()[1].dueDate);
        }
        
        [Fact]
        public void UpdateItem_UpdateTitle()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);

            //Asserts
            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);

            //Update title field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "Clean house", ""));
            
            Assert.Equal("Clean house", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
        }

        [Fact]
        public void UpdateItem_UpdateDescription()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);

            //Asserts
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);

            //Update description field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, DateOnly.MinValue, "", "Make the bed"));

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Make the bed", itemManagement.GetItems()[0].description);
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);
        }

        [Fact]
        public void UpdateItem_UpdateDueDate()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);
            DateOnly newDate = DateOnly.Parse("2030-10-23");

            //Asserts
            Assert.Equal(date, itemManagement.GetItems()[0].dueDate);

            //Update description field
            Assert.True(itemManagement.UpdateItem(itemManagement.GetItems()[0].id, newDate, "", ""));

            Assert.Equal("Clean bedrooms", itemManagement.GetItems()[0].title);
            Assert.Equal("Dust, vacuum, and re-organize", itemManagement.GetItems()[0].description);
            Assert.Equal(newDate, itemManagement.GetItems()[0].dueDate);
        }

        [Fact]
        public void UpdateItem_NoItemFound()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);

            //Finding new guid not in list
            Assert.False(itemManagement.UpdateItem(new Guid(), DateOnly.MinValue, "", ""));
        }

        [Fact]
        public void DeleteItem_NoItemFound()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);

            //Finding new guid not in list
            Assert.False(itemManagement.DeleteItem(new Guid()));
        }

        [Fact]
        public void DeleteItem_ItemDeleted()
        {
            //Setup
            DateOnly date = DateOnly.Parse("2025-10-23");
            itemManagement.AddItem("Clean bedrooms", "Dust, vacuum, and re-organize", date);

            //Asserts
            Assert.Equal(1, itemManagement.GetItems().Count);
            Assert.True(itemManagement.DeleteItem(itemManagement.GetItems()[0].id));
            Assert.Equal(0, itemManagement.GetItems().Count);
        }
    }
}