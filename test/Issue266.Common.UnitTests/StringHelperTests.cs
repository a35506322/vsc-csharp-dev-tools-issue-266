namespace Issue266.Common.UnitTests;

[TestClass]
public class StringHelperTests
{
    [TestMethod]
    public void ToHalfWidth_全形數字_應轉換為半形數字()
    {
        "０１２３４５６７８９".ToHalfWidth().Should().Be("0123456789");
    }

    [TestMethod]
    public void ToHalfWidth_全形英文大寫_應轉換為半形英文大寫()
    {
        "ＡＢＣ".ToHalfWidth().Should().Be("ABC");
    }

    [TestMethod]
    public void ToHalfWidth_全形英文小寫_應轉換為半形英文小寫()
    {
        "ａｂｃ".ToHalfWidth().Should().Be("abc");
    }

    [TestMethod]
    public void ToHalfWidth_全形標點_應轉換為半形標點()
    {
        "！＠＃".ToHalfWidth().Should().Be("!@#");
    }

    [TestMethod]
    public void ToHalfWidth_全形空白_應轉換為半形空白()
    {
        "　".ToHalfWidth().Should().Be(" ");
    }

    [TestMethod]
    public void ToHalfWidth_半形字串_應維持原值()
    {
        "hello".ToHalfWidth().Should().Be("hello");
    }

    [TestMethod]
    public void ToHalfWidth_空字串_應維持空字串()
    {
        "".ToHalfWidth().Should().BeEmpty();
    }

    [TestMethod]
    public void ToHalfWidth_null_應拋出ArgumentNullException()
    {
        var act = () => ((string)null!).ToHalfWidth();
        act.Should().Throw<ArgumentNullException>().WithParameterName("input");
    }

    [TestMethod]
    public void ToHalfWidth_混合字串_應只轉全形部分()
    {
        "AＢ1２".ToHalfWidth().Should().Be("AB12");
    }

    [TestMethod]
    public void ToHalfWidth_中文_應維持原值()
    {
        "測試".ToHalfWidth().Should().Be("測試");
    }

    [TestMethod]
    public void IsBlank_null_應為true()
    {
        ((string?)null).IsBlank().Should().BeTrue();
    }

    [TestMethod]
    public void IsBlank_空字串_應為true()
    {
        "".IsBlank().Should().BeTrue();
    }

    [TestMethod]
    public void IsBlank_空白_應為true()
    {
        "   ".IsBlank().Should().BeTrue();
    }

    [TestMethod]
    public void IsBlank_有內容_應為false()
    {
        "x".IsBlank().Should().BeFalse();
    }

    [TestMethod]
    public void IsBlank_中文_應為false()
    {
        "測".IsBlank().Should().BeFalse();
    }
}
