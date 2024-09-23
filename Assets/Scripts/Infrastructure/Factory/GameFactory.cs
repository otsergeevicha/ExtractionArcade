using Canvases;
using GameCamera;
using Player;
using Services.Assets;
using Services.Factory;
using WorldScene;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public GameFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public Environs CreatePlane() => 
            _assetsProvider.InstantiateEntity(Constants.EnvironsPath)
                .GetComponent<Environs>();

        public Hero CreateHero() => 
            _assetsProvider.InstantiateEntity(Constants.HeroPath)
                .GetComponent<Hero>();

        public MainCamera CreateMainCamera() => 
            _assetsProvider.InstantiateEntity(Constants.CameraPath)
                .GetComponent<MainCamera>();

        public Hud CreateHud() => 
            _assetsProvider.InstantiateEntity(Constants.HudPath)
                .GetComponent<Hud>();
    }
}