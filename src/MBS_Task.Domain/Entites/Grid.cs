namespace MBS_Task.Entities.Models;

public class Grid : ICloneable
{
    public int Rows { get; }
    public int Cols { get; }
    public Cell[,] Cells { get; }

    public Position Start { get; }
    public Position End { get; }

    public IReadOnlyDictionary<char, List<Position>> Portals { get; }

    public Grid(Cell[,] cells,
                Position start,
                Position end,
                Dictionary<char, List<Position>> portals)
    {
        Cells = cells;
        Rows = cells.GetLength(0);
        Cols = cells.GetLength(1);
        Start = start;
        End = end;
        Portals = new Dictionary<char, List<Position>>(portals);
    }

    public Grid Clone()
    {
        var newCells = new Cell[Rows, Cols];
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                newCells[i, j] = Cells[i, j].Clone();

        var newPortals = Portals.ToDictionary(
            kv => kv.Key,
            kv => kv.Value.Select(p => p.Clone()).ToList()
        );

        return new Grid(newCells, Start.Clone(), End.Clone(), newPortals);
    }

    object ICloneable.Clone() => Clone();
}
