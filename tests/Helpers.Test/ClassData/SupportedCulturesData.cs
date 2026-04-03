using Xunit;

namespace Helpers.Test.ClassData;

public class SupportedCulturesData : TheoryData<string>
{
    public SupportedCulturesData()
    {
        Add("en");
        Add("pt-BR");
    }
}
