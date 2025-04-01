using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp;

namespace ToDoListUnitTests
{
    public class ValidationUnitTests
    {
        Validation validate;
        public ValidationUnitTests()
        {
            validate = new Validation();
        }

        [Fact]
        public void ValidateDate_Succeed()
        {
            //Setup
            //Fails as it is not a date.
            Console.SetIn(new StringReader("notValid"));
            //Fails as it is before the current date.
            Console.SetIn(new StringReader("2022-03-04"));
            //Succeeds as the date is in the future
            Console.SetIn(new StringReader("2030-07-04"));

            //Asserts
            Assert.Equal(DateOnly.Parse("2030-07-04"), validate.ValidateDate());
        }

        [Fact]
        public void OptionValidation_Succeed()
        {
            //Setup
            //Fails as it is not a number.
            Console.SetIn(new StringReader("notValid"));
            //Fails as it is not a number in the valid range.
            Console.SetIn(new StringReader("34"));
            //Succeeds as it is within the allowed range
            Console.SetIn(new StringReader("2"));

            //Asserts
            Assert.Equal(2, validate.OptionValidation(3));
        }
    }
}
