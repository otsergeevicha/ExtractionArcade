using Canvases;
using GameCamera;
using Inventory.Views;
using Player;
using WorldScene;

namespace Services.Factory
{
    public interface IGameFactory
    {
        Environs CreatePlane();
        Hero CreateHero();
        MainCamera CreateMainCamera();
        Hud CreateHud();
        InventoryScreenView CreateInventoryScreen();
    }
}