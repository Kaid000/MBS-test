using MBS_Task.Entities.Models;

namespace MBS_Task.Application.Helpers.DistanceMap;

public class DistanceMap
{
    private readonly int[,] _dist;

    public DistanceMap(int rows, int cols)
    {
        _dist = new int[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                _dist[i, j] = int.MaxValue;
    }

    public int Get(Position pos) => _dist[pos.X, pos.Y];

    public void Set(Position pos, int value) => _dist[pos.X, pos.Y] = value;

    public bool IsVisited(Position pos) => _dist[pos.X, pos.Y] != int.MaxValue;
}