namespace SummerGame.Core;

public interface IGameObject
{
    void Initialize();
    void LoadContent();
    void Update(double delta);
    void Exit();
}


