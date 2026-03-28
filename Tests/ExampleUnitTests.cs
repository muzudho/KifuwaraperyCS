namespace KifuwarabeCSharp.Tests;

using Xunit;

/// <summary>
///     <pre>
/// 単体テストの例。
/// 
///     - Grok の書いたものを調整しました。
///     </pre>
/// </summary>
public class ExampleUnitTests
{
    /// <summary>
    /// 2 と 3 を足したら 5 になることをテストする例。
    /// </summary>
    [Fact]
    public void Add_TwoPlusThree_ReturnsFive()
    {
        // Arrange
        var calc = new Calculator();

        // Act
        int result = calc.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }


    /// <summary>
    ///     <pre>
    /// データ駆動テスト。
    /// 
    ///     - 複数のケースを一気にやる例。
    ///     </pre>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="expected"></param>
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, 3, -2)]
    public void Add_VariousInputs_ReturnsExpected(int a, int b, int expected)
    {
        var calc = new Calculator();
        int result = calc.Add(a, b);
        Assert.Equal(expected, result);
    }


    /// <summary>
    /// 0 で割ろうとしたら例外が投げられることをテストする例。
    /// </summary>
    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calc = new Calculator();
        Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
    }
}


/// <summary>
/// テスト対象クラス（実際はメインのプロジェクト側に置く）。
/// </summary>
public class Calculator
{
    /// <summary>
    /// x + y をします。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Add(int x, int y) => x + y;


    /// <summary>
    /// x / y をします。y が 0 のときは例外を投げます。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public int Divide(int x, int y) => y == 0
        ? throw new DivideByZeroException()
        : x / y;
}
