using Canvases;
using GameCamera;
using Player;
using Plugins.MonoCache;
using Reflex.Core;
using Services.Factory;
using Services.Inputs;
using WorldScene;

namespace Reflex
{
    public class MainSceneInstaller : MonoCache, IInstaller
    {
        private IInputService _input;
        private IGameFactory _gameFactory;
        
        private Environs _environs;
        private MainCamera _camera;
        private Hud _hud;
        private Hero _hero;

        public void InstallBindings(ContainerBuilder descriptor) => 
            descriptor.OnContainerBuilt += LoadLevel;

        private void LoadLevel(Container container)
        {
            _input = container.Single<IInputService>();
            _gameFactory = container.Single<IGameFactory>();
            
            CreateGame();
        }

        private void CreateGame()
        {
            _camera = _gameFactory.CreateMainCamera();
            _hud = _gameFactory.CreateHud();
            _environs = _gameFactory.CreatePlane();
            _hero = _gameFactory.CreateHero();

            Injects();
        }

        private void Injects()
        {
            _camera.Construct(_hero.GetRootCamera, _input);
            _hud.Construct(_camera.GetCacheCamera, _input);
        }
    }
}