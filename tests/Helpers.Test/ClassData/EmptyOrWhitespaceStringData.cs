using Xunit;

namespace Helpers.Test.ClassData;

public class EmptyOrWhitespaceStringData : TheoryData<string>
{
    public EmptyOrWhitespaceStringData()
    {
        Add(string.Empty);
        Add(" ");
        Add("  ");
        Add("\t");
        Add("\n");
    }
}
