using MotoRent.Application.Validators;
using Xunit;

namespace Helpers.Test.ClassData;

public class InvalidYearData : TheoryData<int>
{
    public InvalidYearData()
    {
        Add(YearConstraints.MinYear - 1);
        Add(YearConstraints.MaxYear + 1);
    }
}
