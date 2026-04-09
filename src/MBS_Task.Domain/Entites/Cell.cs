using MBS_Task.Domain.Enums;

namespace MBS_Task.Entities.Models;

public interface ICostableCell
{
    int GetCost();
}

public class Cell : ICloneable, ICostableCell
{
    public Position Position { get; }
    public char Raw { get; }
    public CellType Type { get; }

    public Cell(Position position, char raw)
    {
        Position = position;
        Raw = raw;
        Type = ResolveType(raw);
    }

    private CellType ResolveType(char c) => c switch
    {
        'S' => CellType.Start,
        'E' => CellType.End,
        '.' => CellType.Empty,
        '#' => CellType.Wall,
        >= 'A' and <= 'Z' => CellType.Portal,
        >= '1' and <= '9' => CellType.Weighted,
        _ => CellType.Empty
    };

    public int GetCost() => Type switch
    {
        CellType.Empty => 1,
        CellType.Start => 0,
        CellType.End => 1,
        CellType.Portal => 1,
        CellType.Weighted => Raw - '0',
        CellType.Wall => int.MaxValue,
        _ => 1
    };

    #region ICloneable
    public Cell Clone() => new Cell(Position.Clone(), Raw);
    object ICloneable.Clone() => Clone();
    #endregion

    public override string ToString() => $"{Type}({Position.X},{Position.Y})";
}