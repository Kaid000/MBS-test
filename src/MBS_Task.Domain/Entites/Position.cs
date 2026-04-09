namespace MBS_Task.Entities.Models;

public class Position : ICloneable, IDisposable, IComparable<Position>, IEquatable<Position>
{
    public int X { get; }
    public int Y { get; }

    private bool _disposed;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }


    public Position Clone() => (Position)this.MemberwiseClone();

    object ICloneable.Clone() => Clone();

    public bool Equals(Position? other)
    {
        if (other is null) return false;
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj) => Equals(obj as Position);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public int CompareTo(Position? other)
    {
        if (other is null) return 1;
        int cmp = X.CompareTo(other.X);
        return cmp != 0 ? cmp : Y.CompareTo(other.Y);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
        }

        _disposed = true;
    }

    ~Position()
    {
        Dispose(false);
    }

    public override string ToString() => $"({X}, {Y})";
}
