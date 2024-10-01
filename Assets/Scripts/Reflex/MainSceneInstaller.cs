using Canvases;
using GameCamera;
using Infrastructure.Factory.Pools;
using Inventory.Config;
using Player;
using Player.Module.Parent;
using Plugins.MonoCache;
using Reflex.Core;
using Services.Factory;
using Services.Inputs;
using SO;
using Spawners;
using UnityEngine;
using WorldScene;

namespace Reflex
{
    public class MainSceneInstaller : MonoCache, IInstaller
    {
        [SerializeField] private HeroData _heroData;
        [SerializeField] private InventoryData _inventoryData;
        [SerializeField] private GameData _gameData;

        private IInputService _input;
        private IGameFactory _gameFactory;
        
        private Environs _environs;
        private MainCamera _camera;
        private Hud _hud;
        private Hero _hero;
        private Coroutines _coroutines;
        private WindowModule _windowModule;
        private Pool _pool;
        private LootSpawner _lootSpawner;

        public void InstallBindings(ContainerBuilder descriptor) => 
            descriptor.OnContainerBuilt += LoadLevel;

        protected override void OnDisabled() => 
            _windowModule.Dispose();

        private void LoadLevel(Container container)
        {
            _input = container.Single<IInputService>();
            _gameFactory = container.Single<IGameFactory>();

            _coroutines = new GameObject(Constants.NameCoroutines).AddComponent<Coroutines>();
            
            CreateGame();
        }

        private void CreateGame()
        {
            _camera = _gameFactory.CreateMainCamera();
            _hero = _gameFactory.CreateHero();
            _hud = _gameFactory.CreateHud();
            _environs = _gameFactory.CreatePlane();
            _pool = _gameFactory.CreatePool();

            _hero.Construct(_input, _camera.GetCacheCamera, _heroData, _environs.GetHeroSpawnPoint);
            _windowModule = new WindowModule(_inventoryData.Inventories.AsReadOnly(),
                _gameFactory.CreateInventoryScreen(), _hud, _input, _hero.HeroModule);

            _camera.Construct(_hero.GetRootCamera);
            _pool.Construct(_gameFactory, _gameData);
            _hud.Construct(_coroutines, _camera.GetCacheCamera, _input, _hero.HeroModule, _windowModule.GetInventoryService, _pool.PoolItems.Items);
            
            _lootSpawner = new LootSpawner(_pool.PoolItems.Items, _environs.GetItemsSpawnPoint);
        }
    }
}