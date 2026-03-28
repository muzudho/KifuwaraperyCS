namespace KifuwarabeCSharp.Tests;

using KifuwarabeCSharp.Core.Usi.Elements;
using Xunit;

public class ElementsTest
{
    /// <summary>
    /// 2 と 3 を足したら 5 になることをテストする例。
    /// </summary>
    [Fact]
    public void Value_RadixHalfPlyFive_ReturnsFive()
    {
        // Expected
        var expected = 5;

        // Act
        int result = new MuzRadixHalfPlyModel(expected).Value;

        // Assert
        Assert.Equal(expected, result);
    }
}
