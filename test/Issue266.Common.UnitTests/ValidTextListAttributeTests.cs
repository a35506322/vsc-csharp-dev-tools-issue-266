using System.ComponentModel.DataAnnotations;

namespace Issue266.Common.UnitTests;

[TestClass]
public class ValidTextListAttributeTests
{
    private class TestModel
    {
        [RequiredText]
        public string? Title { get; set; }
    }

    private static (bool Ok, List<ValidationResult> Results) Validate(string? title)
    {
        var model = new TestModel { Title = title };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var ok = Validator.TryValidateObject(model, context, results, true);
        return (ok, results);
    }

    [TestMethod]
    public void Validate_WithValidText_ShouldReturnTrue()
    {
        var (ok, results) = Validate("ok");
        ok.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WithSingleWord_ShouldReturnTrue()
    {
        var (ok, results) = Validate("one");
        ok.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WithEmptyString_ShouldReturnFalse()
    {
        var (ok, results) = Validate("");
        ok.Should().BeFalse();
        results.Should().HaveCount(1);
    }

    [TestMethod]
    public void Validate_WithNullValue_ShouldReturnFalse()
    {
        var (ok, results) = Validate(null);
        ok.Should().BeFalse();
        results.Should().HaveCount(1);
    }

    [TestMethod]
    public void Validate_WithWhitespace_ShouldReturnFalse()
    {
        var (ok, results) = Validate("  ");
        ok.Should().BeFalse();
        results.Should().HaveCount(1);
    }

    [TestMethod]
    public void RequiredTextAttribute_IsValid_WithText_ShouldBeTrue()
    {
        new RequiredTextAttribute().IsValid("x").Should().BeTrue();
    }

    [TestMethod]
    public void RequiredTextAttribute_IsValid_WithEmpty_ShouldBeFalse()
    {
        new RequiredTextAttribute().IsValid("").Should().BeFalse();
    }

    [TestMethod]
    public void RequiredTextAttribute_IsValid_WithNull_ShouldBeFalse()
    {
        new RequiredTextAttribute().IsValid(null).Should().BeFalse();
    }

    [TestMethod]
    public void Validate_WithChineseText_ShouldReturnTrue()
    {
        var (ok, results) = Validate("標題");
        ok.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WithMixedText_ShouldReturnTrue()
    {
        var (ok, results) = Validate("title 標題");
        ok.Should().BeTrue();
        results.Should().BeEmpty();
    }
}
