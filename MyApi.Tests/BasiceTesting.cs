// using Xunit;
// namespace MyApi.Tests;

// public class BasicTesting
// {
//     [Fact]
//     public void BasicTests()
//     {
//         int a = 10;
//         int b = 20;
        
//         int result = a + b;

//         Assert.Equal(30, result);
//     }
//     [Theory]
//     [InlineData(12, false)]
//     [InlineData(19, true)]
//     [InlineData(1, false)]
//     [InlineData(67, true)]
//     [InlineData(2, false)]
//     [InlineData(84, true)]
//     [InlineData(3, false)]
//     [InlineData(24, true)]
//     [InlineData(4, false)]
//     [InlineData(18, true)]
//     public void IsAdultTests(int age, bool expected)
//     {
//         AgeValidator ageValidator = new();

//         bool result = ageValidator.IsAdult(age);

//         Assert.Equal(expected, result);
//     }
// }

// public class AgeValidator
// {
//     public bool IsAdult(int age)
//     {
//         if(age >= 18) return true;
//         return false;
//     }
// }