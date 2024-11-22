using WebShopSolution.Sql.Entities;
using Xunit.Abstractions;

namespace WebShopTests.DesignPatternTests;

public class StrategyPatternTest // Känns som att det räcker att testa endast en metod
{
    private readonly EmailNotification _emailNotification;
    private readonly ITestOutputHelper _output;

    public StrategyPatternTest(ITestOutputHelper output)
    {
        _emailNotification = new EmailNotification();
        _output = output;
    }

    [Fact]
    public void SendEmailTest_Should_Send_Email()
    {
        // Arrange
        Product product = new Product { Id = 1, Name = "Product 1", Description = "Description 1", };
        var expected = $"Email: Product {product.Name} has been added to the shop";

        var writer = new StringWriter();
        Console.SetOut(writer);

        // Act
        _emailNotification.Update(product);

        // Assert
        var result = writer.GetStringBuilder().ToString().Trim();
        Assert.Equal(expected, result);
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
    }
}