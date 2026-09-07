namespace Issue266.Common.UnitTests;

[TestClass]
public class ObjectMapperTests
{
    #region Test Models

    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class EmptyModel { }

    #endregion

    [TestMethod]
    public void ToHalfWidth_單一物件名稱_應該轉換成功()
    {
        var model = new TestModel
        {
            Id = 1,
            Name = "Ｔｅｓｔ",
            IsActive = true,
        };

        model.Name.ToHalfWidth().Should().Be("Test");
        model.Id.Should().Be(1);
        model.IsActive.Should().BeTrue();
    }

    [TestMethod]
    public void ToHalfWidth_空名稱_應該保持空字串()
    {
        var model = new TestModel { Name = "" };
        model.Name.ToHalfWidth().Should().BeEmpty();
    }

    [TestMethod]
    public void ToHalfWidth_半形名稱_應該維持原值()
    {
        var model = new TestModel { Name = "Test" };
        model.Name.ToHalfWidth().Should().Be("Test");
    }

    [TestMethod]
    public void ToHalfWidth_全形數字名稱_應該轉換成功()
    {
        var model = new TestModel { Name = "１２３" };
        model.Name.ToHalfWidth().Should().Be("123");
    }

    [TestMethod]
    public void ToHalfWidth_List集合_應該全部轉換()
    {
        var list = new List<TestModel>
        {
            new() { Id = 1, Name = "Ａ" },
            new() { Id = 2, Name = "Ｂ" },
            new() { Id = 3, Name = "Ｃ" },
        };

        list.Select(x => x.Name.ToHalfWidth()).Should().Equal("A", "B", "C");
    }

    [TestMethod]
    public void ToHalfWidth_空List_應該回傳空集合()
    {
        var list = new List<TestModel>();
        list.Select(x => x.Name.ToHalfWidth()).Should().BeEmpty();
    }

    [TestMethod]
    public void IsBlank_空名稱_應該為true()
    {
        var model = new TestModel { Name = "   " };
        model.Name.IsBlank().Should().BeTrue();
    }

    [TestMethod]
    public void IsBlank_有名稱_應該為false()
    {
        var model = new TestModel { Name = "ok" };
        model.Name.IsBlank().Should().BeFalse();
    }

    [TestMethod]
    public void EmptyModel_應該可以建立()
    {
        var empty = new EmptyModel();
        empty.Should().NotBeNull();
    }

    [TestMethod]
    public void TestModel_預設值_應該正確()
    {
        var model = new TestModel();
        model.Id.Should().Be(0);
        model.Name.Should().BeEmpty();
        model.IsActive.Should().BeFalse();
    }

    [TestMethod]
    public void ToHalfWidth_全形空白名稱_應該轉成半形空白()
    {
        var model = new TestModel { Name = "　" };
        model.Name.ToHalfWidth().Should().Be(" ");
    }

    [TestMethod]
    public void ToHalfWidth_混合名稱_應該只轉全形()
    {
        var model = new TestModel { Name = "AＢC" };
        model.Name.ToHalfWidth().Should().Be("ABC");
    }
}
