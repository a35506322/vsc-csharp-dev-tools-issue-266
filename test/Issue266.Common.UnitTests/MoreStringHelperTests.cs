namespace Issue266.Common.UnitTests;

[TestClass]
public class MoreStringHelperTests
{
    [TestMethod]
    public void ToHalfWidth_全形減號_應轉換()
    {
        "－".ToHalfWidth().Should().Be("-");
    }

    [TestMethod]
    public void ToHalfWidth_全形底線_應轉換()
    {
        "＿".ToHalfWidth().Should().Be("_");
    }

    [TestMethod]
    public void ToHalfWidth_全形括號_應轉換()
    {
        "（）".ToHalfWidth().Should().Be("()");
    }

    [TestMethod]
    public void ToHalfWidth_全形方括號_應轉換()
    {
        "［］".ToHalfWidth().Should().Be("[]");
    }

    [TestMethod]
    public void ToHalfWidth_全形大括號_應轉換()
    {
        "｛｝".ToHalfWidth().Should().Be("{}");
    }

    [TestMethod]
    public void ToHalfWidth_全形冒號_應轉換()
    {
        "：".ToHalfWidth().Should().Be(":");
    }

    [TestMethod]
    public void ToHalfWidth_全形分號_應轉換()
    {
        "；".ToHalfWidth().Should().Be(";");
    }

    [TestMethod]
    public void ToHalfWidth_全形問號_應轉換()
    {
        "？".ToHalfWidth().Should().Be("?");
    }

    [TestMethod]
    public void ToHalfWidth_長字串_應全部轉換()
    {
        "ＡＢＣＤＥＦＧＨＩＪ".ToHalfWidth().Should().Be("ABCDEFGHIJ");
    }

    [TestMethod]
    public void IsBlank_Tab_應為true()
    {
        "\t".IsBlank().Should().BeTrue();
    }

    [TestMethod]
    public void IsBlank_換行_應為true()
    {
        "\n".IsBlank().Should().BeTrue();
    }

    [TestMethod]
    public void IsBlank_半形空白_應為true()
    {
        " ".IsBlank().Should().BeTrue();
    }
}
