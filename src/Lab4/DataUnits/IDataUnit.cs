using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;

namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits;

public interface IDataUnit : IComparable<IDataUnit>
{
    Uri Uri { get; }

    void Accept(IDataUnitVisitor visitor);
}