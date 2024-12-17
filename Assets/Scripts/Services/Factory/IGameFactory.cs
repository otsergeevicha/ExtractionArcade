using GameCamera;
using Player;

namespace Services.Factory
{
    public interface IGameFactory
    {
        Hero CreateHero(Hero hero);
        MainCamera CreateMainCamera(MainCamera mainCamera);
    }
}