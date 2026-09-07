using System.ComponentModel.DataAnnotations;

namespace Issue266.Common.UnitTests;

[TestClass]
public class RequiredTextAttributeTests
{
    private static bool Validate(string? value)
    {
        var model = new TestModel { Name = value };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        return Validator.TryValidateObject(model, context, results, true);
    }

    private class TestModel
    {
        [RequiredText]
        public string? Name { get; set; }
    }

    [TestMethod]
    public void 有文字_應通過()
    {
        Validate("hello").Should().BeTrue();
    }

    [TestMethod]
    public void 空白字串_應不通過()
    {
        Validate("").Should().BeFalse();
    }

    [TestMethod]
    public void 只有空白_應不通過()
    {
        Validate("   ").Should().BeFalse();
    }

    [TestMethod]
    public void null_應不通過()
    {
        Validate(null).Should().BeFalse();
    }

    [TestMethod]
    public void 中文_應通過()
    {
        Validate("測試").Should().BeTrue();
    }

    [TestMethod]
    public void 全形空白_應不通過()
    {
        Validate("　").Should().BeFalse();
    }

    [TestMethod]
    public void 數字字串_應通過()
    {
        Validate("123").Should().BeTrue();
    }
}
