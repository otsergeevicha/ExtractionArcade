using Canvases;
using GameCamera;
using Player;
using Plugins.MonoCache;
using Reflex.Core;
using Services.Factory;
using Services.Inputs;
using SO;
using UnityEngine;
using WorldScene;

namespace Reflex
{
    public class MainSceneInstaller : MonoCache, IInstaller
    {
        [SerializeField] private HeroData _heroData;
        [SerializeField] private InventoryData _inventoryData;
        
        private IInputService _input;
        private IGameFactory _gameFactory;
        
        private Environs _environs;
        private MainCamera _camera;
        private Hud _hud;
        private Hero _hero;
        private Coroutines _coroutines;
        private WindowModule _windowModule;

        public void InstallBindings(ContainerBuilder descriptor) => 
            descriptor.OnContainerBuilt += LoadLevel;

        private void LoadLevel(Container container)
        {
            _input = container.Single<IInputService>();
            _gameFactory = container.Single<IGameFactory>();

            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            
            CreateGame();
        }

        private void CreateGame()
        {
            _windowModule = new WindowModule(_inventoryData, _gameFactory);
            
            _camera = _gameFactory.CreateMainCamera();
            _hero = _gameFactory.CreateHero();
            _hud = _gameFactory.CreateHud();
            _environs = _gameFactory.CreatePlane();

            Injects();
        }

        private void Injects()
        {
            _camera.Construct(_hero.GetRootCamera);
            _hero.Construct(_input, _camera.GetCacheCamera, _heroData, _environs.GetHeroSpawnPoint);
            _hud.Construct(_coroutines, _camera.GetCacheCamera, _input, _hero.HeroModule);
        }
    }
}