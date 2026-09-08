namespace SummerGame.Core.Simulation.Worlds;

public class ChunkLoader
{
    public Chunk[,] Chunks { get; set; }

    public int LoadDistance { get; set; }

    public ChunkLoader(int loadDistance)
    {
        LoadDistance = loadDistance;

        Chunks = new Chunk[loadDistance, loadDistance];
    }

    public bool TryLoadChunk(out Chunk chunk)
    {
        chunk = default;
        return false;
    }

    public void Update()
    {
        // for (int i = 0; i < LoadDistance*LoadDistance; i++)
        // {
        //     var x = i % LoadDistance;
        // }
    }
}


