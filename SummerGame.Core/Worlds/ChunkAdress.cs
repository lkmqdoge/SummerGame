namespace SummerGame.Core.Worlds;

public struct ChunkAdress(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public override readonly int GetHashCode()
        => (((17*23) + X) * 23) + Y;

    public override readonly bool Equals(object obj)
        => obj is not null && obj is ChunkAdress chunkObj && (X==chunkObj.X)&&(Y==chunkObj.Y);
}


